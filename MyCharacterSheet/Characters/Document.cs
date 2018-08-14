using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.Characters
{
    public class Document
    {
        public Document(string id, string name, string rtf)
        {
            ID = id;
            Name = name;
            Rtf = rtf;
        }

        public override string ToString()
        {
            return Name;
        }

        public string ID
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Rtf
        {
            get;
            set;
        }
    }
}
