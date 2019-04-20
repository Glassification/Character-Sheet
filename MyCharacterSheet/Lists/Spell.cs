using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents details about a spell the player can learn.
    /// </summary>
    public class Spell
    {

        #region Constructor

        public Spell()
        {
            Name = "";
            Level = "";
            Page = "";
            School = "";
            Ritual = "";
            Components = "";
            Concentration = "";
            Range = "";
            Duration = "";
            Area = "";
            Save = "";
            Damage = "";
            Description = "";
            Prepared = "";
            ID = Guid.Empty;
        }

        #endregion

        #region Methods

        /// =========================================
        /// ToString()
        /// =========================================
        public override string ToString()
        {
            return Name;//Level.Equals("Cantrip") ? "0" : Level + " - " + Name;
        }

        #endregion

        #region Accessors

        public string Prepared
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Level
        {
            get;
            set;
        }

        public string Page
        {
            get;
            set;
        }

        public string School
        {
            get;
            set;
        }

        public string Ritual
        {
            get;
            set;
        }

        public string Components
        {
            get;
            set;
        }

        public string Concentration
        {
            get;
            set;
        }

        public string Range
        {
            get;
            set;
        }

        public string Duration
        {
            get;
            set;
        }

        public string Area
        {
            get;
            set;
        }

        public string Save
        {
            get;
            set;
        }

        public string Damage
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public Guid ID
        {
            get;
            set;
        }

        #endregion

    }
}
