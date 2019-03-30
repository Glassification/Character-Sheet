using MyCharacterSheet.SavingThrowsNamespace;
using MyCharacterSheet.SkillsNamespace;
using MyCharacterSheet.TypeConverters;
using MyCharacterSheet.Utility;
using System;
using System.ComponentModel;
using System.Drawing;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Conditions : ExpandableObjectConverter
    {

        #region Constants

        public const string BlindedDescription = "Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.";
        public const string CharmedDescription = "You cannot attack the charmer. The charmer has advantage on ability checks when interacting socially.";
        public const string DeafenedDescription = "You cannot hear and automatically fails any ability check that requires hearing.";
        public const string EncumbranceDescription = "A carry weight exceding 5 and 10 times Strength will reduce speed by 10 and 20 respectivly.";
        public const string Exausted1 = "Disadvantage on Ability Checks";
        public const string Exausted2 = "Speed halved";
        public const string Exausted3 = "Disadvantage on Attack rolls and Saving Throws";
        public const string Exausted4 = "Hit point maximum halved";
        public const string Exausted5 = "Speed reduced to 0";
        public const string Exausted6 = "Death.";
        public const string FatiguedDescription = "Exaustion levels stack up to 6. A long rest reduces the level by 1.";
        public const string FrightenedDescription = "You have disadvantage on Ability Checks and Attack rolls while the source of fear is within line of sight. You can’t willingly move closer to the source.";
        public const string GrappledDescription = "Your speed becomes 0. It ends when the grappler is incapacitated or you are thrown away.";
        public const string IncapacitatedDescription = "You Cannot take Actions or reactions.";
        public const string InvisibleDescription = "You are impossible to see without the aid of magic or a Special sense. Attacks against you have disadvantage, your attacks have advantage.";
        public const string ParalyzedDescription = "You are incapacitated and automatically fail Strength and Dexterity Saving Throws. Attacks have advantage, and melee are auto crit.";
        public const string PetrifiedDescription = "You are transformed into an inanimate substance and are incapacitated. Resistant to all damage and immune to posion and disease.";
        public const string PoisonedDescription = "You have disadvantage on Attack rolls and Ability Checks";
        public const string ProneDescription = "Your only movement option is to crawl and have disadvantage on attacks. Melee attack is advantage, ranged is disadantage.";
        public const string RestrainedDescription = "Your speed becomes 0. Your attacks have disadvantage, enemies have advantage. Dexterity Saving Throws are disadvantage.";
        public const string StunnedDescription = "You are incapacitated and speak falteringly, and automatically fail Strength and Dexterity Saving Throws. Attacks against have advantage.";
        public const string UnconsciousDescription = "You are incapacitated, drop what you're holding, and fall prone. Attacks against have advantage and hits are auto crit.";

        #endregion

        #region Constructor

        public Conditions()
        {
            Blinded = "Cured";
            Charmed = "Cured";
            Deafened = "Cured";
            Fatigued = "Cured";
            Frightened = "Cured";
            Grappled = "Cured";
            Incapacitated = "Cured";
            Invisible = "Cured";
            Paralyzed = "Cured";
            Petrified = "Cured";
            Poisoned = "Cured";
            Prone = "Cured";
            Restrained = "Cured";
            Stunned = "Cured";
            Unconscious = "Cured";
        }

        #endregion

        #region Methods

        private bool Afflicted(string condition)
        {
            return !condition.Equals("Cured");
        }

        /// =========================================
        /// FormatChecks()
        /// =========================================
        public void FormatChecks()
        {
            if (Fatigued.Equals("Exhaustion 1") || Fatigued.Equals("Exhaustion 2") || Fatigued.Equals("Exhaustion 3") || Fatigued.Equals("Exhaustion 4") || Fatigued.Equals("Exhaustion 5") || Afflicted(Frightened) || Afflicted(Poisoned))
            {
                foreach (Skills skill in Program.Character.oSkills)
                {
                    skill.Checks = Constants.Checks.Disadvantage;
                }
            }

            if (Afflicted(Blinded))
            {
                foreach (Skills skill in Program.Character.oSkills)
                {
                    skill.Checks = Constants.Checks.Fail;
                }
            }
             
            if (Fatigued.Equals("Exhaustion 3") || Fatigued.Equals("Exhaustion 4") || Fatigued.Equals("Exhaustion 5"))
            {
                foreach (SavingThrows saves in Program.Character.oSavingThrows)
                {
                    saves.Checks = Constants.Checks.Disadvantage;
                }
            }

            if (Afflicted(Restrained))
            {
                Program.Character.oSavingThrows[(int)MainPage.Saves.Dexterity].Checks = Constants.Checks.Disadvantage;
            }

            if (Afflicted(Paralyzed) || Afflicted(Stunned))
            {
                Program.Character.oSavingThrows[(int)MainPage.Saves.Strength].Checks = Constants.Checks.Fail;
                Program.Character.oSavingThrows[(int)MainPage.Saves.Dexterity].Checks = Constants.Checks.Fail;
            }
        }

        /// =========================================
        /// ToArray()
        /// =========================================
        public string[] ToArray()
        {
            string[] array = new string[16];

            array[0] = Blinded.Equals("Cured") ? "" : "Blinded";
            array[1] = Charmed.Equals("Cured") ? "" : "Charmed";
            array[2] = Deafened.Equals("Cured") ? "" : "Deafened";
            array[3] = Encumbrance.Equals("Normal") ? "" : Encumbrance;
            array[4] = Fatigued.Equals("Cured") ? "" : Fatigued;
            array[5] = Frightened.Equals("Cured") ? "" : "Frightened";
            array[6] = Grappled.Equals("Cured") ? "" : "Grappled";
            array[7] = Incapacitated.Equals("Cured") ? "" : "Incapacitated";
            array[8] = Invisible.Equals("Cured") ? "" : "Invisible";
            array[9] = Paralyzed.Equals("Cured") ? "" : "Paralyzed";
            array[10] = Petrified.Equals("Cured") ? "" : "Petrified";
            array[11] = Poisoned.Equals("Cured") ? "" : "Poisoned";
            array[12] = Prone.Equals("Cured") ? "" : "Prone";
            array[13] = Restrained.Equals("Cured") ? "" : "Restrained";
            array[14] = Stunned.Equals("Cured") ? "" : "Stunned";
            array[15] = Unconscious.Equals("Cured") ? "" : "Unconscious";

            return array;
        }

        /// =========================================
        /// GetDescription()
        /// =========================================
        public string GetDescription(string name)
        {
            string description;
            string str = ", ";

            switch (name)
            {
                case "Blinded":
                    description = BlindedDescription;
                    break;
                case "Charmed":
                    description = CharmedDescription;
                    break;
                case "Deafened":
                    description = DeafenedDescription;
                    break;
                case "Encumbered":
                case "Heavily Encumbered":
                    description = EncumbranceDescription;
                    break;
                case "Frightened":
                    description = FrightenedDescription;
                    break;
                case "Grappled":
                    description = GrappledDescription;
                    break;
                case "Incapacitated":
                    description = IncapacitatedDescription;
                    break;
                case "Invisible":
                    description = InvisibleDescription;
                    break;
                case "Paralyzed":
                    description = ParalyzedDescription;
                    break;
                case "Petrified":
                    description = PetrifiedDescription;
                    break;
                case "Poisoned":
                    description = PoisonedDescription;
                    break;
                case "Prone":
                    description = ProneDescription;
                    break;
                case "Restrained":
                    description = RestrainedDescription;
                    break;
                case "Stunned":
                    description = StunnedDescription;
                    break;
                case "Unconscious":
                    description = UnconsciousDescription;
                    break;
                case "Exhaustion 1":
                    description = Exausted1;
                    break;
                case "Exhaustion 2":
                    description = Exausted1 + str + Exausted2;
                    break;
                case "Exhaustion 3":
                    description = Exausted1 + str + Exausted2 + str + Exausted3;
                    break;
                case "Exhaustion 4":
                    description = Exausted1 + str + Exausted2 + str + Exausted3 + str + Exausted4;
                    break;
                case "Exhaustion 5":
                    description = Exausted1 + str + Exausted2 + str + Exausted3 + str + Exausted4 + str + Exausted5;
                    break;
                case "Exhaustion 6":
                    description = Exausted6;
                    break;
                default:
                    description = "";
                    break;
            }

            return description;
        }

        /// =========================================
        /// FormatMovement()
        /// =========================================
        public string FormatMovement()
        {
            string movement = Program.Character.Movement;
            int intMovement;

            if (int.TryParse(Program.Character.Movement, out intMovement))
            {
                if (Fatigued.Equals("Exhaustion 5") || Grappled.Equals("Afflicted") || Restrained.Equals("Afflicted"))
                {
                    movement = "0";
                }
                else
                {
                    if (Encumbrance.Equals("Encumbered"))
                    {
                        movement = (intMovement - 10).ToString();
                        intMovement = intMovement - 10;
                    }
                    else if (Encumbrance.Equals("Heavily Encumbered"))
                    {
                        movement = (intMovement - 20).ToString();
                        intMovement = intMovement - 20;
                    }

                    if (Fatigued.Equals("Exhaustion 2") || Fatigued.Equals("Exhaustion 3") || Fatigued.Equals("Exhaustion 4"))
                    {
                        movement = (intMovement / 2).ToString();
                    }
                }
            }

            return movement;
        }

        /// =========================================
        /// FormatHealth()
        /// =========================================
        public string FormatHealth()
        {
            string health = Program.Character.HitPoints.MaxHP.ToString();
            int halfHP = Program.Character.HitPoints.MaxHP / 2;

            if (Fatigued.Equals("Exhaustion 4") || Fatigued.Equals("Exhaustion 5"))
            {
                health = halfHP.ToString();
                
                if (Program.Character.HitPoints.HP > halfHP)
                {
                    Program.Character.HitPoints.HP = halfHP;
                }
            }
            else if (Fatigued.Equals("Exhaustion 6"))
            {
                health = "0";
                Program.Character.HitPoints.HP = 0;
            }

            return health;
        }

        #endregion

        #region Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Blinded")]
        [Description(BlindedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Blinded
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Charmed")]
        [Description(CharmedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Charmed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Deafened")]
        [Description(DeafenedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Deafened
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Conditions")]
        [DisplayName("Encumbrance")]
        [Description(EncumbranceDescription)]
        public string Encumbrance
        {
            get
            {
                string str = "Normal";

                if (Program.Character.ArmorClass.ArmorStrength > Program.Character.Strength)
                {
                    str = "Encumbered";
                }

                if (Settings.UseEncumbrance)
                {
                    if (Program.Character.CarryWeight > Program.Character.Light && Program.Character.CarryWeight <= Program.Character.Medium)
                    {
                        str = "Encumbered";
                    }
                    else if (Program.Character.CarryWeight > Program.Character.Medium)
                    {
                        str = "Heavily Encumbered";
                    }

                }

                return str;
            }
        }
        
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Fatigued")]
        [Description(FatiguedDescription)]
        [TypeConverter(typeof(ExhaustionConverter))]
        public string Fatigued
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Frightened")]
        [Description(FrightenedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Frightened
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Grappled")]
        [Description(GrappledDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Grappled
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Incapacitated")]
        [Description(IncapacitatedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Incapacitated
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Invisible")]
        [Description(InvisibleDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Invisible
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Paralyzed")]
        [Description(ParalyzedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Paralyzed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Petrified")]
        [Description(PetrifiedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Petrified
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Poisoned")]
        [Description(PoisonedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Poisoned
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Prone")]
        [Description(ProneDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Prone
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Restrained")]
        [Description(RestrainedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Restrained
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Stunned")]
        [Description(StunnedDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Stunned
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Unconscious")]
        [Description(UnconsciousDescription)]
        [TypeConverter(typeof(ConditionConverter))]
        public string Unconscious
        {
            get;
            set;
        }

        #endregion

    }
}
