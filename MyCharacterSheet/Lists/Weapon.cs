using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents details about a weapon a player can wield.
    /// </summary>
    public class Weapon
    {

        #region Constructor

        public Weapon()
        {
            Name = "";
            Ability = "";
            Damage = "";
            Misc = "";
            Type = "";
            Range = "";
            Notes = "";
            Weight = "";
            ID = Guid.Empty;
        }

        #endregion

        #region Methods

        /// =========================================
        /// ToString()
        /// =========================================
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Accessors

        public string Name
        {
            get;
            set;
        }

        public string Ability
        {
            get;
            set;
        }

        public string Damage
        {
            get;
            set;
        }

        public string Misc
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Range
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }

        public string Weight
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
