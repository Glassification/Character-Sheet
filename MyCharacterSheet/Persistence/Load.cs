using MyCharacterSheet.Characters;
using MyCharacterSheet.SavingThrowsNamespace;
using MyCharacterSheet.SkillsNamespace;
using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyCharacterSheet.Persistence
{
    public static class Load
    {

        #region Methods

        /// =========================================
        /// LoadCharacterSheetXML()
        /// =========================================
        public static void LoadCharacterSheetXML(Character character)
        {
            try
            {
                XDocument xml = XDocument.Load(Program.FileLocation);
                XElement root = xml.Element("Character");
                XElement element;

                List<string> spellList = new List<string>();

                string armorType, armorWorn, stealth, shield, conditions, name, speed, senses, hitDice;
                int armorAC, shieldAC, miscAC, magicAC, maxHP, currentHP, tempHP, success, failure, d6, d8, d10, d12, spentD6, spentD8, spentD10, spentD12;
                int totalOne, totalTwo, totalThree, totalFour, totalFive, totalSix, totalSeven, totalEight, totalNine, totalPact;
                int usedOne, usedTwo, usedThree, usedFour, usedFive, usedSix, usedSeven, usedEight, usedNine, usedPact;
                int level, ac, hp, currHP, strength, dexterity, constitution, intelligence, wisdom, charisma, perception;
                Pair attack, type, atkBonus, damage, reach, note;
                PresetPair dmgBonus;

                //Parse Settings
                element = root.Element("Settings");
                Settings.MuteState           = bool.Parse(    (string)element.Element("Mute").Attribute("value"));
                Settings.RememberMute        = bool.Parse(    (string)element.Element("Mute").Attribute("remember"));
                Settings.RememberLastTab     = bool.Parse(    (string)element.Element("Tab").Attribute("remember"));
                Settings.LastTab             = int.Parse(     (string)element.Element("Tab").Attribute("last"));
                Settings.AutosaveEnable      = bool.Parse(    (string)element.Element("AutoSave").Attribute("enabled"));
                Settings.AutosaveInterval    = int.Parse(     (string)element.Element("AutoSave").Attribute("interval"));
                Settings.HideAnimalCompanion = bool.Parse(    (string)element.Element("AnimalCompanion").Attribute("hidden"));

                //Parse Attributes
                element = root.Element("Attributes");
                character.Strength     = int.Parse((string)element.Element("Strength").Attribute("value"));
                character.Dexterity    = int.Parse((string)element.Element("Dexterity").Attribute("value"));
                character.Constitution = int.Parse((string)element.Element("Constitution").Attribute("value"));
                character.Intelligence = int.Parse((string)element.Element("Intelligence").Attribute("value"));
                character.Wisdom       = int.Parse((string)element.Element("Wisdom").Attribute("value"));
                character.Charisma     = int.Parse((string)element.Element("Charisma").Attribute("value"));

                //Parse Saving Throws
                element = root.Element("Proficiency").Element("SavingThrows");
                character.oSavingThrows.Add(new Strength(    (bool)element.Element("Strength").Attribute("proficiency")));
                character.oSavingThrows.Add(new Dexterity(   (bool)element.Element("Dexterity").Attribute("proficiency")));
                character.oSavingThrows.Add(new Constitution((bool)element.Element("Constitution").Attribute("proficiency")));
                character.oSavingThrows.Add(new Intelligence((bool)element.Element("Intelligence").Attribute("proficiency")));
                character.oSavingThrows.Add(new Wisdom(      (bool)element.Element("Wisdom").Attribute("proficiency")));
                character.oSavingThrows.Add(new Charisma(    (bool)element.Element("Charisma").Attribute("proficiency")));

                //Parse Skills
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

                //Parse Equipment
                element = root.Element("Proficiency").Element("Equipment");
                character.Armor   = (string)element.Element("Armor").Attribute("proficiency");
                character.Shields = (string)element.Element("Shields").Attribute("proficiency");
                character.Weapons = (string)element.Element("Weapons").Attribute("proficiency");
                character.Tools   = (string)element.Element("Tools").Attribute("proficiency");

                //Parse Details
                element = root.Element("Details");
                character.Name                   = (string)element.Element("Name").Attribute("value");
                character.Race                   = (string)element.Element("Race").Attribute("value");
                character.Background             = (string)element.Element("Background").Attribute("value");
                character.Alignment              = (string)element.Element("Alignment").Attribute("value");
                character.Level                  = (int)   element.Element("Level").Attribute("value");
                character.EXP                    = (int)   element.Element("Experience").Attribute("value");
                character.Language               = (string)element.Element("Language").Attribute("value");
                character.InitiativeBonus        = (int)   element.Element("InitiativeBonus").Attribute("value");
                character.Class                  = (string)element.Element("Class").Attribute("value");
                character.PassivePerceptionBonus = (int)   element.Element("PerceptionBonus").Attribute("value");
                character.Movement               = (string)element.Element("Movement").Attribute("value");
                character.Vision                 = (string)element.Element("Vision").Attribute("value");


                //Parse Appearance
                element = root.Element("Appearance");
                character.Gender     = (string)element.Element("Gender").Attribute("value");
                character.Age        = (string)element.Element("Age").Attribute("value");
                character.Height     = (string)element.Element("Height").Attribute("value");
                character.Weight     = (string)element.Element("Weight").Attribute("value");
                character.SkinColour = (string)element.Element("SkinColour").Attribute("value");
                character.HairColour = (string)element.Element("HairColour").Attribute("value");
                character.EyeColour  = (string)element.Element("EyeColour").Attribute("value");
                character.Marks      = (string)element.Element("Marks").Attribute("value");

                //Parse Personality
                element = root.Element("Personality");
                character.Trait1                = (string)element.Element("Trait1").Attribute("value");
                character.Trait2                = (string)element.Element("Trait2").Attribute("value");
                character.Ideal                 = (string)element.Element("Ideal").Attribute("value");
                character.Bond                  = (string)element.Element("Bond").Attribute("value");
                character.Flaw                  = (string)element.Element("Flaw").Attribute("value");
                character.PersonalityBackground = (string)element.Element("Background").Attribute("value");
                character.PersonalityNotes      = (string)element.Element("Notes").Attribute("value");

                //Parse Wealth
                element = root.Element("Wealth");
                character.CP = (int)element.Element("Copper").Attribute("value");
                character.SP = (int)element.Element("Silver").Attribute("value");
                character.EP = (int)element.Element("Electrum").Attribute("value");
                character.GP = (int)element.Element("Gold").Attribute("value");
                character.PP = (int)element.Element("Platinum").Attribute("value");

                //Parse Armor Class
                element = root.Element("ArmorClass");
                armorWorn = (string)element.Element("ArmorWorn").Attribute("value");
                armorType = (string)element.Element("ArmorType").Attribute("value");
                armorAC   = (int)   element.Element("ArmorAC").Attribute("value");
                stealth   = (string)element.Element("Stealth").Attribute("value");
                shield    = (string)element.Element("Shield").Attribute("value");
                shieldAC  = (int)   element.Element("ShieldAC").Attribute("value");
                miscAC    = (int)   element.Element("MiscAC").Attribute("value");
                magicAC   = (int)   element.Element("MagicAC").Attribute("value");

                character.ArmorClass = new ArmorClass(armorWorn, armorType, armorAC, stealth, shield, shieldAC, miscAC, magicAC);

                //Parse Hit Points
                element    = root.Element("HitPoints");
                maxHP      = (int)   element.Element("MaxHP").Attribute("value");
                currentHP  = (int)   element.Element("CurrentHP").Attribute("value");
                tempHP     = (int)   element.Element("TemporaryHP").Attribute("value");
                conditions = (string)element.Element("Conditions").Attribute("value");

                element = element.Element("HitDice");
                d6       = (int)element.Element("D6").Attribute("total");
                d8       = (int)element.Element("D8").Attribute("total");
                d10      = (int)element.Element("D10").Attribute("total");
                d12      = (int)element.Element("D12").Attribute("total");
                spentD6  = (int)element.Element("D6").Attribute("spent");
                spentD8  = (int)element.Element("D8").Attribute("spent");
                spentD10 = (int)element.Element("D10").Attribute("spent");
                spentD12 = (int)element.Element("D12").Attribute("spent");

                element = root.Element("HitPoints").Element("DeathSaves");
                success = (int)element.Element("Success").Attribute("value");
                failure = (int)element.Element("Failure").Attribute("value");

                character.HitPoints = new HitPoints(currentHP, maxHP, tempHP, conditions, success, failure, d6, d8, d10, d12, spentD6, spentD8, spentD10, spentD12);

                //Parse Weapons
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
                    builder.Append((string)elem.Attribute("notes"));

                    character.oWeapons.Add(builder.ToString());
                }

                //Parse Ammo
                var ammunitions = root.Element("Ammunitions").Elements("Ammunition");

                foreach (XElement elem in ammunitions)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("ammount")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("miscDmg")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("dmgType")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("used"));

                    character.oAmmo.Add(builder.ToString());
                }

                //Parse Inventory
                var inventory = root.Element("Inventory").Elements("Item");

                foreach (XElement elem in inventory)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("ammount")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("wgt"));

                    character.oInventory.Add(builder.ToString());
                }

                //Parse Abilities
                var abilities = root.Element("Abilities").Elements("Ability");

                foreach (XElement elem in abilities)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("name"));     builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("level"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("uses"));     builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("recovery")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("action"));   builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("notes"));

                    character.oAbility.Add(builder.ToString());
                }

                //Parse Notes
                var notes = root.Element("Notes").Elements("Note");

                foreach (XElement elem in notes)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("value"));

                    character.oNotes.Add(builder.ToString());
                }

                //Parse Spellcasting
                element = root.Element("Spellcasting");
                level      = (int)element.Element("Level").Attribute("value");

                element = element.Element("SpellSlots");
                totalPact  = (int)element.Element("Pact").Attribute("total");
                totalOne   = (int)element.Element("One").Attribute("total");
                totalTwo   = (int)element.Element("Two").Attribute("total");
                totalThree = (int)element.Element("Three").Attribute("total");
                totalFour  = (int)element.Element("Four").Attribute("total");
                totalFive  = (int)element.Element("Five").Attribute("total");
                totalSix   = (int)element.Element("Six").Attribute("total");
                totalSeven = (int)element.Element("Seven").Attribute("total");
                totalEight = (int)element.Element("Eight").Attribute("total");
                totalNine  = (int)element.Element("Nine").Attribute("total");
                usedPact   = (int)element.Element("Pact").Attribute("used");
                usedOne    = (int)element.Element("One").Attribute("used");
                usedTwo    = (int)element.Element("Two").Attribute("used");
                usedThree  = (int)element.Element("Three").Attribute("used");
                usedFour   = (int)element.Element("Four").Attribute("used");
                usedFive   = (int)element.Element("Five").Attribute("used");
                usedSix    = (int)element.Element("Six").Attribute("used");
                usedSeven  = (int)element.Element("Seven").Attribute("used");
                usedEight  = (int)element.Element("Eight").Attribute("used");
                usedNine   = (int)element.Element("Nine").Attribute("used");

                character.Spellcasting = new Spellcasting(level, totalPact, totalOne, totalTwo, totalThree, totalFour, totalFive, totalSix, totalSeven, 
                                                          totalEight, totalNine, usedPact, usedOne, usedTwo, usedThree, usedFour, usedFive, usedSix, 
                                                          usedSeven, usedEight, usedNine);

                var classes = root.Element("Spellcasting").Element("SpellClasses").Elements("SpellClass");

                foreach (XElement elem in classes)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append((string)elem.Attribute("class"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("ability"));  builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("cantrips")); builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("known"));    builder.Append(Constants.DELIMITER);
                    builder.Append((string)elem.Attribute("prepared"));

                    character.Spellcasting.spellClass.Add(builder.ToString());
                }

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
                    builder.Append((string)elem.Attribute("prepared"));

                    character.Spellcasting.spellList.Add(builder.ToString());
                }

                //Parse Companion
                element = root.Element("Companion");
                name         = (string)element.Element("Name").Attribute("value");
                ac           = (int)   element.Element("AC").Attribute("value");
                hitDice      = (string)element.Element("HitDice").Attribute("value");
                hp           = (int)   element.Element("HP").Attribute("value");
                currHP       = (int)   element.Element("CurrentHP").Attribute("value");
                speed        = (string)element.Element("Speed").Attribute("value");
                strength     = (int)   element.Element("Strength").Attribute("value");
                dexterity    = (int)   element.Element("Dexterity").Attribute("value");
                constitution = (int)   element.Element("Constitution").Attribute("value");
                intelligence = (int)   element.Element("Intelligence").Attribute("value");
                wisdom       = (int)   element.Element("Wisdom").Attribute("value");
                charisma     = (int)   element.Element("Charisma").Attribute("value");
                perception   = (int)   element.Element("Perception").Attribute("value");
                senses       = (string)element.Element("Senses").Attribute("value");
                attack       = new Pair(      (string)element.Element("Attack").Attribute("one"),   (string)element.Element("Attack").Attribute("two"));
                type         = new Pair(      (string)element.Element("Type").Attribute("one"),     (string)element.Element("Type").Attribute("two"));
                atkBonus     = new Pair(      (string)element.Element("AtkBonus").Attribute("one"), (string)element.Element("AtkBonus").Attribute("two"));
                damage       = new Pair(      (string)element.Element("Damage").Attribute("one"),   (string)element.Element("Damage").Attribute("two"));
                dmgBonus     = new PresetPair((string)element.Element("DmgType").Attribute("one"),  (string)element.Element("DmgType").Attribute("two"));
                reach        = new Pair(      (string)element.Element("Reach").Attribute("one"),    (string)element.Element("Reach").Attribute("two"));
                note         = new Pair(      (string)element.Element("Notes").Attribute("one"),    (string)element.Element("Notes").Attribute("two"));

                character.Companion = new Companion(name, ac, hitDice, hp, currHP, speed, strength, dexterity, constitution, intelligence, wisdom, charisma,
                                                    perception, senses, attack, type, atkBonus, damage, dmgBonus, reach, note);

                //Parse Campain Notes
                var campainNotes = root.Element("CampainNotes").Elements("Note");

                foreach (XElement elem in campainNotes)
                {
                    Program.Character.oDocuments.Add(new Document((string)elem.Attribute("id"), (string)elem.Attribute("name"), elem.Value));
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
            character.Class = "";
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
            character.Level = 1;
            character.Marks = "";
            character.Movement = "";
            character.Name = "";
            character.PassivePerceptionBonus = 0;
            character.PersonalityBackground = "";
            character.PersonalityNotes = "";
            character.PP = 0;
            character.Race = "";
            character.Shields = "";
            character.SkinColour = "";
            character.SP = 0;
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

            character.ArmorClass = new ArmorClass("", "None", 0, "", "", 0, 0, 0);
            character.HitPoints = new HitPoints(0, 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            character.Spellcasting = new Spellcasting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            character.Companion = new Companion("", 0, "", 0, 0, "", 0, 0, 0, 0, 0, 0, 0, "", new Pair(), new Pair(), new Pair(), new Pair(), new PresetPair(), new Pair(), new Pair());

            Settings.Default();
        }

        #endregion

    }
}
