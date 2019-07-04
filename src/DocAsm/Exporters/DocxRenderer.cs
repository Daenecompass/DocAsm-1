using Markdig.Renderers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocAsm.Exporters
{
    public class DocxRenderer : IMarkdownRenderer
    {
        public ObjectRendererCollection ObjectRenderers { get; set; } = new ObjectRendererCollection()
        {
        };

        public event Action<IMarkdownRenderer, MarkdownObject> ObjectWriteBefore;
        public event Action<IMarkdownRenderer, MarkdownObject> ObjectWriteAfter;

        public object Render(MarkdownObject markdownObject)
        {
            throw new NotImplementedException();
        }
    }
}
