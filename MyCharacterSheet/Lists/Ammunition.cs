using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents information about the ammunition a player can carry.
    /// </summary>
    public class Ammunition
    {
        #region Constructor

        public Ammunition()
        {
            Name = "";
            Quantity = "";
            Bonus = "";
            Type = "";
            Used = "";
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
