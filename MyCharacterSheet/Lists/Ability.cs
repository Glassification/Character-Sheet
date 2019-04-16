using static MyCharacterSheet.Utility.Constants;
using System;

namespace MyCharacterSheet.Lists
{
    #nullable enable
    /// <summary>
    /// Represents details about a players feats, abilities, or traits.
    /// </summary>
    public class Ability
    {

        #region Constructor

        public Ability(string ability)
        {
            string[] tokens;

            tokens = ability.Split(DELIMITER);

            Name = tokens[0];
            Level = tokens[1];
            Uses = tokens[2];
            Recovery = tokens[3];
            Action = tokens[4];
            Note = tokens[5];
            ID = new Guid(tokens[6]);
        }

        #endregion

        #region Accessors

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

        public string Uses
        {
            get;
            set;
        }

        public string Recovery
        {
            get;
            set;
        }

        public string Action
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
