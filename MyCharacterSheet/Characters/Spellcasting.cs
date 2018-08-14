using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Spellcasting : ExpandableObjectConverter
    {
        public List<string> spellList = new List<string>();
        public List<string> spellClass = new List<string>();

        public Spellcasting(int level, int totalPact, int totalOne, int totalTwo, int totalThree, int totalFour, int totalFive, int toalSix, int totalSeven, 
                            int totalEight, int totalNine, int usedPact, int usedOne, int usedTwo, int usedThree, int usedFour, int usedFive, int usedSix, 
                            int usedSeven, int usedEight, int usedNine)
        {
            Level = level;
            PactTotal = totalPact;
            OneTotal = totalOne;
            TwoTotal = totalTwo;
            ThreeTotal = totalThree;
            FourTotal = totalFour;
            FiveTotal = totalFive;
            SixTotal = toalSix;
            SevenTotal = totalSeven;
            EightTotal = totalEight;
            NineTotal = totalNine;
            PactUsed = usedPact;
            OneUsed = usedOne;
            TwoUsed = usedTwo;
            ThreeUsed = usedThree;
            FourUsed = usedFour;
            FiveUsed = usedFive;
            SixUsed = usedSix;
            SevenUsed = usedSeven;
            EightUsed = usedEight;
            NineUsed = usedNine;
        }

        /// =========================================
        /// ResetSpellSlots()
        /// =========================================
        public void ResetSpellSlots()
        {
            PactUsed = 0;
            OneUsed = 0;
            TwoUsed = 0;
            ThreeUsed = 0;
            FourUsed = 0;
            FiveUsed = 0;
            SixUsed = 0;
            SevenUsed = 0;
            EightUsed = 0;
            NineUsed = 0;
        }

        #region Detail Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spellcasting")]
        [DisplayName("Caster Level")]
        [Description("The total spellcasting class level.")]
        public int Level
        {
            get;
            set;
        }

        #endregion

        #region Total Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total Pact")]
        [Description("Available pact spell slots.")]
        public int PactTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 1st")]
        [Description("Available 1st level spell slots.")]
        public int OneTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 2nd")]
        [Description("Available 2nd level spell slots.")]
        public int TwoTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 3rd")]
        [Description("Available 3rd level spell slots.")]
        public int ThreeTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 4th")]
        [Description("Available 4th level spell slots.")]
        public int FourTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 5th")]
        [Description("Available 5th level spell slots.")]
        public int FiveTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 6th")]
        [Description("Available 6th level spell slots.")]
        public int SixTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 7th")]
        [Description("Available 7th level spell slots.")]
        public int SevenTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 8th")]
        [Description("Available 8th level spell slots.")]
        public int EightTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Total Spell Slots")]
        [DisplayName("Total 9th")]
        [Description("Available 9th level spell slots.")]
        public int NineTotal
        {
            get;
            set;
        }

        #endregion

        #region Used Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used Pact")]
        [Description("Spent Pact level spells.")]
        public int PactUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 1st")]
        [Description("Spent 1st level spells.")]
        public int OneUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 2nd")]
        [Description("Spent 2nd level spells.")]
        public int TwoUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 3rd")]
        [Description("Spent 3rd level spells.")]
        public int ThreeUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 4th")]
        [Description("Spent 4th level spells.")]
        public int FourUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 5th")]
        [Description("Spent 5th level spells.")]
        public int FiveUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 6th")]
        [Description("Spent 6th level spells.")]
        public int SixUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 7th")]
        [Description("Spent 7th level spells.")]
        public int SevenUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 8th")]
        [Description("Spent 8th level spells.")]
        public int EightUsed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Used Spells")]
        [DisplayName("Used 9th")]
        [Description("Spent 9th level spells.")]
        public int NineUsed
        {
            get;
            set;
        }

        #endregion

    }
}
