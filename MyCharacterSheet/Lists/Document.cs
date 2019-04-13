using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents the rtf data of a text document.
    /// </summary>
    public class Document
    {
        public Document(Guid id, string name, string rtf)
        {
            ID = id;
            Name = name;
            Rtf = rtf;
        }

        public override string ToString()
        {
            return Name;
        }

        public Guid ID
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
