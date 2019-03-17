using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    /// <summary>
    /// Represents details about a weapon a player can wield.
    /// </summary>
    public class Weapon
    {

        #region Constructor

        public Weapon(string weapon)
        {
            string[] tokens;

            tokens = weapon.Split(DELIMITER);

            Name = tokens[0];
            Ability = tokens[1];
            Damage = tokens[2];
            Misc = tokens[3];
            Type = tokens[4];
            Range = tokens[5];
            Notes = tokens[6];
            Weight = tokens[7];
            ID = new Guid(tokens[8]);
        }

        public Weapon(bool unique)
        {
            if (unique)
                ID = Guid.NewGuid();
            else
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
