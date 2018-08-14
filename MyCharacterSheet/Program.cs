using MyCharacterSheet.Characters;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    static class Program
    {

        #region Methods

        /// =========================================
        /// Main()
        /// =========================================
        [STAThread]
        static void Main()
        {
            Character = new Character();
            Loading = true;
            Mute = false;
            Typing = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPage());
        }

        /// =========================================
        /// Termination()
        /// =========================================
        public static void Termination()
        {
            DateTime date = DateTime.Now;
            CultureInfo culture = new CultureInfo("en-GB");

            Console.WriteLine("\nProgrammed by Thomas Beckett");
            Console.WriteLine("Date: {0}", date.ToString(culture));
            Console.WriteLine("** End of processing. **\n");
        }

        #endregion

        #region Accessors

        public static Character Character
        {
            get;
            private set;
        }

        public static bool Loading
        {
            get;
            set;
        }

        public static string FileLocation
        {
            get;
            set;
        }

        public static bool Modified
        {
            get;
            set;
        }

        public static bool Mute
        {
            get;
            set;
        }

        public static bool Typing
        {
            get;
            set;
        }

        #endregion

    }
}
