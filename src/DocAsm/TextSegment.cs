using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocAsm
{
    public class TextSegment
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public string Title { get; set; }
        public List<TextSegment> SubSegments { get; set; } = new List<TextSegment>();
    }
}
