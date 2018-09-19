using MyCharacterSheet.Characters;
using MyCharacterSheet.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MyCharacterSheet.Persistence
{
    public static class Save
    {

        #region Members

        private static List<string[]> weapons     = new List<string[]>();
        private static List<string[]> ammunitions = new List<string[]>();
        private static List<string[]> inventory   = new List<string[]>();
        private static List<string[]> abilities   = new List<string[]>();
        private static List<string[]> classes     = new List<string[]>();
        private static List<string[]> spells      = new List<string[]>();

        #endregion

        #region Methods

        /// =========================================
        /// SaveCharacterSheetXML()
        /// =========================================
        public static void SaveCharacterSheetXML(Character character)
        {
            clearLists();

            splitList(weapons,      character.oWeapons);
            splitList(ammunitions,  character.oAmmo);
            splitList(inventory,    character.oInventory);
            splitList(abilities,    character.oAbility);
            splitList(classes,      character.Spellcasting.spellClass);
            splitList(spells,       character.Spellcasting.spellList);

            XDocument xml = new XDocument(
                new XElement("Character",
                    new XElement("Settings",
                        new XElement("Mute",            new XAttribute("remember", Settings.RememberMute),                new XAttribute("value",    Settings.MuteState)),
                        new XElement("Tab",             new XAttribute("remember", Settings.RememberLastTab),             new XAttribute("last",     Settings.LastTab)),
                        new XElement("AutoSave",        new XAttribute("enabled",  Settings.AutosaveEnable),              new XAttribute("interval", Settings.AutosaveInterval)),
                        new XElement("AnimalCompanion", new XAttribute("hidden",   Settings.HideAnimalCompanion))
                        ),
                    new XElement("Attributes",
                        new XElement("Strength",     new XAttribute("value", character.Strength)),
                        new XElement("Dexterity",    new XAttribute("value", character.Dexterity)),
                        new XElement("Constitution", new XAttribute("value", character.Constitution)),
                        new XElement("Intelligence", new XAttribute("value", character.Intelligence)),
                        new XElement("Wisdom",       new XAttribute("value", character.Wisdom)),
                        new XElement("Charisma",     new XAttribute("value", character.Charisma))
                        ),
                    new XElement("Proficiency",
                        new XElement("SavingThrows",
                            new XElement("Strength",     new XAttribute("proficiency", character.oSavingThrows[0].Proficiency)),
                            new XElement("Dexterity",    new XAttribute("proficiency", character.oSavingThrows[1].Proficiency)),
                            new XElement("Constitution", new XAttribute("proficiency", character.oSavingThrows[2].Proficiency)),
                            new XElement("Intelligence", new XAttribute("proficiency", character.oSavingThrows[3].Proficiency)),
                            new XElement("Wisdom",       new XAttribute("proficiency", character.oSavingThrows[4].Proficiency)),
                            new XElement("Charisma",     new XAttribute("proficiency", character.oSavingThrows[5].Proficiency))
                            ),
                        new XElement("Skills",
                            new XElement("Athletics",      new XAttribute("proficiency", character.oSkills[0].Proficiency),  new XAttribute("expertise", character.oSkills[0].Expertise)),
                            new XElement("Acrobatics",     new XAttribute("proficiency", character.oSkills[1].Proficiency),  new XAttribute("expertise", character.oSkills[1].Expertise)),
                            new XElement("SleightOfHand",  new XAttribute("proficiency", character.oSkills[2].Proficiency),  new XAttribute("expertise", character.oSkills[2].Expertise)),
                            new XElement("Stealth",        new XAttribute("proficiency", character.oSkills[3].Proficiency),  new XAttribute("expertise", character.oSkills[3].Expertise)),
                            new XElement("Arcana",         new XAttribute("proficiency", character.oSkills[4].Proficiency),  new XAttribute("expertise", character.oSkills[4].Expertise)),
                            new XElement("History",        new XAttribute("proficiency", character.oSkills[5].Proficiency),  new XAttribute("expertise", character.oSkills[5].Expertise)),
                            new XElement("Investigation",  new XAttribute("proficiency", character.oSkills[6].Proficiency),  new XAttribute("expertise", character.oSkills[6].Expertise)),
                            new XElement("Nature",         new XAttribute("proficiency", character.oSkills[7].Proficiency),  new XAttribute("expertise", character.oSkills[7].Expertise)),
                            new XElement("Religion",       new XAttribute("proficiency", character.oSkills[8].Proficiency),  new XAttribute("expertise", character.oSkills[8].Expertise)),
                            new XElement("AnimalHandling", new XAttribute("proficiency", character.oSkills[9].Proficiency),  new XAttribute("expertise", character.oSkills[9].Expertise)),
                            new XElement("Insight",        new XAttribute("proficiency", character.oSkills[10].Proficiency), new XAttribute("expertise", character.oSkills[10].Expertise)),
                            new XElement("Medicine",       new XAttribute("proficiency", character.oSkills[11].Proficiency), new XAttribute("expertise", character.oSkills[11].Expertise)),
                            new XElement("Perception",     new XAttribute("proficiency", character.oSkills[12].Proficiency), new XAttribute("expertise", character.oSkills[12].Expertise)),
                            new XElement("Survival",       new XAttribute("proficiency", character.oSkills[13].Proficiency), new XAttribute("expertise", character.oSkills[13].Expertise)),
                            new XElement("Deception",      new XAttribute("proficiency", character.oSkills[14].Proficiency), new XAttribute("expertise", character.oSkills[14].Expertise)),
                            new XElement("Intimidation",   new XAttribute("proficiency", character.oSkills[15].Proficiency), new XAttribute("expertise", character.oSkills[15].Expertise)),
                            new XElement("Performance",    new XAttribute("proficiency", character.oSkills[16].Proficiency), new XAttribute("expertise", character.oSkills[16].Expertise)),
                            new XElement("Persuasion",     new XAttribute("proficiency", character.oSkills[17].Proficiency), new XAttribute("expertise", character.oSkills[17].Expertise))
                            ),
                        new XElement("Equipment",
                            new XElement("Armor",   new XAttribute("proficiency", character.Armor)),
                            new XElement("Shields", new XAttribute("proficiency", character.Shields)),
                            new XElement("Weapons", new XAttribute("proficiency", character.Weapons)),
                            new XElement("Tools",   new XAttribute("proficiency", character.Tools))
                            )
                        ),
                    new XElement("Details",
                        new XElement("Name",            new XAttribute("value", character.Name)),
                        new XElement("Race",            new XAttribute("value", character.Race)),
                        new XElement("Background",      new XAttribute("value", character.Background)),
                        new XElement("Alignment",       new XAttribute("value", character.Alignment)),
                        new XElement("Level",           new XAttribute("value", character.Level)),
                        new XElement("Experience",      new XAttribute("value", character.EXP)),
                        new XElement("Language",        new XAttribute("value", character.Language)),
                        new XElement("Class",           new XAttribute("value", character.Class)),
                        new XElement("InitiativeBonus", new XAttribute("value", character.InitiativeBonus)),
                        new XElement("PerceptionBonus", new XAttribute("value", character.PassivePerceptionBonus)),
                        new XElement("Movement",        new XAttribute("value", character.Movement)),
                        new XElement("Vision",          new XAttribute("value", character.Vision))
                        ),
                    new XElement("Appearance",
                        new XElement("Gender",     new XAttribute("value", character.Gender)),
                        new XElement("Age",        new XAttribute("value", character.Age)),
                        new XElement("Height",     new XAttribute("value", character.Height)),
                        new XElement("Weight",     new XAttribute("value", character.Weight)),
                        new XElement("SkinColour", new XAttribute("value", character.SkinColour)),
                        new XElement("HairColour", new XAttribute("value", character.HairColour)),
                        new XElement("EyeColour",  new XAttribute("value", character.EyeColour)),
                        new XElement("Marks",      new XAttribute("value", character.Marks))
                        ),
                    new XElement("Personality",
                        new XElement("Trait1",     new XAttribute("value", character.Trait1)),
                        new XElement("Trait2",     new XAttribute("value", character.Trait2)),
                        new XElement("Ideal",      new XAttribute("value", character.Ideal)),
                        new XElement("Bond",       new XAttribute("value", character.Bond)),
                        new XElement("Flaw",       new XAttribute("value", character.Flaw)),
                        new XElement("Background", new XAttribute("value", character.PersonalityBackground)),
                        new XElement("Notes",      new XAttribute("value", character.PersonalityNotes))
                        ),
                    new XElement("Wealth",
                        new XElement("Copper",   new XAttribute("value", character.CP)),
                        new XElement("Silver",   new XAttribute("value", character.SP)),
                        new XElement("Electrum", new XAttribute("value", character.EP)),
                        new XElement("Gold",     new XAttribute("value", character.GP)),
                        new XElement("Platinum", new XAttribute("value", character.PP))
                        ),
                    new XElement("ArmorClass",
                        new XElement("ArmorWorn", new XAttribute("value", character.ArmorClass.ArmorWorn)),
                        new XElement("ArmorType", new XAttribute("value", character.ArmorClass.ArmorType)),
                        new XElement("ArmorAC",   new XAttribute("value", character.ArmorClass.ArmorAC)),
                        new XElement("Stealth",   new XAttribute("value", character.ArmorClass.ArmorStealth)),
                        new XElement("Shield",    new XAttribute("value", character.ArmorClass.ShieldType)),
                        new XElement("ShieldAC",  new XAttribute("value", character.ArmorClass.ShieldAC)),
                        new XElement("MiscAC",    new XAttribute("value", character.ArmorClass.MiscAC)),
                        new XElement("MagicAC",   new XAttribute("value", character.ArmorClass.MagicAC))
                        ),
                    new XElement("HitPoints",
                        new XElement("MaxHP",       new XAttribute("value", character.HitPoints.MaxHP)),
                        new XElement("CurrentHP",   new XAttribute("value", character.HitPoints.HP)),
                        new XElement("TemporaryHP", new XAttribute("value", character.HitPoints.TempHP)),
                        new XElement("Conditions",  new XAttribute("value", character.HitPoints.Conditions)),
                        new XElement("HitDice",
                            new XElement("D6",  new XAttribute("total", character.HitPoints.D6),  new XAttribute("spent", character.HitPoints.SpentD6)),
                            new XElement("D8",  new XAttribute("total", character.HitPoints.D8),  new XAttribute("spent", character.HitPoints.SpentD8)),
                            new XElement("D10", new XAttribute("total", character.HitPoints.D10), new XAttribute("spent", character.HitPoints.SpentD10)),
                            new XElement("D12", new XAttribute("total", character.HitPoints.D12), new XAttribute("spent", character.HitPoints.SpentD12))
                            ),
                        new XElement("DeathSaves",
                            new XElement("Success", new XAttribute("value", character.HitPoints.DeathSaveSuccess)),
                            new XElement("Failure", new XAttribute("value", character.HitPoints.DeathSaveFailure))
                            )
                        ),
                    new XElement("Weapons",
                        from weapon in weapons
                        select
                            new XElement("Weapon", 
                                new XAttribute("name",    weapon[0]),
                                new XAttribute("ability", weapon[1]),
                                new XAttribute("dmg",     weapon[2]),
                                new XAttribute("misc",    weapon[3]),
                                new XAttribute("dmgType", weapon[4]),
                                new XAttribute("range",   weapon[5]),
                                new XAttribute("notes",   weapon[6])
                                )
                        ),
                    new XElement("Ammunitions",
                        from ammo in ammunitions
                        select
                            new XElement("Ammunition",
                                new XAttribute("name",    ammo[0]),
                                new XAttribute("ammount", ammo[1]),
                                new XAttribute("miscDmg", ammo[2]),
                                new XAttribute("dmgType", ammo[3]),
                                new XAttribute("used",    ammo[4])
                            )
                        ),
                    new XElement("Inventory",
                        from item in inventory
                        select
                            new XElement("Item",
                                new XAttribute("name",    item[0]),
                                new XAttribute("ammount", item[1]),
                                new XAttribute("wgt",     item[2])
                            )
                        ),
                    new XElement("Abilities",
                        from ability in abilities
                        select
                            new XElement("Ability",
                                new XAttribute("name",     ability[0]),
                                new XAttribute("level",    ability[1]),
                                new XAttribute("uses",     ability[2]),
                                new XAttribute("recovery", ability[3]),
                                new XAttribute("action",   ability[4]),
                                new XAttribute("notes",    ability[5])
                            )
                        ),
                    new XElement("Notes",
                        from note in character.oNotes
                        select
                            new XElement("Note", new XAttribute("value", note))
                        ),
                    new XElement("Spellcasting",
                        new XElement("Level", new XAttribute("value", character.Spellcasting.Level)),
                        new XElement("SpellClasses",
                            from spellClass in classes
                            select
                                new XElement("SpellClass",
                                    new XAttribute("class",    spellClass[0]),
                                    new XAttribute("ability",  spellClass[1]),
                                    new XAttribute("cantrips", spellClass[2]),
                                    new XAttribute("known",    spellClass[3]),
                                    new XAttribute("prepared", spellClass[4])
                                )
                            ),
                        new XElement("SpellSlots",
                            new XElement("Pact",  new XAttribute("total", character.Spellcasting.PactTotal),  new XAttribute("used", character.Spellcasting.PactUsed)),
                            new XElement("One",   new XAttribute("total", character.Spellcasting.OneTotal),   new XAttribute("used", character.Spellcasting.OneUsed)),
                            new XElement("Two",   new XAttribute("total", character.Spellcasting.TwoTotal),   new XAttribute("used", character.Spellcasting.TwoUsed)),
                            new XElement("Three", new XAttribute("total", character.Spellcasting.ThreeTotal), new XAttribute("used", character.Spellcasting.ThreeUsed)),
                            new XElement("Four",  new XAttribute("total", character.Spellcasting.FourTotal),  new XAttribute("used", character.Spellcasting.FourUsed)),
                            new XElement("Five",  new XAttribute("total", character.Spellcasting.FiveTotal),  new XAttribute("used", character.Spellcasting.FiveUsed)),
                            new XElement("Six",   new XAttribute("total", character.Spellcasting.SixTotal),   new XAttribute("used", character.Spellcasting.SixUsed)),
                            new XElement("Seven", new XAttribute("total", character.Spellcasting.SevenTotal), new XAttribute("used", character.Spellcasting.SevenUsed)),
                            new XElement("Eight", new XAttribute("total", character.Spellcasting.EightTotal), new XAttribute("used", character.Spellcasting.EightUsed)),
                            new XElement("Nine",  new XAttribute("total", character.Spellcasting.NineTotal),  new XAttribute("used", character.Spellcasting.NineUsed))
                            ),
                        new XElement("SpellList",
                            from spell in spells
                            select
                                new XElement("Spell",
                                    new XAttribute("name",        spell[0]),
                                    new XAttribute("level",       spell[1]),
                                    new XAttribute("page",        spell[2]),
                                    new XAttribute("school",      spell[3]),
                                    new XAttribute("ritual",      spell[4]),
                                    new XAttribute("comp",        spell[5]),
                                    new XAttribute("concen",      spell[6]),
                                    new XAttribute("range",       spell[7]),
                                    new XAttribute("duration",    spell[8]),
                                    new XAttribute("area",        spell[9]),
                                    new XAttribute("save",        spell[10]),
                                    new XAttribute("damage",      spell[11]),
                                    new XAttribute("description", spell[12]),
                                    new XAttribute("prepared",    spell[13])
                                )
                            )
                        ),
                    new XElement("Companion",
                        new XElement("Name",         new XAttribute("value", character.Companion.Name)),
                        new XElement("AC",           new XAttribute("value", character.Companion.AC)),
                        new XElement("HitDice",      new XAttribute("value", character.Companion.HitDice)),
                        new XElement("HP",           new XAttribute("value", character.Companion.HP)),
                        new XElement("CurrentHP",    new XAttribute("value", character.Companion.CurrentHP)),
                        new XElement("Speed",        new XAttribute("value", character.Companion.Speed)),
                        new XElement("Strength",     new XAttribute("value", character.Companion.Strength)),
                        new XElement("Dexterity",    new XAttribute("value", character.Companion.Dexterity)),
                        new XElement("Constitution", new XAttribute("value", character.Companion.Constitution)),
                        new XElement("Intelligence", new XAttribute("value", character.Companion.Intelligence)),
                        new XElement("Wisdom",       new XAttribute("value", character.Companion.Wisdom)),
                        new XElement("Charisma",     new XAttribute("value", character.Companion.Charisma)),
                        new XElement("Perception",   new XAttribute("value", character.Companion.Perception)),
                        new XElement("Senses",       new XAttribute("value", character.Companion.Senses)),
                        new XElement("Attack",       new XAttribute("one",   character.Companion.Attack.First),   new XAttribute("two", character.Companion.Attack.Second)),
                        new XElement("Type",         new XAttribute("one",   character.Companion.Type.First),     new XAttribute("two", character.Companion.Type.Second)),
                        new XElement("AtkBonus",     new XAttribute("one",   character.Companion.AtkBonus.First), new XAttribute("two", character.Companion.AtkBonus.Second)),
                        new XElement("Damage",       new XAttribute("one",   character.Companion.Damage.First),   new XAttribute("two", character.Companion.Damage.Second)),
                        new XElement("DmgType",      new XAttribute("one",   character.Companion.DmgType.First),  new XAttribute("two", character.Companion.DmgType.Second)),
                        new XElement("Reach",        new XAttribute("one",   character.Companion.Reach.First),    new XAttribute("two", character.Companion.Reach.Second)),
                        new XElement("Notes",        new XAttribute("one",   character.Companion.Notes.First),    new XAttribute("two", character.Companion.Notes.Second))
                        ),
                    new XElement("CampainNotes",
                        from campainNote in character.oDocuments
                        select
                            new XElement("Note", new XAttribute("id", campainNote.ID), 
                                                 new XAttribute("name", campainNote.Name), 
                                                 new XCData(campainNote.Rtf))
                        )
                    )
                );

            xml.Save(Program.FileLocation);
        }

        /// =========================================
        /// splitList()
        /// =========================================
        private static void splitList(List<string[]> list, List<string> characterList)
        {
            string[] tokens;

            foreach (string str in characterList)
            {
                tokens = str.Split(Constants.DELIMITER);
                list.Add(tokens);
            }
        }

        /// =========================================
        /// clearLists()
        /// =========================================
        private static void clearLists()
        {
            weapons.Clear();
            ammunitions.Clear();
            inventory.Clear();
            abilities.Clear();
            classes.Clear();
            spells.Clear();
        }

        #endregion

    }
}