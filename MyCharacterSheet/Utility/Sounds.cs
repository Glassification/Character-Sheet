using System;
using System.IO;
using System.Media;
using System.Speech.Synthesis;

namespace MyCharacterSheet.Utility
{
    #nullable enable
    public static class Sounds
    {
        
        #region Members

        private static SoundPlayer oClickSound;
        private static SoundPlayer oInputSound;
        private static SoundPlayer oEasterEggSound;
        private static SoundPlayer oHeMan;

        #endregion

        #region Constructor

        static Sounds()
        {
            Stream stream;
            
            stream = Properties.Resources.click;
            oClickSound = new SoundPlayer(stream);

            stream = Properties.Resources.input;
            oInputSound = new SoundPlayer(stream);

            stream = Properties.Resources.Secret;
            oEasterEggSound = new SoundPlayer(stream);

            stream = Properties.Resources.I_Say_Hey;
            oHeMan = new SoundPlayer(stream);

            stream.Close();
        }

        #endregion

        #region Methods

        /// =========================================
        /// ButtonClick()
        /// =========================================
        public static void ButtonClick()
        {
            if (!Program.Mute)
            {
                oClickSound.Play();
            }
        }

        /// =========================================
        /// InputEnter()
        /// =========================================
        public static void InputEnter()
        {
            if (!Program.Mute)
            {
                oInputSound.Play();
            }
        }

        /// =========================================
        /// EasterEgg()
        /// =========================================
        public static void EasterEgg()
        {
            oEasterEggSound.Play();
        }

        /// =========================================
        /// HeMan()
        /// =========================================
        public static void HeMan()
        {
            oHeMan.PlayLooping();
        }

        /// =========================================
        /// StopLooping()
        /// =========================================
        public static void StopLooping()
        {
            oHeMan.Stop();
        }

        #endregion

    }
}
