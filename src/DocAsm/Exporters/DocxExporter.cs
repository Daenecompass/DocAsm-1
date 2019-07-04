using System;
using System.Collections.Generic;
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
using Markdig;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Syntax;

using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace DocAsm.Exporters
{
    public class DocxExporter
    {
        public WebBrowser Browser { get; set; }
        public string Source { get; set; }
        public string SourcePath { get; set; }
        public string TemplatePath { get; set; }

        private static int m_NumberingCounter = 1;
        private static int m_NumberingLevel = 0;

        public void Export()
        {
            m_NumberingCounter = 1;
            m_NumberingLevel = 1;

            using (WordprocessingDocument doc = WordprocessingDocument.CreateFromTemplate(this.TemplatePath))
            {
                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                var markdown = Markdown.Parse(this.Source, pipeline);

                string fragment = "";
                bool fragmentBuilding = false;
                bool sameLine = false;
                foreach (StringLine slg in (markdown.LastChild as HtmlBlock).Lines)
                {
                    sameLine = false;
                    IMarkdownRenderer i;
                    if(!slg.Slice.IsEmpty && slg.Slice.Text != null)
                    {
                        string frag = slg.Slice.Text.Substring(slg.Slice.Start, slg.Slice.Length);

                        #region Header
                        if(frag.StartsWith("<h"))
                        {

                            string text = frag.Substring(4, frag.Length - 9);

                            AddHeader(doc, text, int.Parse(frag[2].ToString()));

                            fragment = "";
                            fragmentBuilding = false;
                            continue;
                        }
                        #endregion

                        #region Image
                        if (frag.StartsWith("<p><img"))
                        {
                            string file = frag.Substring(frag.IndexOf("src=") + 5);
                            file = file.Substring(0, file.IndexOf("\"")).Replace('/', '\\');

                            List<string> path = this.SourcePath.Split('\\').ToList();
                            path.RemoveAt(path.Count - 1);

                            string dir = String.Join("\\", path);
                            file = dir + "\\" + file;

                            string title = frag.Substring(frag.IndexOf("alt=") + 5);
                            title = title.Substring(0, title.IndexOf("\""));

                            AddImage(doc, file, title);

                            fragment = "";
                            fragmentBuilding = false;
                            continue;
                        }
                        #endregion

                        #region Paragraph
                        if (frag.StartsWith("<p>"))
                        {
                            fragment += frag;
                            fragmentBuilding = true;
                            sameLine = true;
                        }

                        if (frag.EndsWith("</p>"))
                        {
                            if(!sameLine)
                            {
                                fragment += frag;
                            }

                            string text = fragment.Substring(3, fragment.Length - 7);

                            AddParagpaph(doc, text);


                            fragment = "";
                            fragmentBuilding = false;
                            continue;
                        }
                        #endregion

                        #region Lists
                        if (frag.StartsWith("<ul>"))
                        {
                            //SetNumbering(doc, false);
                            //m_NumberingLevel++;

                            //fragment = "";
                            //fragmentBuilding = false;
                            //continue;
                        }

                        if (frag.StartsWith("<ol>"))
                        {
                            //SetNumbering(doc, true);
                            //m_NumberingLevel++;

                            //fragment = "";
                            //fragmentBuilding = false;
                            //continue;
                        }

                        if (frag.StartsWith("<li>"))
                        {
                            string text = frag.Substring(4, frag.Length - 9);

                            AddParagpaph(doc, "* " +text);

                            fragment = "";
                            fragmentBuilding = false;
                            continue;
                        }

                        if (frag.EndsWith("</ol>"))
                        {
                            //m_NumberingLevel--;

                            //fragment = "";
                            //fragmentBuilding = false;
                            //continue;
                        }

                        if (frag.EndsWith("</ul>"))
                        {
                            //m_NumberingLevel--;

                            //fragment = "";
                            //fragmentBuilding = false;
                            //continue;
                        }
                        #endregion

                        #region Blocks
                        if (frag.StartsWith("<blockquote>"))
                        {

                        }

                        if (frag.StartsWith("</blockquote>"))
                        {

                        }
                        #endregion

                        #region Tables
                        //TODO
                        #endregion
                    }
                }

                //// Save changes to the main document part. 
                doc.MainDocumentPart.Document.Save();

                var doc2 = doc.SaveAs(@"D:\test.docx") as WordprocessingDocument;

                doc2.MainDocumentPart.DocumentSettingsPart.Settings.PrependChild<UpdateFieldsOnOpen>(new UpdateFieldsOnOpen()
                {
                    Val = new OnOffValue(true)
                });
                doc2.MainDocumentPart.DocumentSettingsPart.Settings.Save();

                doc.Close();
                doc2.Close();
            }
        }

        public static Paragraph AddParagpaph(WordprocessingDocument doc, string text)
        {
            Paragraph p = new Paragraph();

            string[] fragments = text.Split('<');

            foreach(string fragment in fragments)
            {
                if(fragment.StartsWith("strong>"))
                {
                    string frag = fragment.Substring(7);
                    RunProperties rp = new RunProperties(new Bold() { Val = new OnOffValue(true) });
                    p.Append(new Run(rp, new Text() { Text = frag, Space = SpaceProcessingModeValues.Preserve }));

                }
                else if (fragment.StartsWith("/strong>"))
                {
                    string frag = fragment.Substring(8);
                    RunProperties rp = new RunProperties(new Bold() { Val = new OnOffValue(false) });
                    p.Append(new Run(rp, new Text() { Text = frag, Space = SpaceProcessingModeValues.Preserve }));
                }
                else
                {
                    p.Append(new Run(new Text() { Text = fragment, Space = SpaceProcessingModeValues.Preserve }));
                }
            }

            doc.MainDocumentPart.Document.Body.Append(p);
            return p;
        }

        public static Paragraph AddHeader(WordprocessingDocument doc, string text, int indent)
        {
            Paragraph p = AddParagpaph(doc, text);

            if (p.ParagraphProperties == null)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            p.ParagraphProperties.Append(new ParagraphStyleId() { Val = "Heading" + indent });

            return p;
        }

        public static void AddImage(WordprocessingDocument doc, string imagePath, string caption)
        {
            ImagePart imagePart = doc.MainDocumentPart.AddImagePart(ImagePartType.Jpeg);
            int imgWidth = 0;
            int imgHeight = 0;

            using (Bitmap bmp = new Bitmap(imagePath))
            {
                imgWidth = bmp.Width * 9525;
                imgHeight = bmp.Height * 9525;

                long pageWidth = (int)(GetPageWidth(doc) * 635.27); //Pacsiszám de ez jött ki

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
            // Define the reference of the image.
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
                                         new PIC.NonVisualDrawingProperties(){ Id = (UInt32Value)0U, Name = caption },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(new A.BlipExtensionList(new A.BlipExtension(){  Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}" }))
                                         {
                                             Embed = doc.MainDocumentPart.GetIdOfPart(imagePart),
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

            doc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new OpenXmlElement[]
            {
                centerProps,
                new Run(element),
                new Break(),
                new Run(rp, new Text(caption)),
            }));
        }

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

        public static void AddListElement(WordprocessingDocument doc, string text)
        {
            ParagraphProperties pp = new ParagraphProperties(new NumberingProperties(new NumberingLevelReference() { Val = m_NumberingLevel }, new NumberingId() { Val = m_NumberingCounter }));

            Paragraph p = AddParagpaph(doc, text);
            p.InsertAt(pp, 0);
        }

        public static string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>().Where(s => s.Val.Value.Equals(styleName.ToLower()) && (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();

            if(styleId == null)
            {
                string type = stylePart.Styles.LatentStyles.First().GetType().ToString();

                var styles = stylePart.Styles.LatentStyles.Descendants<LatentStyleExceptionInfo>().Where(s => s.Name == styleName.ToLower());

                styleId = stylePart.Styles.LatentStyles.Descendants<StyleName>().Where(s => s.Val.Value.Equals(styleName) && (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            }
            return styleId;
        }

        public static long GetPageWidth(WordprocessingDocument doc)
        {
            SectionProperties sectionProperties = doc.MainDocumentPart.Document.Body.GetFirstChild<SectionProperties>();

            PageSize pageSize = sectionProperties.GetFirstChild<PageSize>();
            PageMargin pageMargin = sectionProperties.GetFirstChild<PageMargin>();

            return pageSize.Width - pageMargin.Left - pageMargin.Right;
        }
    }
}
