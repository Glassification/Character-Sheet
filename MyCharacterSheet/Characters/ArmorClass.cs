using System;
using System.ComponentModel;
using MyCharacterSheet.TypeConverters;
using MyCharacterSheet.Utility;

namespace MyCharacterSheet.Characters
{
    #nullable enable
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ArmorClass : ExpandableObjectConverter
    {

        #region Members

        private int bonus;
        private int baseAC;

        #endregion

        #region Constructor

        /// =========================================
        /// ArmorClass()
        /// =========================================
        public ArmorClass()
        {
            ArmorWorn = "";
            ArmorType = "None";
            ArmorAC = 0;
            ArmorStealth = "Normal";
            ArmorWeight = 0;
            ShieldType = "";
            ShieldAC = 0;
            ShieldWeight = 0;
            MiscAC = 0;
            MagicAC = 0;
            ArmorStrength = 0;
        }

        /// =========================================
        /// ArmorClass()
        /// =========================================
        public ArmorClass(  string armorWorn, 
                            string armorType, 
                            int armorAC, 
                            string armorStealth, 
                            int armorWeight,       
                            string shieldType, 
                            int shieldAC, 
                            int shieldWeight, 
                            int miscAC, 
                            int magicAC, 
                            int strength)
        {
            ArmorWorn = armorWorn;
            ArmorType = armorType;
            ArmorAC = armorAC;
            ArmorStealth = armorStealth;
            ArmorWeight = armorWeight;
            ShieldType = shieldType;
            ShieldAC = shieldAC;
            ShieldWeight = shieldWeight;
            MiscAC = miscAC;
            MagicAC = magicAC;
            ArmorStrength = strength;
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
        [DisplayName("Armor Strength")]
        [Description("Armor reduces speed by 10 unless strength score is equal to or greater than the listed score.")]
        public int ArmorStrength
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Armor Class")]
        [DisplayName("Armor Weight")]
        [Description("Armor reduces speed by 10 unless strength score is equal to or greater than the listed score.")]
        public int ArmorWeight
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
        [DisplayName("Shield Weight")]
        [Description("Wielding a Shield increases your Armor Class by 2.")]
        public int ShieldWeight
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
                        bonus = Constants.Bonus(Program.Character.Dexterity);
                        baseAC = 10;
                        break;
                    case "Light":
                        bonus = Constants.Bonus(Program.Character.Dexterity);
                        bonus += ArmorAC;
                        baseAC = 0;
                        break;
                    case "Medium":
                        bonus = Math.Min(Constants.Bonus(Program.Character.Dexterity), 2);
                        bonus += ArmorAC;
                        baseAC = 0;
                        break;
                    case "Heavy":
                        bonus = 0;
                        bonus += ArmorAC;
                        baseAC = 0;
                        break;
                }

                bonus = bonus + baseAC + ShieldAC + MiscAC + MagicAC;

                return bonus;
            }
        }

        #endregion
    }
}