using MyCharacterSheet.Lists;
using MyCharacterSheet.SavingThrowsNamespace;
using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static MyCharacterSheet.Utility.Constants;

namespace MyCharacterSheet
{
#nullable enable
    public partial class MainPage : Form
    {

        #region Constants

        private const int MENU_HEIGHT = 26;
        private const int MENU_RATE = 2;
        private const int TAB_WIDTH = 47;
        private const int TAB_RATE = 4;
        private const int SAVE_X_OFFSET = 170;
        private const int SAVE_Y_OFFSET = 95;

        #endregion

        #region Members

        private PropertyPage oPropertyPage = new PropertyPage();
        private DivideLootPage oDivideLootPage = new DivideLootPage();
        private DiceRollerPage oDiceRollerPage = new DiceRollerPage();
        private SettingsPage oSettingsPage = new SettingsPage();
        public TablePage oTablePage = new TablePage();
        private EasterEggPage oEasterEggPage = new EasterEggPage();

        private List<Label> oLabels = new List<Label>();
        private List<Button> oButtons = new List<Button>();
        private List<float> oLabelSizes = new List<float>();
        private List<float> oButtonSizes = new List<float>();

        private SecondaryPage oSecondaryPage = new SecondaryPage();
        private TertiaryPage oTertiaryPage = new TertiaryPage();
        private CampainPage oCampainPage = new CampainPage();

        private VerticalButton btnPrimary = new VerticalButton();
        private VerticalButton btnSecondary = new VerticalButton();
        private VerticalButton btnTertiary = new VerticalButton();
        private VerticalButton btnCampain = new VerticalButton();

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

        public enum Pages { Primary, Secondary, Tertiary, Campain };

        public enum Saves { Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma };

        public enum Skills
        {
            Athletics, Acrobatics, SleightOfHand, Stealth, Arcana, History,
            Investigation, Nature, Religion, AnimalHandling, Insight, Medicine,
            Perception, Survival, Deception, Intimidation, Performance, Persuasion
        };

        #endregion

        #region Constructor

        /// =========================================
        /// MainPage()
        /// =========================================
        public MainPage()
        {
            Program.Loading = true;

            InitializeComponent();

            //Create message filter
            NativeMethods filter = new NativeMethods(this);
            filter.FormClicked += mouseFilter_FormClicked;
            Application.AddMessageFilter(filter);

            //Initialize variables
            Fullscreen = false;
            Drawing = false;
            MinimumSize = Size;

            //Load blank character sheet
            Program.FileLocation = NEW_FILE;
            Program.Character.CreateCharacterSheet();

            //Load secondary page
            Controls.Add(oSecondaryPage);
            oSecondaryPage.Visible = false;
            oSecondaryPage.Dock = DockStyle.Fill;

            //Load tertiary page
            Controls.Add(oTertiaryPage);
            oTertiaryPage.Visible = false;
            oTertiaryPage.Dock = DockStyle.Fill;

            //Load Campain Page
            Controls.Add(oCampainPage);
            oCampainPage.Visible = false;
            oCampainPage.Dock = DockStyle.Fill;

            //Set intial program state
            FillLabelList();
            FillSizes();
            LoadPageLists();
            CreateMenuStrip();
            SetTabPanel();
            CreateTabButtons();
            CreateSaveProgressBar();
            FormatInputBoxes();
            FormatContextMenus();
            Mute(Program.Mute);

            //Get absolute position of mouse
            AddNestedMouseHandler(this, NestedControl_Mousemove);

            Program.Loading = false;
        }

        #endregion

        #region Methods

        /// =========================================
        /// NestedControl_Mousemove()
        /// ========================================= 
        protected virtual void NestedControl_Mousemove(object sender, MouseEventArgs e)
        {
            Control? current = sender as Control;

            if (current != null && current.Parent != null && current.Parent != this)
            {
                MouseEventArgs newArgs = new MouseEventArgs
                (
                    e.Button,
                    e.Clicks,
                    e.X + current.Location.X,
                    e.Y + current.Location.Y,
                    e.Delta
                );
                NestedControl_Mousemove(current.Parent, newArgs);
            }
            else
            {
                MainPage_MouseMove(current == null ? new object() : current, e);
            }
        }

        /// =========================================
        /// AddNestedMouseHandler()
        /// =========================================        
        protected virtual void AddNestedMouseHandler(Control root, MouseEventHandler nestedHandler)
        {
            root.MouseMove += new MouseEventHandler(nestedHandler);

            if (root.Controls.Count > 0)
            {
                foreach (Control c in root.Controls)
                {
                    //Ignore specified componets
                    if (!c.Name.Equals(""))
                    {
                        AddNestedMouseHandler(c, nestedHandler);
                    }
                }
            }
        }

        /// =========================================
        /// LoadPageLists()
        /// =========================================
        private void LoadPageLists()
        {
            FillConditions();
            FillWeapons();
            FillAmmo();
            oSecondaryPage.FillAbility();
            oSecondaryPage.FillInventory();
            oTertiaryPage.FillSpellclass();
            oTertiaryPage.FillSpellList();
        }

        /// =========================================
        /// CreateTabButtons()
        /// =========================================
        private void CreateTabButtons()
        {
            btnPrimary.Dock = DockStyle.Fill;
            btnPrimary.FlatStyle = FlatStyle.Popup;
            btnPrimary.BackColor = DarkBlue;
            btnPrimary.Font = new Font(btnPrimary.Font.FontFamily, 10f);
            btnPrimary.VerticalText = "Character Status";

            btnSecondary.Dock = DockStyle.Fill;
            btnSecondary.FlatStyle = FlatStyle.Popup;
            btnSecondary.BackColor = DarkGrey;
            btnSecondary.Font = new Font(btnSecondary.Font.FontFamily, 10f);
            btnSecondary.VerticalText = "Character Details";

            btnTertiary.Dock = DockStyle.Fill;
            btnTertiary.FlatStyle = FlatStyle.Popup;
            btnTertiary.BackColor = DarkGrey;
            btnTertiary.Font = new Font(btnTertiary.Font.FontFamily, 10f);
            btnTertiary.VerticalText = "Spellcasting";

            btnCampain.Dock = DockStyle.Fill;
            btnCampain.FlatStyle = FlatStyle.Popup;
            btnCampain.BackColor = DarkGrey;
            btnCampain.Font = new Font(btnTertiary.Font.FontFamily, 10f);
            btnCampain.VerticalText = "Campain Notes";

            btnPrimary.Click += new EventHandler(btnPrimaryPanel_Click);
            btnSecondary.Click += new EventHandler(btnSecondaryPanel_Click);
            btnTertiary.Click += new EventHandler(btnTertiaryPanel_Click);
            btnCampain.Click += new EventHandler(btnCampainPanel_Click);

            oTabPanelTable.Controls.Add(btnPrimary, 0, 0);
            oTabPanelTable.Controls.Add(btnSecondary, 0, 1);
            oTabPanelTable.Controls.Add(btnTertiary, 0, 2);
            oTabPanelTable.Controls.Add(btnCampain, 0, 3);
        }

        /// =========================================
        /// SetTabPanel()
        /// =========================================
        private void SetTabPanel()
        {
            oTabPanel.Location = new Point(0, 0);
            oTabPanel.Height = Size.Height;
            oTabPanel.Width = 0;

            TabHidden = true;
            TabHiding = false;
        }

        /// =========================================
        /// CreateSaveProgressBar()
        /// =========================================
        private void CreateSaveProgressBar()
        {
            oSavePanel.Location = new Point(Size.Width - SAVE_X_OFFSET, Size.Height - SAVE_Y_OFFSET);
            oSavePanel.Visible = false;
        }

        /// =========================================
        /// CreateMenuStrip()
        /// =========================================
        private void CreateMenuStrip()
        {
            oMenuStrip.BackColor = DarkGrey;
            oMenuStrip.ForeColor = Color.White;

            foreach (ToolStripMenuItem item in oMenuStrip.Items)
            {
                item.BackColor = DarkGrey;
                item.ForeColor = Color.White;

                foreach (ToolStripMenuItem children in item.DropDownItems)
                {
                    children.BackColor = DarkGrey;
                    children.ForeColor = Color.White;

                    foreach (ToolStripMenuItem children2 in children.DropDownItems)
                    {
                        children2.BackColor = DarkGrey;
                        children2.ForeColor = Color.White;
                    }
                }
            }

            MenuPanel.Location = new Point(0, 0);
            MenuPanel.Height = 0;
            MenuPanel.BorderStyle = BorderStyle.FixedSingle;
            MenuPanel.Width = Size.Width;
            MenuPanel.BringToFront();

            MenuHidden = true;
            MenuHiding = false;
        }

        /// =========================================
        /// FillSizes()
        /// =========================================
        private void FillSizes()
        {
            foreach (Label l in oLabels)
                oLabelSizes.Add(l.Font.Size);

            foreach (Button b in oButtons)
                oButtonSizes.Add(b.Font.Size);

            OriginalSize = Size;
            HeaderSize = oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font.Size;
            RowSize = oWeaponDataGrid.DefaultCellStyle.Font.Size;
            InputFontHP = oInputHP.Font.Size;
            CheckBoxSize = checkBox1.Font.Size;
        }

        /// =========================================
        /// FillAmmo()
        /// =========================================
        private void FillAmmo()
        {
            oAmmoGridView.Rows.Clear();

            foreach (Ammunition ammo in Program.Character.oAmmo)
            {
                int index = oAmmoGridView.Rows.Add();
                DataGridViewRow row = oAmmoGridView.Rows[index];

                row.Cells[Ammo.Index].Value = ammo.Name;
                row.Cells[Qty.Index].Value = ammo.Quantity;
                row.Cells[ammoDmgBonus.Index].Value = ammo.Bonus;
                row.Cells[AmmoDmgType.Index].Value = ammo.Type;
                row.Cells[Used.Index].Value = ammo.Used;

                // row.Cells[oIncrement.Index].

                row.Tag = ammo.ID;
            }
        }

        /// =========================================
        /// FillWeapons()
        /// =========================================
        private void FillWeapons()
        {
            oWeaponDataGrid.Rows.Clear();

            foreach (Weapon weapon in Program.Character.oWeapons)
            {
                int index = oWeaponDataGrid.Rows.Add();
                DataGridViewRow row = oWeaponDataGrid.Rows[index];

                row.Cells[Weapons.Index].Value = weapon.Name;
                row.Cells[AttackBonus.Index].Value = Program.Character.GetBonus(weapon.Ability);
                row.Cells[Ability.Index].Value = weapon.Ability;
                row.Cells[Dmg.Index].Value = weapon.Damage;
                row.Cells[MiscBonus.Index].Value = weapon.Misc;
                row.Cells[DmgType.Index].Value = weapon.Type;
                row.Cells[Range.Index].Value = weapon.Range;
                row.Cells[Notes.Index].Value = weapon.Notes;

                row.Tag = weapon.ID;
            }
        }

        /// =========================================
        /// LoadSettings()
        /// =========================================
        private void LoadSettings()
        {
            //Load mute state
            if (Settings.RememberMute)
            {
                Mute(Settings.MuteState);
            }

            //Load tab state
            if (Settings.RememberLastTab)
            {
                switch ((Pages)Settings.LastTab)
                {
                    default:
                    case Pages.Primary:
                        btnPrimaryPanel_Click(new object(), EventArgs.Empty);
                        break;
                    case Pages.Secondary:
                        btnSecondaryPanel_Click(new object(), EventArgs.Empty);
                        break;
                    case Pages.Tertiary:
                        btnTertiaryPanel_Click(new object(), EventArgs.Empty);
                        break;
                    case Pages.Campain:
                        btnCampainPanel_Click(new object(), EventArgs.Empty);
                        break;
                }
            }

            //Load animal companion
            oTertiaryPage.SetAnimalCompanionVisibility();
        }

        /// =========================================
        /// Save()
        /// =========================================
        private void Save()
        {
            Program.Character.oDocuments.Clear();
            oCampainPage.WriteCampainNotes();

            Settings.LastTab = (int)GetCurrentTab();
            Settings.MuteState = Program.Mute;
            Program.Modified = false;
            Program.Character.SaveCharacterSheet();
        }

        /// =========================================
        /// SaveFile()
        /// =========================================
        private void SaveFile(bool control, bool shift)
        {
            DialogResult result;

            if (control && (shift || Program.FileLocation.Equals(NEW_FILE)))
            {
                result = oSaveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Program.FileLocation = oSaveFileDialog.FileName;
                    Save();
                    ShowSavePopup();
                }
            }
            //Save File
            else if (control)
            {
                Save();
                ShowSavePopup();
            }
        }

        /// =========================================
        /// OpenFile()
        /// =========================================
        private void OpenFile(bool control)
        {
            DialogResult result;

            if (control)
            {
                result = oOpenFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (IsXML(oOpenFileDialog.FileName))
                    {
                        Program.FileLocation = oOpenFileDialog.FileName;
                        Program.Character.LoadCharacterSheetFromFile();
                        LoadPageLists();
                        oCampainPage.ClearDocumentList();
                        oCampainPage.FillDocumentList();

                        InvalidateAll();

                        AutosaveReset();
                        LoadSettings();

                        Program.Modified = false;
                    }
                    else
                    {
                        MessageBox.Show("Error: Non-XML file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// =========================================
        /// NewFile()
        /// =========================================
        private void NewFile(bool control)
        {
            DialogResult result;
            string fileName = GetExitString(1);

            if (control)
            {
                result = MessageBox.Show("Save changes" + fileName + "?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result != DialogResult.Cancel)
                {
                    if (result == DialogResult.Yes)
                        Save();

                    Program.FileLocation = NEW_FILE;
                    Program.Character.CreateCharacterSheet();
                    Settings.Default();
                    SetAutosaveState();
                    oTertiaryPage.SetAnimalCompanionVisibility();
                    oCampainPage.ClearDocumentList();
                    LoadPageLists();
                    InvalidateAll();
                }
            }
        }

        /// =========================================
        /// FullScreen()
        /// =========================================
        private void FullScreen()
        {
            if (Fullscreen)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = OriginalWindowState;
            }
            else
            {
                OriginalWindowState = WindowState;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            Fullscreen = !Fullscreen;

            //Toggle image
            if (Fullscreen)
                fullscreenToolStripMenuItem.Image = Properties.Resources.fullscreen_selected_128;
            else
                fullscreenToolStripMenuItem.Image = Properties.Resources.fullscreen_128;
        }

        /// =========================================
        /// Mute()
        /// =========================================
        private void Mute(bool muteState)
        {
            Program.Mute = muteState;
            muteToolStripMenuItem.Checked = Program.Mute;

            //Toggle image
            if (Program.Mute)
                muteToolStripMenuItem.Image = Properties.Resources.mute_selected_128;
            else
                muteToolStripMenuItem.Image = Properties.Resources.mute_128;
        }

        /// =========================================
        /// OpenProperties()
        /// =========================================
        private void OpenProperties(bool control)
        {
            if (!control)
            {
                oPropertyPage.ShowPane();
                FillConditions();
                InvalidateAll();
            }
        }

        /// =========================================
        /// OpenSettings()
        /// =========================================
        private void OpenSettings(bool control)
        {
            bool animal = Settings.HideAnimalCompanion;

            if (!control)
            {
                oSettingsPage.ShowPane();
                SetAutosaveState();

                if (animal != Settings.HideAnimalCompanion)
                {
                    oTertiaryPage.SetAnimalCompanionVisibility();
                }

                FillConditions();
                InvalidateAll();
            }
        }

        /// =========================================
        /// OpenTables()
        /// =========================================
        public void OpenTables()
        {
            oTablePage.ShowPane();
            LoadPageLists();
        }

        /// =========================================
        /// OpenDivideLoot()
        /// =========================================
        private void OpenDivideLoot(bool control)
        {
            if (!control)
            {
                oDivideLootPage.ShowPane();
            }
        }

        /// =========================================
        /// OpenRollDice()
        /// =========================================
        private void OpenRollDice(bool control)
        {
            if (!control)
            {
                oDiceRollerPage.ShowPane();
            }
        }

        /// =========================================
        /// LongRest()
        /// =========================================
        private void LongRest(bool control)
        {
            if (!control)
            {
                longRestToolStripMenuItem_Click(new object(), EventArgs.Empty);
            }
        }

        /// =========================================
        /// IsXML()
        /// =========================================
        public bool IsXML(string fileName)
        {
            bool isXML;

            if (Path.GetExtension(fileName).Equals(".xml"))
                isXML = true;
            else
                isXML = false;

            return isXML;
        }

        /// =========================================
        /// ShowSavePopup()
        /// =========================================
        private void ShowSavePopup()
        {
            string[] tokens = Program.FileLocation.Split('\\');

            oSaveProgressBar.Value = oSaveProgressBar.Minimum;
            oSaveBarText.Text = "..\\" + tokens[tokens.Length - 1]; ;
            oSavePanel.Visible = true;
            oSaveTimer.Start();
        }

        /// =========================================
        /// FormatCurrencyInput()
        /// =========================================
        private void FormatInputBoxes()
        {
            oInputHP.Width = oPanelHP.Width;
            oInputHP.Location = new Point(0, (oPanelHP.Height / 2) - (oInputHP.Height / 2));
        }

        /// =========================================
        /// formatContextMenu()
        /// =========================================
        private void FormatContextMenus()
        {
            oWeaponContextMenu.BackColor = DarkGrey;
            oWeaponContextMenu.ForeColor = Color.White;

            oAddWeaponContextMenu.BackColor = DarkGrey;
            oAddWeaponContextMenu.ForeColor = Color.White;

            oAmmoContextMenu.BackColor = DarkGrey;
            oAmmoContextMenu.ForeColor = Color.White;

            oAddAmmoContextMenu.BackColor = DarkGrey;
            oAddAmmoContextMenu.ForeColor = Color.White;
        }

        /// =========================================
        /// TrimLeadingZero()
        /// =========================================
        private string TrimLeadingZero(string text)
        {
            text = text.TrimStart(new char[] { '0' });

            if (text.Equals(""))
            {
                text = "0";
            }

            return text;
        }

        /// =========================================
        /// GetExitString()
        /// =========================================
        private string GetExitString(int padding)
        {
            string str = "";

            if (!Program.FileLocation.Equals(NEW_FILE))
            {
                for (int i = 0; i < padding; i++)
                    str += " ";
                str += "to '" + Path.GetFileName(Program.FileLocation) + "' ";
            }

            return str;
        }

        /// =========================================
        /// InvalidateAll()
        /// =========================================
        private void InvalidateAll()
        {
            Invalidate();
            oSecondaryPage.Invalidate();
            oTertiaryPage.Invalidate();
            oCampainPage.Invalidate();
        }

        /// =========================================
        /// InsideDataGrid()
        /// =========================================
        private bool InsideDataGrid()
        {
            bool isInside = false;

            switch (GetCurrentTab())
            {
                case Pages.Primary:
                    isInside |= IsInside(oAmmoGridView);
                    isInside |= IsInside(oWeaponDataGrid);
                    break;
                case Pages.Secondary:
                    isInside |= IsInside(oSecondaryPage.AbilityGridView());
                    isInside |= IsInside(oSecondaryPage.InventoryGridView());
                    break;
                case Pages.Tertiary:
                    isInside |= IsInside(oTertiaryPage.SpellListGridView());
                    isInside |= IsInside(oTertiaryPage.MagicGridView());
                    break;
            }

            return isInside;
        }

        /// =========================================
        /// IsInside()
        /// =========================================
        private bool IsInside(Control dataGrid)
        {
            Point point = dataGrid.PointToScreen(Point.Empty);
            Rectangle rec = new Rectangle(point, dataGrid.Size);

            return rec.Contains(Cursor.Position);
        }

        /// =========================================
        /// GetCurrentTab()
        /// =========================================
        public Pages GetCurrentTab()
        {
            Pages tab = 0;

            if (oPrimaryTable.Visible) { tab = Pages.Primary; }
            else if (oSecondaryPage.Visible) { tab = Pages.Secondary; }
            else if (oTertiaryPage.Visible) { tab = Pages.Tertiary; }
            else if (oCampainPage.Visible) { tab = Pages.Campain; }

            return tab;
        }

        /// =========================================
        /// CheckEasterEgg()
        /// =========================================
        private bool CheckEasterEgg(Keys key)
        {
            bool result = false;

            if (EasterEggs.KonamiCode(key))
            {
                result = true;
                oEasterEggPage.ShowPane();
            }

            return result;
        }

        /// =========================================
        /// SetAutosaveState()
        /// =========================================
        public void SetAutosaveState()
        {
            if (oAutosaveTimer.Enabled && !Settings.AutosaveEnable)
            {
                oAutosaveTimer.Stop();
            }
            else if (!oAutosaveTimer.Enabled && Settings.AutosaveEnable)
            {
                AutosaveTick = 1;
                oAutosaveTimer.Start();
            }
        }

        /// =========================================
        /// CheckReset()
        /// =========================================
        public void AutosaveReset()
        {
            if (oAutosaveTimer.Enabled && Settings.AutosaveEnable)
            {
                AutosaveTick = 1;
            }
            else
            {
                SetAutosaveState();
            }
        }

        /// =========================================
        /// FillConditions()
        /// =========================================
        private void FillConditions()
        {
            oConditionsDataGrid.Rows.Clear();

            foreach (string condition in Program.Character.HitPoints.Conditions.ToArray())
            {
                if (!condition.Equals(""))
                {
                    int index = oConditionsDataGrid.Rows.Add();
                    DataGridViewRow row = oConditionsDataGrid.Rows[index];

                    row.Cells[oCondition.Index].Value = condition;
                    row.Cells[oCondition.Index].ToolTipText = Program.Character.HitPoints.Conditions.GetDescription(condition);
                }
            }
        }

        /// =========================================
        /// ResizeText()
        /// =========================================
        public void ResizeLabels()
        {
            //Resize Labels
            for (int i = 0; i < oLabels.Count; i++)
            {
                oLabels[i].Font = new Font(oLabels[i].Font.FontFamily, oLabelSizes[i] * Ratio, oLabels[i].Font.Style);
                ScaleFont(oLabels[i]);
            }
        }

        /// =========================================
        /// GetCheckColor()
        /// =========================================
        private Color GetCheckColor(Checks check)
        {
            Color color;

            switch (check)
            {
                case Checks.Fail:
                case Checks.Disadvantage:
                    color = Color.IndianRed;
                    break;
                case Checks.Normal:
                default:
                    color = Color.Black;
                    break;
            }

            return color;
        }

        /// =========================================
        /// GetSavingThrowCheckStatus()
        /// =========================================
        private string GetSavingThrowCheckStatus(SavingThrows savingThrow)
        {
            string status = "";

            switch (savingThrow.Checks)
            {
                case Checks.Fail:
                    status = "Fail (" + savingThrow.Bonus + ")";
                    break;
                case Checks.Disadvantage:
                case Checks.Normal:
                default:
                    status = savingThrow.Bonus + "";
                    break;
            }

            return status;
        }

        /// =========================================
        /// GetSkillCheckStatus()
        /// =========================================
        private string GetSkillCheckStatus(SkillsNamespace.Skills skill)
        {
            string status = "";

            switch (skill.Checks)
            {
                case Checks.Fail:
                    status = "Fail (" + skill.Bonus + ")";
                    break;
                case Checks.Disadvantage:
                case Checks.Normal:
                default:
                    status = skill.Bonus + "";
                    break;
            }

            return status;
        }

        #endregion

        #region Accessors

        private int Row
        {
            get;
            set;
        }

        public Size OriginalSize
        {
            get;
            set;
        }

        private float HeaderSize
        {
            get;
            set;
        }

        private float CheckBoxSize
        {
            get;
            set;
        }

        private float RowSize
        {
            get;
            set;
        }

        private float InputFontHP
        {
            get;
            set;
        }

        private bool Fullscreen
        {
            get;
            set;
        }

        private FormWindowState OriginalWindowState
        {
            get;
            set;
        }

        private bool MenuHidden
        {
            get;
            set;
        }

        private bool MenuHiding
        {
            get;
            set;
        }

        private bool TabHidden
        {
            get;
            set;
        }

        private bool TabHiding
        {
            get;
            set;
        }

        private bool Drawing
        {
            get;
            set;
        }

        private int AutosaveTick
        {
            get;
            set;
        }

        public float Ratio
        {
            get
            {
                return Size.Width / (OriginalSize.Width + 0f);
            }
        }

        #endregion

        #region Frame Events

        /// =========================================
        /// MainPage_Paint()
        /// =========================================
        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            if (!Program.Loading)
            {
                Drawing = true;

                Program.Character.ResetChecks();

                oCharName.Text = Program.Character.Name;
                oRace.Text = Program.Character.Race;
                oBackground.Text = Program.Character.Background;
                oAlignment.Text = Program.Character.Alignment;
                oLevel.Text = Program.Character.Level + "";
                oEXP.Text = Program.Character.EXP + " / " + Constants.Experience(Program.Character.Level);
                oClass.Text = Program.Character.Class;
                oLanguage.Text = Program.Character.Language;
                oMovement.Text = Program.Character.HitPoints.Conditions.FormatMovement();
                oVision.Text = Program.Character.Vision;

                oStrScore.Text = Program.Character.Strength + "";
                oDexScore.Text = Program.Character.Dexterity + "";
                oConScore.Text = Program.Character.Constitution + "";
                oIntScore.Text = Program.Character.Intelligence + "";
                oWisScore.Text = Program.Character.Wisdom + "";
                oChaScore.Text = Program.Character.Charisma + "";

                oStrBonus.Text = Constants.Bonus(Program.Character.Strength) + "";
                oDexBonus.Text = Constants.Bonus(Program.Character.Dexterity) + "";
                oConBonus.Text = Constants.Bonus(Program.Character.Constitution) + "";
                oIntBonus.Text = Constants.Bonus(Program.Character.Intelligence) + "";
                oWisBonus.Text = Constants.Bonus(Program.Character.Wisdom) + "";
                oChaBonus.Text = Constants.Bonus(Program.Character.Charisma) + "";

                oStrSavingThrow.Text = GetSavingThrowCheckStatus(Program.Character.oSavingThrows[0]);
                oDexSavingThrow.Text = GetSavingThrowCheckStatus(Program.Character.oSavingThrows[1]);
                oConSavingThrow.Text = GetSavingThrowCheckStatus(Program.Character.oSavingThrows[2]);
                oIntSavingThrow.Text = GetSavingThrowCheckStatus(Program.Character.oSavingThrows[3]);
                oWisSavingThrow.Text = GetSavingThrowCheckStatus(Program.Character.oSavingThrows[4]);
                oChaSavingThrow.Text = GetSavingThrowCheckStatus(Program.Character.oSavingThrows[5]);

                oStrSavingThrow.ForeColor = GetCheckColor(Program.Character.oSavingThrows[0].Checks);
                oDexSavingThrow.ForeColor = GetCheckColor(Program.Character.oSavingThrows[1].Checks);
                oConSavingThrow.ForeColor = GetCheckColor(Program.Character.oSavingThrows[2].Checks);
                oIntSavingThrow.ForeColor = GetCheckColor(Program.Character.oSavingThrows[3].Checks);
                oWisSavingThrow.ForeColor = GetCheckColor(Program.Character.oSavingThrows[4].Checks);
                oChaSavingThrow.ForeColor = GetCheckColor(Program.Character.oSavingThrows[5].Checks);

                chkStrengthP.Checked = Program.Character.oSavingThrows[0].Proficiency;
                chkDexterityP.Checked = Program.Character.oSavingThrows[1].Proficiency;
                chkConstitutionP.Checked = Program.Character.oSavingThrows[2].Proficiency;
                chkIntelligenceP.Checked = Program.Character.oSavingThrows[3].Proficiency;
                chkWisdomP.Checked = Program.Character.oSavingThrows[4].Proficiency;
                chkCharismaP.Checked = Program.Character.oSavingThrows[5].Proficiency;

                oAthleticsSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[0]);
                oAcrobaticsSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[1]);
                oSleightSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[2]);
                oStealthSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[3]);
                oArcanaSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[4]);
                oHistorySkill.Text = GetSkillCheckStatus(Program.Character.oSkills[5]);
                oInvestigationSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[6]);
                oNatureSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[7]);
                oReligionSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[8]);
                oAnimalSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[9]);
                oInsightSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[10]);
                oMedicineSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[11]);
                oPerceptionSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[12]);
                oSurvivalSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[13]);
                oDeceptionSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[14]);
                oIntimidationSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[15]);
                oPerformanceSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[16]);
                oPersuasionSkill.Text = GetSkillCheckStatus(Program.Character.oSkills[17]);

                oAthleticsSkill.ForeColor = GetCheckColor(Program.Character.oSkills[0].Checks);
                oAcrobaticsSkill.ForeColor = GetCheckColor(Program.Character.oSkills[1].Checks);
                oSleightSkill.ForeColor = GetCheckColor(Program.Character.oSkills[2].Checks);
                oStealthSkill.ForeColor = GetCheckColor(Program.Character.oSkills[3].Checks);
                oArcanaSkill.ForeColor = GetCheckColor(Program.Character.oSkills[4].Checks);
                oHistorySkill.ForeColor = GetCheckColor(Program.Character.oSkills[5].Checks);
                oInvestigationSkill.ForeColor = GetCheckColor(Program.Character.oSkills[6].Checks);
                oNatureSkill.ForeColor = GetCheckColor(Program.Character.oSkills[7].Checks);
                oReligionSkill.ForeColor = GetCheckColor(Program.Character.oSkills[8].Checks);
                oAnimalSkill.ForeColor = GetCheckColor(Program.Character.oSkills[9].Checks);
                oInsightSkill.ForeColor = GetCheckColor(Program.Character.oSkills[10].Checks);
                oMedicineSkill.ForeColor = GetCheckColor(Program.Character.oSkills[11].Checks);
                oPerceptionSkill.ForeColor = GetCheckColor(Program.Character.oSkills[12].Checks);
                oSurvivalSkill.ForeColor = GetCheckColor(Program.Character.oSkills[13].Checks);
                oDeceptionSkill.ForeColor = GetCheckColor(Program.Character.oSkills[14].Checks);
                oIntimidationSkill.ForeColor = GetCheckColor(Program.Character.oSkills[15].Checks);
                oPerformanceSkill.ForeColor = GetCheckColor(Program.Character.oSkills[16].Checks);
                oPersuasionSkill.ForeColor = GetCheckColor(Program.Character.oSkills[17].Checks);

                chkAthleticsP.Checked = Program.Character.oSkills[0].Proficiency;
                chkAthleticsE.Checked = Program.Character.oSkills[0].Expertise;
                ckAcrobaticsP.Checked = Program.Character.oSkills[1].Proficiency;
                ckAcrobaticsE.Checked = Program.Character.oSkills[1].Expertise;
                chkSleightP.Checked = Program.Character.oSkills[2].Proficiency;
                chkSleightE.Checked = Program.Character.oSkills[2].Expertise;
                chkStealthP.Checked = Program.Character.oSkills[3].Proficiency;
                chkStealthE.Checked = Program.Character.oSkills[3].Expertise;
                chkArcanaP.Checked = Program.Character.oSkills[4].Proficiency;
                chkArcanaE.Checked = Program.Character.oSkills[4].Expertise;
                chkHistoryP.Checked = Program.Character.oSkills[5].Proficiency;
                chkHistoryE.Checked = Program.Character.oSkills[5].Expertise;
                chkInvestigationP.Checked = Program.Character.oSkills[6].Proficiency;
                chkInvestigationE.Checked = Program.Character.oSkills[6].Expertise;
                chkNatureP.Checked = Program.Character.oSkills[7].Proficiency;
                chkNatureE.Checked = Program.Character.oSkills[7].Expertise;
                chkReligionP.Checked = Program.Character.oSkills[8].Proficiency;
                chkReligionE.Checked = Program.Character.oSkills[8].Expertise;
                chkAnimalP.Checked = Program.Character.oSkills[9].Proficiency;
                chkAnimalE.Checked = Program.Character.oSkills[9].Expertise;
                chkInsightP.Checked = Program.Character.oSkills[10].Proficiency;
                chkInsightE.Checked = Program.Character.oSkills[10].Expertise;
                chkMedicineP.Checked = Program.Character.oSkills[11].Proficiency;
                chkMedicineE.Checked = Program.Character.oSkills[11].Expertise;
                chkPerceptionP.Checked = Program.Character.oSkills[12].Proficiency;
                chkPerceptionE.Checked = Program.Character.oSkills[12].Expertise;
                chkSurvivalP.Checked = Program.Character.oSkills[13].Proficiency;
                chkSurvivalE.Checked = Program.Character.oSkills[13].Expertise;
                chkDeceptionP.Checked = Program.Character.oSkills[14].Proficiency;
                chkDeceptionE.Checked = Program.Character.oSkills[14].Expertise;
                chkIntimidationP.Checked = Program.Character.oSkills[15].Proficiency;
                chkIntimidationE.Checked = Program.Character.oSkills[15].Expertise;
                chkPerformanceP.Checked = Program.Character.oSkills[16].Proficiency;
                chkPerformanceE.Checked = Program.Character.oSkills[16].Expertise;
                chkPersuasionP.Checked = Program.Character.oSkills[17].Proficiency;
                chkPersuasionE.Checked = Program.Character.oSkills[17].Expertise;

                oAC.Text = Program.Character.ArmorClass.AC + "";
                oArmorWorn.Text = Program.Character.ArmorClass.ArmorWorn;
                oArmorType.Text = Program.Character.ArmorClass.ArmorType;
                oArmorAC.Text = Program.Character.ArmorClass.ArmorAC + "";
                oArmorStealth.Text = Program.Character.ArmorClass.ArmorStealth;
                oShieldName.Text = Program.Character.ArmorClass.ShieldType;
                oShieldAC.Text = Program.Character.ArmorClass.ShieldAC + "";
                oMiscAC.Text = Program.Character.ArmorClass.MiscAC + "";
                oMagicAC.Text = Program.Character.ArmorClass.MagicAC + "";

                oTotalHitPoints.Text = "/" + Program.Character.HitPoints.Conditions.FormatHealth();
                oHitPoints.Text = (Program.Character.HitPoints.HP + Program.Character.HitPoints.TempHP) + "";
                oHitPoints.ForeColor = Program.Character.HitPoints.HitPointsColour;
                //oTotalHitPoints.ForeColor = Program.Character.HitPoints.HitPointsColour;
                oInitiative.Text = Program.Character.Initiative + "";
                oPassivePerception.Text = Program.Character.PassivePerception + "";

                oClassType.Text = Program.Character.ClassResource;
                oPool.Text = Program.Character.Pool + "";
                oSpent.Text = Program.Character.Spent + "";

                oPool.BackColor = Constants.TotalBoxColour(Program.Character.Pool, Program.Character.Spent);
                oSpent.ForeColor = Constants.TextColour(Program.Character.Pool, Program.Character.Spent);
                oSpent.BackColor = Constants.UsedBoxColour(Program.Character.Pool, Program.Character.Spent);

                oTotalD6.Text = Program.Character.HitPoints.D6 + "";
                oTotalD8.Text = Program.Character.HitPoints.D8 + "";
                oTotalD10.Text = Program.Character.HitPoints.D10 + "";
                oTotalD12.Text = Program.Character.HitPoints.D12 + "";
                oSpentD6.Text = Program.Character.HitPoints.SpentD6 + "";
                oSpentD8.Text = Program.Character.HitPoints.SpentD8 + "";
                oSpentD10.Text = Program.Character.HitPoints.SpentD10 + "";
                oSpentD12.Text = Program.Character.HitPoints.SpentD12 + "";

                oSpentD6.ForeColor = Constants.TextColour(Program.Character.HitPoints.D6, Program.Character.HitPoints.SpentD6);
                oSpentD6.BackColor = Constants.UsedBoxColour(Program.Character.HitPoints.D6, Program.Character.HitPoints.SpentD6);
                oSpentD8.ForeColor = Constants.TextColour(Program.Character.HitPoints.D8, Program.Character.HitPoints.SpentD8);
                oSpentD8.BackColor = Constants.UsedBoxColour(Program.Character.HitPoints.D8, Program.Character.HitPoints.SpentD8);
                oSpentD10.ForeColor = Constants.TextColour(Program.Character.HitPoints.D10, Program.Character.HitPoints.SpentD10);
                oSpentD10.BackColor = Constants.UsedBoxColour(Program.Character.HitPoints.D10, Program.Character.HitPoints.SpentD10);
                oSpentD12.ForeColor = Constants.TextColour(Program.Character.HitPoints.D12, Program.Character.HitPoints.SpentD12);
                oSpentD12.BackColor = Constants.UsedBoxColour(Program.Character.HitPoints.D12, Program.Character.HitPoints.SpentD12);

                oTotalD6.BackColor = Constants.TotalBoxColour(Program.Character.HitPoints.D6, Program.Character.HitPoints.SpentD6);
                oTotalD8.BackColor = Constants.TotalBoxColour(Program.Character.HitPoints.D8, Program.Character.HitPoints.SpentD8);
                oTotalD10.BackColor = Constants.TotalBoxColour(Program.Character.HitPoints.D10, Program.Character.HitPoints.SpentD10);
                oTotalD12.BackColor = Constants.TotalBoxColour(Program.Character.HitPoints.D12, Program.Character.HitPoints.SpentD12);

                foreach (DataGridViewRow row in oWeaponDataGrid.Rows)
                {
                    row.Cells[AttackBonus.Index].Value = Program.Character.GetBonus((string)row.Cells[Ability.Index].Value);
                }

                ResizeLabels();

                Drawing = false;
            }
        }

        /// =========================================
        /// MainPage_FormClosing()
        /// =========================================
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            string fileName = GetExitString(0);

            if (Program.Modified)
            {
                result = MessageBox.Show("Save changes " + fileName + "before exiting?", "Character Sheet", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (Program.FileLocation.Equals(NEW_FILE))
                        {
                            if (oSaveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                Program.FileLocation = oSaveFileDialog.FileName;
                            }
                            else
                            {
                                e.Cancel = true;
                                break;
                            }
                        }
                        Save();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                    default:
                        e.Cancel = true;
                        break;
                }
            }

            if (!e.Cancel)
            {
                Program.Termination();
            }
        }

        /// =========================================
        /// MainPage_KeyDown()
        /// =========================================
        private void MainPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Program.Typing && !CheckEasterEgg(e.KeyCode))
            {
                switch (e.KeyCode)
                {
                    //Save/Save As File
                    case Keys.S:
                        SaveFile(e.Control, e.Shift);
                        OpenSettings(e.Control);
                        break;
                    //Open File
                    case Keys.O:
                        OpenFile(e.Control);
                        break;
                    //New File
                    case Keys.N:
                        NewFile(e.Control);
                        break;
                    //Quit Program
                    case Keys.Q:
                        if (e.Control)
                            Close();
                        break;
                    //Full Screen
                    case Keys.F11:
                        FullScreen();
                        fullscreenToolStripMenuItem.Checked = Fullscreen;
                        break;
                    //----------------------------------------------------------------
                    //Open Properties
                    case Keys.C:
                        OpenProperties(e.Control);
                        break;
                    //Open Divide Loot
                    case Keys.D:
                        OpenDivideLoot(e.Control);
                        break;
                    //Open Roll Dice
                    case Keys.R:
                        OpenRollDice(e.Control);
                        break;
                    //Open Tables
                    case Keys.T:
                        OpenTables();
                        break;
                    //Long Rest
                    case Keys.L:
                        LongRest(e.Control);
                        break;
                    //Mute
                    case Keys.M:
                        Mute(!Program.Mute);
                        break;
                    //----------------------------------------------------------------
                    //Main Page
                    case Keys.D1:
                        if (!e.Control)
                            btnPrimaryPanel_Click(new object(), EventArgs.Empty);
                        break;
                    //Secondary Page
                    case Keys.D2:
                        if (!e.Control)
                            btnSecondaryPanel_Click(new object(), EventArgs.Empty);
                        break;
                    //Tertiary Page
                    case Keys.D3:
                        if (!e.Control)
                            btnTertiaryPanel_Click(new object(), EventArgs.Empty);
                        break;
                    // Campain Page
                    case Keys.D4:
                        if (!e.Control)
                            btnCampainPanel_Click(new object(), EventArgs.Empty);
                        break;
                }
            }
        }

        /// =========================================
        /// MainPage_Resize()
        /// =========================================
        private void MainPage_Resize(object sender, EventArgs e)
        {
            FormatInputBoxes();
            SetTabPanel();
            MenuPanel.Width = Size.Width;
            oSavePanel.Location = new Point(Size.Width - SAVE_X_OFFSET, Size.Height - SAVE_Y_OFFSET);

            if (Ratio != float.PositiveInfinity)
            {
                oSecondaryPage.ResizeText();
                oTertiaryPage.ResizeText();
                oCampainPage.ResizeText();

                ResizeLabels();

                //Resize Weapon/Ammo Headers
                oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font.FontFamily, HeaderSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font.Style);
                oAmmoGridView.ColumnHeadersDefaultCellStyle.Font = new Font(oAmmoGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, HeaderSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), oAmmoGridView.ColumnHeadersDefaultCellStyle.Font.Style);

                //Resize Weapon/Ammo Rows
                oWeaponDataGrid.DefaultCellStyle.Font = new Font(oWeaponDataGrid.DefaultCellStyle.Font.FontFamily, RowSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), oWeaponDataGrid.Font.Style);
                oAmmoGridView.DefaultCellStyle.Font = new Font(oAmmoGridView.DefaultCellStyle.Font.FontFamily, RowSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), oAmmoGridView.Font.Style);

                //Resize checkboxes
                checkBox1.Font = new Font(checkBox1.Font.FontFamily, CheckBoxSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), checkBox1.Font.Style);
                checkBox2.Font = new Font(checkBox2.Font.FontFamily, CheckBoxSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), checkBox2.Font.Style);
                checkBox3.Font = new Font(checkBox3.Font.FontFamily, CheckBoxSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), checkBox3.Font.Style);
                checkBox4.Font = new Font(checkBox4.Font.FontFamily, CheckBoxSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), checkBox4.Font.Style);
                checkBox5.Font = new Font(checkBox5.Font.FontFamily, CheckBoxSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), checkBox5.Font.Style);
                checkBox6.Font = new Font(checkBox6.Font.FontFamily, CheckBoxSize * (Ratio == 1 ? Ratio : (Ratio / SIZE_MOD)), checkBox6.Font.Style);
            }
        }

        /// =========================================
        /// MainPage_MouseMove()
        /// =========================================
        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            //Menu bar
            if (e.Y < MENU_HEIGHT && !MenuHiding && MenuHidden)
            {
                oMenuTimer.Start();
                return;
            }
            else if (e.Y >= MENU_HEIGHT && !MenuHidden && !MenuHiding)
            {
                oMenuTimer.Start();
                return;
            }

            //Sidebar
            if (e.X < TAB_WIDTH && !TabHiding && TabHidden)
            {
                oTabTimer.Start();
                return;
            }
            else if (e.X >= TAB_WIDTH && !TabHiding && !TabHidden)
            {
                oTabTimer.Start();
                return;
            }
        }

        /// =========================================
        /// oMenuTimer_Tick()
        /// =========================================
        private void oMenuTimer_Tick(object sender, EventArgs e)
        {
            //Show menu
            if (MenuHidden)
            {
                MenuPanel.Height += MENU_RATE;
                if (MenuPanel.Height >= MENU_HEIGHT)
                {
                    MenuHidden = false;
                    oMenuTimer.Stop();
                }
            }
            //Hide menu
            else
            {
                MenuPanel.Height -= MENU_RATE;
                if (MenuPanel.Height <= 0)
                {
                    MenuHidden = true;
                    oMenuTimer.Stop();
                }
            }
        }

        /// =========================================
        /// oTabTimer_Tick()
        /// =========================================
        private void oTabTimer_Tick(object sender, EventArgs e)
        {
            //Show tab
            if (TabHidden)
            {
                oTabPanel.Width += TAB_RATE;
                if (oTabPanel.Width >= TAB_WIDTH)
                {
                    TabHidden = false;
                    oTabTimer.Stop();
                }
            }
            //Hide tab
            else
            {
                oTabPanel.Width -= TAB_RATE;
                if (oTabPanel.Width <= 0)
                {
                    TabHidden = true;
                    oTabTimer.Stop();
                }
            }
        }

        /// =========================================
        /// oAutosaveTimer_Tick()
        /// =========================================
        private void oAutosaveTimer_Tick(object sender, EventArgs e)
        {
            if (AutosaveTick >= Settings.AutosaveInterval)
            {
                AutosaveTick = 1;

                if (!Program.FileLocation.Equals(NEW_FILE))
                {
                    Save();
                    ShowSavePopup();
                }
            }

            AutosaveTick++;
        }

        /// =========================================
        /// mouseFilter_FormClicked()
        /// =========================================
        void mouseFilter_FormClicked(object sender, EventArgs e)
        {
            //Make sure input is not on DataGridView
            if (!InsideDataGrid())
            {
                //Move focus to page default
                switch (GetCurrentTab())
                {
                    default:
                    case Pages.Primary:
                        oDefaultFocus.Focus();
                        break;
                    case Pages.Secondary:
                        oSecondaryPage.DefaultFocus();
                        break;
                    case Pages.Tertiary:
                        oTertiaryPage.DefaultFocus();
                        break;
                    case Pages.Campain:
                        oCampainPage.DefaultFocus();
                        break;
                }

                Program.Typing = false;
            }
        }

        #endregion

        #region Input Events

        /// =========================================
        /// oInputHP_KeyPress()
        /// =========================================
        private void oInputHP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                int hp, temp, damage;

                e.SuppressKeyPress = true;

                if (int.TryParse(oInputHP.Text, out hp))
                {
                    damage = hp;

                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    //Display death message
                    if (Program.Character.IsDead(damage))
                    {
                        MessageBox.Show(Program.Character.Name + " has died.", "Character Sheet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //temp hit points
                    if (hp < 0)
                    {
                        temp = hp;
                        hp += Program.Character.HitPoints.TempHP;
                        Program.Character.HitPoints.TempHP = Math.Max(Program.Character.HitPoints.TempHP + temp, 0);
                    }

                    //regular hit points
                    hp = hp + Program.Character.HitPoints.HP;
                    hp = Math.Max(Math.Min(hp, Program.Character.HitPoints.MaxHP), 0);
                    oHitPoints.Text = hp + "";
                    oInputHP.Text = "";
                    Program.Character.HitPoints.HP = hp;
                }
                else
                {
                    oInputHP.Text = "";
                }
            }
        }

        /// =========================================
        /// oPanelHP_Click()
        /// =========================================
        private void oPanelHP_Click(object sender, EventArgs e)
        {
            oInputHP.Select();
            oInputHP.SelectionStart = oInputHP.Text.Length;
            oInputHP.SelectionLength = 0;
        }

        /// =========================================
        /// oInputHP_Enter()
        /// =========================================
        private void oInputHP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputHP_Leave()
        /// =========================================
        private void oInputHP_Leave(object sender, EventArgs e)
        {
            oInputHP.Text = "";
            Program.Typing = false;
        }

        /// =========================================
        /// oInputHP_TextChanged()
        /// =========================================
        private void oInputHP_TextChanged(object sender, EventArgs e)
        {
            Program.Modified = true;
            Invalidate();
        }

        #endregion

        #region Button Events

        /// =========================================
        /// btnPrimaryPanel_Click()
        /// =========================================
        private void btnPrimaryPanel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            btnPrimary.BackColor = DarkBlue;
            btnSecondary.BackColor = DarkGrey;
            btnTertiary.BackColor = DarkGrey;
            btnCampain.BackColor = DarkGrey;

            oSecondaryPage.Visible = false;
            oTertiaryPage.Visible = false;
            oCampainPage.Visible = false;
            oPrimaryTable.Visible = true;
            oDefaultFocus.Focus();

            FormatInputBoxes();
        }

        /// =========================================
        /// btnSecondaryPanel_Click()
        /// =========================================
        private void btnSecondaryPanel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            btnPrimary.BackColor = DarkGrey;
            btnSecondary.BackColor = DarkBlue;
            btnTertiary.BackColor = DarkGrey;
            btnCampain.BackColor = DarkGrey;

            oPrimaryTable.Visible = false;
            oTertiaryPage.Visible = false;
            oCampainPage.Visible = false;
            oSecondaryPage.Visible = true;
            oSecondaryPage.DefaultFocus();

            oSecondaryPage.FormatInputBoxes();
        }

        /// =========================================
        /// btnTertiaryPanel_Click()
        /// =========================================
        private void btnTertiaryPanel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            btnPrimary.BackColor = DarkGrey;
            btnSecondary.BackColor = DarkGrey;
            btnTertiary.BackColor = DarkBlue;
            btnCampain.BackColor = DarkGrey;

            oPrimaryTable.Visible = false;
            oSecondaryPage.Visible = false;
            oCampainPage.Visible = false;
            oTertiaryPage.Visible = true;
            oTertiaryPage.DefaultFocus();
        }

        /// =========================================
        /// btnCampainPanel_Click()
        /// =========================================
        private void btnCampainPanel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            btnPrimary.BackColor = DarkGrey;
            btnSecondary.BackColor = DarkGrey;
            btnTertiary.BackColor = DarkGrey;
            btnCampain.BackColor = DarkBlue;

            oPrimaryTable.Visible = false;
            oSecondaryPage.Visible = false;
            oTertiaryPage.Visible = false;
            oCampainPage.Visible = true;
            oCampainPage.DefaultFocus();
        }

        #endregion

        #region Hit Dice Events

        /// =========================================
        /// oSpent_DoubleClick()
        /// =========================================
        private void oSpent_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.Spent + 1) <= Program.Character.Pool)
            {
                Program.Character.Spent++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.Spent == Program.Character.Pool)
                {
                    oSpent.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oSpent_MouseEnter()
        /// =========================================
        private void oSpent_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.Spent + 1) <= Program.Character.Pool)
            {
                oSpent.Cursor = Cursors.Hand;
            }
            else
                oSpent.Cursor = Cursors.Default;
        }

        /// =========================================
        /// oSpentD6_DoubleClick()
        /// =========================================
        private void oSpentD6_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD6 + 1) <= Program.Character.HitPoints.D6)
            {
                Program.Character.HitPoints.SpentD6++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.HitPoints.SpentD6 == Program.Character.HitPoints.D6)
                {
                    oSpentD6.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oSpentD8_DoubleClick()
        /// =========================================
        private void oSpentD8_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD8 + 1) <= Program.Character.HitPoints.D8)
            {
                Program.Character.HitPoints.SpentD8++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.HitPoints.SpentD8 == Program.Character.HitPoints.D8)
                {
                    oSpentD8.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oSpentD10_DoubleClick()
        /// =========================================
        private void oSpentD10_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD10 + 1) <= Program.Character.HitPoints.D10)
            {
                Program.Character.HitPoints.SpentD10++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.HitPoints.SpentD10 == Program.Character.HitPoints.D10)
                {
                    oSpentD10.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oSpentD12_DoubleClick()
        /// =========================================
        private void oSpentD12_DoubleClick(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD12 + 1) <= Program.Character.HitPoints.D12)
            {
                Program.Character.HitPoints.SpentD12++;
                Program.Modified = true;
                Sounds.ButtonClick();

                if (Program.Character.HitPoints.SpentD12 == Program.Character.HitPoints.D12)
                {
                    oSpentD12.Cursor = Cursors.Default;
                }

                Invalidate();
            }
        }

        /// =========================================
        /// oSpentD6_MouseEnter()
        /// =========================================
        private void oSpentD6_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD6 + 1) <= Program.Character.HitPoints.D6)
            {
                oSpentD6.Cursor = Cursors.Hand;
            }
            else
                oSpentD6.Cursor = Cursors.Default;
        }

        /// =========================================
        /// oSpentD8_MouseEnter()
        /// =========================================
        private void oSpentD8_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD8 + 1) <= Program.Character.HitPoints.D8)
            {
                oSpentD8.Cursor = Cursors.Hand;
            }
            else
                oSpentD8.Cursor = Cursors.Default;
        }

        /// =========================================
        /// oSpentD10_MouseEnter()
        /// =========================================
        private void oSpentD10_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD10 + 1) <= Program.Character.HitPoints.D10)
            {
                oSpentD10.Cursor = Cursors.Hand;
            }
            else
                oSpentD10.Cursor = Cursors.Default;
        }

        /// =========================================
        /// oSpentD12_MouseEnter()
        /// =========================================
        private void oSpentD12_MouseEnter(object sender, EventArgs e)
        {
            if ((Program.Character.HitPoints.SpentD12 + 1) <= Program.Character.HitPoints.D12)
            {
                oSpentD12.Cursor = Cursors.Hand;
            }
            else
                oSpentD12.Cursor = Cursors.Default;
        }

        #endregion

        #region DataGrid Events

        // =========================================
        /// oWeaponDataGrid_Sorted()
        /// =========================================
        private void oWeaponDataGrid_Sorted(object sender, EventArgs e)
        {
            int index;
            Guid rowID;
            Weapon item;

            // Sort each item
            for (int i = 0; i < oWeaponDataGrid.Rows.Count; i++)
            {
                rowID = (Guid)oWeaponDataGrid.Rows[i].Tag;

                // Check if already in correct position 
                if (!rowID.Equals(Program.Character.oWeapons[i].ID))
                {
                    index = Program.Character.GetWeaponIndex(rowID);
                    item = Program.Character.oWeapons[index];

                    Program.Character.oWeapons.RemoveAt(index);
                    Program.Character.oWeapons.Insert(index, Program.Character.oWeapons[i]);

                    Program.Character.oWeapons.RemoveAt(i);
                    Program.Character.oWeapons.Insert(i, item);
                }
            }
        }

        /// =========================================
        /// oAmmoGridView_Sorted()
        /// =========================================
        private void oAmmoGridView_Sorted(object sender, EventArgs e)
        {
            int index;
            Guid rowID;
            Ammunition item;

            // Sort each item
            for (int i = 0; i < oAmmoGridView.Rows.Count; i++)
            {
                rowID = (Guid)oAmmoGridView.Rows[i].Tag;

                // Check if already in correct position 
                if (!rowID.Equals(Program.Character.oAmmo[i].ID))
                {
                    index = Program.Character.GetAmmoIndex(rowID);
                    item = Program.Character.oAmmo[index];

                    Program.Character.oAmmo.RemoveAt(index);
                    Program.Character.oAmmo.Insert(index, Program.Character.oAmmo[i]);

                    Program.Character.oAmmo.RemoveAt(i);
                    Program.Character.oAmmo.Insert(i, item);
                }
            }
        }

        /// =========================================
        /// oWeaponDataGrid_CellValueChanged()
        /// =========================================
        private void oWeaponDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!Drawing)
            {
                Program.Modified = true;
            }

            if (e.ColumnIndex == Ability.Index && !Program.Loading)
            {
                oWeaponDataGrid.Rows[e.RowIndex].Cells[AttackBonus.Index].Value = Program.Character.GetBonus((string)oWeaponDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        /// =========================================
        /// oWeaponDataGrid_CellMouseClick()
        /// =========================================
        private void oWeaponDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oWeaponDataGrid.RowCount)
            {
                Rectangle rect = oWeaponDataGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oWeaponContextMenu.Show(oWeaponDataGrid, new Point(rect.X + e.X + OFFSET, rect.Y + e.Y + OFFSET));
            }
        }

        /// =========================================
        /// deleteRowToolStripMenuItem_Click()
        /// =========================================
        private void weaponEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            oTablePage.ShowPane(Tables.Weapons, Program.Character.oWeapons[Row]);
            FillWeapons();
        }

        /// =========================================
        /// deleteRowToolStripMenuItem_Click()
        /// =========================================
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.Character.RemoveWeaponItem((Guid)oWeaponDataGrid.Rows[Row].Tag);
            oWeaponDataGrid.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oAmmoGridView_CellMouseClick()
        /// =========================================
        private void oAmmoGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oAmmoGridView.RowCount)
            {
                Rectangle rect = oAmmoGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oAmmoContextMenu.Show(oAmmoGridView, new Point(rect.X + e.X + OFFSET, rect.Y + e.Y + OFFSET));
            }
        }

        /// =========================================
        /// addAmmoToolStripMenuItem_Click()
        /// =========================================
        private void addAmmoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            oTablePage.ShowPane(Tables.Ammunition, Program.Character.oAmmo[Row]);
            FillAmmo();
        }

        /// =========================================
        /// deleteRowToolStripMenuItem1_Click()
        /// =========================================
        private void deleteRowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.Modified = true;

            Program.Character.RemoveAmmoItem((Guid)oAmmoGridView.Rows[Row].Tag);
            oAmmoGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oAmmoGridView_CellEnter()
        /// =========================================
        private void oAmmoGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = true;

            //Open dropdown menu when clicked
            if (e.ColumnIndex == AmmoDmgType.Index)
            {
                oAmmoGridView.CurrentCell = oAmmoGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                oAmmoGridView.BeginEdit(true);
            }
        }

        /// =========================================
        /// oAmmoGridView_CellContentClick()
        /// ========================================= 
        private void oAmmoGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == oIncrement.Index || e.ColumnIndex == oDecrement.Index)
            {
                const int MIN = 0, MAX = 100;

                Guid ID = (Guid)oAmmoGridView.Rows[e.RowIndex].Tag;
                int dataGridUsed = int.Parse((string)oAmmoGridView.Rows[e.RowIndex].Cells[Used.Index].Value);
                int quantity = int.Parse((string)oAmmoGridView.Rows[e.RowIndex].Cells[Qty.Index].Value);

                if (e.ColumnIndex == oIncrement.Index && dataGridUsed < MAX && dataGridUsed < quantity)
                {
                    dataGridUsed++;
                    Program.Character.IncrementAmmoQuantity(ID);
                }
                else if (e.ColumnIndex == oDecrement.Index && dataGridUsed > MIN)
                {
                    dataGridUsed--;
                    Program.Character.DecrementAmmoQuantity(ID);
                }

                oAmmoGridView.Rows[e.RowIndex].Cells[Used.Index].Value = dataGridUsed.ToString();
            }
        }

        /// =========================================
        /// oAmmoGridView_CellLeave()
        /// =========================================
        private void oAmmoGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = false;
        }

        /// =========================================
        /// oAmmoGridView_RowsAdded()
        /// =========================================
        private void oAmmoGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Program.Modified = true;
        }

        /// =========================================
        /// oWeaponDataGrid_RowsAdded()
        /// =========================================
        private void oWeaponDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Program.Modified = true;
        }

        /// =========================================
        /// weaponEditToolStripMenuItem_MouseEnter()
        /// =========================================
        private void weaponEditToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            weaponEditToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// weaponEditToolStripMenuItem_MouseLeave()
        /// =========================================
        private void weaponEditToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            weaponEditToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// weaponDeleteToolStripMenuItem_MouseEnter()
        /// =========================================
        private void weaponDeleteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            weaponDeleteToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// weaponDeleteToolStripMenuItem_MouseLeave()
        /// =========================================
        private void weaponDeleteToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            weaponDeleteToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// addAmmoToolStripMenuItem_MouseEnter()
        /// =========================================
        private void addAmmoToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            editAmmoToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addAmmoToolStripMenuItem_MouseLeave()
        /// =========================================
        private void addAmmoToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            editAmmoToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// ammoDeleteRowToolStripMenuItem_MouseEnter()
        /// =========================================
        private void ammoDeleteRowToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            ammoDeleteRowToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// ammoDeleteRowToolStripMenuItem_MouseLeave()
        /// =========================================
        private void ammoDeleteRowToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            ammoDeleteRowToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// oAmmoGridView_MouseMove()
        /// ========================================= 
        private void oAmmoGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oAmmoGridView.DoDragDrop(oAmmoGridView.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oAmmoGridView_MouseDown()
        /// ========================================= 
        private void oAmmoGridView_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oAmmoGridView.HitTest(e.X, e.Y).RowIndex;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (rowIndexFromMouseDown != -1)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                    }
                    else
                    {
                        dragBoxFromMouseDown = Rectangle.Empty;
                    }
                    break;
                case MouseButtons.Right:
                    if (rowIndexFromMouseDown == -1)
                        oAddAmmoContextMenu.Show(oAmmoGridView, new Point(e.X + OFFSET, e.Y + OFFSET));
                    break;
            }
        }

        /// =========================================
        /// oAmmoGridView_DragOver()
        /// ========================================= 
        private void oAmmoGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oAmmoGridView_DragDrop()
        /// ========================================= 
        private void oAmmoGridView_DragDrop(object sender, DragEventArgs e)
        {
            Ammunition item;

            if (oAmmoGridView.Rows.Count > 1)
            {
                Point clientPoint = oAmmoGridView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oAmmoGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow? rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oAmmoGridView.Rows.Count)
                        rowIndexOfItemUnderMouseToDrop = oAmmoGridView.Rows.Count - 1;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    // Move list item
                    oAmmoGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oAmmoGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                    // Move data item
                    item = Program.Character.oAmmo[rowIndexFromMouseDown];
                    Program.Character.oAmmo.RemoveAt(rowIndexFromMouseDown);
                    Program.Character.oAmmo.Insert(rowIndexOfItemUnderMouseToDrop, item);
                }
            }
        }

        /// =========================================
        /// oWeaponDataGrid_MouseMove()
        /// ========================================= 
        private void oWeaponDataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffects = oWeaponDataGrid.DoDragDrop(oWeaponDataGrid.Rows[rowIndexFromMouseDown], DragDropEffects.Move);
                }
            }
        }

        /// =========================================
        /// oWeaponDataGrid_MouseDown()
        /// ========================================= 
        private void oWeaponDataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = oWeaponDataGrid.HitTest(e.X, e.Y).RowIndex;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (rowIndexFromMouseDown != -1)
                    {
                        Size dragSize = SystemInformation.DragSize;
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                    }
                    else
                    {
                        dragBoxFromMouseDown = Rectangle.Empty;
                    }
                    break;
                case MouseButtons.Right:
                    if (rowIndexFromMouseDown == -1)
                        oAddWeaponContextMenu.Show(oWeaponDataGrid, new Point(e.X + OFFSET, e.Y + OFFSET));
                    break;
            }
        }

        /// =========================================
        /// oWeaponDataGrid_DragOver()
        /// ========================================= 
        private void oWeaponDataGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// =========================================
        /// oWeaponDataGrid_DragDrop()
        /// ========================================= 
        private void oWeaponDataGrid_DragDrop(object sender, DragEventArgs e)
        {
            Weapon item;

            if (oWeaponDataGrid.Rows.Count > 1)
            {
                Point clientPoint = oWeaponDataGrid.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oWeaponDataGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow? rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oWeaponDataGrid.Rows.Count)
                        rowIndexOfItemUnderMouseToDrop = oWeaponDataGrid.Rows.Count - 1;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    // Move list item
                    oWeaponDataGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                    oWeaponDataGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);

                    // Move data item
                    item = Program.Character.oWeapons[rowIndexFromMouseDown];
                    Program.Character.oWeapons.RemoveAt(rowIndexFromMouseDown);
                    Program.Character.oWeapons.Insert(rowIndexOfItemUnderMouseToDrop, item);
                }
            }
        }

        /// =========================================
        /// addWeaponToolStripMenuItem_Click()
        /// ========================================= 
        private void addWeaponToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oTablePage.ShowPane(Tables.Weapons);
            FillWeapons();
        }

        /// =========================================
        /// addWeaponToolStripMenuItem_MouseEnter()
        /// ========================================= 
        private void addWeaponToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            addWeaponToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addWeaponToolStripMenuItem_MouseLeave()
        /// ========================================= 
        private void addWeaponToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            addWeaponToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// addAmmoToolStripMenuItem_Click_1()
        /// ========================================= 
        private void addAmmoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            oTablePage.ShowPane(Tables.Ammunition);
            FillAmmo();
        }

        /// =========================================
        /// addAmmoToolStripMenuItem_MouseEnter_1()
        /// ========================================= 
        private void addAmmoToolStripMenuItem_MouseEnter_1(object sender, EventArgs e)
        {
            addAmmoToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// addAmmoToolStripMenuItem_MouseLeave_1()
        /// ========================================= 
        private void addAmmoToolStripMenuItem_MouseLeave_1(object sender, EventArgs e)
        {
            addAmmoToolStripMenuItem.ForeColor = Color.White;
        }

        #endregion

        #region Checkbox Events

        /// =========================================
        /// chkStrengthP_CheckedChanged()
        /// =========================================
        private void chkStrengthP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[(int)Saves.Strength].Proficiency = chkStrengthP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkDexterityP_CheckedChanged()
        /// =========================================
        private void chkDexterityP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[(int)Saves.Dexterity].Proficiency = chkDexterityP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkConstitutionP_CheckedChanged()
        /// =========================================
        private void chkConstitutionP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[(int)Saves.Constitution].Proficiency = chkConstitutionP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkIntelligenceP_CheckedChanged()
        /// =========================================
        private void chkIntelligenceP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[(int)Saves.Intelligence].Proficiency = chkIntelligenceP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkWisdomP_CheckedChanged()
        /// =========================================
        private void chkWisdomP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[(int)Saves.Wisdom].Proficiency = chkWisdomP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkCharismaP_CheckedChanged()
        /// =========================================
        private void chkCharismaP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[(int)Saves.Charisma].Proficiency = chkCharismaP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkAthleticsP_CheckedChanged()
        /// =========================================
        private void chkAthleticsP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Athletics].Proficiency = chkAthleticsP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkAthleticsE_CheckedChanged()
        /// =========================================
        private void chkAthleticsE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Athletics].Expertise = chkAthleticsE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// ckAcrobaticsP_CheckedChanged()
        /// =========================================
        private void ckAcrobaticsP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Acrobatics].Proficiency = ckAcrobaticsP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// ckAcrobaticsE_CheckedChanged()
        /// =========================================
        private void ckAcrobaticsE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Acrobatics].Expertise = ckAcrobaticsE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkSleightP_CheckedChanged()
        /// =========================================
        private void chkSleightP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.SleightOfHand].Proficiency = chkSleightP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkSleightE_CheckedChanged()
        /// =========================================
        private void chkSleightE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.SleightOfHand].Expertise = chkSleightE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkStealthP_CheckedChanged()
        /// =========================================
        private void chkStealthP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Stealth].Proficiency = chkStealthP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkStealthE_CheckedChanged()
        /// =========================================
        private void chkStealthE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Stealth].Expertise = chkStealthE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkArcanaP_CheckedChanged()
        /// =========================================
        private void chkArcanaP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Arcana].Proficiency = chkArcanaP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkArcanaE_CheckedChanged()
        /// =========================================
        private void chkArcanaE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Arcana].Expertise = chkArcanaE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkHistoryP_CheckedChanged()
        /// =========================================
        private void chkHistoryP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.History].Proficiency = chkHistoryP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkHistoryE_CheckedChanged()
        /// =========================================
        private void chkHistoryE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.History].Expertise = chkHistoryE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkInvestigationP_CheckedChanged()
        /// =========================================
        private void chkInvestigationP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Investigation].Proficiency = chkInvestigationP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkInvestigationE_CheckedChanged()
        /// =========================================
        private void chkInvestigationE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Investigation].Expertise = chkInvestigationE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkNatureP_CheckedChanged()
        /// =========================================
        private void chkNatureP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Nature].Proficiency = chkNatureP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkNatureE_CheckedChanged()
        /// =========================================
        private void chkNatureE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Nature].Expertise = chkNatureE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkReligionP_CheckedChanged()
        /// =========================================
        private void chkReligionP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Religion].Proficiency = chkReligionP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkReligionE_CheckedChanged()
        /// =========================================
        private void chkReligionE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Religion].Expertise = chkReligionE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkAnimalP_CheckedChanged()
        /// =========================================
        private void chkAnimalP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.AnimalHandling].Proficiency = chkAnimalP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkAnimalE_CheckedChanged()
        /// =========================================
        private void chkAnimalE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.AnimalHandling].Expertise = chkAnimalE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkInsightP_CheckedChanged()
        /// =========================================
        private void chkInsightP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Insight].Proficiency = chkInsightP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkInsightE_CheckedChanged()
        /// =========================================
        private void chkInsightE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Insight].Expertise = chkInsightE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkMedicineP_CheckedChanged()
        /// =========================================
        private void chkMedicineP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Medicine].Proficiency = chkMedicineP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkMedicineE_CheckedChanged()
        /// =========================================
        private void chkMedicineE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Medicine].Expertise = chkMedicineE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkPerceptionP_CheckedChanged()
        /// =========================================
        private void chkPerceptionP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Perception].Proficiency = chkPerceptionP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkPerceptionE_CheckedChanged()
        /// =========================================
        private void chkPerceptionE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Perception].Expertise = chkPerceptionE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkSurvivalP_CheckedChanged()
        /// =========================================
        private void chkSurvivalP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Survival].Proficiency = chkSurvivalP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkSurvivalE_CheckedChanged()
        /// =========================================
        private void chkSurvivalE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Survival].Expertise = chkSurvivalE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkDeceptionP_CheckedChanged()
        /// =========================================
        private void chkDeceptionP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Deception].Proficiency = chkDeceptionP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkDeceptionE_CheckedChanged()
        /// =========================================
        private void chkDeceptionE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Deception].Expertise = chkDeceptionE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkIntimidationP_CheckedChanged()
        /// =========================================
        private void chkIntimidationP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Intimidation].Proficiency = chkIntimidationP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkIntimidationE_CheckedChanged()
        /// =========================================
        private void chkIntimidationE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Intimidation].Expertise = chkIntimidationE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkPerformanceP_CheckedChanged()
        /// =========================================
        private void chkPerformanceP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Performance].Proficiency = chkPerformanceP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkPerformanceE_CheckedChanged()
        /// =========================================
        private void chkPerformanceE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Performance].Expertise = chkPerformanceE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkPersuasionP_CheckedChanged()
        /// =========================================
        private void chkPersuasionP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Persuasion].Proficiency = chkPersuasionP.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        /// =========================================
        /// chkPersuasionE_CheckedChanged()
        /// =========================================
        private void chkPersuasionE_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSkills[(int)Skills.Persuasion].Expertise = chkPersuasionE.Checked;
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
            Invalidate();
        }

        #endregion

        #region Tool Strip Events

        /// =========================================
        /// aboutToolStripMenuItem_Click()
        /// =========================================
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            MessageBox.Show("Character Sheet Current Version: " + version.Major + "." + version.Minor + "." + version.Build, 
                            "About Character Sheet", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);

            Sounds.ButtonClick();
        }

        /// =========================================
        /// helpToolStripMenuItem_DropDownOpened()
        /// =========================================
        private void helpToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            helpToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// helpToolStripMenuItem_DropDownClosed()
        /// =========================================
        private void helpToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            helpToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// aboutToolStripMenuItem_MouseEnter()
        /// =========================================
        private void aboutToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            aboutToolStripMenuItem.ForeColor = Color.Black;
            aboutToolStripMenuItem.Image = Properties.Resources.about_selected_128;
        }

        /// =========================================
        /// aboutToolStripMenuItem_MouseLeave()
        /// =========================================
        private void aboutToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            aboutToolStripMenuItem.ForeColor = Color.White;
            aboutToolStripMenuItem.Image = Properties.Resources.about_128;
        }

        /// =========================================
        /// newToolStripMenuItem_Click()
        /// =========================================
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            NewFile(true);
        }

        /// =========================================
        /// openToolStripMenuItem_Click()
        /// =========================================
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            OpenFile(true);
        }

        /// =========================================
        /// saveToolStripMenuItem_Click()
        /// =========================================
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            SaveFile(true, false);
        }

        /// =========================================
        /// saveAsToolStripMenuItem_Click()
        /// =========================================
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            SaveFile(true, true);
        }

        /// =========================================
        /// propertiesToolStripMenuItem_Click()
        /// =========================================
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            oPropertyPage.ShowPane();
            FillConditions();
            InvalidateAll();
        }

        /// =========================================
        /// settingsToolStripMenuItem_Click()
        /// =========================================
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            OpenSettings(false);
        }

        /// =========================================
        /// tablesToolStripMenuItem_Click()
        /// =========================================
        private void tablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            OpenTables();
        }

        /// =========================================
        /// muteToolStripMenuItem_Click()
        /// =========================================
        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Mute = !Program.Mute;
        }

        /// =========================================
        /// divideLootToolStripMenuItem_Click()
        /// =========================================
        private void divideLootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            oDivideLootPage.ShowPane();
        }

        /// =========================================
        /// diceRollerToolStripMenuItem_Click()
        /// =========================================
        private void diceRollerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            oDiceRollerPage.ShowPane();
        }

        /// =========================================
        /// longRestToolStripMenuItem_Click()
        /// =========================================
        private void longRestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tmp;

            Sounds.ButtonClick();

            Program.Character.HitPoints.HP = Program.Character.HitPoints.MaxHP;
            Program.Modified = true;

            tmp = Program.Character.HitPoints.SpentD6;
            tmp -= Math.Max(Program.Character.HitPoints.D6 / 2, 1);
            Program.Character.HitPoints.SpentD6 = Math.Max(tmp, 0);

            tmp = Program.Character.HitPoints.SpentD8;
            tmp -= Math.Max(Program.Character.HitPoints.D8 / 2, 1);
            Program.Character.HitPoints.SpentD8 = Math.Max(tmp, 0);

            tmp = Program.Character.HitPoints.SpentD10;
            tmp -= Math.Max(Program.Character.HitPoints.D10 / 2, 1);
            Program.Character.HitPoints.SpentD10 = Math.Max(tmp, 0);

            tmp = Program.Character.HitPoints.SpentD12;
            tmp -= Math.Max(Program.Character.HitPoints.D12 / 2, 1);
            Program.Character.HitPoints.SpentD12 = Math.Max(tmp, 0);

            tmp = Program.Character.Spent;
            tmp -= Math.Max(Program.Character.Pool / 2, 1);
            Program.Character.Spent = Math.Max(tmp, 0);

            Program.Character.Spellcasting.ResetSpellSlots();

            InvalidateAll();
        }

        /// =========================================
        /// fullscreenToolStripMenuItem_Click()
        /// =========================================
        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            FullScreen();
        }

        /// =========================================
        /// exitToolStripMenuItem_Click()
        /// =========================================
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            Close();
        }

        /// =========================================
        /// fileToolStripMenuItem_DropDownOpened()
        /// =========================================
        private void fileToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            fileToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// fileToolStripMenuItem_DropDownClosed()
        /// =========================================
        private void fileToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            fileToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// editToolStripMenuItem_DropDownOpened()
        /// =========================================
        private void editToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            editToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// editToolStripMenuItem_DropDownClosed()
        /// =========================================
        private void editToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            editToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// viewToolStripMenuItem_DropDownOpened()
        /// =========================================
        private void viewToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            viewToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// viewToolStripMenuItem_DropDownClosed()
        /// =========================================
        private void viewToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            viewToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// toolsToolStripMenuItem_DropDownOpened()
        /// =========================================
        private void toolsToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            toolsToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// toolsToolStripMenuItem_DropDownClosed()
        /// =========================================
        private void toolsToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            toolsToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// fileToolStripMenuItem_Click()
        /// =========================================
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// editToolStripMenuItem_Click()
        /// =========================================
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// viewToolStripMenuItem_Click()
        /// =========================================
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// toolsToolStripMenuItem_Click()
        /// =========================================
        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
        }

        /// =========================================
        /// propertiesToolStripMenuItem_MouseEnter()
        /// =========================================
        private void propertiesToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            propertiesToolStripMenuItem.ForeColor = Color.Black;
            propertiesToolStripMenuItem.Image = Properties.Resources.properties_selected_128;
        }

        /// =========================================
        /// propertiesToolStripMenuItem_MouseLeave()
        /// =========================================
        private void propertiesToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            propertiesToolStripMenuItem.ForeColor = Color.White;
            propertiesToolStripMenuItem.Image = Properties.Resources.properties_128;
        }

        /// =========================================
        /// settingsToolStripMenuItem_MouseEnter()
        /// =========================================
        private void settingsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            settingsToolStripMenuItem.ForeColor = Color.Black;
            settingsToolStripMenuItem.Image = Properties.Resources.settings_selected_128;
        }

        /// =========================================
        /// settingsToolStripMenuItem_MouseLeave()
        /// =========================================
        private void settingsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            settingsToolStripMenuItem.ForeColor = Color.White;
            settingsToolStripMenuItem.Image = Properties.Resources.settings_128;
        }

        /// =========================================
        /// tablesToolStripMenuItem_MouseEnter()
        /// =========================================
        private void tablesToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            tablesToolStripMenuItem.ForeColor = Color.Black;
            tablesToolStripMenuItem.Image = Properties.Resources.table_selected_128;
        }

        /// =========================================
        /// tablesToolStripMenuItem_MouseLeave()
        /// =========================================
        private void tablesToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            tablesToolStripMenuItem.ForeColor = Color.White;
            tablesToolStripMenuItem.Image = Properties.Resources.table_128;
        }

        /// =========================================
        /// muteToolStripMenuItem_MouseEnter()
        /// =========================================
        private void muteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            muteToolStripMenuItem.ForeColor = Color.Black;
            muteToolStripMenuItem.Image = Properties.Resources.mute_selected_128;
        }

        /// =========================================
        /// muteToolStripMenuItem_MouseLeave()
        /// =========================================
        private void muteToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            muteToolStripMenuItem.ForeColor = Color.White;
            if (!Program.Mute)
                muteToolStripMenuItem.Image = Properties.Resources.mute_128;
        }

        /// =========================================
        /// newToolStripMenuItem_MouseEnter()
        /// =========================================
        private void newToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            newToolStripMenuItem.ForeColor = Color.Black;
            newToolStripMenuItem.Image = Properties.Resources.new_file_selected_128;
        }

        /// =========================================
        /// newToolStripMenuItem_MouseLeave()
        /// =========================================
        private void newToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            newToolStripMenuItem.ForeColor = Color.White;
            newToolStripMenuItem.Image = Properties.Resources.new_file_128;
        }

        /// =========================================
        /// openToolStripMenuItem_MouseEnter()
        /// =========================================
        private void openToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            openToolStripMenuItem.ForeColor = Color.Black;
            openToolStripMenuItem.Image = Properties.Resources.open_file_selected_128;
        }

        /// =========================================
        /// openToolStripMenuItem_MouseLeave()
        /// =========================================
        private void openToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            openToolStripMenuItem.ForeColor = Color.White;
            openToolStripMenuItem.Image = Properties.Resources.open_file_128;
        }

        /// =========================================
        /// saveToolStripMenuItem_MouseEnter()
        /// =========================================
        private void saveToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            saveToolStripMenuItem.ForeColor = Color.Black;
            saveToolStripMenuItem.Image = Properties.Resources.save_file_selected_128;
        }

        /// =========================================
        /// saveToolStripMenuItem_MouseLeave()
        /// =========================================
        private void saveToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            saveToolStripMenuItem.ForeColor = Color.White;
            saveToolStripMenuItem.Image = Properties.Resources.save_file_128;
        }

        /// =========================================
        /// saveAsToolStripMenuItem_MouseEnter()
        /// =========================================
        private void saveAsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem.ForeColor = Color.Black;
            saveAsToolStripMenuItem.Image = Properties.Resources.save_as_file_selected_128;
        }

        /// =========================================
        /// saveAsToolStripMenuItem_MouseLeave()
        /// =========================================
        private void saveAsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem.ForeColor = Color.White;
            saveAsToolStripMenuItem.Image = Properties.Resources.save_as_file_128;
        }

        /// =========================================
        /// exitToolStripMenuItem_MouseEnter()
        /// =========================================
        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.Black;
            exitToolStripMenuItem.Image = Properties.Resources.quit_selected_128;
        }

        /// =========================================
        /// exitToolStripMenuItem_MouseLeave()
        /// =========================================
        private void exitToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.White;
            exitToolStripMenuItem.Image = Properties.Resources.quit_128;
        }

        /// =========================================
        /// fullscreenToolStripMenuItem_MouseEnter()
        /// =========================================
        private void fullscreenToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            fullscreenToolStripMenuItem.ForeColor = Color.Black;
            fullscreenToolStripMenuItem.Image = Properties.Resources.fullscreen_selected_128;
        }

        /// =========================================
        /// fullscreenToolStripMenuItem_MouseLeave()
        /// =========================================
        private void fullscreenToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            fullscreenToolStripMenuItem.ForeColor = Color.White;
            fullscreenToolStripMenuItem.Image = Properties.Resources.fullscreen_128;

            if (Fullscreen)
                fullscreenToolStripMenuItem.Image = Properties.Resources.fullscreen_selected_128;
        }

        /// =========================================
        /// divideLootToolStripMenuItem_MouseEnter()
        /// =========================================
        private void divideLootToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            divideLootToolStripMenuItem.ForeColor = Color.Black;
            divideLootToolStripMenuItem.Image = Properties.Resources.divide_loot_selected_128;
        }

        /// =========================================
        /// divideLootToolStripMenuItem_MouseLeave()
        /// =========================================
        private void divideLootToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            divideLootToolStripMenuItem.ForeColor = Color.White;
            divideLootToolStripMenuItem.Image = Properties.Resources.divide_loot_128;
        }

        /// =========================================
        /// diceRollerToolStripMenuItem_MouseEnter()
        /// =========================================
        private void diceRollerToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            diceRollerToolStripMenuItem.ForeColor = Color.Black;
            diceRollerToolStripMenuItem.Image = Properties.Resources.roll_dice_selected_128;
        }

        /// =========================================
        /// diceRollerToolStripMenuItem_MouseLeave()
        /// =========================================
        private void diceRollerToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            diceRollerToolStripMenuItem.ForeColor = Color.White;
            diceRollerToolStripMenuItem.Image = Properties.Resources.roll_dice_128;
        }

        /// =========================================
        /// longRestToolStripMenuItem_MouseEnter()
        /// =========================================
        private void longRestToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            longRestToolStripMenuItem.ForeColor = Color.Black;
            longRestToolStripMenuItem.Image = Properties.Resources.long_rest_selected_128;
        }

        /// =========================================
        /// longRestToolStripMenuItem_MouseLeave()
        /// =========================================
        private void longRestToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            longRestToolStripMenuItem.ForeColor = Color.White;
            longRestToolStripMenuItem.Image = Properties.Resources.long_rest_128;
        }

        #endregion

        #region ProgressBar Events

        /// =========================================
        /// oSaveTimer_Tick()
        /// =========================================
        private void oSaveTimer_Tick(object sender, EventArgs e)
        {
            oSaveProgressBar.PerformStep();

            if (oSaveProgressBar.Value >= oSaveProgressBar.Maximum)
            {
                oSaveTimer.Stop();
                oSavePanel.Visible = false;
            }
        }


        #endregion

    }

}