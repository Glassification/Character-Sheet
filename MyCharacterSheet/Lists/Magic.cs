using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents the details of a players spell casting class.
    /// </summary>
    public class Magic
    {

        #region Constructor

        public Magic()
        {
            Class = "";
            Ability = "";
            Cantrips = "";
            Spells = "";
            Prepared = "";
            ID = Guid.Empty;
        }

        #endregion

        #region Accessors

        public string Class
        {
            get;
            set;
        }

        public string Ability
        {
            get;
            set;
        }

        public string Cantrips
        {
            get;
            set;
        }

        public string Spells
        {
            get;
            set;
        }

        public string Prepared
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
