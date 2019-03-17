using System;

namespace MyCharacterSheet.Utility
{
    /// <summary>
    /// Provides constants and static methods for global properties of the character sheet.
    /// </summary>
    public static class Settings
    {

        #region Static Constructor

        static Settings()
        {
            Default();
        }

        #endregion

        #region Methods

        /// =========================================
        /// Default()
        /// =========================================
        public static void Default()
        {
            RememberMute        = false;
            MuteState           = Program.Mute;
            RememberLastTab     = false;
            LastTab             = 0;
            AutosaveEnable      = false;
            AutosaveInterval    = 1;
            HideAnimalCompanion = false;
            UseCoinWeight          = false;
            UseEncumbrance         = false;
        }

        #endregion

        #region Accessors

        public static bool RememberMute
        {
            get;
            set;
        }

        public static bool MuteState
        {
            get;
            set;
        }

        public static bool RememberLastTab
        {
            get;
            set;
        }

        public static int LastTab
        {
            get;
            set;
        }

        public static bool AutosaveEnable
        {
            get;
            set;
        }

        public static int AutosaveInterval
        {
            get;
            set;
        }

        public static bool HideAnimalCompanion
        {
            get;
            set;
        }

        public static bool UseCoinWeight
        {
            get;
            set;
        }

        public static bool UseEncumbrance
        {
            get;
            set;
        }

        #endregion

    }
}
