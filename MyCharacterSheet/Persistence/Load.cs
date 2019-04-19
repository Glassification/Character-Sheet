using MyCharacterSheet.Characters;
using MyCharacterSheet.Lists;
using MyCharacterSheet.SavingThrowsNamespace;
using MyCharacterSheet.SkillsNamespace;
using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyCharacterSheet.Persistence
{
    /// <summary>
    /// Provides static methods for loading a character sheet from xml.
    /// </summary>
    public static class Load
    {

        #region Methods

        /// =========================================
        /// LoadCharacterSheetFromFile()
        /// =========================================
        public static void LoadCharacterSheetFromFile(Character character)
        {
            XDocument xml = XDocument.Load(Program.FileLocation);
            LoadCharacterSheetXML(character, xml);
        }

        /// =========================================
        /// LoadCharacterSheetFromString()
        /// =========================================
        public static void LoadCharacterSheetFromString(Character character, string xmlString)
        {
            XDocument xml = XDocument.Parse(xmlString);
            LoadCharacterSheetXML(character, xml);
        }

        /// =========================================
        /// LoadCharacterSheetXML()
        /// =========================================
        private static void LoadCharacterSheetXML(Character character, XDocument xml)
        {
            try
            {
                XElement root = xml.Element("Character");
                XElement element;

                //-------------------------------------------------------------------------------------------------------
                // Parse Settings
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Settings");
                Settings.MuteState           = bool.Parse(    (string)element.Element("Mute").Attribute("value"));
                Settings.RememberMute        = bool.Parse(    (string)element.Element("Mute").Attribute("remember"));
                Settings.RememberLastTab     = bool.Parse(    (string)element.Element("Tab").Attribute("remember"));
                Settings.LastTab             = int.Parse(     (string)element.Element("Tab").Attribute("last"));
                Settings.AutosaveEnable      = bool.Parse(    (string)element.Element("AutoSave").Attribute("enabled"));
                Settings.AutosaveInterval    = int.Parse(     (string)element.Element("AutoSave").Attribute("interval"));
                Settings.HideAnimalCompanion = bool.Parse(    (string)element.Element("AnimalCompanion").Attribute("hidden"));
                Settings.UseCoinWeight          = bool.Parse(    (string)element.Element("CoinWeight").Attribute("ignore"));
                Settings.UseEncumbrance         = bool.Parse(    (string)element.Element("Encumbrance").Attribute("use"));

                //-------------------------------------------------------------------------------------------------------
                // Parse Attributes
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Attributes");
                character.Strength     = int.Parse((string)element.Element("Strength").Attribute("value"));
                character.Dexterity    = int.Parse((string)element.Element("Dexterity").Attribute("value"));
                character.Constitution = int.Parse((string)element.Element("Constitution").Attribute("value"));
                character.Intelligence = int.Parse((string)element.Element("Intelligence").Attribute("value"));
                character.Wisdom       = int.Parse((string)element.Element("Wisdom").Attribute("value"));
                character.Charisma     = int.Parse((string)element.Element("Charisma").Attribute("value"));

                //-------------------------------------------------------------------------------------------------------
                // Parse Saving Throws
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Proficiency").Element("SavingThrows");
                character.oSavingThrows.Add(new Strength(    (bool)element.Element("Strength").Attribute("proficiency")));
                character.oSavingThrows.Add(new Dexterity(   (bool)element.Element("Dexterity").Attribute("proficiency")));
                character.oSavingThrows.Add(new Constitution((bool)element.Element("Constitution").Attribute("proficiency")));
                character.oSavingThrows.Add(new Intelligence((bool)element.Element("Intelligence").Attribute("proficiency")));
                character.oSavingThrows.Add(new Wisdom(      (bool)element.Element("Wisdom").Attribute("proficiency")));
                character.oSavingThrows.Add(new Charisma(    (bool)element.Element("Charisma").Attribute("proficiency")));

                //-------------------------------------------------------------------------------------------------------
                // Parse Skills
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Proficiency").Element("Skills");
                character.oSkills.Add(new Athletics(     (bool)element.Element("Athletics").Attribute("proficiency"),      (bool)element.Element("Athletics").Attribute("expertise")));
                character.oSkills.Add(new Acrobatics(    (bool)element.Element("Acrobatics").Attribute("proficiency"),     (bool)element.Element("Acrobatics").Attribute("expertise")));
                character.oSkills.Add(new SleightOfHand( (bool)element.Element("SleightOfHand").Attribute("proficiency"),  (bool)element.Element("SleightOfHand").Attribute("expertise")));
                character.oSkills.Add(new Stealth(       (bool)element.Element("Stealth").Attribute("proficiency"),        (bool)element.Element("Stealth").Attribute("expertise")));
                character.oSkills.Add(new Arcana(        (bool)element.Element("Arcana").Attribute("proficiency"),         (bool)element.Element("Arcana").Attribute("expertise")));
                character.oSkills.Add(new History(       (bool)element.Element("History").Attribute("proficiency"),        (bool)element.Element("History").Attribute("expertise")));
                character.oSkills.Add(new Investigation( (bool)element.Element("Investigation").Attribute("proficiency"),  (bool)element.Element("Investigation").Attribute("expertise")));
                character.oSkills.Add(new Nature(        (bool)element.Element("Nature").Attribute("proficiency"),         (bool)element.Element("Nature").Attribute("expertise")));
                character.oSkills.Add(new Religion(      (bool)element.Element("Religion").Attribute("proficiency"),       (bool)element.Element("Religion").Attribute("expertise")));
                character.oSkills.Add(new AnimalHandling((bool)element.Element("AnimalHandling").Attribute("proficiency"), (bool)element.Element("AnimalHandling").Attribute("expertise")));
                character.oSkills.Add(new Insight(       (bool)element.Element("Insight").Attribute("proficiency"),        (bool)element.Element("Insight").Attribute("expertise")));
                character.oSkills.Add(new Medicine(      (bool)element.Element("Medicine").Attribute("proficiency"),       (bool)element.Element("Medicine").Attribute("expertise")));
                character.oSkills.Add(new Perception(    (bool)element.Element("Perception").Attribute("proficiency"),     (bool)element.Element("Perception").Attribute("expertise")));
                character.oSkills.Add(new Survival(      (bool)element.Element("Survival").Attribute("proficiency"),       (bool)element.Element("Survival").Attribute("expertise")));
                character.oSkills.Add(new Deception(     (bool)element.Element("Deception").Attribute("proficiency"),      (bool)element.Element("Deception").Attribute("expertise")));
                character.oSkills.Add(new Intimidation(  (bool)element.Element("Intimidation").Attribute("proficiency"),   (bool)element.Element("Intimidation").Attribute("expertise")));
                character.oSkills.Add(new Performance(   (bool)element.Element("Performance").Attribute("proficiency"),    (bool)element.Element("Performance").Attribute("expertise")));
                character.oSkills.Add(new Persuasion(    (bool)element.Element("Persuasion").Attribute("proficiency"),     (bool)element.Element("Persuasion").Attribute("expertise")));

                //-------------------------------------------------------------------------------------------------------
                // Parse Equipment
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Proficiency").Element("Equipment");
                character.Armor   = (string)element.Element("Armor").Attribute("proficiency");
                character.Shields = (string)element.Element("Shields").Attribute("proficiency");
                character.Weapons = (string)element.Element("Weapons").Attribute("proficiency");
                character.Tools   = (string)element.Element("Tools").Attribute("proficiency");

                //-------------------------------------------------------------------------------------------------------
                // Parse Details
                //-------------------------------------------------------------------------------------------------------
                string playerClass;
                int playerLevel;

                element = root.Element("Details");
                character.Name                   = (string)element.Element("Name").Attribute("value");
                character.Race                   = (string)element.Element("Race").Attribute("value");
                character.Background             = (string)element.Element("Background").Attribute("value");
                character.Alignment              = (string)element.Element("Alignment").Attribute("value");
                character.EXP                    = (int)   element.Element("Experience").Attribute("value");
                character.Language               = (string)element.Element("Language").Attribute("value");
                character.InitiativeBonus        = (int)   element.Element("InitiativeBonus").Attribute("value");
                character.PassivePerceptionBonus = (int)   element.Element("PerceptionBonus").Attribute("value");
                character.Movement               = (string)element.Element("Movement").Attribute("value");
                character.Vision                 = (string)element.Element("Vision").Attribute("value");

                playerClass = (string)element.Element("Class1").Attribute("class");
                playerLevel = (int)element.Element("Class1").Attribute("level");
                character.PlayerClass1 = new PlayerClass(playerClass, playerLevel, 0);

                playerClass = (string)element.Element("Class2").Attribute("class");
                playerLevel = (int)element.Element("Class2").Attribute("level");
                character.PlayerClass2 = new PlayerClass(playerClass, playerLevel, 1);

                playerClass = (string)element.Element("Class3").Attribute("class");
                playerLevel = (int)element.Element("Class3").Attribute("level");
                character.PlayerClass3 = new PlayerClass(playerClass, playerLevel, 2);

                //-------------------------------------------------------------------------------------------------------
                // Parse Appearance
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Appearance");
                character.Gender     = (string)element.Element("Gender").Attribute("value");
                character.Age        = (string)element.Element("Age").Attribute("value");
                character.Height     = (string)element.Element("Height").Attribute("value");
                character.Weight     = (string)element.Element("Weight").Attribute("value");
                character.SkinColour = (string)element.Element("SkinColour").Attribute("value");
                character.HairColour = (string)element.Element("HairColour").Attribute("value");
                character.EyeColour  = (string)element.Element("EyeColour").Attribute("value");
                character.Marks      = (string)element.Element("Marks").Attribute("value");

                //-------------------------------------------------------------------------------------------------------
                // Parse Personality
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Personality");
                character.Trait1                = (string)element.Element("Trait1").Attribute("value");
                character.Trait2                = (string)element.Element("Trait2").Attribute("value");
                character.Ideal                 = (string)element.Element("Ideal").Attribute("value");
                character.Bond                  = (string)element.Element("Bond").Attribute("value");
                character.Flaw                  = (string)element.Element("Flaw").Attribute("value");
                character.PersonalityBackground = (string)element.Element("Background").Attribute("value");
                character.PersonalityNotes      = (string)element.Element("Notes").Attribute("value");

                //-------------------------------------------------------------------------------------------------------
                // Parse Wealth
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("Wealth");
                character.CP = (int)element.Element("Copper").Attribute("value");
                character.SP = (int)element.Element("Silver").Attribute("value");
                character.EP = (int)element.Element("Electrum").Attribute("value");
                character.GP = (int)element.Element("Gold").Attribute("value");
                character.PP = (int)element.Element("Platinum").Attribute("value");

                //-------------------------------------------------------------------------------------------------------
                // Parse class resource
                //-------------------------------------------------------------------------------------------------------
                element = root.Element("ClassResource");
                character.ClassResource = (string)element.Element("Type").Attribute("value");
                character.Pool = (int)element.Element("Pool").Attribute("value");
                character.Spent = (int)element.Element("Spent").Attribute("value");

                //-------------------------------------------------------------------------------------------------------
                // Parse Armor Class
                //-------------------------------------------------------------------------------------------------------
                character.ArmorClass = new ArmorClass();

                element = root.Element("ArmorClass");
                character.ArmorClass.ArmorWorn      = (string)element.Element("ArmorWorn").Attribute("value");
                character.ArmorClass.ArmorType      = (string)element.Element("ArmorType").Attribute("value");
                character.ArmorClass.ArmorAC        = (int)   element.Element("ArmorAC").Attribute("value");
                character.ArmorClass.ArmorStrength  = (int)   element.Element("Strength").Attribute("value");
                character.ArmorClass.ArmorWeight    = (int)   element.Element("ArmorWeight").Attribute("value");
                character.ArmorClass.ArmorStealth   = (string)element.Element("Stealth").Attribute("value");
                character.ArmorClass.ShieldType     = (string)element.Element("Shield").Attribute("value");
                character.ArmorClass.ShieldAC       = (int)   element.Element("ShieldAC").Attribute("value");
                character.ArmorClass.ShieldWeight   = (int)   element.Element("ShieldWeight").Attribute("value");
                character.ArmorClass.MiscAC         = (int)   element.Element("MiscAC").Attribute("value");
                character.ArmorClass.MagicAC        = (int)   element.Element("MagicAC").Attribute("value");

                //-------------------------------------------------------------------------------------------------------
                // Parse Hit Points
                //-------------------------------------------------------------------------------------------------------
                character.HitPoints = new HitPoints();

                element = root.Element("HitPoints");
                character.HitPoints.MaxHP       = (int)   element.Element("MaxHP").Attribute("value");
                character.HitPoints.HP          = (int)   element.Element("CurrentHP").Attribute("value");
                character.HitPoints.TempHP      = (int)   element.Element("TemporaryHP").Attribute("value");

                element = element.Element("Conditions");
                character.HitPoints.Conditions.Blinded          = (string)element.Element("Blinded").Attribute("value");
                character.HitPoints.Conditions.Charmed          = (string)element.Element("Charmed").Attribute("value");
                character.HitPoints.Conditions.Deafened         = (string)element.Element("Deafened").Attribute("value");
                character.HitPoints.Conditions.Fatigued         = (string)element.Element("Fatigued").Attribute("value");
                character.HitPoints.Conditions.Frightened       = (string)element.Element("Frightened").Attribute("value");
                character.HitPoints.Conditions.Grappled         = (string)element.Element("Grappled").Attribute("value");
                character.HitPoints.Conditions.Incapacitated    = (string)element.Element("Incapacitated").Attribute("value");
                character.HitPoints.Conditions.Invisible        = (string)element.Element("Invisible").Attribute("value");
                character.HitPoints.Conditions.Paralyzed        = (string)element.Element("Paralyzed").Attribute("value");
                character.HitPoints.Conditions.Petrified        = (string)element.Element("Petrified").Attribute("value");
                character.HitPoints.Conditions.Poisoned         = (string)element.Element("Poisoned").Attribute("value");
                character.HitPoints.Conditions.Prone            = (string)element.Element("Prone").Attribute("value");
                character.HitPoints.Conditions.Restrained       = (string)element.Element("Restrained").Attribute("value");
                character.HitPoints.Conditions.Stunned          = (string)element.Element("Stunned").Attribute("value");
                character.HitPoints.Conditions.Unconscious      = (string)element.Element("Unconscious").Attribute("value");
                
                element = root.Element("HitPoints").Element("HitDice");
                character.HitPoints.D6          = (int)element.Element("D6").Attribute("total");
                character.HitPoints.D8          = (int)element.Element("D8").Attribute("total");
                character.HitPoints.D10         = (int)element.Element("D10").Attribute("total");
                character.HitPoints.D12         = (int)element.Element("D12").Attribute("total");
                character.HitPoints.SpentD6     = (int)element.Element("D6").Attribute("spent");
                character.HitPoints.SpentD8     = (int)element.Element("D8").Attribute("spent");
                character.HitPoints.SpentD10    = (int)element.Element("D10").Attribute("spent");
                character.HitPoints.SpentD12    = (int)element.Element("D12").Attribute("spent");

                //-------------------------------------------------------------------------------------------------------
                // Parse Weapons
                //-------------------------------------------------------------------------------------------------------
                var weapons = root.Element("Weapons").Elements("Weapon");

                foreach (XElement elem in weapons)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("ability")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("dmg"));     builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("misc"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("dmgType")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("range"));   builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("notes"));   builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("weight"));  builder.Append(Constants.DELIMITER);
                    builder.Append( (string)elem.Attribute("id"));

                    character.oWeapons.Add(new Weapon(builder.ToString()));
                }

                //-------------------------------------------------------------------------------------------------------
                // Parse Ammo
                //-------------------------------------------------------------------------------------------------------
                var ammunitions = root.Element("Ammunitions").Elements("Ammunition");

                foreach (XElement elem in ammunitions)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("amount")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("miscDmg")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("dmgType")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("used"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("id"));

                    character.oAmmo.Add(new Ammunition(builder.ToString()));
                }

                //-------------------------------------------------------------------------------------------------------
                // Parse Inventory
                //-------------------------------------------------------------------------------------------------------
                var inventory = root.Element("Inventory").Elements("Item");

                foreach (XElement elem in inventory)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("amount"));  builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("wgt"));     builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("note"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("id"));

                    character.oInventory.Add(new Inventory(builder.ToString()));
                }

                //-------------------------------------------------------------------------------------------------------
                // Parse Abilities
                //-------------------------------------------------------------------------------------------------------
                var abilities = root.Element("Abilities").Elements("Ability");

                foreach (XElement elem in abilities)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));     builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("level"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("uses"));     builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("recovery")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("action"));   builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("notes"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("id"));

                    character.oAbility.Add(new Ability(builder.ToString()));
                }

                //-------------------------------------------------------------------------------------------------------
                // Parse Spellcasting
                //-------------------------------------------------------------------------------------------------------
                character.Spellcasting = new Spellcasting();

                element = root.Element("Spellcasting");
                character.Spellcasting.Level = (int)element.Element("Level").Attribute("value");

                // Spell slots
                element = element.Element("SpellSlots");
                character.Spellcasting.PactTotal    = (int)element.Element("Pact").Attribute("total");
                character.Spellcasting.OneTotal     = (int)element.Element("One").Attribute("total");
                character.Spellcasting.TwoTotal     = (int)element.Element("Two").Attribute("total");
                character.Spellcasting.ThreeTotal   = (int)element.Element("Three").Attribute("total");
                character.Spellcasting.FourTotal    = (int)element.Element("Four").Attribute("total");
                character.Spellcasting.FiveTotal    = (int)element.Element("Five").Attribute("total");
                character.Spellcasting.SixTotal     = (int)element.Element("Six").Attribute("total");
                character.Spellcasting.SevenTotal   = (int)element.Element("Seven").Attribute("total");
                character.Spellcasting.EightTotal   = (int)element.Element("Eight").Attribute("total");
                character.Spellcasting.NineTotal    = (int)element.Element("Nine").Attribute("total");
                character.Spellcasting.PactUsed     = (int)element.Element("Pact").Attribute("used");
                character.Spellcasting.OneUsed      = (int)element.Element("One").Attribute("used");
                character.Spellcasting.TwoUsed      = (int)element.Element("Two").Attribute("used");
                character.Spellcasting.ThreeUsed    = (int)element.Element("Three").Attribute("used");
                character.Spellcasting.FourUsed     = (int)element.Element("Four").Attribute("used");
                character.Spellcasting.FiveUsed     = (int)element.Element("Five").Attribute("used");
                character.Spellcasting.SixUsed      = (int)element.Element("Six").Attribute("used");
                character.Spellcasting.SevenUsed    = (int)element.Element("Seven").Attribute("used");
                character.Spellcasting.EightUsed    = (int)element.Element("Eight").Attribute("used");
                character.Spellcasting.NineUsed     = (int)element.Element("Nine").Attribute("used");

                // Spell class
                var classes = root.Element("Spellcasting").Element("SpellClasses").Elements("SpellClass");

                foreach (XElement elem in classes)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("class"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("ability"));  builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("cantrips")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("known"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("prepared")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("id"));

                    character.Spellcasting.oMagic.Add(new Magic(builder.ToString()));
                }

                // Spell list
                var spells = root.Element("Spellcasting").Element("SpellList").Elements("Spell");

                foreach (XElement elem in spells)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));        builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("level"));       builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("page"));        builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("school"));      builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("ritual"));      builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("comp"));        builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("concen"));      builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("range"));       builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("duration"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("area"));        builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("save"));        builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("damage"));      builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("description")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("prepared"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("id"));

                    character.Spellcasting.oSpells.Add(new Spell(builder.ToString()));
                }

                //-------------------------------------------------------------------------------------------------------
                // Parse Companion
                //-------------------------------------------------------------------------------------------------------
                character.Companion = new Companion();

                element = root.Element("Companion");
                character.Companion.Name         =                (string)element.Element("Name").Attribute("value");
                character.Companion.AC           =                (int)   element.Element("AC").Attribute("value");
                character.Companion.HitDice      =                (string)element.Element("HitDice").Attribute("value");
                character.Companion.HP           =                (int)   element.Element("HP").Attribute("value");
                character.Companion.CurrentHP    =                (int)   element.Element("CurrentHP").Attribute("value");
                character.Companion.Speed        =                (string)element.Element("Speed").Attribute("value");
                character.Companion.Strength     =                (int)   element.Element("Strength").Attribute("value");
                character.Companion.Dexterity    =                (int)   element.Element("Dexterity").Attribute("value");
                character.Companion.Constitution =                (int)   element.Element("Constitution").Attribute("value");
                character.Companion.Intelligence =                (int)   element.Element("Intelligence").Attribute("value");
                character.Companion.Wisdom       =                (int)   element.Element("Wisdom").Attribute("value");
                character.Companion.Charisma     =                (int)   element.Element("Charisma").Attribute("value");
                character.Companion.Perception   =                (int)   element.Element("Perception").Attribute("value");
                character.Companion.Senses       =                (string)element.Element("Senses").Attribute("value");
                character.Companion.Attack       = new Pair(      (string)element.Element("Attack").Attribute("one"),   (string)element.Element("Attack").Attribute("two"));
                character.Companion.Type         = new Pair(      (string)element.Element("Type").Attribute("one"),     (string)element.Element("Type").Attribute("two"));
                character.Companion.AtkBonus     = new Pair(      (string)element.Element("AtkBonus").Attribute("one"), (string)element.Element("AtkBonus").Attribute("two"));
                character.Companion.Damage       = new Pair(      (string)element.Element("Damage").Attribute("one"),   (string)element.Element("Damage").Attribute("two"));
                character.Companion.DmgType      = new PresetPair((string)element.Element("DmgType").Attribute("one"),  (string)element.Element("DmgType").Attribute("two"));
                character.Companion.Reach        = new Pair(      (string)element.Element("Reach").Attribute("one"),    (string)element.Element("Reach").Attribute("two"));
                character.Companion.Notes        = new Pair(      (string)element.Element("Notes").Attribute("one"),    (string)element.Element("Notes").Attribute("two"));

                //-------------------------------------------------------------------------------------------------------
                // Parse Campain Notes
                //-------------------------------------------------------------------------------------------------------
                string value, id, names;

                var campainNotes = root.Element("CampainNotes").Elements("Note");

                foreach (XElement elem in campainNotes)
                {
                    id = (string)elem.Attribute("id");
                    names = (string)elem.Attribute("name");
                    value = elem.Value;

                    Program.Character.oDocuments.Add(new Document(new Guid(id), names, value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                MessageBox.Show("Error: Cannot load character sheet xml.\nGenerating new character sheet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                CreateCharacterSheetXML(character);
            }
        }

        /// =========================================
        /// SetCharacterData()
        /// =========================================
        public static void CreateCharacterSheetXML(Character character)
        {
            //Generate empty character sheet
            character.Age = "";
            character.Alignment = "";
            character.Armor = "";
            character.Background = "";
            character.Bond = "";
            character.Charisma = 1;
            character.ClassResource = "None";
            character.Constitution = 1;
            character.CP = 0;
            character.Dexterity = 1;
            character.EP = 0;
            character.EXP = 0;
            character.EyeColour = "";
            character.Flaw = "";
            character.Gender = "";
            character.GP = 0;
            character.HairColour = "";
            character.Height = "";
            character.Ideal = "";
            character.InitiativeBonus = 0;
            character.Intelligence = 1;
            character.Language = "";
            character.Marks = "";
            character.Movement = "";
            character.Name = "";
            character.PassivePerceptionBonus = 0;
            character.PersonalityBackground = "";
            character.PersonalityNotes = "";
            character.Pool = 0;
            character.PP = 0;
            character.Race = "";
            character.Shields = "";
            character.SkinColour = "";
            character.SP = 0;
            character.Spent = 0;
            character.Strength = 1;
            character.Tools = "";
            character.Trait1 = "";
            character.Trait2 = "";
            character.Vision = "";
            character.Weapons = "";
            character.Weight = "";
            character.Wisdom = 1;

            character.ClearLists();

            character.oSavingThrows.Add(new Strength(false));
            character.oSavingThrows.Add(new Dexterity(false));
            character.oSavingThrows.Add(new Constitution(false));
            character.oSavingThrows.Add(new Intelligence(false));
            character.oSavingThrows.Add(new Wisdom(false));
            character.oSavingThrows.Add(new Charisma(false));

            character.oSkills.Add(new Athletics(false, false));
            character.oSkills.Add(new Acrobatics(false, false));
            character.oSkills.Add(new SleightOfHand(false, false));
            character.oSkills.Add(new Stealth(false, false));
            character.oSkills.Add(new Arcana(false, false));
            character.oSkills.Add(new History(false, false));
            character.oSkills.Add(new Investigation(false, false));
            character.oSkills.Add(new Nature(false, false));
            character.oSkills.Add(new Religion(false, false));
            character.oSkills.Add(new AnimalHandling(false, false));
            character.oSkills.Add(new Insight(false, false));
            character.oSkills.Add(new Medicine(false, false));
            character.oSkills.Add(new Perception(false, false));
            character.oSkills.Add(new Survival(false, false));
            character.oSkills.Add(new Deception(false, false));
            character.oSkills.Add(new Intimidation(false, false));
            character.oSkills.Add(new Performance(false, false));
            character.oSkills.Add(new Persuasion(false, false));

            character.PlayerClass1 = new PlayerClass(0);
            character.PlayerClass2 = new PlayerClass(1);
            character.PlayerClass3 = new PlayerClass(2);

            character.ArmorClass = new ArmorClass();
            character.HitPoints = new HitPoints();
            character.Spellcasting = new Spellcasting();
            character.Companion = new Companion();

            Settings.Default();
        }

        /// =========================================
        /// LoadWeaponLists()
        /// =========================================
        public static List<Weapon> LoadWeaponLists()
        {
            List<Weapon> weapons = new List<Weapon>();

            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.WeaponList);
                XElement root = xml.Element("Weapons");

                var SimpleMelee = root.Elements("Weapon");
                foreach (XElement elem in SimpleMelee)
                {
                    Weapon weapon = new Weapon(false);

                    weapon.Name = (string)elem.Attribute("name");
                    weapon.Damage = (string)elem.Attribute("damage");
                    weapon.Type = (string)elem.Attribute("type");
                    weapon.Weight = (string)elem.Attribute("weight");
                    weapon.Range = (string)elem.Attribute("range");
                    weapon.Notes = (string)elem.Attribute("notes");

                    weapons.Add(weapon);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default weapon list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return weapons;
        }

        /// =========================================
        /// LoadItemLists()
        /// =========================================
        public static List<Inventory> LoadItemLists()
        {
            List<Inventory> inventories = new List<Inventory>();

            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.ItemList);
                XElement root = xml.Element("Items");

                var AdventuringGear = root.Elements("Item");
                foreach (XElement elem in AdventuringGear)
                {
                    Inventory inventory = new Inventory(false);

                    inventory.Name = (string)elem.Attribute("name");
                    inventory.Weight = (string)elem.Attribute("weight");
                    inventory.Note = (string)elem.Attribute("notes");

                    inventories.Add(inventory);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default item list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return inventories;
        }

        /// =========================================
        /// LoadItemLists()
        /// =========================================
        public static List<Ammunition> LoadAmmoLists()
        {
            List<Ammunition> ammo = new List<Ammunition>();

            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.AmmoList);
                XElement root = xml.Element("Ammunitions");

                var Ammunitions = root.Elements("Ammunition");
                foreach (XElement elem in Ammunitions)
                {
                    Ammunition ammunition = new Ammunition(false);

                    ammunition.Name = (string)elem.Attribute("name");
                    ammunition.Quantity = (string)elem.Attribute("qty");

                    ammo.Add(ammunition);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default ammunition list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ammo;
        }

        /// =========================================
        /// LoadSpellList()
        /// =========================================
        public static List<Spell> LoadSpellList()
        {
            List<Spell> spells = new List<Spell>();

            try
            {
                XDocument xml = XDocument.Parse(Properties.Resources.SpellList);
                XElement root = xml.Element("Spells");

                var Spells = root.Elements("Spell");
                foreach (XElement elem in Spells)
                {
                    Spell spell = new Spell(false);

                    spell.Name = (string)elem.Attribute("name");
                    spell.Level = (string)elem.Attribute("level");
                    spell.Page = (string)elem.Attribute("page");
                    spell.School = (string)elem.Attribute("school");
                    spell.Ritual = (string)elem.Attribute("ritual");
                    spell.Components = ((string)elem.Attribute("comp")).Equals("") ? "N/A" : (string)elem.Attribute("comp");
                    spell.Concentration = (string)elem.Attribute("concen");
                    spell.Range = ((string)elem.Attribute("range")).Equals("") ? "N/A" : (string)elem.Attribute("range");
                    spell.Duration = (string)elem.Attribute("duration");
                    spell.Area = ((string)elem.Attribute("area")).Equals("") ? "N/A" : (string)elem.Attribute("area");
                    spell.Save = ((string)elem.Attribute("save")).Equals("") ? "N/A" : (string)elem.Attribute("save");
                    spell.Damage = ((string)elem.Attribute("damage")).Equals("") ? "N/A" : (string)elem.Attribute("damage");
                    spell.Description = (string)elem.Attribute("description");
                    spell.Prepared = (string)elem.Attribute("prepared");

                    spells.Add(spell);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Error: Default spell list not loaded successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return spells;
        }

        #endregion

    }
}
