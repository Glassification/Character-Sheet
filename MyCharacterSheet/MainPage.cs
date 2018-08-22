using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    public partial class MainPage : Form
    {

        #region Constants

        private const int MENU_HEIGHT    = 26;
        private const int MENU_RATE      = 2;
        private const int TAB_WIDTH      = 47;
        private const int TAB_RATE       = 4;
        private const int SAVE_X_OFFSET  = 170;
        private const int SAVE_Y_OFFSET  = 95;
        private const int CONTEXT_OFFSET = 2;

        #endregion

        #region Members

        private PropertyPage    oPropertyPage    = new PropertyPage();
        private DivideLootPage  oDivideLootPage  = new DivideLootPage();
        private DiceRollerPage  oDiceRollerPage  = new DiceRollerPage();
        private SettingsPage    oSettingsPage    = new SettingsPage();
        private EasterEggPage   oEasterEggPage   = new EasterEggPage();

        private List<Label>     oLabels          = new List<Label>();
        private List<Button>    oButtons         = new List<Button>();
        private List<float>     oLabelSizes      = new List<float>();
        private List<float>     oButtonSizes     = new List<float>();

        private SecondaryPage   oSecondaryPage   = null;
        private TertiaryPage    oTertiaryPage    = null;
        private CampainPage     oCampainPage     = null;

        private VerticalButton  btnPrimary       = new VerticalButton();
        private VerticalButton  btnSecondary     = new VerticalButton();
        private VerticalButton  btnTertiary      = new VerticalButton();
        private VerticalButton  btnCampain       = new VerticalButton();

        private Rectangle       dragBoxFromMouseDown;
        private int             rowIndexFromMouseDown;
        private int             rowIndexOfItemUnderMouseToDrop;

        public enum Pages { Primary, Secondary, Tertiary, Campain};

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
            MouseDownFilter filter = new MouseDownFilter(this);
            filter.FormClicked += mouseFilter_FormClicked;
            Application.AddMessageFilter(filter);

            //Initialize variables
            Fullscreen = false;
            Drawing = false;
            MinimumSize = Size;

            //Load blank character sheet
            Program.FileLocation = Constants.NEW_FILE;
            Program.Character.CreateCharacterSheet();

            //Load secondary page
            oSecondaryPage = new SecondaryPage();
            Controls.Add(oSecondaryPage);
            oSecondaryPage.Visible = false;
            oSecondaryPage.Dock = DockStyle.Fill;

            //Load tertiary page
            oTertiaryPage = new TertiaryPage();
            Controls.Add(oTertiaryPage);
            oTertiaryPage.Visible = false;
            oTertiaryPage.Dock = DockStyle.Fill;

            //Load Campain Page
            oCampainPage = new CampainPage();
            Controls.Add(oCampainPage);
            oCampainPage.Visible = false;
            oCampainPage.Dock = DockStyle.Fill;

            //Set intial program state
            FillLabelList();
            fillSizes();
            fillComboBoxes();
            loadPageLists();
            createMenuStrip();
            setTabPanel();
            createTabButtons();
            createSaveProgressBar();
            FormatInputBoxes();
            FormatContextMenus();

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
            Control current = sender as Control;

            if (current.Parent != null && current.Parent != this)
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
                MainPage_MouseMove(current, e);
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
                    if (!c.Name.Equals("oSpellListDataView"))
                    {
                        AddNestedMouseHandler(c, nestedHandler);
                    }
                }
            }
        }

        /// =========================================
        /// loadMainPage()
        /// =========================================
        private void loadPageLists()
        {
            FillWeapons();
            FillAmmo();
            oSecondaryPage.FillAbility();
            oSecondaryPage.FillInventory();
            oSecondaryPage.FillNotes();
            oTertiaryPage.FillSpellclass();
            oTertiaryPage.FillSpellList();
        }

        /// =========================================
        /// createTabButtons()
        /// =========================================
        private void createTabButtons()
        {
            btnPrimary.Dock = DockStyle.Fill;
            btnPrimary.FlatStyle = FlatStyle.Popup;
            btnPrimary.BackColor = Constants.DarkBlue;
            btnPrimary.Font = new Font(btnPrimary.Font.FontFamily, 10f);
            btnPrimary.VerticalText = "Character Status";

            btnSecondary.Dock = DockStyle.Fill;
            btnSecondary.FlatStyle = FlatStyle.Popup;
            btnSecondary.BackColor = Constants.DarkGrey;
            btnSecondary.Font = new Font(btnSecondary.Font.FontFamily, 10f);
            btnSecondary.VerticalText = "Character Details";

            btnTertiary.Dock = DockStyle.Fill;
            btnTertiary.FlatStyle = FlatStyle.Popup;
            btnTertiary.BackColor = Constants.DarkGrey;
            btnTertiary.Font = new Font(btnTertiary.Font.FontFamily, 10f);
            btnTertiary.VerticalText = "Spellcasting";

            btnCampain.Dock = DockStyle.Fill;
            btnCampain.FlatStyle = FlatStyle.Popup;
            btnCampain.BackColor = Constants.DarkGrey;
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
        /// setTabPanel()
        /// =========================================
        private void setTabPanel()
        {
            oTabPanel.Location = new Point(0, 0);
            oTabPanel.Height = Size.Height;
            oTabPanel.Width = 0;

            TabHidden = true;
            TabHiding = false; 
        }

        /// =========================================
        /// setDeathSaves()
        /// =========================================
        private void setDeathSaves()
        {
            switch (Program.Character.HitPoints.DeathSaveSuccess)
            {
                case 3:
                    chkSuccess1.Checked = true;
                    chkSuccess2.Checked = true;
                    chkSuccess3.Checked = true;
                    break;
                case 2:
                    chkSuccess1.Checked = true;
                    chkSuccess2.Checked = true;
                    break;
                case 1:
                    chkSuccess1.Checked = true;
                    break;
            }

            switch (Program.Character.HitPoints.DeathSaveFailure)
            {
                case 3:
                    chkFailure1.Checked = true;
                    chkFailure2.Checked = true;
                    chkFailure3.Checked = true;
                    break;
                case 2:
                    chkFailure1.Checked = true;
                    chkFailure2.Checked = true;
                    break;
                case 1:
                    chkFailure1.Checked = true;
                    break;
            }
        }

        /// =========================================
        /// getDeathSaves()
        /// =========================================
        private void getDeathSaves()
        {
            int success = 0, failure = 0;

            if (chkSuccess1.Checked)
                success++;
            if (chkSuccess2.Checked)
                success++;
            if (chkSuccess3.Checked)
                success++;

            if (chkFailure1.Checked)
                failure++;
            if (chkFailure2.Checked)
                failure++;
            if (chkFailure3.Checked)
                failure++;

            Program.Character.HitPoints.DeathSaveSuccess = success;
            Program.Character.HitPoints.DeathSaveFailure = failure;
        }

        /// =========================================
        /// createSaveProgressBar()
        /// =========================================
        private void createSaveProgressBar()
        {
            oSavePanel.Location = new Point(Size.Width  - SAVE_X_OFFSET, Size.Height - SAVE_Y_OFFSET);
            oSavePanel.Visible = false;
        }

        /// =========================================
        /// createMenuStrip()
        /// =========================================
        private void createMenuStrip()
        {
            oMenuStrip.BackColor = Constants.DarkGrey;
            oMenuStrip.ForeColor = Color.White;
            
            foreach (ToolStripMenuItem item in oMenuStrip.Items)
            {
                item.BackColor = Constants.DarkGrey;
                item.ForeColor = Color.White;

                foreach (ToolStripMenuItem children in item.DropDownItems)
                {
                    children.BackColor = Constants.DarkGrey;
                    children.ForeColor = Color.White;

                    foreach (ToolStripMenuItem children2 in children.DropDownItems)
                    {
                        children2.BackColor = Constants.DarkGrey;
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
        /// fillComboBoxes()
        /// =========================================
        private void fillComboBoxes()
        {
            DataGridViewComboBoxColumn dmgDropDown, abilityDropDown, ammoDmgDropdown;

            //fill weapon damage dropdown list
            dmgDropDown = oWeaponDataGrid.Columns[DmgType.Index] as DataGridViewComboBoxColumn;
            dmgDropDown.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            for (int i = 0; i < Constants.DamageTypeLength(); i++)
                dmgDropDown.Items.Add(Constants.DamageType(i));

            //fill weapon ability dropdown list
            abilityDropDown = oWeaponDataGrid.Columns[Ability.Index] as DataGridViewComboBoxColumn;
            abilityDropDown.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            for (int i = 0; i < Constants.AbilitiesLength(); i++)
                abilityDropDown.Items.Add(Constants.Ability(i));

            //fill ammo damage dropdown list
            ammoDmgDropdown = oAmmoGridView.Columns[AmmoDmgType.Index] as DataGridViewComboBoxColumn;
            ammoDmgDropdown.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            for (int i = 0; i < Constants.DamageTypeLength(); i++)
                ammoDmgDropdown.Items.Add(Constants.DamageType(i));
        }

        /// =========================================
        /// fillSizes()
        /// =========================================
        private void fillSizes()
        {
            foreach (Label l in oLabels)
                oLabelSizes.Add(l.Font.Size);

            foreach (Button b in oButtons)
                oButtonSizes.Add(b.Font.Size);

            OriginalSize = Size;
            HeaderSize = oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font.Size;
            RowSize = oWeaponDataGrid.DefaultCellStyle.Font.Size;
            InputFontHP = oInputHP.Font.Size;
            CurrencySize = oInputGP.Font.Size;
        }

        /// =========================================
        /// fillAmmoList()
        /// =========================================
        private void FillAmmo()
        {
            string[] tokens;
            oAmmoGridView.Rows.Clear();

            foreach (string ammo in Program.Character.oAmmo)
            {
                int index = oAmmoGridView.Rows.Add();
                DataGridViewRow row = oAmmoGridView.Rows[index];
                tokens = ammo.Split(Constants.DELIMITER);

                row.Cells[Ammo.Index].Value = tokens[0];
                row.Cells[Qty.Index].Value = tokens[1];
                row.Cells[ammoDmgBonus.Index].Value = tokens[2];
                row.Cells[AmmoDmgType.Index].Value = tokens[3];
                row.Cells[Used.Index].Value = tokens[4];
            }
        }

        /// =========================================
        /// fillWeaponsList()
        /// =========================================
        private void FillWeapons()
        {
            oWeaponDataGrid.Rows.Clear();
            string[] tokens;

            foreach (string weapon in Program.Character.oWeapons)
            {
                int index = oWeaponDataGrid.Rows.Add();
                DataGridViewRow row = oWeaponDataGrid.Rows[index];
                tokens = weapon.Split(Constants.DELIMITER);

                row.Cells[Weapons.Index].Value = tokens[0];
                row.Cells[AttackBonus.Index].Value = Program.Character.GetBonus(tokens[1]);
                row.Cells[Ability.Index].Value = tokens[1];
                row.Cells[Dmg.Index].Value = tokens[2];
                row.Cells[MiscBonus.Index].Value = tokens[3];
                row.Cells[DmgType.Index].Value = tokens[4];
                row.Cells[Range.Index].Value = tokens[5];
                row.Cells[Notes.Index].Value = tokens[6];
            }
        }

        /// =========================================
        /// fillWeapons()
        /// =========================================
        private void WriteWeapons(List<string> list)
        {
            string str;

            foreach (DataGridViewRow row in oWeaponDataGrid.Rows)
            {
                str = (string)row.Cells[Weapons.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Ability.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Dmg.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[MiscBonus.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[DmgType.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Range.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Notes.Index].Value;

                list.Add(str);
            }
            list.RemoveAt(list.Count - 1);
        }

        /// =========================================
        /// fillAmmo()
        /// =========================================
        private void WriteAmmo(List<string> list)
        {
            string str;

            foreach (DataGridViewRow row in oAmmoGridView.Rows)
            {
                str = (string)row.Cells[Ammo.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Qty.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[ammoDmgBonus.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[AmmoDmgType.Index].Value + Constants.DELIMITER +
                      (string)row.Cells[Used.Index].Value;

                list.Add(str);
            }
            list.RemoveAt(list.Count - 1);
        }

        /// =========================================
        /// LoadSettings()
        /// =========================================
        private void LoadSettings()
        {
            //Load mute state
            if (Settings.RememberMute)
            {
                Program.Mute = Settings.MuteState;
                muteToolStripMenuItem.Checked = Program.Mute;
            }

            //Load tab state
            if (Settings.RememberLastTab)
            {
                switch (Settings.LastTab)
                {
                    default:
                    case 0:
                        btnPrimaryPanel_Click(new object(), new EventArgs());
                        break;
                    case 1:
                        btnSecondaryPanel_Click(new object(), new EventArgs());
                        break;
                    case 2:
                        btnTertiaryPanel_Click(new object(), new EventArgs());
                        break;
                    case 3:
                        btnCampainPanel_Click(new object(), new EventArgs());
                        break;
                }
            }
        }

        /// =========================================
        /// Save()
        /// =========================================
        private void Save()
        {
            Program.Character.oWeapons.Clear();
            WriteWeapons(Program.Character.oWeapons);

            Program.Character.oAmmo.Clear();
            WriteAmmo(Program.Character.oAmmo);

            Program.Character.oInventory.Clear();
            oSecondaryPage.WriteInventory(Program.Character.oInventory);

            Program.Character.oAbility.Clear();
            oSecondaryPage.WriteAbility(Program.Character.oAbility);

            Program.Character.oNotes.Clear();
            oSecondaryPage.WriteNotes(Program.Character.oNotes);

            Program.Character.Spellcasting.spellClass.Clear();
            oTertiaryPage.WriteSpellclass(Program.Character.Spellcasting.spellClass);

            Program.Character.Spellcasting.spellList.Clear();
            oTertiaryPage.WriteSpellList(Program.Character.Spellcasting.spellList);

            Program.Character.oDocuments.Clear();
            oCampainPage.WriteCampainNotes();

            getDeathSaves();
            Settings.LastTab = (int)GetCurrentTab();
            Program.Modified = false;
            Program.Character.SaveCharacterSheet();
        }

        /// =========================================
        /// SaveFile()
        /// =========================================
        private void SaveFile(bool control, bool shift)
        {
            DialogResult result;

            if (control && (shift || Program.FileLocation.Equals(Constants.NEW_FILE)))
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
                        Program.Character.LoadCharacterSheet();
                        loadPageLists();
                        setDeathSaves();
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

                    Program.FileLocation = Constants.NEW_FILE;
                    Program.Character.CreateCharacterSheet();
                    Settings.Default();
                    SetAutosaveState();
                    oCampainPage.ClearDocumentList();
                    loadPageLists();
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
        }

        /// =========================================
        /// OpenProperties()
        /// =========================================
        private void OpenProperties(bool control)
        {
            if (!control)
            {
                oPropertyPage.ShowPane();
                InvalidateAll();
            }
        }

        /// =========================================
        /// OpenSettings()
        /// =========================================
        private void OpenSettings(bool control)
        {
            if (!control)
            {
                oSettingsPage.ShowPane();
                SetAutosaveState();

                InvalidateAll();
            }
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
                longRestToolStripMenuItem_Click(new object(), new EventArgs());
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
            oInputCP.Width = oPanelCP.Width;
            oInputSP.Width = oPanelSP.Width;
            oInputEP.Width = oPanelEP.Width;
            oInputGP.Width = oPanelGP.Width;
            oInputPP.Width = oPanelPP.Width;

            oInputCP.Location = new Point(0, (oPanelCP.Height / 2) - (oInputCP.Height / 2));
            oInputSP.Location = new Point(0, (oPanelSP.Height / 2) - (oInputSP.Height / 2));
            oInputEP.Location = new Point(0, (oPanelEP.Height / 2) - (oInputEP.Height / 2));
            oInputGP.Location = new Point(0, (oPanelGP.Height / 2) - (oInputGP.Height / 2));
            oInputPP.Location = new Point(0, (oPanelPP.Height / 2) - (oInputPP.Height / 2));

            oInputHP.Width = oPanelHP.Width;
            oInputHP.Location = new Point(0, (oPanelHP.Height / 2) - (oInputHP.Height / 2));
        }

        /// =========================================
        /// formatContextMenu()
        /// =========================================
        private void FormatContextMenus()
        {
            oWeaponDeleteRowMenu.BackColor = Constants.DarkGrey;
            oWeaponDeleteRowMenu.ForeColor = Color.White;

            oAmmoDeleteRowMenu.BackColor = Constants.DarkGrey;
            oAmmoDeleteRowMenu.ForeColor = Color.White;
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

            if (!Program.FileLocation.Equals(Constants.NEW_FILE))
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
                    isInside |= IsInside(oSecondaryPage.NoteGridView());
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

            if      (oPrimaryTable.Visible)  { tab = Pages.Primary; }
            else if (oSecondaryPage.Visible) { tab = Pages.Secondary; }
            else if (oTertiaryPage.Visible)  { tab = Pages.Tertiary; }
            else if (oCampainPage.Visible)   { tab = Pages.Campain; }

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

        private float CurrencySize
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

                oCharName.Text = Program.Character.Name;
                oRace.Text = Program.Character.Race;
                oBackground.Text = Program.Character.Background;
                oAlignment.Text = Program.Character.Alignment;
                oLevel.Text = Program.Character.Level + "";
                oEXP.Text = Program.Character.EXP + " / " + Constants.Experience(Program.Character.Level);
                oClass.Text = Program.Character.Class;
                oLanguage.Text = Program.Character.Language;
                oMovement.Text = Program.Character.Movement;
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

                oStrSavingThrow.Text = Program.Character.oSavingThrows[0].Bonus + "";
                oDexSavingThrow.Text = Program.Character.oSavingThrows[1].Bonus + "";
                oConSavingThrow.Text = Program.Character.oSavingThrows[2].Bonus + "";
                oIntSavingThrow.Text = Program.Character.oSavingThrows[3].Bonus + "";
                oWisSavingThrow.Text = Program.Character.oSavingThrows[4].Bonus + "";
                oChaSavingThrow.Text = Program.Character.oSavingThrows[5].Bonus + "";

                chkStrengthP.Checked = Program.Character.oSavingThrows[0].Proficiency;
                chkDexterityP.Checked = Program.Character.oSavingThrows[1].Proficiency;
                chkConstitutionP.Checked = Program.Character.oSavingThrows[2].Proficiency;
                chkIntelligenceP.Checked = Program.Character.oSavingThrows[3].Proficiency;
                chkWisdomP.Checked = Program.Character.oSavingThrows[4].Proficiency;
                chkCharismaP.Checked = Program.Character.oSavingThrows[5].Proficiency;

                oAthleticsSkill.Text = Program.Character.oSkills[0].Bonus + "";
                oAcrobaticsSkill.Text = Program.Character.oSkills[1].Bonus + "";
                oSleightSkill.Text = Program.Character.oSkills[2].Bonus + "";
                oStealthSkill.Text = Program.Character.oSkills[3].Bonus + "";
                oArcanaSkill.Text = Program.Character.oSkills[4].Bonus + "";
                oHistorySkill.Text = Program.Character.oSkills[5].Bonus + "";
                oInvestigationSkill.Text = Program.Character.oSkills[6].Bonus + "";
                oNatureSkill.Text = Program.Character.oSkills[7].Bonus + "";
                oReligionSkill.Text = Program.Character.oSkills[8].Bonus + "";
                oAnimalSkill.Text = Program.Character.oSkills[9].Bonus + "";
                oInsightSkill.Text = Program.Character.oSkills[10].Bonus + "";
                oMedicineSkill.Text = Program.Character.oSkills[11].Bonus + "";
                oPerceptionSkill.Text = Program.Character.oSkills[12].Bonus + "";
                oSurvivalSkill.Text = Program.Character.oSkills[13].Bonus + "";
                oDeceptionSkill.Text = Program.Character.oSkills[14].Bonus + "";
                oIntimidationSkill.Text = Program.Character.oSkills[15].Bonus + "";
                oPerformanceSkill.Text = Program.Character.oSkills[16].Bonus + "";
                oPersuasionSkill.Text = Program.Character.oSkills[17].Bonus + "";

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

                oHitPoints.Text = (Program.Character.HitPoints.HP + Program.Character.HitPoints.TempHP) + "";
                oHitPoints.ForeColor = Program.Character.HitPoints.HitPointsColour;
                oInitiative.Text = Program.Character.Initiative + "";
                oPassivePerception.Text = Program.Character.PassivePerception + "";
                oConditions.Text = Program.Character.HitPoints.Conditions;

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

                oInputCP.Text = Program.Character.CP + "";
                oInputSP.Text = Program.Character.SP + "";
                oInputEP.Text = Program.Character.EP + "";
                oInputGP.Text = Program.Character.GP + "";
                oInputPP.Text = Program.Character.PP + "";
                oGoldValue.Text = Program.Character.TotalGold + "";

                foreach (DataGridViewRow row in oWeaponDataGrid.Rows)
                {
                    row.Cells[AttackBonus.Index].Value = Program.Character.GetBonus((string)row.Cells[Ability.Index].Value);
                }

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
                        if (Program.FileLocation.Equals(Constants.NEW_FILE))
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
                        fullscreenToolStripMenuItem.Checked = !fullscreenToolStripMenuItem.Checked;
                        break;
                    //Open Properties
                    case Keys.C:
                        OpenProperties(e.Control);
                        break;
                    //Open Divide Loot
                    case Keys.L:
                        OpenDivideLoot(e.Control);
                        break;
                    //Open Roll Dice
                    case Keys.D:
                        OpenRollDice(e.Control);
                        break;
                    //Long Rest
                    case Keys.R:
                        LongRest(e.Control);
                        break;
                    //Mute
                    case Keys.M:
                        Program.Mute = !Program.Mute;
                        muteToolStripMenuItem.Checked = Program.Mute;
                        break;
                    //Main Page
                    case Keys.D1:
                        if (!e.Control)
                            btnPrimaryPanel_Click(new object(), new EventArgs());
                        break;
                    //Secondary Page
                    case Keys.D2:
                        if (!e.Control)
                            btnSecondaryPanel_Click(new object(), new EventArgs());
                        break;
                    //Tertiary Page
                    case Keys.D3:
                        if (!e.Control)
                            btnTertiaryPanel_Click(new object(), new EventArgs());
                        break;
                    // Campain Page
                    case Keys.D4:
                        if (!e.Control)
                            btnCampainPanel_Click(new object(), new EventArgs());
                        break;
                }
            }
        }

        /// =========================================
        /// MainPage_Resize()
        /// =========================================
        private void MainPage_Resize(object sender, EventArgs e)
        {
            float mod = 1.25f;
            float ratio = Size.Width / (OriginalSize.Width + 0f);
            
            FormatInputBoxes();
            setTabPanel();
            MenuPanel.Width = Size.Width;
            oSavePanel.Location = new Point(Size.Width - SAVE_X_OFFSET, Size.Height - SAVE_Y_OFFSET);

            if (ratio != float.PositiveInfinity)
            {
                oSecondaryPage.ResizeText(mod, ratio);
                oTertiaryPage.ResizeText(mod, ratio);
                oCampainPage.ResizeText(mod, ratio);

                //Resize Labels
                for (int i = 0; i < oLabels.Count; i++)
                {
                    oLabels[i].Font = new Font(oLabels[i].Font.FontFamily, oLabelSizes[i] * ratio, oLabels[i].Font.Style);
                }

                //Resize Currency Input
                oInputCP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / mod)), oInputCP.Font.Style);
                oInputSP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / mod)), oInputCP.Font.Style);
                oInputEP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / mod)), oInputCP.Font.Style);
                oInputGP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / mod)), oInputCP.Font.Style);
                oInputPP.Font = new Font(oInputCP.Font.FontFamily, CurrencySize * (ratio == 1 ? ratio : (ratio / mod)), oInputCP.Font.Style);

                //Resize Weapon/Ammo Headers
                oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font.FontFamily, HeaderSize * (ratio == 1 ? ratio : (ratio / mod)), oWeaponDataGrid.ColumnHeadersDefaultCellStyle.Font.Style);
                oAmmoGridView.ColumnHeadersDefaultCellStyle.Font = new Font(oAmmoGridView.ColumnHeadersDefaultCellStyle.Font.FontFamily, HeaderSize * (ratio == 1 ? ratio : (ratio / mod)), oAmmoGridView.ColumnHeadersDefaultCellStyle.Font.Style);

                //Resize Weapon/Ammo Rows
                oWeaponDataGrid.DefaultCellStyle.Font = new Font(oWeaponDataGrid.DefaultCellStyle.Font.FontFamily, RowSize * (ratio == 1 ? ratio : (ratio / mod)), oWeaponDataGrid.Font.Style);
                oAmmoGridView.DefaultCellStyle.Font = new Font(oAmmoGridView.DefaultCellStyle.Font.FontFamily, RowSize * (ratio == 1 ? ratio : (ratio / mod)), oAmmoGridView.Font.Style);
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

                if (!Program.FileLocation.Equals(Constants.NEW_FILE))
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
            int hp, temp, damage;

            if (e.KeyCode == Keys.Return)
            {
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
        /// oInputCP_KeyPress()
        /// =========================================
        private void oInputCP_KeyDown(object sender, KeyEventArgs e)
        {
            int cp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputCP.Text = TrimLeadingZero(oInputCP.Text);

                if (int.TryParse(oInputCP.Text, out cp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.CP = cp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputCP.Text = Program.Character.CP + "";
                }
            }
        }

        /// =========================================
        /// oInputSP_KeyPress()
        /// =========================================
        private void oInputSP_KeyDown(object sender, KeyEventArgs e)
        {
            int sp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputSP.Text = TrimLeadingZero(oInputSP.Text);

                if (int.TryParse(oInputSP.Text, out sp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.SP = sp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputSP.Text = Program.Character.SP + "";
                }
            }
        }

        /// =========================================
        /// oInputEP_KeyPress()
        /// =========================================
        private void oInputEP_KeyDown(object sender, KeyEventArgs e)
        {
            int ep;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputEP.Text = TrimLeadingZero(oInputEP.Text);

                if (int.TryParse(oInputEP.Text, out ep))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.EP = ep;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputEP.Text = Program.Character.EP + "";
                }
            }
        }

        /// =========================================
        /// oInputGP_KeyPress()
        /// =========================================
        private void oInputGP_KeyDown(object sender, KeyEventArgs e)
        {
            int gp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputGP.Text = TrimLeadingZero(oInputGP.Text);

                if (int.TryParse(oInputGP.Text, out gp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.GP = gp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputGP.Text = Program.Character.GP + "";
                }
            }
        }

        /// =========================================
        /// oInputPP_KeyPress()
        /// =========================================
        private void oInputPP_KeyDown(object sender, KeyEventArgs e)
        {
            int pp;

            if (e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                oInputPP.Text = TrimLeadingZero(oInputPP.Text);

                if (int.TryParse(oInputPP.Text, out pp))
                {
                    if (!Drawing)
                    {
                        Program.Modified = true;
                    }

                    Program.Character.PP = pp;
                    oGoldValue.Text = Program.Character.TotalGold + "";
                }
                else
                {
                    oInputPP.Text = Program.Character.PP + "";
                }
            }
        }

        /// =========================================
        /// oInputHP_TextChanged()
        /// =========================================
        private void oInputHP_TextChanged(object sender, EventArgs e)
        {
            Program.Modified = true;
            Invalidate();
        }

        /// =========================================
        /// oInputCP_Enter()
        /// =========================================
        private void oInputCP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputSP_Enter()
        /// =========================================
        private void oInputSP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputEP_Enter()
        /// =========================================
        private void oInputEP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputGP_Enter()
        /// =========================================
        private void oInputGP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputPP_Enter()
        /// =========================================
        private void oInputPP_Enter(object sender, EventArgs e)
        {
            Program.Typing = true;
        }

        /// =========================================
        /// oInputCP_Leave()
        /// =========================================
        private void oInputCP_Leave(object sender, EventArgs e)
        {
            int cp;

            oInputCP.Text = TrimLeadingZero(oInputCP.Text);

            if (int.TryParse(oInputCP.Text, out cp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.CP = cp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputCP.Text = Program.Character.CP + "";
            }

            Program.Typing = false;
        }

        /// =========================================
        /// oInputSP_Leave()
        /// =========================================
        private void oInputSP_Leave(object sender, EventArgs e)
        {
            int sp;

            oInputSP.Text = TrimLeadingZero(oInputSP.Text);

            if (int.TryParse(oInputSP.Text, out sp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.SP = sp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputSP.Text = Program.Character.SP + "";
            }

            Program.Typing = false;
        }

        /// =========================================
        /// oInputEP_Leave()
        /// =========================================
        private void oInputEP_Leave(object sender, EventArgs e)
        {
            int ep;

            oInputEP.Text = TrimLeadingZero(oInputEP.Text);

            if (int.TryParse(oInputEP.Text, out ep))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.EP = ep;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputEP.Text = Program.Character.EP + "";
            }

            Program.Typing = false;
        }

        /// =========================================
        /// oInputGP_Leave()
        /// =========================================
        private void oInputGP_Leave(object sender, EventArgs e)
        {
            int gp;

            oInputGP.Text = TrimLeadingZero(oInputGP.Text);

            if (int.TryParse(oInputGP.Text, out gp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.GP = gp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputGP.Text = Program.Character.GP + "";
            }

            Program.Typing = false;
        }

        /// =========================================
        /// oInputPP_Leave()
        /// =========================================
        private void oInputPP_Leave(object sender, EventArgs e)
        {
            int pp;

            oInputPP.Text = TrimLeadingZero(oInputPP.Text);

            if (int.TryParse(oInputPP.Text, out pp))
            {
                if (!Drawing)
                {
                    Program.Modified = true;
                }

                Program.Character.PP = pp;
                oGoldValue.Text = Program.Character.TotalGold + "";
            }
            else
            {
                oInputPP.Text = Program.Character.PP + "";
            }

            Program.Typing = false;
        }

        /// =========================================
        /// oPanelCP_Click()
        /// =========================================
        private void oPanelCP_Click(object sender, EventArgs e)
        {
            oInputCP.Select();
            oInputCP.SelectionStart = oInputCP.Text.Length;
            oInputCP.SelectionLength = 0;
        }

        /// =========================================
        /// oPanelSP_Click()
        /// =========================================
        private void oPanelSP_Click(object sender, EventArgs e)
        {
            oInputSP.Select();
            oInputSP.SelectionStart = oInputSP.Text.Length;
            oInputSP.SelectionLength = 0;
        }

        /// =========================================
        /// oInputEP_Click()
        /// =========================================
        private void oPanelEP_Click(object sender, EventArgs e)
        {
            oInputEP.Select();
            oInputEP.SelectionStart = oInputEP.Text.Length;
            oInputEP.SelectionLength = 0;
        }

        /// =========================================
        /// oInputGP_Click()
        /// =========================================
        private void oPanelGP_Click(object sender, EventArgs e)
        {
            oInputGP.Select();
            oInputGP.SelectionStart = oInputGP.Text.Length;
            oInputGP.SelectionLength = 0;
        }

        /// =========================================
        /// oInputPP_Click()
        /// =========================================
        private void oPanelPP_Click(object sender, EventArgs e)
        {
            oInputPP.Select();
            oInputPP.SelectionStart = oInputPP.Text.Length;
            oInputPP.SelectionLength = 0;
        }

        #endregion

        #region Button Events

        /// =========================================
        /// btnPrimaryPanel_Click()
        /// =========================================
        private void btnPrimaryPanel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            btnPrimary.BackColor = Constants.DarkBlue;
            btnSecondary.BackColor = Constants.DarkGrey;
            btnTertiary.BackColor = Constants.DarkGrey;
            btnCampain.BackColor = Constants.DarkGrey;

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

            btnPrimary.BackColor = Constants.DarkGrey;
            btnSecondary.BackColor = Constants.DarkBlue;
            btnTertiary.BackColor = Constants.DarkGrey;
            btnCampain.BackColor = Constants.DarkGrey;

            oPrimaryTable.Visible = false;
            oTertiaryPage.Visible = false;
            oCampainPage.Visible = false;
            oSecondaryPage.Visible = true;
            oSecondaryPage.DefaultFocus();
        }

        /// =========================================
        /// btnTertiaryPanel_Click()
        /// =========================================
        private void btnTertiaryPanel_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();

            btnPrimary.BackColor = Constants.DarkGrey;
            btnSecondary.BackColor = Constants.DarkGrey;
            btnTertiary.BackColor = Constants.DarkBlue;
            btnCampain.BackColor = Constants.DarkGrey;

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

            btnPrimary.BackColor = Constants.DarkGrey;
            btnSecondary.BackColor = Constants.DarkGrey;
            btnTertiary.BackColor = Constants.DarkGrey;
            btnCampain.BackColor = Constants.DarkBlue;

            oPrimaryTable.Visible = false;
            oSecondaryPage.Visible = false;
            oTertiaryPage.Visible = false;
            oCampainPage.Visible = true;
            oCampainPage.DefaultFocus();
        }

        #endregion

        #region Hit Dice Events

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
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oWeaponDataGrid.RowCount-1)
            {
                Rectangle rect = oWeaponDataGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oWeaponDeleteRowMenu.Show(oWeaponDataGrid ,new Point(rect.X + e.X + CONTEXT_OFFSET, rect.Y + e.Y + CONTEXT_OFFSET));
            }
        }

        /// =========================================
        /// deleteRowToolStripMenuItem_Click()
        /// =========================================
        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            oWeaponDataGrid.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oAmmoGridView_CellMouseClick()
        /// =========================================
        private void oAmmoGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < oAmmoGridView.RowCount-1)
            {
                Rectangle rect = oAmmoGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Row = e.RowIndex;
                oAmmoDeleteRowMenu.Show(oAmmoGridView, new Point(rect.X + e.X + CONTEXT_OFFSET, rect.Y + e.Y + CONTEXT_OFFSET));
            }
        }

        /// =========================================
        /// deleteRowToolStripMenuItem1_Click()
        /// =========================================
        private void deleteRowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.Modified = true;
            oAmmoGridView.Rows.RemoveAt(Row);
        }

        /// =========================================
        /// oWeaponDataGrid_CellEnter()
        /// =========================================
        private void oWeaponDataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = true;

            //Open dropdown menu when clicked
            if (e.ColumnIndex == DmgType.Index || e.ColumnIndex == Ability.Index)
            {
                oWeaponDataGrid.CurrentCell = oWeaponDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                oWeaponDataGrid.BeginEdit(true);
            }
        }

        /// =========================================
        /// oWeaponDataGrid_CellLeave()
        /// =========================================
        private void oWeaponDataGrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Program.Typing = false;
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
        /// oWeaponDeleteRowMenu_MouseEnter()
        /// =========================================
        private void oWeaponDeleteRowMenu_MouseEnter(object sender, EventArgs e)
        {
            oWeaponDeleteRowMenu.ForeColor = Color.Black;
        }

        /// =========================================
        /// oWeaponDeleteRowMenu_MouseLeave()
        /// =========================================
        private void oWeaponDeleteRowMenu_MouseLeave(object sender, EventArgs e)
        {
            oWeaponDeleteRowMenu.ForeColor = Color.White;
        }

        /// =========================================
        /// oAmmoDeleteRowMenu_MouseEnter()
        /// =========================================
        private void oAmmoDeleteRowMenu_MouseEnter(object sender, EventArgs e)
        {
            oAmmoDeleteRowMenu.ForeColor = Color.Black;
        }

        /// =========================================
        /// oAmmoDeleteRowMenu_MouseLeave()
        /// =========================================
        private void oAmmoDeleteRowMenu_MouseLeave(object sender, EventArgs e)
        {
            oAmmoDeleteRowMenu.ForeColor = Color.White;
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

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
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
            if (oAmmoGridView.Rows.Count > 1)
            {
                Point clientPoint = oAmmoGridView.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oAmmoGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oAmmoGridView.Rows.Count - 1)
                        rowIndexOfItemUnderMouseToDrop = oAmmoGridView.Rows.Count - 2;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    oAmmoGridView.Rows.RemoveAt(rowIndexFromMouseDown);
                    oAmmoGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
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

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                dragBoxFromMouseDown = Rectangle.Empty;
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
            if (oWeaponDataGrid.Rows.Count > 1)
            {
                Point clientPoint = oWeaponDataGrid.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = oWeaponDataGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (e.Effect == DragDropEffects.Move)
                {

                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                    //set as last row
                    if (rowIndexOfItemUnderMouseToDrop < 0 || rowIndexOfItemUnderMouseToDrop >= oWeaponDataGrid.Rows.Count - 1)
                        rowIndexOfItemUnderMouseToDrop = oWeaponDataGrid.Rows.Count - 2;

                    if (rowIndexFromMouseDown != rowIndexOfItemUnderMouseToDrop)
                        Program.Modified = true;

                    oWeaponDataGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                    oWeaponDataGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                }
            }
        }

        #endregion

        #region Checkbox Events

        /// =========================================
        /// chkStrengthP_CheckedChanged()
        /// =========================================
        private void chkStrengthP_CheckedChanged(object sender, EventArgs e)
        {
            Program.Character.oSavingThrows[0].Proficiency = chkStrengthP.Checked;
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
            Program.Character.oSavingThrows[1].Proficiency = chkDexterityP.Checked;
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
            Program.Character.oSavingThrows[2].Proficiency = chkConstitutionP.Checked;
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
            Program.Character.oSavingThrows[3].Proficiency = chkIntelligenceP.Checked;
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
            Program.Character.oSavingThrows[4].Proficiency = chkWisdomP.Checked;
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
            Program.Character.oSavingThrows[5].Proficiency = chkCharismaP.Checked;
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
            Program.Character.oSkills[0].Proficiency = chkAthleticsP.Checked;
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
            Program.Character.oSkills[0].Expertise = chkAthleticsE.Checked;
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
            Program.Character.oSkills[1].Proficiency = ckAcrobaticsP.Checked;
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
            Program.Character.oSkills[1].Expertise = ckAcrobaticsE.Checked;
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
            Program.Character.oSkills[2].Proficiency = chkSleightP.Checked;
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
            Program.Character.oSkills[2].Expertise = chkSleightE.Checked;
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
            Program.Character.oSkills[3].Proficiency = chkStealthP.Checked;
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
            Program.Character.oSkills[3].Expertise = chkStealthE.Checked;
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
            Program.Character.oSkills[4].Proficiency = chkArcanaP.Checked;
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
            Program.Character.oSkills[4].Expertise = chkArcanaE.Checked;
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
            Program.Character.oSkills[5].Proficiency = chkHistoryP.Checked;
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
            Program.Character.oSkills[5].Expertise = chkHistoryE.Checked;
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
            Program.Character.oSkills[6].Proficiency = chkInvestigationP.Checked;
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
            Program.Character.oSkills[6].Expertise = chkInvestigationE.Checked;
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
            Program.Character.oSkills[7].Proficiency = chkNatureP.Checked;
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
            Program.Character.oSkills[7].Expertise = chkNatureE.Checked;
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
            Program.Character.oSkills[8].Proficiency = chkReligionP.Checked;
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
            Program.Character.oSkills[8].Expertise = chkReligionE.Checked;
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
            Program.Character.oSkills[9].Proficiency = chkAnimalP.Checked;
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
            Program.Character.oSkills[9].Expertise = chkAnimalE.Checked;
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
            Program.Character.oSkills[10].Proficiency = chkInsightP.Checked;
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
            Program.Character.oSkills[10].Expertise = chkInsightE.Checked;
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
            Program.Character.oSkills[11].Proficiency = chkMedicineP.Checked;
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
            Program.Character.oSkills[11].Expertise = chkMedicineE.Checked;
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
            Program.Character.oSkills[12].Proficiency = chkPerceptionP.Checked;
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
            Program.Character.oSkills[12].Expertise = chkPerceptionE.Checked;
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
            Program.Character.oSkills[13].Proficiency = chkSurvivalP.Checked;
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
            Program.Character.oSkills[13].Expertise = chkSurvivalE.Checked;
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
            Program.Character.oSkills[14].Proficiency = chkDeceptionP.Checked;
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
            Program.Character.oSkills[14].Expertise = chkDeceptionE.Checked;
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
            Program.Character.oSkills[15].Proficiency = chkIntimidationP.Checked;
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
            Program.Character.oSkills[15].Expertise = chkIntimidationE.Checked;
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
            Program.Character.oSkills[16].Proficiency = chkPerformanceP.Checked;
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
            Program.Character.oSkills[16].Expertise = chkPerformanceE.Checked;
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
            Program.Character.oSkills[17].Proficiency = chkPersuasionP.Checked;
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
            Program.Character.oSkills[17].Expertise = chkPersuasionE.Checked;
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
            InvalidateAll();
        }

        /// =========================================
        /// settingsToolStripMenuItem_Click()
        /// =========================================
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            oSettingsPage.ShowPane();
            SetAutosaveState();

            InvalidateAll();
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
        }

        /// =========================================
        /// propertiesToolStripMenuItem_MouseLeave()
        /// =========================================
        private void propertiesToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            propertiesToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// muteToolStripMenuItem_MouseEnter()
        /// =========================================
        private void muteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            muteToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// muteToolStripMenuItem_MouseLeave()
        /// =========================================
        private void muteToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            muteToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// newToolStripMenuItem_MouseEnter()
        /// =========================================
        private void newToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            newToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// newToolStripMenuItem_MouseLeave()
        /// =========================================
        private void newToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            newToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// openToolStripMenuItem_MouseEnter()
        /// =========================================
        private void openToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            openToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// openToolStripMenuItem_MouseLeave()
        /// =========================================
        private void openToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            openToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// saveToolStripMenuItem_MouseEnter()
        /// =========================================
        private void saveToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            saveToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// saveToolStripMenuItem_MouseLeave()
        /// =========================================
        private void saveToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            saveToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// saveAsToolStripMenuItem_MouseEnter()
        /// =========================================
        private void saveAsToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// saveAsToolStripMenuItem_MouseLeave()
        /// =========================================
        private void saveAsToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// exitToolStripMenuItem_MouseEnter()
        /// =========================================
        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// exitToolStripMenuItem_MouseLeave()
        /// =========================================
        private void exitToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// fullscreenToolStripMenuItem_MouseEnter()
        /// =========================================
        private void fullscreenToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            fullscreenToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// fullscreenToolStripMenuItem_MouseLeave()
        /// =========================================
        private void fullscreenToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            fullscreenToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// divideLootToolStripMenuItem_MouseEnter()
        /// =========================================
        private void divideLootToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            divideLootToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// divideLootToolStripMenuItem_MouseLeave()
        /// =========================================
        private void divideLootToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            divideLootToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// diceRollerToolStripMenuItem_MouseEnter()
        /// =========================================
        private void diceRollerToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            diceRollerToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// diceRollerToolStripMenuItem_MouseLeave()
        /// =========================================
        private void diceRollerToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            diceRollerToolStripMenuItem.ForeColor = Color.White;
        }

        /// =========================================
        /// longRestToolStripMenuItem_MouseEnter()
        /// =========================================
        private void longRestToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            longRestToolStripMenuItem.ForeColor = Color.Black;
        }

        /// =========================================
        /// longRestToolStripMenuItem_MouseLeave()
        /// =========================================
        private void longRestToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            longRestToolStripMenuItem.ForeColor = Color.White;
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

        #region DeathSaves

        /// =========================================
        /// chkSuccess1_CheckedChanged()
        /// =========================================
        private void chkSuccess1_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
        }

        /// =========================================
        /// chkSuccess2_CheckedChanged()
        /// =========================================
        private void chkSuccess2_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
        }

        /// =========================================
        /// chkSuccess3_CheckedChanged()
        /// =========================================
        private void chkSuccess3_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
        }

        /// =========================================
        /// chkFailure1_CheckedChanged()
        /// =========================================
        private void chkFailure1_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
        }

        /// =========================================
        /// chkFailure2_CheckedChanged()
        /// =========================================
        private void chkFailure2_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
        }

        /// =========================================
        /// chkFailure3_CheckedChanged()
        /// =========================================
        private void chkFailure3_CheckedChanged(object sender, EventArgs e)
        {
            if (!Drawing)
            {
                Sounds.ButtonClick();
                Program.Modified = true;
            }
        }



        #endregion

    }

}