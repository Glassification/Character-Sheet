using System;
using System.ComponentModel;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Pair : ExpandableObjectConverter
    {
        public Pair(string str1, string str2)
        {
            First = str1;
            Second = str2;
        }

        public Pair()
        {
            First = "";
            Second = "";
        }

        public Pair Copy()
        {
            Pair copy = new Pair();

            copy.First = First;
            copy.Second = Second;

            return copy;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Pair")]
        [DisplayName("First")]
        [Description("The first value in the pair.")]
        public string First
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Pair")]
        [DisplayName("Second")]
        [Description("The second value in the pair.")]
        public string Second
        {
            get;
            set;
        }
    }
}
