using MyCharacterSheet.Characters;
using MyCharacterSheet.Utility;
using System.Linq;
using System.Xml.Linq;

namespace MyCharacterSheet.Persistence
{
    /// <summary>
    /// Provides static methods for saving a character sheet in xml.
    /// </summary>
    public static class Save
    {

        #region Methods

        /// =========================================
        /// SaveCharacterSheetToFile()
        /// =========================================
        public static void SaveCharacterSheetToFile(Character character)
        {
            XDocument xml = SaveCharacterSheet(character);

            xml.Save(Program.FileLocation);
        }

        /// =========================================
        /// SaveCharacterSheetToString()
        /// =========================================
        public static string SaveCharacterSheetToString(Character character)
        {
            string xmlString = "";

            xmlString = SaveCharacterSheet(character).ToString();

            return xmlString;
        }

        /// =========================================
        /// SaveCharacterSheet()
        /// =========================================
        private static XDocument SaveCharacterSheet(Character character)
        {
            XDocument xml = new XDocument(
                new XElement("Character",
                    new XElement("Settings",
                        new XElement("Mute", new XAttribute("remember", Settings.RememberMute), new XAttribute("value", Settings.MuteState)),
                        new XElement("Tab", new XAttribute("remember", Settings.RememberLastTab), new XAttribute("last", Settings.LastTab)),
                        new XElement("AutoSave", new XAttribute("enabled", Settings.AutosaveEnable), new XAttribute("interval", Settings.AutosaveInterval)),
                        new XElement("AnimalCompanion", new XAttribute("hidden", Settings.HideAnimalCompanion)),
                        new XElement("CoinWeight", new XAttribute("ignore", Settings.UseCoinWeight)),
                        new XElement("Encumbrance", new XAttribute("use", Settings.UseEncumbrance))
                        ),
                    new XElement("Attributes",
                        new XElement("Strength", new XAttribute("value", character.Strength)),
                        new XElement("Dexterity", new XAttribute("value", character.Dexterity)),
                        new XElement("Constitution", new XAttribute("value", character.Constitution)),
                        new XElement("Intelligence", new XAttribute("value", character.Intelligence)),
                        new XElement("Wisdom", new XAttribute("value", character.Wisdom)),
                        new XElement("Charisma", new XAttribute("value", character.Charisma))
                        ),
                    new XElement("Proficiency",
                        new XElement("SavingThrows",
                            new XElement("Strength", new XAttribute("proficiency", character.oSavingThrows[0].Proficiency)),
                            new XElement("Dexterity", new XAttribute("proficiency", character.oSavingThrows[1].Proficiency)),
                            new XElement("Constitution", new XAttribute("proficiency", character.oSavingThrows[2].Proficiency)),
                            new XElement("Intelligence", new XAttribute("proficiency", character.oSavingThrows[3].Proficiency)),
                            new XElement("Wisdom", new XAttribute("proficiency", character.oSavingThrows[4].Proficiency)),
                            new XElement("Charisma", new XAttribute("proficiency", character.oSavingThrows[5].Proficiency))
                            ),
                        new XElement("Skills",
                            new XElement("Athletics", new XAttribute("proficiency", character.oSkills[0].Proficiency), new XAttribute("expertise", character.oSkills[0].Expertise)),
                            new XElement("Acrobatics", new XAttribute("proficiency", character.oSkills[1].Proficiency), new XAttribute("expertise", character.oSkills[1].Expertise)),
                            new XElement("SleightOfHand", new XAttribute("proficiency", character.oSkills[2].Proficiency), new XAttribute("expertise", character.oSkills[2].Expertise)),
                            new XElement("Stealth", new XAttribute("proficiency", character.oSkills[3].Proficiency), new XAttribute("expertise", character.oSkills[3].Expertise)),
                            new XElement("Arcana", new XAttribute("proficiency", character.oSkills[4].Proficiency), new XAttribute("expertise", character.oSkills[4].Expertise)),
                            new XElement("History", new XAttribute("proficiency", character.oSkills[5].Proficiency), new XAttribute("expertise", character.oSkills[5].Expertise)),
                            new XElement("Investigation", new XAttribute("proficiency", character.oSkills[6].Proficiency), new XAttribute("expertise", character.oSkills[6].Expertise)),
                            new XElement("Nature", new XAttribute("proficiency", character.oSkills[7].Proficiency), new XAttribute("expertise", character.oSkills[7].Expertise)),
                            new XElement("Religion", new XAttribute("proficiency", character.oSkills[8].Proficiency), new XAttribute("expertise", character.oSkills[8].Expertise)),
                            new XElement("AnimalHandling", new XAttribute("proficiency", character.oSkills[9].Proficiency), new XAttribute("expertise", character.oSkills[9].Expertise)),
                            new XElement("Insight", new XAttribute("proficiency", character.oSkills[10].Proficiency), new XAttribute("expertise", character.oSkills[10].Expertise)),
                            new XElement("Medicine", new XAttribute("proficiency", character.oSkills[11].Proficiency), new XAttribute("expertise", character.oSkills[11].Expertise)),
                            new XElement("Perception", new XAttribute("proficiency", character.oSkills[12].Proficiency), new XAttribute("expertise", character.oSkills[12].Expertise)),
                            new XElement("Survival", new XAttribute("proficiency", character.oSkills[13].Proficiency), new XAttribute("expertise", character.oSkills[13].Expertise)),
                            new XElement("Deception", new XAttribute("proficiency", character.oSkills[14].Proficiency), new XAttribute("expertise", character.oSkills[14].Expertise)),
                            new XElement("Intimidation", new XAttribute("proficiency", character.oSkills[15].Proficiency), new XAttribute("expertise", character.oSkills[15].Expertise)),
                            new XElement("Performance", new XAttribute("proficiency", character.oSkills[16].Proficiency), new XAttribute("expertise", character.oSkills[16].Expertise)),
                            new XElement("Persuasion", new XAttribute("proficiency", character.oSkills[17].Proficiency), new XAttribute("expertise", character.oSkills[17].Expertise))
                            ),
                        new XElement("Equipment",
                            new XElement("Armor", new XAttribute("proficiency", character.Armor)),
                            new XElement("Shields", new XAttribute("proficiency", character.Shields)),
                            new XElement("Weapons", new XAttribute("proficiency", character.Weapons)),
                            new XElement("Tools", new XAttribute("proficiency", character.Tools))
                            )
                        ),
                    new XElement("Details",
                        new XElement("Name", new XAttribute("value", character.Name)),
                        new XElement("Race", new XAttribute("value", character.Race)),
                        new XElement("Background", new XAttribute("value", character.Background)),
                        new XElement("Alignment", new XAttribute("value", character.Alignment)),
                        new XElement("Class1", new XAttribute("class", character.PlayerClass1.ClassName), new XAttribute("level", character.PlayerClass1.ClassLevel)),
                        new XElement("Class2", new XAttribute("class", character.PlayerClass2.ClassName), new XAttribute("level", character.PlayerClass2.ClassLevel)),
                        new XElement("Class3", new XAttribute("class", character.PlayerClass3.ClassName), new XAttribute("level", character.PlayerClass3.ClassLevel)),
                        new XElement("Experience", new XAttribute("value", character.EXP)),
                        new XElement("Language", new XAttribute("value", character.Language)),
                        new XElement("InitiativeBonus", new XAttribute("value", character.InitiativeBonus)),
                        new XElement("PerceptionBonus", new XAttribute("value", character.PassivePerceptionBonus)),
                        new XElement("Movement", new XAttribute("value", character.Movement)),
                        new XElement("Vision", new XAttribute("value", character.Vision))
                        ),
                    new XElement("Appearance",
                        new XElement("Gender", new XAttribute("value", character.Gender)),
                        new XElement("Age", new XAttribute("value", character.Age)),
                        new XElement("Height", new XAttribute("value", character.Height)),
                        new XElement("Weight", new XAttribute("value", character.Weight)),
                        new XElement("SkinColour", new XAttribute("value", character.SkinColour)),
                        new XElement("HairColour", new XAttribute("value", character.HairColour)),
                        new XElement("EyeColour", new XAttribute("value", character.EyeColour)),
                        new XElement("Marks", new XAttribute("value", character.Marks))
                        ),
                    new XElement("Personality",
                        new XElement("Trait1", new XAttribute("value", character.Trait1)),
                        new XElement("Trait2", new XAttribute("value", character.Trait2)),
                        new XElement("Ideal", new XAttribute("value", character.Ideal)),
                        new XElement("Bond", new XAttribute("value", character.Bond)),
                        new XElement("Flaw", new XAttribute("value", character.Flaw)),
                        new XElement("Background", new XAttribute("value", character.PersonalityBackground)),
                        new XElement("Notes", new XAttribute("value", character.PersonalityNotes))
                        ),
                    new XElement("Wealth",
                        new XElement("Copper", new XAttribute("value", character.CP)),
                        new XElement("Silver", new XAttribute("value", character.SP)),
                        new XElement("Electrum", new XAttribute("value", character.EP)),
                        new XElement("Gold", new XAttribute("value", character.GP)),
                        new XElement("Platinum", new XAttribute("value", character.PP))
                        ),
                    new XElement("ClassResource",
                        new XElement("Type", new XAttribute("value", character.ClassResource)),
                        new XElement("Pool", new XAttribute("value", character.Pool)),
                        new XElement("Spent", new XAttribute("value", character.Spent))
                        ),
                    new XElement("ArmorClass",
                        new XElement("ArmorWorn", new XAttribute("value", character.ArmorClass.ArmorWorn)),
                        new XElement("ArmorType", new XAttribute("value", character.ArmorClass.ArmorType)),
                        new XElement("ArmorAC", new XAttribute("value", character.ArmorClass.ArmorAC)),
                        new XElement("Strength", new XAttribute("value", character.ArmorClass.ArmorStrength)),
                        new XElement("ArmorWeight", new XAttribute("value", character.ArmorClass.ArmorWeight)),
                        new XElement("Stealth", new XAttribute("value", character.ArmorClass.ArmorStealth)),
                        new XElement("Shield", new XAttribute("value", character.ArmorClass.ShieldType)),
                        new XElement("ShieldAC", new XAttribute("value", character.ArmorClass.ShieldAC)),
                        new XElement("ShieldWeight", new XAttribute("value", character.ArmorClass.ShieldWeight)),
                        new XElement("MiscAC", new XAttribute("value", character.ArmorClass.MiscAC)),
                        new XElement("MagicAC", new XAttribute("value", character.ArmorClass.MagicAC))
                        ),
                    new XElement("HitPoints",
                        new XElement("MaxHP", new XAttribute("value", character.HitPoints.MaxHP)),
                        new XElement("CurrentHP", new XAttribute("value", character.HitPoints.HP)),
                        new XElement("TemporaryHP", new XAttribute("value", character.HitPoints.TempHP)),
                        new XElement("Conditions",
                            new XElement("Blinded", new XAttribute("value", character.HitPoints.Conditions.Blinded)),
                            new XElement("Charmed", new XAttribute("value", character.HitPoints.Conditions.Charmed)),
                            new XElement("Deafened", new XAttribute("value", character.HitPoints.Conditions.Deafened)),
                            new XElement("Fatigued", new XAttribute("value", character.HitPoints.Conditions.Fatigued)),
                            new XElement("Frightened", new XAttribute("value", character.HitPoints.Conditions.Frightened)),
                            new XElement("Grappled", new XAttribute("value", character.HitPoints.Conditions.Grappled)),
                            new XElement("Incapacitated", new XAttribute("value", character.HitPoints.Conditions.Incapacitated)),
                            new XElement("Invisible", new XAttribute("value", character.HitPoints.Conditions.Invisible)),
                            new XElement("Paralyzed", new XAttribute("value", character.HitPoints.Conditions.Paralyzed)),
                            new XElement("Petrified", new XAttribute("value", character.HitPoints.Conditions.Petrified)),
                            new XElement("Poisoned", new XAttribute("value", character.HitPoints.Conditions.Poisoned)),
                            new XElement("Prone", new XAttribute("value", character.HitPoints.Conditions.Prone)),
                            new XElement("Restrained", new XAttribute("value", character.HitPoints.Conditions.Restrained)),
                            new XElement("Stunned", new XAttribute("value", character.HitPoints.Conditions.Stunned)),
                            new XElement("Unconscious", new XAttribute("value", character.HitPoints.Conditions.Unconscious))
                            ),
                        new XElement("HitDice",
                            new XElement("D6", new XAttribute("total", character.HitPoints.D6), new XAttribute("spent", character.HitPoints.SpentD6)),
                            new XElement("D8", new XAttribute("total", character.HitPoints.D8), new XAttribute("spent", character.HitPoints.SpentD8)),
                            new XElement("D10", new XAttribute("total", character.HitPoints.D10), new XAttribute("spent", character.HitPoints.SpentD10)),
                            new XElement("D12", new XAttribute("total", character.HitPoints.D12), new XAttribute("spent", character.HitPoints.SpentD12))
                            )
                        ),
                    new XElement("Weapons",
                        from weapon in character.oWeapons
                        select
                            new XElement("Weapon",
                                new XAttribute("name", weapon.Name),
                                new XAttribute("ability", weapon.Ability),
                                new XAttribute("dmg", weapon.Damage),
                                new XAttribute("misc", weapon.Misc),
                                new XAttribute("dmgType", weapon.Type),
                                new XAttribute("range", weapon.Range),
                                new XAttribute("notes", weapon.Notes),
                                new XAttribute("weight", weapon.Weight),
                                new XAttribute("id", weapon.ID)
                                )
                        ),
                    new XElement("Ammunitions",
                        from ammo in character.oAmmo
                        select
                            new XElement("Ammunition",
                                new XAttribute("name", ammo.Name),
                                new XAttribute("amount", ammo.Quantity),
                                new XAttribute("miscDmg", ammo.Bonus),
                                new XAttribute("dmgType", ammo.Type),
                                new XAttribute("used", ammo.Used),
                                new XAttribute("id", ammo.ID)
                            )
                        ),
                    new XElement("Inventory",
                        from item in character.oInventory
                        select
                            new XElement("Item",
                                new XAttribute("name", item.Name),
                                new XAttribute("amount", item.Amount),
                                new XAttribute("wgt", item.Weight),
                                new XAttribute("note", item.Note),
                                new XAttribute("id", item.ID)
                            )
                        ),
                    new XElement("Abilities",
                        from ability in character.oAbility
                        select
                            new XElement("Ability",
                                new XAttribute("name", ability.Name),
                                new XAttribute("level", ability.Level),
                                new XAttribute("uses", ability.Uses),
                                new XAttribute("recovery", ability.Recovery),
                                new XAttribute("action", ability.Action),
                                new XAttribute("notes", ability.Note),
                                new XAttribute("id", ability.ID)
                            )
                        ),
                    new XElement("Spellcasting",
                        new XElement("Level", new XAttribute("value", character.Spellcasting.Level)),
                        new XElement("SpellClasses",
                            from magic in character.Spellcasting.oMagic
                            select
                                new XElement("SpellClass",
                                    new XAttribute("class", magic.Class),
                                    new XAttribute("ability", magic.Ability),
                                    new XAttribute("cantrips", magic.Cantrips),
                                    new XAttribute("known", magic.Spells),
                                    new XAttribute("prepared", magic.Prepared),
                                    new XAttribute("id", magic.ID)
                                )
                            ),
                        new XElement("SpellSlots",
                            new XElement("Pact", new XAttribute("total", character.Spellcasting.PactTotal), new XAttribute("used", character.Spellcasting.PactUsed)),
                            new XElement("One", new XAttribute("total", character.Spellcasting.OneTotal), new XAttribute("used", character.Spellcasting.OneUsed)),
                            new XElement("Two", new XAttribute("total", character.Spellcasting.TwoTotal), new XAttribute("used", character.Spellcasting.TwoUsed)),
                            new XElement("Three", new XAttribute("total", character.Spellcasting.ThreeTotal), new XAttribute("used", character.Spellcasting.ThreeUsed)),
                            new XElement("Four", new XAttribute("total", character.Spellcasting.FourTotal), new XAttribute("used", character.Spellcasting.FourUsed)),
                            new XElement("Five", new XAttribute("total", character.Spellcasting.FiveTotal), new XAttribute("used", character.Spellcasting.FiveUsed)),
                            new XElement("Six", new XAttribute("total", character.Spellcasting.SixTotal), new XAttribute("used", character.Spellcasting.SixUsed)),
                            new XElement("Seven", new XAttribute("total", character.Spellcasting.SevenTotal), new XAttribute("used", character.Spellcasting.SevenUsed)),
                            new XElement("Eight", new XAttribute("total", character.Spellcasting.EightTotal), new XAttribute("used", character.Spellcasting.EightUsed)),
                            new XElement("Nine", new XAttribute("total", character.Spellcasting.NineTotal), new XAttribute("used", character.Spellcasting.NineUsed))
                            ),
                        new XElement("SpellList",
                            from spell in character.Spellcasting.oSpells
                            select
                                new XElement("Spell",
                                    new XAttribute("name", spell.Name),
                                    new XAttribute("level", spell.Level),
                                    new XAttribute("page", spell.Page),
                                    new XAttribute("school", spell.School),
                                    new XAttribute("ritual", spell.Ritual),
                                    new XAttribute("comp", spell.Components),
                                    new XAttribute("concen", spell.Concentration),
                                    new XAttribute("range", spell.Range),
                                    new XAttribute("duration", spell.Duration),
                                    new XAttribute("area", spell.Area),
                                    new XAttribute("save", spell.Save),
                                    new XAttribute("damage", spell.Damage),
                                    new XAttribute("description", spell.Description),
                                    new XAttribute("prepared", spell.Prepared),
                                    new XAttribute("id", spell.ID)
                                )
                            )
                        ),
                    new XElement("Companion",
                        new XElement("Name", new XAttribute("value", character.Companion.Name)),
                        new XElement("AC", new XAttribute("value", character.Companion.AC)),
                        new XElement("HitDice", new XAttribute("value", character.Companion.HitDice)),
                        new XElement("HP", new XAttribute("value", character.Companion.HP)),
                        new XElement("CurrentHP", new XAttribute("value", character.Companion.CurrentHP)),
                        new XElement("Speed", new XAttribute("value", character.Companion.Speed)),
                        new XElement("Strength", new XAttribute("value", character.Companion.Strength)),
                        new XElement("Dexterity", new XAttribute("value", character.Companion.Dexterity)),
                        new XElement("Constitution", new XAttribute("value", character.Companion.Constitution)),
                        new XElement("Intelligence", new XAttribute("value", character.Companion.Intelligence)),
                        new XElement("Wisdom", new XAttribute("value", character.Companion.Wisdom)),
                        new XElement("Charisma", new XAttribute("value", character.Companion.Charisma)),
                        new XElement("Perception", new XAttribute("value", character.Companion.Perception)),
                        new XElement("Senses", new XAttribute("value", character.Companion.Senses)),
                        new XElement("Attack", new XAttribute("one", character.Companion.Attack.First), new XAttribute("two", character.Companion.Attack.Second)),
                        new XElement("Type", new XAttribute("one", character.Companion.Type.First), new XAttribute("two", character.Companion.Type.Second)),
                        new XElement("AtkBonus", new XAttribute("one", character.Companion.AtkBonus.First), new XAttribute("two", character.Companion.AtkBonus.Second)),
                        new XElement("Damage", new XAttribute("one", character.Companion.Damage.First), new XAttribute("two", character.Companion.Damage.Second)),
                        new XElement("DmgType", new XAttribute("one", character.Companion.DmgType.First), new XAttribute("two", character.Companion.DmgType.Second)),
                        new XElement("Reach", new XAttribute("one", character.Companion.Reach.First), new XAttribute("two", character.Companion.Reach.Second)),
                        new XElement("Notes", new XAttribute("one", character.Companion.Notes.First), new XAttribute("two", character.Companion.Notes.Second))
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

            return xml;
        }

        #endregion

    }
}