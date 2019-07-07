using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GEV.Layouts.Vanilla;
using Markdig;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace DocAsm.Exporters
{
    public class DocxExporter
    {
        public string Source { get; set; }
        public string SourcePath { get; set; }
        public string TemplatePath { get; set; }
        public SortableBindingList<FieldInfo> DocumentInfo { get; set; }

        private int m_NumberingCounter = 200;

        private WordprocessingDocument m_Document;

        public void Export(string targetPath)
        {
            using (this.m_Document = WordprocessingDocument.CreateFromTemplate(this.TemplatePath))
            {
                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseGridTables().UsePipeTables().Build();
                var markdown = Markdown.Parse(this.Source, pipeline);

                foreach(Block block in markdown)
                {
                    if(block is HeadingBlock)
                    {
                        HeadingBlock b = block as HeadingBlock;
                        string s = "";
                        foreach (Inline run in b.Inline)
                        {
                            s += this.Source.Substring(run.Span.Start, run.Span.Length);
                        }

                        this.m_Document.MainDocumentPart.Document.Body.Append(this.MakeHeaderParagprah(s.ToString(), b.Level));
                    }

                    if(block is ParagraphBlock)
                    {
                        this.m_Document.MainDocumentPart.Document.Body.Append(this.ParseParagaph(block as ParagraphBlock));
                    }

                    if (block is ListBlock)
                    {
                        this.m_Document.MainDocumentPart.Document.Body.Append(this.ParseList(block as ListBlock));
                    }

                    if (block is Markdig.Extensions.Tables.Table)
                    {
                        //TODO Header row

                        Markdig.Extensions.Tables.Table b = block as Markdig.Extensions.Tables.Table;
                        Table table = this.MakeTable();

                        foreach(Block rowBlock in b)
                        {
                            Markdig.Extensions.Tables.TableRow mdRow = rowBlock as Markdig.Extensions.Tables.TableRow;
                            TableRow row = this.MakeTableRow();

                            foreach(Block cellBlock in mdRow)
                            {
                                Markdig.Extensions.Tables.TableCell mdCell = cellBlock as Markdig.Extensions.Tables.TableCell;
                                TableCell cell = this.MakTableCell();

                                foreach(Block content in mdCell)
                                {
                                    if(content is ParagraphBlock)
                                    {
                                        cell.Append(this.ParseParagaph(content as ParagraphBlock));
                                    }
                                }
                                if(mdCell.Count == 0)
                                {
                                    cell.Append(this.MakeParagraph());
                                }
                                row.Append(cell);
                            }
                            table.Append(row);
                        }

                        this.m_Document.MainDocumentPart.Document.Body.Append(table);
                    }

                    if (block is BlockQuote)
                    {

                    }

                    if(block is CodeBlock)
                    {

                    }
                }

                this.m_Document.PackageProperties.Creator = this.DocumentInfo.First(di => di.Name == "Creator").Value;
                this.m_Document.PackageProperties.Category = this.DocumentInfo.First(di => di.Name == "Category").Value;
                this.m_Document.PackageProperties.Description = this.DocumentInfo.First(di => di.Name == "Description").Value;
                this.m_Document.PackageProperties.Subject = this.DocumentInfo.First(di => di.Name == "Subject").Value;
                this.m_Document.PackageProperties.Title = this.DocumentInfo.First(di => di.Name == "Title").Value;

                //// Save changes to the main document part. 
                this.m_Document.MainDocumentPart.Document.Save();

                var doc2 = this.m_Document.SaveAs(targetPath) as WordprocessingDocument;

                doc2.MainDocumentPart.DocumentSettingsPart.Settings.PrependChild<UpdateFieldsOnOpen>(new UpdateFieldsOnOpen()
                {
                    Val = new OnOffValue(true)
                });
                doc2.MainDocumentPart.DocumentSettingsPart.Settings.Save();

                this.m_Document.Close();
                doc2.Close();
            }
        }

        #region Block parsers

        public Paragraph ParseParagaph(ParagraphBlock block)
        {
            ParagraphBlock b = block as ParagraphBlock;

            Paragraph p = this.MakeParagraph();

            string s = "";
            foreach (Inline run in b.Inline)
            {
                if (run is LinkInline && (run as LinkInline).IsImage)
                {
                    LinkInline url = run as LinkInline;

                    List<string> sourceFolder = this.SourcePath.Split('\\').ToList();
                    sourceFolder.RemoveAt(sourceFolder.Count - 1);
                    string file = String.Join("\\", sourceFolder) + "\\" + url.Url.Replace('/', '\\');
                    p = this.MakeImageParagraph(file, url.FirstChild.ToString());
                }
                else if (run is EmphasisInline)
                {
                    bool bold = false;
                    bool italic = false;
                    bool underline = false;

                    EmphasisInline r = run as EmphasisInline;
                    if (r.DelimiterCount == 1)
                    {
                        italic = !italic;
                    }
                    else if (r.DelimiterCount == 2)
                    {
                        bold = !bold;
                    }

                    string text = this.Source.Substring(r.FirstChild.Span.Start, r.FirstChild.Span.Length);
                    p.Append(this.MakeRun(text, bold, underline, italic));
                }
                else if (run is LiteralInline)
                {
                    p.Append(this.MakeRun(this.Source.Substring(run.Span.Start, run.Span.Length), false, false, false));
                }
            }

            return p;
        }

        public List<Paragraph> ParseList(ListBlock block, int indent = 0, int numbering = -1)
        {
            List<Paragraph> result = new List<Paragraph>();

            if (numbering == -1)
            {
                numbering = this.InsertNewNumbering();
            }

            ListBlock b = block as ListBlock;
            foreach (var item in b)
            {
                ListItemBlock li = item as ListItemBlock;

                if (li.First() is ParagraphBlock)
                {
                    Paragraph p = this.ParseParagaph(li.First() as ParagraphBlock);
                    this.ChangeToListItem(p, numbering, indent);
                    result.Add(p);

                    if (li.Count > 1 && li[1] is ListBlock)
                    {
                        result.AddRange(this.ParseList(li[1] as ListBlock, indent + 1, numbering));
                    }
                }
            }

            return result;
        }

        #endregion

        #region Maker methods

        public Paragraph MakeParagraph()
        {
            return new Paragraph();
        }

        public Run MakeRun(string text, bool isBold, bool isUnderline, bool isItalic)
        {
            RunProperties rp = new RunProperties(
                new Bold() { Val = new OnOffValue(isBold) },
                new Italic() { Val = new OnOffValue(isItalic) }
                //TODO Underline
                );
            return new Run(rp, new Text() { Text = text, Space = SpaceProcessingModeValues.Preserve });
        }

        public Paragraph MakeImageParagraph(string imagePath, string caption)
        {
            ImagePart imagePart = this.m_Document.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            int imgWidth = 0;
            int imgHeight = 0;

            using (Bitmap bmp = new Bitmap(imagePath))
            {
                imgWidth = bmp.Width * 9525;
                imgHeight = bmp.Height * 9525;

                long pageWidth = (int)(this.GetPageWidth() * 635.27); //Pacsiszám de ez jött ki

                if (imgWidth > pageWidth)
                {
                    imgHeight = (int)(imgHeight * (pageWidth / (float)imgWidth));
                    imgWidth = (int)pageWidth;
                }
            }

            using (FileStream stream = new FileStream(imagePath, FileMode.Open))
            {
                imagePart.FeedData(stream);
            }

            Drawing element = new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = imgWidth, Cy = imgHeight },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = caption
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties() { Id = (UInt32Value)0U, Name = caption },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(new A.BlipExtensionList(new A.BlipExtension() { Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" }))
                                         {
                                             Embed = this.m_Document.MainDocumentPart.GetIdOfPart(imagePart),
                                             CompressionState = A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(new A.FillRectangle())
                                         ),
                                     new PIC.ShapeProperties
                                     (
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },
                                             new A.Extents() { Cx = imgWidth, Cy = imgHeight }),
                                         new A.PresetGeometry(new A.AdjustValueList())
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            //paragraph properties 
            ParagraphProperties centerProps = new ParagraphProperties();
            centerProps.Append(new Justification() { Val = JustificationValues.Center });

            RunProperties rp = new RunProperties(new Italic() { Val = new OnOffValue(true) });

            return new Paragraph(new OpenXmlElement[]
            {
                centerProps,
                new Run(element),
                new Break(),
                new Run(rp, new Text(caption)),
            });
        }

        public Paragraph MakeHeaderParagprah(string text, int indent)
        {
            Paragraph p = this.MakeParagraph();

            if (p.ParagraphProperties == null)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            p.ParagraphProperties.Append(new ParagraphStyleId() { Val = "Heading" + indent });
            p.Append(this.MakeRun(text, false, false, false));

            return p;
        }

        public Table MakeTable()
        {
            Table table = new Table();

            TableProperties tblProp = new TableProperties(
                new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct }, //újabb pacsiszám
                new TableBorders(
                    new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                    new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                    new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                    new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                    new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                    new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
                )
            );

            table.AppendChild<TableProperties>(tblProp);

            return table;
        }

        public TableRow MakeTableRow()
        {
            TableRow row = new TableRow();

            return row;
        }

        public TableCell MakTableCell()
        {
            TableCell cell = new TableCell();

            return cell;
        }

        public int InsertNewNumbering()
        {
            int result = this.m_NumberingCounter++;

            NumberingDefinitionsPart numberingPart = this.m_Document.MainDocumentPart.NumberingDefinitionsPart;
            if (numberingPart == null)
            {
                numberingPart = this.m_Document.MainDocumentPart.AddNewPart<NumberingDefinitionsPart>("NumberingDefinitionsPart");
            }

            Numbering element = new Numbering(
                new AbstractNum(
                  new Level()
                  {
                      LevelIndex = 0,
                      NumberingFormat = new NumberingFormat() { Val = NumberFormatValues.Bullet },
                      LevelText = new LevelText() { Val = "" }
                  }
                )
                {
                    AbstractNumberId = m_NumberingCounter,
                    MultiLevelType = new MultiLevelType() {  Val = MultiLevelValues.HybridMultilevel },
                },
                new NumberingInstance()
                {
                    NumberID = result,
                    AbstractNumId = new AbstractNumId() { Val = m_NumberingCounter }
                }
            );

            numberingPart.Numbering = element;
            element.Save(numberingPart);

            return result;
        }

        public void ChangeToListItem(Paragraph p, int numbering, int indent)
        {
            if (p.ParagraphProperties == null)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            p.ParagraphProperties.Append(new NumberingProperties(new NumberingLevelReference() { Val = indent }, new NumberingId() { Val = numbering }));
        }

        #endregion

        #region Utils

        public static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>().Where(s => s.Val.Value.Equals(styleName.ToLower()) && (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();

            if (styleId == null)
            {
                string type = stylePart.Styles.LatentStyles.First().GetType().ToString();

                var styles = stylePart.Styles.LatentStyles.Descendants<LatentStyleExceptionInfo>().Where(s => s.Name == styleName.ToLower());

                styleId = stylePart.Styles.LatentStyles.Descendants<StyleName>().Where(s => s.Val.Value.Equals(styleName) && (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            }
            return styleId;
        }

        public long GetPageWidth()
        {
            SectionProperties sectionProperties = this.m_Document.MainDocumentPart.Document.Body.GetFirstChild<SectionProperties>();

            PageSize pageSize = sectionProperties.GetFirstChild<PageSize>();
            PageMargin pageMargin = sectionProperties.GetFirstChild<PageMargin>();

            return pageSize.Width - pageMargin.Left - pageMargin.Right;
        }

        #endregion

        //============================================ OLD CODE //============================================

        public static void SetNumbering(WordprocessingDocument doc, bool numbered)
        {
    //        Numbering element = new Numbering(
    //new AbstractNum(
    //    new Level(new NumberingFormat() { Val = numbered ? NumberFormatValues.Chicago : NumberFormatValues.Bullet }, new LevelText() { Val = "·" }) { LevelIndex = 0 }
    //    )
    //{ AbstractNumberId = m_NumberingCounter },
    //new NumberingInstance(
    //  new AbstractNumId() { Val = m_NumberingCounter }
    //)
    //{ NumberID = m_NumberingCounter });

    //        NumberingDefinitionsPart ndp = new NumberingDefinitionsPart();
    //        doc.MainDocumentPart.AddPart<NumberingDefinitionsPart>(ndp);

    //        doc.MainDocumentPart.NumberingDefinitionsPart.Numbering.Append(element);

    //        NumberingDefinitionsPart numberingPart = doc.AddNewPart<NumberingDefinitionsPart>("List" + m_NumberingCounter);


    //        element.Save(numberingPart);

    //        m_NumberingCounter++;
        }

    }
}
