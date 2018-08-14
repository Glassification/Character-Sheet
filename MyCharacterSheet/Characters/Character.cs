using System.Collections.Generic;
using MyCharacterSheet.SavingThrowsNamespace;
using MyCharacterSheet.SkillsNamespace;
using System.ComponentModel;
using MyCharacterSheet.Persistence;
using MyCharacterSheet.Utility;

namespace MyCharacterSheet.Characters
{
    public class Character
    {

        #region Members

        public List<SavingThrows> oSavingThrows = new List<SavingThrows>();
        public List<Skills>       oSkills       = new List<Skills>();
        public List<string>       oWeapons      = new List<string>();
        public List<string>       oAmmo         = new List<string>();
        public List<string>       oInventory    = new List<string>();
        public List<string>       oAbility      = new List<string>();
        public List<string>       oNotes        = new List<string>();
        public List<Document>     oDocuments    = new List<Document>();

        private int iLevel;

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
            string bonus = "+";

            switch (ability)
            {
                case "STR":
                    bonus += (Constants.Bonus(Strength) + ProficiencyBonus) + "";
                    break;
                case "DEX":
                    bonus += (Constants.Bonus(Dexterity) + ProficiencyBonus) + "";
                    break;
                case "CON":
                    bonus += (Constants.Bonus(Constitution) + ProficiencyBonus) + "";
                    break;
                case "INT":
                    bonus += (Constants.Bonus(Intelligence) + ProficiencyBonus) + "";
                    break;
                case "WIS":
                    bonus += (Constants.Bonus(Wisdom) + ProficiencyBonus) + "";
                    break;
                case "CHA":
                    bonus += (Constants.Bonus(Charisma) + ProficiencyBonus) + "";
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

        /// =========================================
        /// LoadCharacterSheet()
        /// =========================================
        public void LoadCharacterSheet()
        {
            ClearLists();

            Load.LoadCharacterSheetXML(this);
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
            Save.SaveCharacterSheetXML(this);
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
            copy.Class                  = Class;
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
            copy.Level                  = Level;
            copy.Marks                  = Marks;
            copy.Movement               = Movement;
            copy.Name                   = Name;
            copy.PassivePerceptionBonus = PassivePerceptionBonus;
            copy.PersonalityBackground  = PersonalityBackground;
            copy.PersonalityNotes       = PersonalityNotes;
            copy.PP                     = PP;
            copy.Race                   = Race;
            copy.Shields                = Shields;
            copy.SkinColour             = SkinColour;
            copy.SP                     = SP;
            copy.Strength               = Strength;
            copy.Tools                  = Tools;
            copy.Trait1                 = Trait1;
            copy.Trait2                 = Trait2;
            copy.Vision                 = Vision;
            copy.Weapons                = Weapons;
            copy.Weight                 = Weight;
            copy.Wisdom                 = Wisdom;

            copy.ArmorClass = new ArmorClass(ArmorClass.ArmorWorn, ArmorClass.ArmorType, ArmorClass.ArmorAC, ArmorClass.ArmorStealth, ArmorClass.ShieldType, 
                                             ArmorClass.ShieldAC, ArmorClass.MiscAC, ArmorClass.MagicAC);

            copy.HitPoints = new HitPoints(HitPoints.HP, HitPoints.MaxHP, HitPoints.TempHP, HitPoints.Conditions, HitPoints.DeathSaveSuccess, HitPoints.DeathSaveFailure, 
                                           HitPoints.D6, HitPoints.D8, HitPoints.D10, HitPoints.D12, HitPoints.SpentD6, HitPoints.SpentD8, HitPoints.SpentD10, 
                                           HitPoints.SpentD12);

            copy.Spellcasting = new Spellcasting(Spellcasting.Level, Spellcasting.PactTotal, Spellcasting.OneTotal, Spellcasting.TwoTotal, Spellcasting.ThreeTotal, 
                                                 Spellcasting.FourTotal, Spellcasting.FiveTotal, Spellcasting.SixTotal, Spellcasting.SevenTotal, Spellcasting.EightTotal, 
                                                 Spellcasting.NineTotal, Spellcasting.PactUsed, Spellcasting.OneUsed, Spellcasting.TwoUsed, Spellcasting.ThreeUsed, 
                                                 Spellcasting.FourUsed, Spellcasting.FiveUsed, Spellcasting.SixUsed, Spellcasting.SevenUsed, Spellcasting.EightUsed, 
                                                 Spellcasting.NineUsed);

            copy.Companion = new Companion(Companion.Name, Companion.AC, Companion.HitDice, Companion.HP, Companion.CurrentHP, Companion.Speed, Companion.Strength,
                                           Companion.Dexterity, Companion.Constitution, Companion.Intelligence, Companion.Wisdom, Companion.Charisma, Companion.Perception,
                                           Companion.Senses, Companion.Attack.Copy(), Companion.Type.Copy(), Companion.AtkBonus.Copy(), Companion.Damage.Copy(), 
                                           Companion.DmgType.Copy(), Companion.Reach.Copy(), Companion.Notes.Copy());
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
            oNotes.Clear();
            Spellcasting.spellClass.Clear();
            Spellcasting.spellList.Clear();
            oDocuments.Clear();
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
        [DisplayName("Class")]
        [Description("Class broadly describes a character’s vocation, what special talents he or she possesses, and the tactics he or she is most likely to employ.")]
        public string Class
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Details")]
        [DisplayName("Race")]
        [Description("Race contributes to a character’s identity in an important way, by establishing a general appearance and the natural talents gained from culture and ancestry.")]
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
        [DisplayName("Level")]
        [Description("Typically, a character starts at 1st level and advances in level by adventuring and gaining experience points (XP).")]
        public int Level
        {
            get
            {
                return iLevel;
            }
            set
            {
                if (value <= Constants.MAX_LEVEL && value > 0)
                {
                    iLevel = value;
                    ProficiencyBonus = Constants.Proficiency(iLevel);
                }
            }
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
        [DisplayName("Trait 1")]
        [Description("They should be self-descriptions that are specific about what makes a character stand out.")]
        public string Trait1
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Character Personality")]
        [DisplayName("Trait 2")]
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

        #region Non-Borwsable Accessors

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
                return 10 + oSkills[12].Bonus + PassivePerceptionBonus;
            }
        }

        [Browsable(false)]
        [ReadOnly(true)]
        public int ProficiencyBonus
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
