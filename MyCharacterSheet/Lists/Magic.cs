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

        public Magic(string magic)
        {
            string[] tokens;

            tokens = magic.Split(DELIMITER);

            Class = tokens[0];
            Ability = tokens[1];
            Cantrips = tokens[2];
            Spells = tokens[3];
            Prepared = tokens[4];
            ID = new Guid(tokens[5]);
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
