using MyCharacterSheet.TypeConverters;
using System;
using System.ComponentModel;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Conditions : ExpandableObjectConverter
    {

        #region Constants

        public const string BlindedDescription = "Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.";
        public const string CharmedDescription = "You cannot attack the charmer. The charmer has advantage on ability checks when interacting socially.";
        public const string DeafenedDescription = "You cannot hear and automatically fails any ability check that requires hearing.";
        public const string Exausted1 = "";
        public const string Exausted2 = "";
        public const string Exausted3 = "";
        public const string Exausted4 = "";
        public const string Exausted5 = "";
        public const string Exausted6 = "";
        public const string FatiguedDescription = "";
        public const string FrightenedDescription = "";
        public const string GrappledDescription = "";
        public const string IncapacitatedDescription = "";
        public const string InvisibleDescription = "";
        public const string ParalyzedDescription = "";
        public const string PetrifiedDescription = "";
        public const string PoisonedDescription = "";
        public const string ProneDescription = "";
        public const string RestrainedDescription = "";
        public const string StunnedDescription = "";
        public const string UnconsciousDescription = "";

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

        /// =========================================
        /// ToArray()
        /// =========================================
        public string[] ToArray()
        {
            string[] array = new string[15];

            array[0] = Blinded.Equals("Cured") ? "" : "Blinded";
            array[1] = Charmed.Equals("Cured") ? "" : "Charmed"; ;
            array[2] = Deafened.Equals("Cured") ? "" : "Deafened"; ;
            array[3] = Fatigued.Equals("Cured") ? "" : Fatigued; ;
            array[4] = Frightened.Equals("Cured") ? "" : "Frightened"; ;
            array[5] = Grappled.Equals("Cured") ? "" : "Grappled"; ;
            array[6] = Incapacitated.Equals("Cured") ? "" : "Incapacitated"; ;
            array[7] = Invisible.Equals("Cured") ? "" : "Invisible"; ;
            array[8] = Paralyzed.Equals("Cured") ? "" : "Paralyzed"; ;
            array[9] = Petrified.Equals("Cured") ? "" : "Petrified"; ;
            array[10] = Poisoned.Equals("Cured") ? "" : "Poisoned"; ;
            array[11] = Prone.Equals("Cured") ? "" : "Prone"; ;
            array[12] = Restrained.Equals("Cured") ? "" : "Restrained"; ;
            array[13] = Stunned.Equals("Cured") ? "" : "Stunned"; ;
            array[14] = Unconscious.Equals("Cured") ? "" : "Unconscious"; ;

            return array;
        }

        /// =========================================
        /// GetDescription()
        /// =========================================
        public string GetDescription(string name)
        {
            string description = "";

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
                    description = Exausted2;
                    break;
                case "Exhaustion 3":
                    description = Exausted3;
                    break;
                case "Exhaustion 4":
                    description = Exausted4;
                    break;
                case "Exhaustion 5":
                    description = Exausted5;
                    break;
                case "Exhaustion 6":
                    description = Exausted6;
                    break;
            }

            return description;
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
