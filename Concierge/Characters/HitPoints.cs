using System.ComponentModel;
using System.Drawing;

namespace Concierge.Characters
{
    #nullable enable
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class HitPoints : ExpandableObjectConverter
    {

        #region Members

        private int iMaxHP;
        private int iCurrentHP;
        private int iTempHP;

        #endregion

        #region Constructor

        /// =========================================
        /// HitPoints()
        /// =========================================
        public HitPoints()
        {
            iCurrentHP = 0;
            iMaxHP = 0;
            iTempHP = 0;
            Conditions = new Conditions();
            D6 = 0;
            D8 = 0;
            D10 = 0;
            D12 = 0;
            SpentD6 = 0;
            SpentD8 = 0;
            SpentD10 = 0;
            SpentD12 = 0;
        }

        /// =========================================
        /// HitPoints()
        /// =========================================
        public HitPoints(   int hp, 
                            int maxHP, 
                            int tempHP, 
                            Conditions conditions, 
                            int d6, 
                            int d8, 
                            int d10, 
                            int d12, 
                            int spentD6, 
                            int spentD8, 
                            int spentD10, 
                            int spentD12)
        {
            iCurrentHP = hp;
            iMaxHP = maxHP;
            iTempHP = tempHP;
            Conditions = conditions;
            D6 = d6;
            D8 = d8;
            D10 = d10;
            D12 = d12;
            SpentD6 = spentD6;
            SpentD8 = spentD8;
            SpentD10 = spentD10;
            SpentD12 = spentD12;
        }

        #endregion

        #region Browsable Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Points")]
        [DisplayName("Max HP")]
        [Description("Max hit points a character can have.")]
        public int MaxHP
        {
            get
            {
                return iMaxHP;
            }
            set
            {
                if (value > 0)
                {
                    iMaxHP = value;
                }
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Points")]
        [DisplayName("Current HP")]
        [Description("The current health of a character.")]
        public int HP
        {
            get
            {
                return iCurrentHP;
            }
            set
            {
                if (value <= MaxHP && value >= 0)
                {
                    iCurrentHP = value;
                }
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Points")]
        [DisplayName("Temporary HP")]
        [Description("Extra hit points a character may get.")]
        public int TempHP
        {
            get
            {
                return iTempHP;
            }
            set
            {
                if (value >= 0)
                {
                    iTempHP = value;
                }
            }
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Points")]
        [DisplayName("Conditions")]
        [Description("Effects that will affect a characters health.")]
        public Conditions Conditions
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Dice")]
        [DisplayName("D6")]
        [Description("Available D6 hit dice.")]
        public int D6
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Dice")]
        [DisplayName("D8")]
        [Description("Available D8 hit dice.")]
        public int D8
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Dice")]
        [DisplayName("D10")]
        [Description("Available D10 hit dice.")]
        public int D10
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Hit Dice")]
        [DisplayName("D12")]
        [Description("Available D12 hit dice.")]
        public int D12
        {
            get;
            set;
        }

        #endregion

        #region Non-Browsable Accessors

        [Browsable(false)]
        [ReadOnly(true)]
        public int SpentD6
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int SpentD8
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int SpentD10
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int SpentD12
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public Color HitPointsColour
        {
            get
            {
                int third = Program.Character.HitPoints.MaxHP / 3;
                Color colour;

                if (HP < third && HP > 0)
                    colour = Color.Red;
                else if (HP >= third * 2)
                    colour = Color.Green;
                else if (HP <= 0)
                    colour = Color.Gray;
                else
                    colour = Color.DarkOrange;

                return colour;
            }
        }

        #endregion

    }
}
