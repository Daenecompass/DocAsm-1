using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocAsm
{
    public class FieldInfo
    {
        public string Name { get; private set; }
        public string Value { get; set; }

        public FieldInfo(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
