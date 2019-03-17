using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    /// <summary>
    /// Represents information about the ammunition a player can carry.
    /// </summary>
    public class Ammunition
    {
        #region Constructor

        public Ammunition(string ammo)
        {
            string[] tokens;

            tokens = ammo.Split(DELIMITER);

            Name = tokens[0];
            Quantity = tokens[1];
            Bonus = tokens[2];
            Type = tokens[3];
            Used = tokens[4];
            ID = new Guid(tokens[5]);
        }

        public Ammunition(bool unique)
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

        public string Quantity
        {
            get;
            set;
        }

        public string Bonus
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Used
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
