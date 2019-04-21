using Concierge.TypeConverters;
using static Concierge.Utility.Constants;
using System;
using System.ComponentModel;

namespace Concierge.Characters
{
    #nullable enable
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PlayerClass : ExpandableObjectConverter
    {
        private int     iLevel;
        private string  sName;

        public PlayerClass(string className, int classLevel, int index)
        {
            sName = className;
            iLevel = classLevel;
            Index = index;
        }

        public PlayerClass(int index)
        {
            sName = "";
            iLevel = 0;
            Index = index;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Class")]
        [DisplayName("Class")]
        [Description("Class broadly describes a character’s vocation, what special talents he or she possesses, and the tactics he or she is most likely to employ.")]
        [TypeConverter(typeof(PlayerClassConverter))]
        public string ClassName
        {
            get
            {
                return sName;
            }
            set
            {
                if (Program.Character.ValidName(value, Index))
                {
                    sName = value;
                }
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Class")]
        [DisplayName("Level")]
        [Description("Typically, a character starts at 1st level and advances in level by adventuring and gaining experience points (XP).")]
        public int ClassLevel
        {
            get
            {
                return iLevel;
            }
            set
            {
                if (value <= MAX_LEVEL && value >= 0)
                {
                    if (Program.Character.ValidTotalLevel(value, Index))
                    {
                        iLevel = value;
                    }
                }
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        private int Index
        {
            get;
            set;
        }
    }
}
