using MyCharacterSheet.Characters;
using MyCharacterSheet.Utility;
using System;
using System.Globalization;
using System.Windows.Forms;
using static MyCharacterSheet.Utility.Constants;

namespace MyCharacterSheet
{
    #nullable enable
    ///<summary>
    /// Provides constants and static methods for the entry point of the application.
    /// </summary>
    static public class Program
    {

        #region Methods

        static Program()
        {
            Character = new Character();
            Loading = true;
            Mute = true;
            Typing = false;
            LastTable = Tables.Abilities;
        }

        /// =========================================
        /// Main()
        /// =========================================
        [STAThread]
        static void Main()
        {
            //SpellParser.ParseSpellCSV();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainForm = new MainPage());
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

        public static MainPage MainForm
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

        public static Tables LastTable
        {
            get;
            set;
        }

        #endregion

    }
}
