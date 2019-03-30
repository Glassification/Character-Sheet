using System.Collections.Generic;
using MyCharacterSheet.SavingThrowsNamespace;
using MyCharacterSheet.SkillsNamespace;
using System.ComponentModel;
using MyCharacterSheet.Persistence;
using MyCharacterSheet.Utility;
using static MyCharacterSheet.Utility.Constants;
using MyCharacterSheet.Lists;
using System;
using MyCharacterSheet.TypeConverters;

namespace MyCharacterSheet.Characters
{
    /// <summary>
    /// Represents all of the information for a player character.
    /// </summary>
    public class Character
    {

        #region Constants

        private const int MOVE_REDUCTION = 10;

        #endregion

        #region Members

        public List<SavingThrows> oSavingThrows = new List<SavingThrows>();
        public List<Skills>       oSkills       = new List<Skills>();
        public List<Weapon>       oWeapons      = new List<Weapon>();
        public List<Ammunition>   oAmmo         = new List<Ammunition>();
        public List<Inventory>    oInventory    = new List<Inventory>();
        public List<Ability>      oAbility      = new List<Ability>();
        public List<Document>     oDocuments    = new List<Document>();

        private enum ClassIndex { First, Second, Third};

        #endregion

        #region Constructor

        /// =========================================
        /// Character()
        /// =========================================
        public Character()
        {
            Spellcasting = new Spellcasting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        #endregion

        #region Methods

        /// =========================================
        /// GetBonus()
        /// =========================================
        public string GetBonus(string ability)
        {
            string bonus;
            int value;

            switch (ability)
            {
                case "STR":
                    value = Constants.Bonus(Strength) + ProficiencyBonus;
                    bonus = (value >= 0 ? "+" : "") + value;
                    break;
                case "DEX":
                    bonus = (Constants.Bonus(Dexterity) + ProficiencyBonus) + "";
                    break;
                case "CON":
                    bonus = (Constants.Bonus(Constitution) + ProficiencyBonus) + "";
                    break;
                case "INT":
                    bonus = (Constants.Bonus(Intelligence) + ProficiencyBonus) + "";
                    break;
                case "WIS":
                    bonus = (Constants.Bonus(Wisdom) + ProficiencyBonus) + "";
                    break;
                case "CHA":
                    bonus = (Constants.Bonus(Charisma) + ProficiencyBonus) + "";
                    break;
                case "NONE":
                default:
                    bonus = "";
                    break;
            }

            return bonus;
        }

        /// =========================================
        /// GetDC()
        /// =========================================
        public string GetDC(string ability)
        {
            string dc;

            switch (ability)
            {
                case "STR":
                    dc = (Constants.Bonus(Strength) + ProficiencyBonus + Constants.BASE_DC) + "";
                    break;
                case "DEX":
                    dc = (Constants.Bonus(Dexterity) + ProficiencyBonus + Constants.BASE_DC) + "";
                    break;
                case "CON":
                    dc = (Constants.Bonus(Constitution) + ProficiencyBonus + Constants.BASE_DC) + "";
                    break;
                case "INT":
                    dc = (Constants.Bonus(Intelligence) + ProficiencyBonus + Constants.BASE_DC) + "";
                    break;
                case "WIS":
                    dc = (Constants.Bonus(Wisdom) + ProficiencyBonus + Constants.BASE_DC) + "";
                    break;
                case "CHA":
                    dc = (Constants.Bonus(Charisma) + ProficiencyBonus + Constants.BASE_DC) + "";
                    break;
                case "NONE":
                default:
                    dc = "";
                    break;
            }

            return dc;
        }

        // =========================================
        /// GetWeaponIndex()
        /// =========================================
        public int GetWeaponIndex(Guid id)
        {
            bool end = false;
            int index = -1;

            for (int i = 0; i < oWeapons.Count && !end; i++)
            {
                if (oWeapons[i].ID.Equals(id))
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        /// =========================================
        /// RemoveWeaponItem()
        /// =========================================
        public void RemoveWeaponItem(Guid id)
        {
            bool end = false;

            for (int i = 0; i < oWeapons.Count && !end; i++)
            {
                if (oWeapons[i].ID.Equals(id))
                {
                    oWeapons.RemoveAt(i);
                    end = true;
                }
            }
        }

        /// =========================================
        /// GetAmmoIndex()
        /// =========================================
        public int GetAmmoIndex(Guid id)
        {
            bool end = false;
            int index = -1;

            for (int i = 0; i < oAmmo.Count && !end; i++)
            {
                if (oAmmo[i].ID.Equals(id))
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        /// =========================================
        /// RemoveAmmoItem()
        /// =========================================
        public void RemoveAmmoItem(Guid id)
        {
            bool end = false;

            for (int i = 0; i < oAmmo.Count && !end; i++)
            {
                if (oAmmo[i].ID.Equals(id))
                {
                    oAmmo.RemoveAt(i);
                    end = true;
                }
            }
        }

        /// =========================================
        /// IncrementAmmoQuantity()
        /// =========================================
        public void IncrementAmmoQuantity(Guid id)
        {
            bool end = false;
            int used;

            for (int i = 0; i < oAmmo.Count && !end; i++)
            {
                if (oAmmo[i].ID.Equals(id))
                {
                    used = int.Parse(oAmmo[i].Used);
                    used++;
                    oAmmo[i].Used = used.ToString();
                    end = true;
                }
            }
        }

        /// =========================================
        /// DecrementAmmoQuantity()
        /// =========================================
        public void DecrementAmmoQuantity(Guid id)
        {
            bool end = false;
            int used;

            for (int i = 0; i < oAmmo.Count && !end; i++)
            {
                if (oAmmo[i].ID.Equals(id))
                {
                    used = int.Parse(oAmmo[i].Used);
                    used--;
                    oAmmo[i].Used = used.ToString();
                    end = true;
                }
            }
        }

        /// =========================================
        /// GetInventoryIndex()
        /// =========================================
        public int GetInventoryIndex(Guid id)
        {
            bool end = false;
            int index = -1;

            for (int i = 0; i < oInventory.Count && !end; i++)
            {
                if (oInventory[i].ID.Equals(id))
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        /// =========================================
        /// RemoveInventoryItem()
        /// =========================================
        public void RemoveInventoryItem(Guid id)
        {
            bool end = false;

            for (int i = 0; i < oInventory.Count && !end; i++)
            {
                if (oInventory[i].ID.Equals(id))
                {
                    oInventory.RemoveAt(i);
                    end = true;
                }
            }
        }

        /// =========================================
        /// GetAbilityIndex()
        /// =========================================
        public int GetAbilityIndex(Guid id)
        {
            bool end = false;
            int index = -1;

            for (int i = 0; i < oAbility.Count && !end; i++)
            {
                if (oAbility[i].ID.Equals(id))
                {
                    index = i;
                    end = true;
                }
            }

            return index;
        }

        /// =========================================
        /// RemoveAbilityItem()
        /// =========================================
        public void RemoveAbilityItem(Guid id)
        {
            bool end = false;

            for (int i = 0; i < oAbility.Count && !end; i++)
            {
                if (oAbility[i].ID.Equals(id))
                {
                    oAbility.RemoveAt(i);
                    end = true;
                }
            }
        }

        /// =========================================
        /// LoadCharacterSheetFromFile()
        /// =========================================
        public void LoadCharacterSheetFromFile()
        {
            ClearLists();

            Load.LoadCharacterSheetFromFile(this);
        }

        /// =========================================
        /// LoadCharacterSheetFromString()
        /// =========================================
        public void LoadCharacterSheetFromString(string xmlString)
        {
            ClearLists();

            Load.LoadCharacterSheetFromString(this, xmlString);
        }

        /// =========================================
        /// CreateCharacterSheet()
        /// =========================================
        public void CreateCharacterSheet()
        {
            ClearLists();

            Load.CreateCharacterSheetXML(this);
        }

        /// =========================================
        /// SaveCharacterSheet()
        /// =========================================
        public void SaveCharacterSheet()
        {
            Save.SaveCharacterSheetToFile(this);
        }

        /// =========================================
        /// Copy()
        /// =========================================
        public void Copy(Character copy)
        {
            copy.Age                    = Age;
            copy.Alignment              = Alignment;
            copy.Armor                  = Armor;
            copy.Background             = Background;
            copy.Bond                   = Bond;
            copy.Charisma               = Charisma;
            copy.ClassResource          = ClassResource;
            copy.Constitution           = Constitution;
            copy.CP                     = CP;
            copy.Dexterity              = Dexterity;
            copy.EP                     = EP;
            copy.EXP                    = EXP;
            copy.EyeColour              = EyeColour;
            copy.Flaw                   = Flaw;
            copy.Gender                 = Gender;
            copy.GP                     = GP;
            copy.HairColour             = HairColour;
            copy.Height                 = Height;
            copy.Ideal                  = Ideal;
            copy.InitiativeBonus        = InitiativeBonus;
            copy.Intelligence           = Intelligence;
            copy.Language               = Language;
            copy.Marks                  = Marks;
            copy.Movement               = Movement;
            copy.Name                   = Name;
            copy.PassivePerceptionBonus = PassivePerceptionBonus;
            copy.PersonalityBackground  = PersonalityBackground;
            copy.PersonalityNotes       = PersonalityNotes;
            copy.Pool                   = Pool;
            copy.PP                     = PP;
            copy.Race                   = Race;
            copy.Shields                = Shields;
            copy.SkinColour             = SkinColour;
            copy.SP                     = SP;
            copy.Spent                  = Spent;
            copy.Strength               = Strength;
            copy.Tools                  = Tools;
            copy.Trait1                 = Trait1;
            copy.Trait2                 = Trait2;
            copy.Vision                 = Vision;
            copy.Weapons                = Weapons;
            copy.Weight                 = Weight;
            copy.Wisdom                 = Wisdom;

            copy.PlayerClass1 = new PlayerClass(PlayerClass1.ClassName, PlayerClass1.ClassLevel, (int)ClassIndex.First);
            copy.PlayerClass2 = new PlayerClass(PlayerClass2.ClassName, PlayerClass2.ClassLevel, (int)ClassIndex.Second);
            copy.PlayerClass3 = new PlayerClass(PlayerClass3.ClassName, PlayerClass3.ClassLevel, (int)ClassIndex.Third);

            copy.ArmorClass = new ArmorClass(ArmorClass.ArmorWorn, ArmorClass.ArmorType, ArmorClass.ArmorAC, ArmorClass.ArmorStealth, ArmorClass.ArmorWeight, ArmorClass.ShieldType, 
                                             ArmorClass.ShieldAC, ArmorClass.ShieldWeight, ArmorClass.MiscAC, ArmorClass.MagicAC, ArmorClass.ArmorStrength);

            copy.HitPoints = new HitPoints(HitPoints.HP, HitPoints.MaxHP, HitPoints.TempHP, HitPoints.Conditions, HitPoints.D6, HitPoints.D8, HitPoints.D10, 
                                           HitPoints.D12, HitPoints.SpentD6, HitPoints.SpentD8, HitPoints.SpentD10, HitPoints.SpentD12);

            copy.Spellcasting = new Spellcasting(Spellcasting.Level, Spellcasting.PactTotal, Spellcasting.OneTotal, Spellcasting.TwoTotal, Spellcasting.ThreeTotal, 
                                                 Spellcasting.FourTotal, Spellcasting.FiveTotal, Spellcasting.SixTotal, Spellcasting.SevenTotal, Spellcasting.EightTotal, 
                                                 Spellcasting.NineTotal, Spellcasting.PactUsed, Spellcasting.OneUsed, Spellcasting.TwoUsed, Spellcasting.ThreeUsed, 
                                                 Spellcasting.FourUsed, Spellcasting.FiveUsed, Spellcasting.SixUsed, Spellcasting.SevenUsed, Spellcasting.EightUsed, 
                                                 Spellcasting.NineUsed);

            copy.Companion = new Companion(Companion.Name, Companion.AC, Companion.HitDice, Companion.HP, Companion.CurrentHP, Companion.Speed, Companion.Strength,
                                           Companion.Dexterity, Companion.Constitution, Companion.Intelligence, Companion.Wisdom, Companion.Charisma, Companion.Perception,
                                           Companion.Senses, Companion.Attack.Copy(), Companion.Type.Copy(), Companion.AtkBonus.Copy(), Companion.Damage.Copy(), 
                                           Companion.DmgType.Copy(), Companion.Reach.Copy(), Companion.Notes.Copy());

            copy.oWeapons   = new List<Weapon>(oWeapons);
            copy.oAmmo      = new List<Ammunition>(oAmmo);
            copy.oInventory = new List<Inventory>(oInventory);
            copy.oAbility   = new List<Ability>(oAbility);

            copy.Spellcasting.oMagic    = new List<Magic>(Spellcasting.oMagic);
            copy.Spellcasting.oSpells   = new List<Spell>(Spellcasting.oSpells);
        }

        /// =========================================
        /// IsDead()
        /// =========================================
        public bool IsDead(int damage)
        {
            bool dead = false;
            int delta;

            if (damage < 0)
            {
                delta = (HitPoints.HP + HitPoints.TempHP) + damage;

                if (delta < (-HitPoints.MaxHP) || HitPoints.HP == 0)
                {
                    dead = true;
                }
            }

            return dead;
        }

        /// =========================================
        /// ClearLists()
        /// =========================================
        public void ClearLists()
        {
            oSavingThrows.Clear();
            oSkills.Clear();
            oWeapons.Clear();
            oAmmo.Clear();
            oInventory.Clear();
            oAbility.Clear();
            Spellcasting.oMagic.Clear();
            Spellcasting.oSpells.Clear();
            oDocuments.Clear();
        }

        /// =========================================
        /// ValidTotalLevel()
        /// =========================================
        public bool ValidTotalLevel(int value, int index)
        {
            bool isValid = false;
            int total;

            if (!Program.Loading)
            {
                switch ((ClassIndex)index)
                {
                    case ClassIndex.First:
                        total = value + PlayerClass2.ClassLevel + PlayerClass3.ClassLevel;
                        break;
                    case ClassIndex.Second:
                        total = PlayerClass1.ClassLevel + value + PlayerClass3.ClassLevel;
                        break;
                    case ClassIndex.Third:
                        total = PlayerClass1.ClassLevel + PlayerClass2.ClassLevel + value;
                        break;
                    default:
                        total = 0;
                        break;
                }

                if (total <= MAX_LEVEL)
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        // =========================================
        /// ValidName()
        /// =========================================
        public bool ValidName(string value, int index)
        {
            bool isValid;

            if (!Program.Loading)
            {
                switch ((ClassIndex)index)
                {
                    case ClassIndex.First:
                        isValid = (!value.Equals(PlayerClass2.ClassName) && !value.Equals(PlayerClass3.ClassName)) || value.Equals("");
                        break;
                    case ClassIndex.Second:
                        isValid = (!value.Equals(PlayerClass1.ClassName) && !value.Equals(PlayerClass3.ClassName)) || value.Equals("");
                        break;
                    case ClassIndex.Third:
                        isValid = (!value.Equals(PlayerClass1.ClassName) && !value.Equals(PlayerClass2.ClassName)) || value.Equals("");
                        break;
                    default:
                        isValid = false;
                        break;
                }
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        #endregion

        #region Browsable Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Armor Class")]
        [Description("Armor protects its wearer from attacks. The armor (and shield) you wear determines your base Armor Class.")]
        public ArmorClass ArmorClass
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Animal Companion")]
        [Description("A creature of the natural world that the character has created a powerful bond with.")]
        public Companion Companion
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Hit Points")]
        [Description("A character’s hit points define how tough they are is in combat and other dangerous situations.")]
        public HitPoints HitPoints
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Spellcasting")]
        [Description("Magic permeates fantasy gaming worlds and often appears in the form of a spell.")]
        public Spellcasting Spellcasting
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Name")]
        [Description("Every character needs a name.")]
        public string Name
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("First Class")]
        [Description("Each character needs a class name and level.")]
        public PlayerClass PlayerClass1
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Second Class")]
        [Description("Each character needs a class name and level.")]
        public PlayerClass PlayerClass2
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Third Class")]
        [Description("Each character needs a class name and level.")]
        public PlayerClass PlayerClass3
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Race")]
        [Description("Race contributes to a character’s identity in an important way, by establishing a general appearance and the natural talents gained from culture and ancestry.")]
        [TypeConverter(typeof(PlayerRaceConverter))]
        public string Race
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Background")]
        [Description("A character’s background describes where he or she came from, his or her original occupation, and the character’s place in the world.")]
        public string Background
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Language")]
        [Description("Race indicates the languages a character can speak by default, and a background might give access to one or more additional languages.")]
        public string Language
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Alignment")]
        [Description("The moral compass that guides a characters decisions.")]
        public string Alignment
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Experince")]
        [Description("Currently awarded EXP.")]
        public int EXP
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Vision")]
        [Description("Visual traits granted to the player.")]
        public string Vision
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Movement Speed")]
        [Description("Maximum a character may moven on a given turn.")]
        public string Movement
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Perception Bonus")]
        [Description("Bonus to a characters awareness.")]
        public int PassivePerceptionBonus
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Initiative Bonus")]
        [Description("Extra bonus to initiative.")]
        public int InitiativeBonus
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Class Resource")]
        [Description("Class specific resource pool.")]
        public string ClassResource
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Resource Pool")]
        [Description("Total points available to use.")]
        public int Pool
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Attributes")]
        [DisplayName("Strength")]
        [Description("Measures: Natural athleticism, bodily power.")]
        public int Strength
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Attributes")]
        [DisplayName("Dexterity")]
        [Description("Measures: Physical agility, reflexes, balance, poise.")]
        public int Dexterity
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Attributes")]
        [DisplayName("Constitution")]
        [Description("Measures: Health, stamina, vital force.")]
        public int Constitution
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Attributes")]
        [DisplayName("Intelligence")]
        [Description("Measures: Mental acuity, information recall, analytical skill.")]
        public int Intelligence
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Attributes")]
        [DisplayName("Wisdom")]
        [Description("Measures: Awareness, intuition, insight.")]
        public int Wisdom
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Attributes")]
        [DisplayName("Charisma")]
        [Description("Measures: Confidence, eloquence, leadership.")]
        public int Charisma
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Gender")]
        [Description("Male or female characters don't gain any special benefits or hindrances.")]
        public string Gender
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Age")]
        [Description("A character should be older than 0")]
        public string Age
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Height")]
        [Description("A strong and tough character might be tall or just heavy.")]
        public string Height
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Weight")]
        [Description("A weak but agile character might be thin.")]
        public string Weight
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Skin Colour")]
        [Description("Select a reasonable skin colour.")]
        public string SkinColour
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Hair Colour")]
        [Description("Select an interesting hair colour.")]
        public string HairColour
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Eye Colour")]
        [Description("Select a distinctive eye colour.")]
        public string EyeColour
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Appearance")]
        [DisplayName("Distinguishing Marks")]
        [Description("Give the character an unusual or memorable physical characteristic, such as a scar, a limp, or a tattoo.")]
        public string Marks
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("First Trait")]
        [Description("They should be self-descriptions that are specific about what makes a character stand out.")]
        public string Trait1
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Second Trait")]
        [Description("They should be self-descriptions that are specific about what makes a character stand out.")]
        public string Trait2
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Ideal")]
        [Description("Ideals are the things that you believe in most strongly, the fundamental moral and ethical principles that compel you to act as you do.")]
        public string Ideal
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Bond")]
        [Description("Bonds represent a character’s connections to people, places, and events in the world.")]
        public string Bond
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Flaw")]
        [Description("Character flaws represents some vice, compulsion, fear, or weakness—in. Anything that someone else could exploit to cause you to act against your best interests.")]
        public string Flaw
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Background Feature")]
        [Description("Special ability granted by the chosen background.")]
        public string PersonalityBackground
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Notes")]
        [Description("Extra descriptions about a characters history.")]
        public string PersonalityNotes
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Proficiencies")]
        [DisplayName("Armor")]
        [Description("List of armor that can be worn.")]
        public string Armor
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Proficiencies")]
        [DisplayName("Shields")]
        [Description("Types of shields that may be used.")]
        public string Shields
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Proficiencies")]
        [DisplayName("Weapons")]
        [Description("Weapons that can be wielded.")]
        public string Weapons
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Proficiencies")]
        [DisplayName("Tools")]
        [Description("Tools that can be used.")]
        public string Tools
        {
            get;
            set;
        }

        #endregion

        #region Non-Browsable Accessors

        [Browsable(false)]
        [ReadOnly(true)]
        public double CarryWeight
        {
            get
            {
                double wgt = 0.0;
                int totalCoins;

                // Inventory weight
                foreach (Inventory item in oInventory)
                {
                    wgt += int.Parse(item.Amount) * double.Parse(item.Weight);
                }

                // Weapon weight
                foreach (Weapon weapon in oWeapons)
                {
                    wgt += double.Parse(weapon.Weight);
                }

                // Armor/Shield weight
                wgt += ArmorClass.ArmorWeight;
                wgt += ArmorClass.ShieldWeight;

                // Coin weight
                if (!Settings.UseCoinWeight)
                {
                    totalCoins = CP + SP + EP + GP + PP;

                    wgt += totalCoins / COIN_GROUP;
                }

                return wgt;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int Level
        {
            get
            {
                return PlayerClass1.ClassLevel + PlayerClass2.ClassLevel + PlayerClass3.ClassLevel;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public string Class
        {
            get
            {
                string str = PlayerClass1.ClassName;

                //check class1 exists
                if (!str.Equals("") && !PlayerClass2.ClassName.Equals(""))
                    str += ", ";
                str += PlayerClass2.ClassName;

                //check class2 exists
                if (!str.Equals("") && (!PlayerClass1.ClassName.Equals("") || !PlayerClass2.ClassName.Equals("")) && !PlayerClass3.ClassName.Equals(""))
                    str += ", ";
                str += PlayerClass3.ClassName;

                return str;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int Initiative
        {
            get
            {
                return Constants.Bonus(Dexterity) + InitiativeBonus;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int PassivePerception
        {
            get
            {
                return 10 + oSkills[(int)MainPage.Skills.Perception].Bonus + PassivePerceptionBonus;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int ProficiencyBonus
        {
            get
            {
                return Constants.Proficiency(Level);
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int Spent
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int CP
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int SP
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int EP
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int GP
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int PP
        {
            get;
            set;
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public double TotalGold
        {
            get
            {
                return (CP / 100.0) + (SP / 10.0) + (EP / 2.0) + GP + (PP * 10.0);
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int Light
        {
            get
            {
                return Strength * 5;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int Medium
        {
            get
            {
                return Strength * 10;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int Heavy
        {
            get
            {
                return Strength * 15;
            }
        }

        #endregion

    }
}
