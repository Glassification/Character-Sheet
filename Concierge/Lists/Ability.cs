using static Concierge.Utility.Constants;
using System;

namespace Concierge.Lists
{
    #nullable enable
    /// <summary>
    /// Represents details about a players feats, abilities, or traits.
    /// </summary>
    public class Ability
    {

        #region Constructor

        public Ability()
        {
            Name = "";
            Level = "";
            Uses = "";
            Recovery = "";
            Action = "";
            Note = "";
            ID = Guid.Empty;
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
