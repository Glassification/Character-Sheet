using System;
using System.ComponentModel;
using MyCharacterSheet.TypeConverters;
using MyCharacterSheet.Utility;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ArmorClass : ExpandableObjectConverter
    {

        #region Members

        private int bonus;

        #endregion

        #region Constructor

        /// =========================================
        /// ArmorClass()
        /// =========================================
        public ArmorClass(string armorWorn, string armorType, int armorAC, string armorStealth,
                           string shieldType, int shieldAC, int miscAC, int magicAC)
        {
            ArmorWorn = armorWorn;
            ArmorType = armorType;
            ArmorAC = armorAC;
            ArmorStealth = armorStealth;
            ShieldType = shieldType;
            ShieldAC = shieldAC;
            MiscAC = miscAC;
            MagicAC = magicAC;
        }

        #endregion

        #region Browsable Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Armor Worn")]
        [Description("The name of the armor worn.")]
        public string ArmorWorn
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Armor Type")]
        [Description("Light, Medium, or Heavy Armor")]
        [TypeConverter(typeof(ArmorConverter))]
        public string ArmorType
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Armor AC")]
        [Description("The armors base Armor Class without any modifiers added.")]
        public int ArmorAC
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Armor Stealth")]
        [Description("If the Armor table shows “Disadvantage” in the Stealth column, the wearer has disadvantage on Dexterity (Stealth) checks.")]
        [TypeConverter(typeof(StealthConverter))]
        public string ArmorStealth
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Shield Type")]
        [Description("A Shield is made from wood or metal and is carried in one hand.")]
        public string ShieldType
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Shield AC")]
        [Description("Wielding a Shield increases your Armor Class by 2.")]
        public int ShieldAC
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Misc AC")]
        [Description("Total of any status bonuses that affect Armor Class")]
        public int MiscAC
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Magic AC")]
        [Description("Total of any magic bonuses that affect Armor Class")]
        public int MagicAC
        {
            get;
            set;
        }

        #endregion

        #region Non-Browsable Accessors

        [Browsable(false)]
        [ReadOnly(true)]
        public int AC
        {
            get
            {
                switch (ArmorType)
                {
                    case "None":
                    case "Light":
                        bonus = Constants.Bonus(Program.Character.Dexterity);
                        break;
                    case "Medium":
                        bonus = Math.Min(Constants.Bonus(Program.Character.Dexterity), 2);
                        break;
                    case "Heavy":
                        bonus = 0;
                        break;
                }

                bonus = bonus + ArmorAC + ShieldAC + MiscAC;

                return bonus;
            }
        }

        #endregion
    }
}