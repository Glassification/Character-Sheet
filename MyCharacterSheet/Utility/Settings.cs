﻿using System;
using System.Drawing;

namespace MyCharacterSheet.Utility
{
    public static class Settings
    {
        static Settings()
        {
            Default();
        }

        public static void Default()
        {
            RememberMute        = false;
            MuteState           = Program.Mute;
            RememberLastTab     = false;
            LastTab             = 0;
            AutosaveEnable      = false;
            AutosaveInterval    = 1;
            HideAnimalCompanion = false;
        }

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
    }
}
