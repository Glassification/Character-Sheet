using static Concierge.Utility.Constants;
using System;

namespace Concierge.Lists
{
    #nullable enable
    /// <summary>
    /// Represents an item that the player can carry or use.
    /// </summary>
    public class Inventory
    {

        #region Constructor

        public Inventory()
        {
            Name = "";
            Amount = "";
            Weight = "";
            Note = "";
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
