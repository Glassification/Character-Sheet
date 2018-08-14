using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyCharacterSheet.Utility
{
    public static class Constants
    {

        #region Constants

        //Arrays
        private static int[]    oLevels        = { 300, 900, 2700, 6500, 14000, 23000, 34000, 48000, 64000, 85000, 100000,
                                                   120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000 , 0};

        private static readonly int[] oProficiencies = { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

        private static readonly int[] oAutosaveIntervals = { 1, 5, 10, 15, 20, 30, 45, 60, 90, 120 };

        private static string[] oDamageTypes   = { "None", "Bludgeoning", "Piercing", "Slashing", "Acid", "Cold", "Fire", "Force",
                                                 "Lightning", "Necrotic", "Poison", "Psychic", "Radiant", "Thunder" };

        private static string[] oAbilities     = { "NONE", "STR", "DEX", "CON", "INT", "WIS", "CHA" };

        private static string[] oArmorTypes    = { "None", "Light", "Medium", "Heavy" };

        private static string[] oStealth       = { "Normal", "Disadvantage" };

        //Values
        public const char   DELIMITER = '|';
        public const int    MAX_LEVEL = 20;
        public const string NEW_FILE  = "%NEW%";
        public const int    BASE_DC   = 8;
        public const int    OFFSET    = 2;

        //Enumerations
        public enum Indentation { Current, Increment, Decrement };

        #endregion

        #region Static Methods

        /// =========================================
        /// TextColour()
        /// =========================================
        public static Color TextColour(int total, int used)
        {
            Color colour;

            if (total == used)
                colour = Color.DarkRed;
            else
                colour = MediumBlue;

            return colour;
        }

        /// =========================================
        /// TotalBoxColour()
        /// =========================================
        public static Color TotalBoxColour(int total, int used)
        {
            Color colour;

            if (total == used)
                colour = Constants.MediumGrey;
            else
                colour = Color.White;

            return colour;
        }

        /// =========================================
        /// UsedBoxColour()
        /// =========================================
        public static Color UsedBoxColour(int total, int used)
        {
            Color colour;

            if (total == used)
                colour = Color.LightPink;
            else
                colour = Color.White;

            return colour;
        }

        /// =========================================
        /// Experience()
        /// =========================================
        public static int Experience(int level)
        {
            int exp = 0;

            if (level > 0 && level <= oLevels.Length)
            {
                exp = oLevels[level - 1];
            }

            return exp;
        }

        /// =========================================
        /// Proficiency()
        /// =========================================
        public static int Proficiency(int level)
        {
            int proficency = 0;

            if (level > 0 && level <= oLevels.Length)
            {
                proficency = oProficiencies[level - 1];
            }

            return proficency;
        }

        /// =========================================
        /// Bonus()
        /// =========================================
        public static int Bonus(int score)
        {
            return (int)Math.Floor((score - 10) / 2.0);
        }

        #endregion

        #region Autosave Accessors

        /// =========================================
        /// AutosaveInterval()
        /// =========================================
        public static int AutosaveInterval(int index)
        {
            int interval = 0;

            if (index >= 0 && index < oAutosaveIntervals.Length)
            {
                interval = oAutosaveIntervals[index];
            }

            return interval;
        }

        /// =========================================
        /// AutosaveIndex()
        /// =========================================
        public static int AutosaveIndex(int value)
        {
            int index = 0;
            bool end = false;

            for (int i = 0; i < oAutosaveIntervals.Length && !end; i++)
            {
                if (oAutosaveIntervals[i] == value)
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        /// =========================================
        /// AutosaveIntervalLength()
        /// =========================================
        public static int AutosaveIntervalLength()
        {
            return oAutosaveIntervals.Length;
        }

        #endregion

        #region DamageType Accessors

        /// =========================================
        /// DamageType()
        /// =========================================
        public static string DamageType(int index)
        {
            string str = "";

            if (index >= 0 && index < oDamageTypes.Length)
            {
                str = oDamageTypes[index];
            }

            return str;
        }

        /// =========================================
        /// DamageTypeLength()
        /// =========================================
        public static int DamageTypeLength()
        {
            return oDamageTypes.Length;
        }

        /// =========================================
        /// GetDamageTypes()
        /// =========================================
        public static string[] GetDamageTypes()
        {
            string[] newDamageTypes = new string[oDamageTypes.Length];

            Array.Copy(oDamageTypes, newDamageTypes, oDamageTypes.Length);

            return newDamageTypes;
        }

        #endregion

        #region Abilities Accessors

        /// =========================================
        /// Ability()
        /// =========================================
        public static string Ability(int index)
        {
            string str = "";

            if (index >= 0 && index < oAbilities.Length)
            {
                str = oAbilities[index];
            }

            return str;
        }

        /// =========================================
        /// AbilitiesLength()
        /// =========================================
        public static int AbilitiesLength()
        {
            return oAbilities.Length;
        }

        /// =========================================
        /// GetAbilities()
        /// =========================================
        public static string[] GetAbilities()
        {
            string[] newAbilities = new string[oAbilities.Length];

            Array.Copy(oAbilities, newAbilities, oAbilities.Length);

            return newAbilities;
        }

        #endregion

        #region ArmorTypes Accessors

        /// =========================================
        /// ArmorType()
        /// =========================================
        public static string ArmorType(int index)
        {
            string str = "";

            if (index >= 0 && index < oArmorTypes.Length)
            {
                str = oArmorTypes[index];
            }

            return str;
        }

        /// =========================================
        /// ArmorTypesLength()
        /// =========================================
        public static int ArmorTypesLength()
        {
            return oArmorTypes.Length;
        }

        /// =========================================
        /// GetArmorTypes()
        /// =========================================
        public static string[] GetArmorTypes()
        {
            string[] newArmorTypes = new string[oArmorTypes.Length];

            Array.Copy(oArmorTypes, newArmorTypes, oArmorTypes.Length);

            return newArmorTypes;
        }

        #endregion

        #region Stealth Accessors

        /// =========================================
        /// Stealth()
        /// =========================================
        public static string Stealth(int index)
        {
            string str = "";

            if (index >= 0 && index < oStealth.Length)
            {
                str = oStealth[index];
            }

            return str;
        }

        /// =========================================
        /// StealthLength()
        /// =========================================
        public static int StealthLength()
        {
            return oStealth.Length;
        }

        /// =========================================
        /// GetStealth()
        /// =========================================
        public static string[] GetStealth()
        {
            string[] newStealth = new string[oStealth.Length];

            Array.Copy(oStealth, newStealth, oStealth.Length);

            return newStealth;
        }

        #endregion

        #region Colour Accessors

        /// =========================================
        /// DarkGrey()
        /// =========================================
        public static Color DarkGrey
        {
            get
            {
                return Color.FromArgb(255, 45, 45, 48);
            }
        }

        /// =========================================
        /// MediumGrey()
        /// =========================================
        public static Color MediumGrey
        {
            get
            {
                return Color.FromArgb(255, 95, 95, 95);
            }
        }

        /// =========================================
        /// ControlGrey()
        /// =========================================
        public static Color ControlGrey
        {
            get
            {
                return Color.FromArgb(255, 28, 28, 28);
            }
        }

        /// =========================================
        /// HighlightGrey()
        /// =========================================
        public static Color HighlightGrey
        {
            get
            {
                return Color.FromArgb(255, 104, 104, 104);
            }
        }

        /// =========================================
        /// DarkBlue()
        /// =========================================
        public static Color DarkBlue
        {
            get
            {
                return Color.FromArgb(255, 22, 54, 92);
            }
        }

        /// =========================================
        /// MediumBlue()
        /// =========================================
        public static Color MediumBlue
        {
            get
            {
                return Color.FromArgb(255, 31, 73, 125);
            }
        }

        /// =========================================
        /// LightGreen()
        /// =========================================
        public static Color LightGreen
        {
            get
            {
                return Color.FromArgb(255, 216, 228, 188);
            }
        }

        /// =========================================
        /// LightYellow()
        /// =========================================
        public static Color LightYellow
        {
            get
            {
                return Color.FromArgb(255, 252, 213, 180);
            }
        }

        /// =========================================
        /// MediumRed()
        /// =========================================
        public static Color MediumRed
        {
            get
            {
                return Color.FromArgb(255, 187, 74, 67);
            }
        }

        /// =========================================
        /// HighlightBlue()
        /// =========================================
        public static Color HighlightBlue
        {
            get
            {
                return Color.FromArgb(255, 181, 215, 243);
            }
        }

        /// =========================================
        /// HighlightBlue()
        /// =========================================
        public static Color OutlineBlue
        {
            get
            {
                return Color.FromArgb(255, 0, 120, 215);
            }
        }

        #endregion

    }
}
