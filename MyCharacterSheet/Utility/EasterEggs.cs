using System.Windows.Forms;

namespace MyCharacterSheet.Utility
{
    #nullable enable
    public static class EasterEggs
    {

        private static readonly Keys[] oKonamiKeys = { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A };

        static EasterEggs()
        {
            KonamiIndex = 0;
        }

        #region Methods

        /// =========================================
        /// KonamiCode()
        /// =========================================
        public static bool KonamiCode(Keys key)
        {
            bool entered = false;

            //Check if key matches
            if (oKonamiKeys[KonamiIndex] == key)
            {
                KonamiIndex++;
            }
            else
            {
                KonamiIndex = 0;
            }

            //Check if code is complete
            if (KonamiIndex == oKonamiKeys.Length)
            {
                entered = true;
                KonamiIndex = 0;
            }

            return entered;
        }

        #endregion

        #region Accessors

        private static int KonamiIndex
        {
            get;
            set;
        }

        #endregion

    }
}
