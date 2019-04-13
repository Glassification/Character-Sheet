using MyCharacterSheet.TypeConverters;
using System;
using System.ComponentModel;

namespace MyCharacterSheet.Characters
{
    #nullable enable
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PresetPair : ExpandableObjectConverter
    {
        public PresetPair(string str1, string str2)
        {
            First = str1;
            Second = str2;
        }

        public PresetPair()
        {
            First = "";
            Second = "";
        }

        public PresetPair Copy()
        {
            PresetPair copy = new PresetPair();

            copy.First = First;
            copy.Second = Second;

            return copy;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Pair")]
        [DisplayName("First")]
        [Description("The first value in the pair.")]
        [TypeConverter(typeof(DamageConverter))]
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
        [TypeConverter(typeof(DamageConverter))]
        public string Second
        {
            get;
            set;
        }
    }
}
