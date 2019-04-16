using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents an item that the player can carry or use.
    /// </summary>
    public class Inventory
    {

        #region Constructor

        public Inventory(string inventory)
        {
            string[] tokens;

            tokens = inventory.Split(DELIMITER);

            Name = tokens[0];
            Amount = tokens[1];
            Weight = tokens[2];
            Note = tokens[3];
            ID = new Guid(tokens[4]);
        }

        public Inventory(bool unique)
        {
            Name = "";
            Amount = "";
            Weight = "";
            Note = "";

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

        public string Amount
        {
            get;
            set;
        }

        public string Weight
        {
            get;
            set;
        }

        public string Note
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
