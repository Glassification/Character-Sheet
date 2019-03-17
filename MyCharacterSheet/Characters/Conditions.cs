using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCharacterSheet.Characters
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Conditions : ExpandableObjectConverter
    {

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

        #region Accessors

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Blinded")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Blinded
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Charmed")]
        [Description("You cannot attack the charmer. The charmer has advantage on ability checks when interacting socially.")]
        public string Charmed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Deafened")]
        [Description("You cannot hear and automatically fails any ability check that requires hearing.")]
        public string Deafened
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Encumbered")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Encumbered
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Fatigued")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Fatigued
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Frightened")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Frightened
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Grappled")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Grappled
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Incapacitated")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Incapacitated
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Invisible")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Invisible
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Paralyzed")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Paralyzed
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Petrified")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Petrified
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Poisoned")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Poisoned
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Prone")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Prone
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Restrained")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Restrained
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Stunned")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Stunned
        {
            get;
            set;
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Conditions")]
        [DisplayName("Unconscious")]
        [Description("Automatically fail any ability checks. Attack rolls against you have advantage, your attacks have disadvantage.")]
        public string Unconscious
        {
            get;
            set;
        }

        #endregion

    }
}
