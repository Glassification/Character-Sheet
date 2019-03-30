using MyCharacterSheet.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Spellcasting : ExpandableObjectConverter
    {

        #region Members

        public List<Spell> oSpells = new List<Spell>();
        public List<Magic> oMagic = new List<Magic>();

        #endregion

        #region Constructor

        public Spellcasting()
        {
            Level = 0;
            PactTotal = 0;
            OneTotal = 0;
            TwoTotal = 0;
            ThreeTotal = 0;
            FourTotal = 0;
            FiveTotal = 0;
            SixTotal = 0;
            SevenTotal = 0;
            EightTotal = 0;
            NineTotal = 0;
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

        public Spellcasting(int level, 
                            int totalPact, 
                            int totalOne, 
                            int totalTwo, 
                            int totalThree, 
                            int totalFour, 
                            int totalFive, 
                            int toalSix, 
                            int totalSeven,            
                            int totalEight, 
                            int totalNine, 
                            int usedPact, 
                            int usedOne, 
                            int usedTwo, 
                            int usedThree, 
                            int usedFour, 
                            int usedFive, 
                            int usedSix,           
                            int usedSeven, 
                            int usedEight, 
                            int usedNine)
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

        #endregion

        #region Methods

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

        // =========================================
        /// GetSpellIndex()
        /// =========================================
        public int GetSpellIndex(Guid id)
        {
            bool end = false;
            int index = -1;

            for (int i = 0; i < oSpells.Count && !end; i++)
            {
                if (oSpells[i].ID.Equals(id))
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        /// =========================================
        /// RemoveSpellItem()
        /// =========================================
        public void RemoveSpellItem(Guid id)
        {
            bool end = false;

            for (int i = 0; i < oSpells.Count && !end; i++)
            {
                if (oSpells[i].ID.Equals(id))
                {
                    oSpells.RemoveAt(i);
                    end = true;
                }
            }
        }

        // =========================================
        /// GetMagicIndex()
        /// =========================================
        public int GetMagicIndex(Guid id)
        {
            bool end = false;
            int index = -1;

            for (int i = 0; i < oMagic.Count && !end; i++)
            {
                if (oMagic[i].ID.Equals(id))
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        // =========================================
        /// RemoveMagicItem()
        /// =========================================
        public void RemoveMagicItem(Guid id)
        {
            bool end = false;

            for (int i = 0; i < oMagic.Count && !end; i++)
            {
                if (oMagic[i].ID.Equals(id))
                {
                    oMagic.RemoveAt(i);
                    end = true;
                }
            }
        }

        #endregion

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
        [Category("Spell Slots")]
        [DisplayName("Pact")]
        [Description("Available pact spell slots.")]
        public int PactTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("1st")]
        [Description("Available 1st level spell slots.")]
        public int OneTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("2nd")]
        [Description("Available 2nd level spell slots.")]
        public int TwoTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("3rd")]
        [Description("Available 3rd level spell slots.")]
        public int ThreeTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("4th")]
        [Description("Available 4th level spell slots.")]
        public int FourTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("5th")]
        [Description("Available 5th level spell slots.")]
        public int FiveTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("6th")]
        [Description("Available 6th level spell slots.")]
        public int SixTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("7th")]
        [Description("Available 7th level spell slots.")]
        public int SevenTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("8th")]
        [Description("Available 8th level spell slots.")]
        public int EightTotal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Spell Slots")]
        [DisplayName("9th")]
        [Description("Available 9th level spell slots.")]
        public int NineTotal
        {
            get;
            set;
        }

        #endregion

        #region Used Accessors

        [Browsable(false)]
        [ReadOnly(true)]
        public int PactUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int OneUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int TwoUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int ThreeUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int FourUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int FiveUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int SixUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int SevenUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int EightUsed
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int NineUsed
        {
            get;
            set;
        }

        #endregion

    }
}
