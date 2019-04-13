using MyCharacterSheet.Lists;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyCharacterSheet.Utility
{
    #nullable enable
    /// <summary>
    /// Provides constants and static methods for a D&D character sheet.
    /// </summary>
    public static class Constants
    {

        #region Constants

        //Arrays
        private static int[]    oLevels             = { 300, 900, 2700, 6500, 14000, 23000, 34000, 48000, 64000, 85000, 100000,
                                                        120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000 , 0};

        private static int[]    oProficiencies      = { 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

        private static int[]    oAutosaveIntervals  = { 1, 5, 10, 15, 20, 30, 45, 60, 90, 120 };

        private static string[] oDamageTypes        = { "None", "Bludgeoning", "Piercing", "Slashing", "Acid", "Cold", "Fire", "Force",
                                                        "Lightning", "Necrotic", "Poison", "Psychic", "Radiant", "Thunder" };

        private static string[] oSchools            = { "Abjuration", "Conjuration", "Divination", "Enchantment", "Evocation", "Illusion",
                                                        "Necromancy", "Transmutation", "Universal" };

        private static string[] oRaces              = { "Aarakocra", "Aasimar", "Dragonborn", "Drow", "Dwarf", "Elf", "Firbolg", "Genasi",
                                                        "Gnome", "Goblin", "Goliath", "Half-Elf", "Half-Giant", "Half-Orc", "Halfling",
                                                        "Human", "Tiefling" };

        private static string[] oClasses            = { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger",
                                                        "Rogue", "Sorcerer", "Warlock", "Wizard" };

        private static string[] oAbilities          = { "NONE", "STR", "DEX", "CON", "INT", "WIS", "CHA" };

        private static string[] oArmorTypes         = { "None", "Light", "Medium", "Heavy" };

        private static string[] oStealth            = { "Normal", "Disadvantage" };

        private static string[] oYesNo              = { "Yes", "No" };

        private static string[] oConditionState     = { "Afflicted", "Cured" };

        private static string[] oFatiguedState      = { "Cured", "Exhaustion 1", "Exhaustion 2", "Exhaustion 3", "Exhaustion 4", "Exhaustion 5", "Exhaustion 6" };

        private static List<Weapon>     oWeaponList     = new List<Weapon>();
        private static List<Inventory>  oInventoryList  = new List<Inventory>();
        private static List<Ammunition> oAmmoList       = new List<Ammunition>();
        private static List<Spell>      oSpellList      = new List<Spell>();

        //Values
        public const char   DELIMITER   = '|';
        public const int    MAX_LEVEL   = 20;
        public const string NEW_FILE    = "%NEW%";
        public const int    BASE_DC     = 8;
        public const int    OFFSET      = 2;
        public const int    COIN_GROUP  = 50;
        public const float  SIZE_MOD    = 1.25f;

        //Enumerations
        public enum Tables { Abilities, Ammunition, Inventory, Magics, Spells, Weapons };
        public enum Checks { Normal, Disadvantage, Fail };

        #endregion

        #region Static Constructor

        static Constants()
        {
            LoadWeaponLists();
            LoadItemLists();
            LoadAmmoLists();
            LoadSpellList();
        }

        #endregion

        #region Static Methods

        /// =========================================
        /// ScaleFont()
        /// -----------------------------------------
        /// It somehow works. DO NOT TOUCH!!!!!! 
        /// I've seriously spent like an hour trying
        /// to optimize this and it just breaks it.
        /// =========================================
        public static void ScaleFont(Label label)
        {
            if (!label.Text.Equals("") && !label.Name.Contains("label"))
            {
                bool fit = false;
                Size textSize = TextRenderer.MeasureText(label.Text, label.Font);
                int line = 1, width = textSize.Width;
                const float DECREMENT = 0.5f;

                // Check text size
                if (label.Width < textSize.Width)
                {
                    // Loop until correct size is found
                    while (!fit)
                    {
                        textSize = TextRenderer.MeasureText(label.Text, label.Font);

                        // Check if a new line will fit
                        if (label.Height > (textSize.Height * line))
                        {
                            // Text length on new line
                            width -= label.Width;

                            // Check if text will fit
                            if (label.Width < (textSize.Width) - width)
                            {
                                fit = true;
                            }
                            // Reduce size
                            else
                            {
                                label.Font = new Font(label.Font.FontFamily, label.Font.Size - DECREMENT, label.Font.Style);
                                line++;
                            }
                        }
                        // Reduce size
                        else
                        {
                            label.Font = new Font(label.Font.FontFamily, label.Font.Size - DECREMENT, label.Font.Style);
                        }
                    }
                }
            }
        }

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

        #region Default Lists

        /// =========================================
        /// LoadWeaponLists()
        /// =========================================
        private static void LoadWeaponLists()
        {
            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.WeaponList);
                XElement root = xml.Element("Weapons");

                var SimpleMelee = root.Elements("Weapon");
                foreach (XElement elem in SimpleMelee)
                {
                    Weapon weapon = new Weapon(false);

                    weapon.Name     = (string)elem.Attribute("name");
                    weapon.Damage   = (string)elem.Attribute("damage");
                    weapon.Type     = (string)elem.Attribute("type");
                    weapon.Weight   = (string)elem.Attribute("weight");
                    weapon.Range    = (string)elem.Attribute("range");
                    weapon.Notes    = (string)elem.Attribute("notes");

                    oWeaponList.Add(weapon);
                }

                // TODO - Sort the actual xml file
                oWeaponList.Sort((x, y) => x.Name.CompareTo(y.Name));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default weapon list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// =========================================
        /// LoadItemLists()
        /// =========================================
        private static void LoadItemLists()
        {
            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.ItemList);
                XElement root = xml.Element("Items");

                var AdventuringGear = root.Elements("Item");
                foreach (XElement elem in AdventuringGear)
                {
                    Inventory inventory = new Inventory(false);

                    inventory.Name      = (string)elem.Attribute("name");
                    inventory.Weight    = (string)elem.Attribute("weight");
                    inventory.Note      = (string)elem.Attribute("notes");

                    oInventoryList.Add(inventory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default item list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// =========================================
        /// LoadItemLists()
        /// =========================================
        private static void LoadAmmoLists()
        {
            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.AmmoList);
                XElement root = xml.Element("Ammunitions");

                var Ammunitions = root.Elements("Ammunition");
                foreach (XElement elem in Ammunitions)
                {
                    Ammunition ammunition = new Ammunition(false);

                    ammunition.Name     = (string)elem.Attribute("name");
                    ammunition.Quantity = (string)elem.Attribute("qty");

                    oAmmoList.Add(ammunition);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default ammunition list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// =========================================
        /// LoadSpellList()
        /// =========================================
        private static void LoadSpellList()
        {
            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.SpellList);
                XElement root = xml.Element("Spells");

                var Spells = root.Elements("Spell");
                foreach (XElement elem in Spells)
                {
                    Spell spell = new Spell(false);

                    spell.Name          = (string)elem.Attribute("name");
                    spell.Level         = (string)elem.Attribute("level");
                    spell.Page          = (string)elem.Attribute("page");
                    spell.School        = (string)elem.Attribute("school");
                    spell.Ritual        = (string)elem.Attribute("ritual");
                    spell.Components    = ((string)elem.Attribute("comp")).Equals("")   ? "N/A" : (string)elem.Attribute("comp");
                    spell.Concentration = (string)elem.Attribute("concen");
                    spell.Range         = ((string)elem.Attribute("range")).Equals("")  ? "N/A" : (string)elem.Attribute("range");
                    spell.Duration      = (string)elem.Attribute("duration");
                    spell.Area          = ((string)elem.Attribute("area")).Equals("")   ? "N/A" : (string)elem.Attribute("area");
                    spell.Save          = ((string)elem.Attribute("save")).Equals("")   ? "N/A" : (string)elem.Attribute("save");
                    spell.Damage        = ((string)elem.Attribute("damage")).Equals("") ? "N/A" : (string)elem.Attribute("damage");
                    spell.Description   = (string)elem.Attribute("description");
                    spell.Prepared      = (string)elem.Attribute("prepared");

                    oSpellList.Add(spell);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default spell list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region List Accessors

        /// =========================================
        /// WeaponList()
        /// =========================================
        public static Weapon? WeaponList(int index)
        {
            Weapon? weapon = null;

            if (index >= 0 && index < oWeaponList.Count)
            {
                weapon = oWeaponList[index];
            }

            return weapon;
        }

        /// =========================================
        /// WeaponListLength()
        /// =========================================
        public static int WeaponListLength()
        {
            return oWeaponList.Count;
        }

        /// =========================================
        /// ItemList()
        /// =========================================
        public static Inventory? ItemList(int index)
        {
            Inventory? inventory = null;

            if (index >= 0 && index < oInventoryList.Count)
            {
                inventory = oInventoryList[index];
            }

            return inventory;
        }

        /// =========================================
        /// ItemListLength()
        /// =========================================
        public static int ItemListLength()
        {
            return oInventoryList.Count;
        }

        /// =========================================
        /// AmmoList()
        /// =========================================
        public static Ammunition? AmmoList(int index)
        {
            Ammunition? ammunition = null;

            if (index >= 0 && index < oAmmoList.Count)
            {
                ammunition = oAmmoList[index];
            }

            return ammunition;
        }

        /// =========================================
        /// AmmoListLength()
        /// =========================================
        public static int AmmoListLength()
        {
            return oAmmoList.Count;
        }

        /// =========================================
        /// SpellList()
        /// =========================================
        public static Spell? SpellList(int index)
        {
            Spell? spell = null;

            if (index >= 0 && index < oSpellList.Count)
            {
                spell = oSpellList[index];
            }

            return spell;
        }

        /// =========================================
        /// SpellListLength()
        /// =========================================
        public static int SpellListLength()
        {
            return oSpellList.Count;
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

        #region School Accessors

        /// =========================================
        /// Stealth()
        /// =========================================
        public static string School(int index)
        {
            string str = "";

            if (index >= 0 && index < oSchools.Length)
            {
                str = oSchools[index];
            }

            return str;
        }

        /// =========================================
        /// StealthLength()
        /// =========================================
        public static int SchoolLength()
        {
            return oSchools.Length;
        }

        /// =========================================
        /// GetStealth()
        /// =========================================
        public static string[] GetSchool()
        {
            string[] newSchool = new string[oSchools.Length];

            Array.Copy(oSchools, newSchool, oSchools.Length);

            return newSchool;
        }

        #endregion

        #region Class Accessors

        /// =========================================
        /// Class()
        /// =========================================
        public static string Class(int index)
        {
            string str = "";

            if (index >= 0 && index < oClasses.Length)
            {
                str = oClasses[index];
            }

            return str;
        }

        /// =========================================
        /// ClassLength()
        /// =========================================
        public static int ClassLength()
        {
            return oClasses.Length;
        }

        /// =========================================
        /// GetClass()
        /// =========================================
        public static string[] GetClass()
        {
            string[] newClass = new string[oClasses.Length];

            Array.Copy(oClasses, newClass, oClasses.Length);

            return newClass;
        }

        #endregion

        #region Race Accessors

        /// =========================================
        /// Class()
        /// =========================================
        public static string Race(int index)
        {
            string str = "";

            if (index >= 0 && index < oRaces.Length)
            {
                str = oRaces[index];
            }

            return str;
        }

        /// =========================================
        /// ClassLength()
        /// =========================================
        public static int RaceLength()
        {
            return oRaces.Length;
        }

        /// =========================================
        /// GetClass()
        /// =========================================
        public static string[] GetRace()
        {
            string[] newRace = new string[oRaces.Length];

            Array.Copy(oRaces, newRace, oRaces.Length);

            return newRace;
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

        #region YesNo Accessors

        /// =========================================
        /// Stealth()
        /// =========================================
        public static string YesNo(int index)
        {
            string str = "";

            if (index >= 0 && index < oYesNo.Length)
            {
                str = oYesNo[index];
            }

            return str;
        }

        /// =========================================
        /// StealthLength()
        /// =========================================
        public static int YesNoLength()
        {
            return oYesNo.Length;
        }

        /// =========================================
        /// GetStealth()
        /// =========================================
        public static string[] GetYesNo()
        {
            string[] newYesNo = new string[oYesNo.Length];

            Array.Copy(oYesNo, newYesNo, oYesNo.Length);

            return newYesNo;
        }

        #endregion

        #region Conditon Accessors

        /// =========================================
        /// ConditionState()
        /// =========================================
        public static string ConditionState(int index)
        {
            string str = "";

            if (index >= 0 && index < oConditionState.Length)
            {
                str = oConditionState[index];
            }

            return str;
        }

        /// =========================================
        /// ConditionStateLength()
        /// =========================================
        public static int ConditionStateLength()
        {
            return oConditionState.Length;
        }

        /// =========================================
        /// GetConditionState()
        /// =========================================
        public static string[] GetConditionState()
        {
            string[] newConditionState = new string[oConditionState.Length];

            Array.Copy(oConditionState, newConditionState, oConditionState.Length);

            return newConditionState;
        }

        #endregion

        #region Fatigued Accessors

        /// =========================================
        /// FatiguedState()
        /// =========================================
        public static string FatiguedState(int index)
        {
            string str = "";

            if (index >= 0 && index < oFatiguedState.Length)
            {
                str = oFatiguedState[index];
            }

            return str;
        }

        /// =========================================
        /// FatiguedStateLength()
        /// =========================================
        public static int FatiguedStateLength()
        {
            return oFatiguedState.Length;
        }

        /// =========================================
        /// GetFatiguedState()
        /// =========================================
        public static string[] GetFatiguedState()
        {
            string[] newFatiguedState = new string[oFatiguedState.Length];

            Array.Copy(oFatiguedState, newFatiguedState, oFatiguedState.Length);

            return newFatiguedState;
        }

        #endregion

        #region Colour Accessors

        /// =========================================
        /// DarkGrey()
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 45, 45, 48]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 95, 95, 95]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 28, 28, 28]
        /// </summary>
        /// =========================================
        public static Color ControlGrey
        {
            get
            {
                return Color.FromArgb(255, 28, 28, 28);
            }
        }

        /// =========================================
        /// DarkBlue()
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 22, 54, 92]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 31, 73, 125]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 216, 228, 188]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 252, 213, 180]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 187, 74, 67]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 181, 215, 243]
        /// </summary>
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
        /// -----------------------------------------
        /// <summary>
        /// ARGB value of [255, 0, 120, 215]
        /// </summary>
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
