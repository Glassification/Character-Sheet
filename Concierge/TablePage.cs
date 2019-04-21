using Concierge.Lists;
using Concierge.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;
using static Concierge.Utility.Constants;

namespace Concierge
{
#nullable enable
    public partial class TablePage : Form
    {

        #region Constructor

        public TablePage()
        {
            InitializeComponent();

            oTableTabControl.ItemSize = new Size((oTableTabControl.Width / oTableTabControl.TabCount) - 1, 0);
            Editing = false;
            Data = new object();

            FillWeaponComboBoxes();
            FillAmmunitionComboBoxes();
            FillInventoryComboBoxes();
            FillMagicComboBoxes();
            FillSpellComboBoxes();
        }

        #endregion

        #region Methods

        /// =========================================
        /// ShowPane()
        /// -----------------------------------------
        /// This method shows the add item table pane.
        /// =========================================
        public void ShowPane()
        {
            oTableTabControl.SelectedIndex = (int)Program.LastTable;

            ShowDialog();

            Program.LastTable = (Tables)oTableTabControl.SelectedIndex;
        }

        /// =========================================
        /// ShowPane()
        /// -----------------------------------------
        /// This method shows the add item table pane
        /// after clicking the context menu.
        /// =========================================
        public void ShowPane(Tables table)
        {
            InitializeEditing(table);

            ShowDialog();

            ResetEditing();
        }

        /// =========================================
        /// ShowPane()
        /// -----------------------------------------
        /// This method shows the edit table pane. 
        /// Only the table type called can be viewed
        /// and the rest are disabled.
        /// =========================================
        public void ShowPane(Tables table, object data)
        {
            Editing = true;
            Data = data;
            InitializeEditing(table);

            btnAdd.Text = "Ok";
            btnClose.Text = "Cancel";

            FillAll();
            ShowDialog();

            btnAdd.Text = "Add";
            btnClose.Text = "Close";

            ResetEditing();
            Editing = false;
        }

        /// =========================================
        /// InitializeEditing()
        /// =========================================
        private void InitializeEditing(Tables index)
        {
            oTableTabControl.SelectedIndex = (int)index;

            for (int i = 0; i < oTableTabControl.TabPages.Count; i++)
            {
                if ((Tables)i != index)
                {
                    oTableTabControl.TabPages[i].Enabled = false;
                }
            }
        }

        /// =========================================
        /// ResetEditing()
        /// =========================================
        private void ResetEditing()
        {
            for (int i = 0; i < oTableTabControl.TabPages.Count; i++)
            {
                oTableTabControl.TabPages[i].Enabled = true;
            }
        }

        /// =========================================
        /// ClearAll()
        /// =========================================
        private void ClearAll()
        {
            ClearAbility();
            ClearAmmunition();
            ClearInventory();
            ClearMagic();
            ClearSpell();
            ClearWeapons();
        }

        // =========================================
        /// FillAll()
        /// =========================================
        private void FillAll()
        {
            switch ((Tables)oTableTabControl.SelectedIndex)
            {
                case Tables.Abilities:
                    FillAbility(Data as Ability);
                    break;
                case Tables.Ammunition:
                    FillAmmunition(Data as Ammunition);
                    break;
                case Tables.Inventory:
                    FillInventory(Data as Inventory);
                    break;
                case Tables.Magics:
                    FillMagic(Data as Magic);
                    break;
                case Tables.Spells:
                    FillSpell(Data as Spell);
                    break;
                case Tables.Weapons:
                    FillWeapon(Data as Weapon);
                    break;
            }
        }

        #endregion

        #region Weapon Methods

        /// =========================================
        /// FillWeaponComboBoxes()
        /// =========================================
        private void FillWeaponComboBoxes()
        {
            for (int i = 0; i < Constants.AbilitiesLength(); i++)
                oWeaponsAbilityComboBox.Items.Add(Constants.Ability(i));

            for (int i = 0; i < Constants.DamageTypeLength(); i++)
                oWeaponsDamageTypeComboBox.Items.Add(Constants.DamageType(i));

            for (int i = 0; i < Constants.WeaponListLength(); i++)
                oWeaponNameComboBox.Items.Add(Constants.WeaponList(i));
        }

        /// =========================================
        /// AddWeapon()
        /// =========================================
        private void AddWeapon()
        {
            Weapon weapon = new Weapon()
            {
                Name = oWeaponNameComboBox.Text,
                Ability = oWeaponsAbilityComboBox.Text,
                Damage = oWeaponsDamageTextBox.Text,
                Misc = oWeaponsMiscTextBox.Text,
                Type = oWeaponsDamageTypeComboBox.Text,
                Range = oWeaponsRangeTextBox.Text,
                Notes = oWeaponsNotesTextBox.Text,
                Weight = oWeaponWgtNumericUpDown.Value.ToString(),
                ID = Guid.NewGuid()
            };

            Program.Character.oWeapons.Add(weapon);
        }

        /// =========================================
        /// EditWeapon()
        /// =========================================
        private void EditWeapon(Weapon? weapon)
        {
            if (weapon != null)
            {
                weapon.Name = oWeaponNameComboBox.Text;
                weapon.Ability = oWeaponsAbilityComboBox.Text;
                weapon.Damage = oWeaponsDamageTextBox.Text;
                weapon.Misc = oWeaponsMiscTextBox.Text;
                weapon.Type = oWeaponsDamageTypeComboBox.Text;
                weapon.Range = oWeaponsRangeTextBox.Text;
                weapon.Notes = oWeaponsNotesTextBox.Text;
                weapon.Weight = oWeaponWgtNumericUpDown.Value.ToString();
            }
        }

        /// =========================================
        /// FillWeapon()
        /// =========================================
        private void FillWeapon(Weapon? weapon)
        {
            if (weapon != null)
            {
                oWeaponNameComboBox.Text = weapon.Name;
                oWeaponsAbilityComboBox.Text = weapon.Ability;
                oWeaponsDamageTextBox.Text = weapon.Damage;
                oWeaponsMiscTextBox.Text = weapon.Misc;
                oWeaponsDamageTypeComboBox.Text = weapon.Type;
                oWeaponsRangeTextBox.Text = weapon.Range;
                oWeaponsNotesTextBox.Text = weapon.Notes;
                oWeaponWgtNumericUpDown.Value = decimal.Parse(weapon.Weight);
            }
        }

        /// =========================================
        /// ClearWeapons()
        /// =========================================
        private void ClearWeapons()
        {
            oWeaponNameComboBox.Text = " ";
            oWeaponNameComboBox.SelectedIndex = -1;
            oWeaponsAbilityComboBox.Text = "NONE";
            oWeaponsDamageTextBox.Text = "";
            oWeaponsMiscTextBox.Text = "";
            oWeaponsDamageTypeComboBox.Text = "None";
            oWeaponsRangeTextBox.Text = "";
            oWeaponsNotesTextBox.Text = "";
            oWeaponWgtNumericUpDown.Value = 0;
        }

        #endregion

        #region Ammunition Methods

        /// =========================================
        /// FillAmmunitionComboBoxes()
        /// =========================================
        private void FillAmmunitionComboBoxes()
        {
            for (int i = 0; i < Constants.DamageTypeLength(); i++)
                oAmmoTypeComboBox.Items.Add(Constants.DamageType(i));

            for (int i = 0; i < Constants.AmmoListLength(); i++)
                oAmmoNameComboBox.Items.Add(Constants.AmmoList(i));
        }

        /// =========================================
        /// AddAmmunition()
        /// =========================================
        private void AddAmmunition()
        {
            Ammunition ammunition = new Ammunition()
            {
                Name = oAmmoNameComboBox.Text,
                Quantity = oAmmoQtyNumericUpDown.Value.ToString(),
                Bonus = oAmmoBonusTextBox.Text,
                Type = oAmmoTypeComboBox.Text,
                Used = oAmmoUsedNumericUpDown.Value.ToString(),
                ID = Guid.NewGuid()
            };

            Program.Character.oAmmo.Add(ammunition);
        }

        /// =========================================
        /// EditAmmunition()
        /// =========================================
        private void EditAmmunition(Ammunition? ammo)
        {
            if (ammo != null)
            {
                ammo.Name = oAmmoNameComboBox.Text;
                ammo.Quantity = ((int)oAmmoQtyNumericUpDown.Value).ToString();
                ammo.Bonus = oAmmoBonusTextBox.Text;
                ammo.Type = oAmmoTypeComboBox.Text;
                ammo.Used = ((int)oAmmoUsedNumericUpDown.Value).ToString();
            }
        }

        /// =========================================
        /// FillAmmunition()
        /// =========================================
        private void FillAmmunition(Ammunition? ammo)
        {
            if (ammo != null)
            {
                oAmmoNameComboBox.Text = ammo.Name;
                oAmmoQtyNumericUpDown.Value = decimal.Parse(ammo.Quantity);
                oAmmoBonusTextBox.Text = ammo.Bonus;
                oAmmoTypeComboBox.Text = ammo.Type;
                oAmmoUsedNumericUpDown.Value = decimal.Parse(ammo.Used);
            }
        }

        /// =========================================
        /// ClearAmmunition()
        /// =========================================
        private void ClearAmmunition()
        {
            oAmmoNameComboBox.Text = "";
            oAmmoNameComboBox.SelectedIndex = -1;
            oAmmoQtyNumericUpDown.Value = 0;
            oAmmoBonusTextBox.Text = "";
            oAmmoTypeComboBox.Text = "None";
            oAmmoUsedNumericUpDown.Value = 0;
        }

        #endregion

        #region Inventory Methods

        /// =========================================
        /// FillInventoryComboBoxes()
        /// =========================================
        private void FillInventoryComboBoxes()
        {
            for (int i = 0; i < Constants.ItemListLength(); i++)
                oInventoryNameComboBox.Items.Add(Constants.ItemList(i));
        }

        /// =========================================
        /// AddInventory()
        /// =========================================
        private void AddInventory()
        {
            Inventory inventory = new Inventory()
            {
                Name = oInventoryNameComboBox.Text,
                Amount = oInventoryQtyNumericUpDown.Value.ToString(),
                Weight = oInventoryWgtNumericUpDown.Value.ToString(),
                Note = oInventoryNoteTextBox.Text,
                ID = Guid.NewGuid()
            };

            Program.Character.oInventory.Add(inventory);
        }

        /// =========================================
        /// EditInventory()
        /// =========================================
        private void EditInventory(Inventory? inventory)
        {
            if (inventory != null)
            {
                inventory.Name = oInventoryNameComboBox.Text;
                inventory.Amount = ((int)oInventoryQtyNumericUpDown.Value).ToString();
                inventory.Weight = oInventoryWgtNumericUpDown.Value.ToString();
                inventory.Note = oInventoryNoteTextBox.Text;
            }
        }

        /// =========================================
        /// FillInventory()
        /// =========================================
        private void FillInventory(Inventory? inventory)
        {
            if (inventory != null)
            {
                oInventoryNameComboBox.Text = inventory.Name;
                oInventoryQtyNumericUpDown.Value = decimal.Parse(inventory.Amount);
                oInventoryWgtNumericUpDown.Value = decimal.Parse(inventory.Weight);
                oInventoryNoteTextBox.Text = inventory.Note;
            }
        }

        /// =========================================
        /// ClearInventory()
        /// =========================================
        private void ClearInventory()
        {
            oInventoryNameComboBox.Text = "";
            oInventoryNameComboBox.SelectedIndex = -1;
            oInventoryQtyNumericUpDown.Value = 0;
            oInventoryWgtNumericUpDown.Value = 0;
            oInventoryNoteTextBox.Text = "";
        }

        #endregion

        #region Ability Methods

        /// =========================================
        /// AddAbility()
        /// =========================================
        private void AddAbility()
        {
            Ability ability = new Ability()
            {
                Name = oAbilityNameTextBox.Text,
                Level = oAbilityLevelTextBox.Text,
                Uses = oAbilityUsesTextBox.Text,
                Recovery = oAbilityRecoveryTextBox.Text,
                Action = oAbilityActionTextBox.Text,
                Note = oAbilityNotesTextBox.Text,
                ID = Guid.NewGuid()
            };

            Program.Character.oAbility.Add(ability);
        }

        /// =========================================
        /// EditAbility()
        /// =========================================
        private void EditAbility(Ability? ability)
        {
            if (ability != null)
            {
                ability.Name = oAbilityNameTextBox.Text;
                ability.Level = oAbilityLevelTextBox.Text;
                ability.Uses = oAbilityUsesTextBox.Text;
                ability.Recovery = oAbilityRecoveryTextBox.Text;
                ability.Action = oAbilityActionTextBox.Text;
                ability.Note = oAbilityNotesTextBox.Text;
            }
        }

        /// =========================================
        /// FillAbility()
        /// =========================================
        private void FillAbility(Ability? ability)
        {
            if (ability != null)
            {
                oAbilityNameTextBox.Text = ability.Name;
                oAbilityLevelTextBox.Text = ability.Level;
                oAbilityUsesTextBox.Text = ability.Uses;
                oAbilityRecoveryTextBox.Text = ability.Recovery;
                oAbilityActionTextBox.Text = ability.Action;
                oAbilityNotesTextBox.Text = ability.Note;
            }
        }

        /// =========================================
        /// ClearAbility()
        /// =========================================
        private void ClearAbility()
        {
            oAbilityNameTextBox.Text = "";
            oAbilityLevelTextBox.Text = "";
            oAbilityUsesTextBox.Text = "";
            oAbilityRecoveryTextBox.Text = "";
            oAbilityActionTextBox.Text = "";
            oAbilityNotesTextBox.Text = "";
        }

        #endregion

        #region Magic Methods

        /// =========================================
        /// FillMagicComboBoxes()
        /// =========================================
        private void FillMagicComboBoxes()
        {
            for (int i = 0; i < Constants.AbilitiesLength(); i++)
                oMagicAbilityComboBox.Items.Add(Constants.Ability(i));
        }

        /// =========================================
        /// AddMagic()
        /// =========================================
        private void AddMagic()
        {
            Magic magic = new Magic()
            {
                Class = oMagicClassTextBox.Text,
                Ability = oMagicAbilityComboBox.Text,
                Cantrips = oMagicCantripsNumericUpDown.Value.ToString(),
                Spells = oMagicSpellsNumericUpDown.Value.ToString(),
                Prepared = oMagicPreparedNumericUpDown.Value.ToString(),
                ID = Guid.NewGuid()
            };

            Program.Character.Spellcasting.oMagic.Add(magic);
        }

        /// =========================================
        /// EditMagic()
        /// =========================================
        private void EditMagic(Magic? magic)
        {
            if (magic != null)
            {
                magic.Class = oMagicClassTextBox.Text;
                magic.Ability = oMagicAbilityComboBox.Text;
                magic.Cantrips = ((int)oMagicCantripsNumericUpDown.Value).ToString();
                magic.Spells = ((int)oMagicSpellsNumericUpDown.Value).ToString();
                magic.Prepared = ((int)oMagicPreparedNumericUpDown.Value).ToString();
            }
        }

        /// =========================================
        /// FillMagic()
        /// =========================================
        private void FillMagic(Magic? magic)
        {
            if (magic != null)
            {
                oMagicClassTextBox.Text = magic.Class;
                oMagicAbilityComboBox.Text = magic.Ability;
                oMagicCantripsNumericUpDown.Value = decimal.Parse(magic.Cantrips);
                oMagicSpellsNumericUpDown.Value = decimal.Parse(magic.Spells);
                oMagicPreparedNumericUpDown.Value = decimal.Parse(magic.Prepared);
            }
        }

        /// =========================================
        /// ClearMagic()
        /// =========================================
        private void ClearMagic()
        {
            oMagicClassTextBox.Text = "";
            oMagicAbilityComboBox.Text = "NONE";
            oMagicCantripsNumericUpDown.Value = 0;
            oMagicSpellsNumericUpDown.Value = 0;
            oMagicPreparedNumericUpDown.Value = 0;
        }

        #endregion

        #region Spell Methods

        /// =========================================
        /// FillSpellComboBoxes()
        /// =========================================
        private void FillSpellComboBoxes()
        {
            for (int i = 0; i < Constants.SpellListLength(); i++)
                oSpellNameComboBox.Items.Add(Constants.SpellList(i));

            for (int i = 0; i < Constants.SchoolLength(); i++)
                oSpellSchoolComboBox.Items.Add(Constants.School(i));

            for (int i = 0; i < Constants.YesNoLength(); i++)
            {
                oSpellPreparedComboBox.Items.Add(Constants.YesNo(i));
                oSpellRitualComboBox.Items.Add(Constants.YesNo(i));
                oSpellConcenComboBox.Items.Add(Constants.YesNo(i));
            }
        }

        /// =========================================
        /// AddNote()
        /// =========================================
        private void AddSpell()
        {
            Spell spell = new Spell()
            {
                Name = oSpellNameComboBox.Text,
                Level = oSpellLevelTextBox.Text,
                Page = oSpellPageTextBox.Text,
                School = oSpellSchoolComboBox.Text,
                Ritual = oSpellRitualComboBox.Text,
                Components = oSpellCompTextBox.Text,
                Concentration = oSpellConcenComboBox.Text,
                Range = oSpellRangeTextBox.Text,
                Duration = oSpellDurationTextBox.Text,
                Area = oSpellAreaTextBox.Text,
                Save = oSpellSaveTextBox.Text,
                Damage = oSpellDamageTextBox.Text,
                Description = oSpellDescriptionTextBox.Text,
                Prepared = oSpellPreparedComboBox.Text,
                ID = Guid.NewGuid()
            };

            Program.Character.Spellcasting.oSpells.Add(spell);
        }

        /// =========================================
        /// EditNote()
        /// =========================================
        private void EditSpell(Spell? spell)
        {
            if (spell != null)
            {
                spell.Name = oSpellNameComboBox.Text;
                spell.Level = oSpellLevelTextBox.Text;
                spell.Page = oSpellPageTextBox.Text;
                spell.School = oSpellSchoolComboBox.Text;
                spell.Ritual = oSpellRitualComboBox.Text;
                spell.Components = oSpellCompTextBox.Text;
                spell.Concentration = oSpellConcenComboBox.Text;
                spell.Range = oSpellRangeTextBox.Text;
                spell.Duration = oSpellDurationTextBox.Text;
                spell.Area = oSpellAreaTextBox.Text;
                spell.Save = oSpellSaveTextBox.Text;
                spell.Damage = oSpellDamageTextBox.Text;
                spell.Description = oSpellDescriptionTextBox.Text;
                spell.Prepared = oSpellPreparedComboBox.Text;
            }
        }

        /// =========================================
        /// FillNote()
        /// =========================================
        private void FillSpell(Spell? spell)
        {
            if (spell != null)
            {
                oSpellNameComboBox.Text = spell.Name;
                oSpellLevelTextBox.Text = spell.Level;
                oSpellPageTextBox.Text = spell.Page;
                oSpellSchoolComboBox.Text = spell.School;
                oSpellRitualComboBox.Text = spell.Ritual;
                oSpellCompTextBox.Text = spell.Components;
                oSpellConcenComboBox.Text = spell.Concentration;
                oSpellRangeTextBox.Text = spell.Range;
                oSpellDurationTextBox.Text = spell.Duration;
                oSpellAreaTextBox.Text = spell.Area;
                oSpellSaveTextBox.Text = spell.Save;
                oSpellDamageTextBox.Text = spell.Damage;
                oSpellDescriptionTextBox.Text = spell.Description;
                oSpellPreparedComboBox.Text = spell.Prepared;
            }
        }

        /// =========================================
        /// ClearNote()
        /// =========================================
        private void ClearSpell()
        {
            oSpellNameComboBox.Text = "";
            oSpellNameComboBox.SelectedIndex = -1;
            oSpellLevelTextBox.Text = "";
            oSpellPageTextBox.Text = "";
            oSpellSchoolComboBox.Text = "Universal";
            oSpellRitualComboBox.Text = "No";
            oSpellCompTextBox.Text = "";
            oSpellConcenComboBox.Text = "No";
            oSpellRangeTextBox.Text = "";
            oSpellDurationTextBox.Text = "";
            oSpellAreaTextBox.Text = "";
            oSpellSaveTextBox.Text = "";
            oSpellDamageTextBox.Text = "";
            oSpellDescriptionTextBox.Text = "";
            oSpellPreparedComboBox.Text = "No";
        }

        #endregion

        #region Accessors

        private bool Editing
        {
            get;
            set;
        }

        private object Data
        {
            get;
            set;
        }

        #endregion

        #region Events

        /// =========================================
        /// TablePage_FormClosing()
        /// =========================================
        private void TablePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearAll();
        }

        /// =========================================
        /// btnClose_Click()
        /// =========================================
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// =========================================
        /// btnReset_Click()
        /// =========================================
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Editing)
            {
                FillAll();
            }
            else
            {
                ClearAll();
            }
        }

        /// =========================================
        /// btnAdd_Click()
        /// =========================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch ((Tables)oTableTabControl.SelectedIndex)
            {
                //------------------------------------
                case Tables.Abilities:
                    if (Editing)
                    {
                        EditAbility(Data as Ability);
                        ClearAbility();
                        Close();
                    }
                    else
                    {
                        AddAbility();
                        ClearAbility();
                    }
                    break;
                //------------------------------------
                case Tables.Ammunition:
                    if (Editing)
                    {
                        EditAmmunition(Data as Ammunition);
                        ClearAmmunition();
                        Close();
                    }
                    else
                    {
                        AddAmmunition();
                        ClearAmmunition();
                    }
                    break;
                //------------------------------------
                case Tables.Inventory:
                    if (Editing)
                    {
                        EditInventory(Data as Inventory);
                        ClearInventory();
                        Close();
                    }
                    else
                    {
                        AddInventory();
                        ClearInventory();
                    }
                    break;
                //------------------------------------
                case Tables.Magics:
                    if (Editing)
                    {
                        EditMagic(Data as Magic);
                        ClearMagic();
                        Close();
                    }
                    else
                    {
                        AddMagic();
                        ClearMagic();
                    }
                    break;
                //------------------------------------
                case Tables.Spells:
                    if (Editing)
                    {
                        EditSpell(Data as Spell);
                        ClearSpell();
                        Close();
                    }
                    else
                    {
                        AddSpell();
                        ClearSpell();
                    }
                    break;
                //------------------------------------
                case Tables.Weapons:
                    if (Editing)
                    {
                        EditWeapon(Data as Weapon);
                        ClearWeapons();
                        Close();
                    }
                    else
                    {
                        AddWeapon();
                        ClearWeapons();
                    }
                    break;
            }
        }

        /// =========================================
        /// oTableTabControl_Selecting()
        /// =========================================
        private void oTableTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex >= 0 && e.TabPageIndex < oTableTabControl.TabPages.Count)
            {
                e.Cancel = !e.TabPage.Enabled;
            }
        }

        /// =========================================
        /// oWeaponNameComboBox_SelectionChangeCommitted()
        /// =========================================
        private void oWeaponNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Weapon? weapon = oWeaponNameComboBox.SelectedItem as Weapon;

            if (weapon != null)
            {
                oWeaponNameComboBox.Text = weapon.Name;
                oWeaponsAbilityComboBox.Text = "";
                oWeaponsDamageTextBox.Text = weapon.Damage;
                oWeaponsMiscTextBox.Text = "";
                oWeaponsDamageTypeComboBox.Text = weapon.Type;
                oWeaponsRangeTextBox.Text = weapon.Range;
                oWeaponsNotesTextBox.Text = weapon.Notes;
                oWeaponWgtNumericUpDown.Value = decimal.Parse(weapon.Weight);
            }
        }

        /// =========================================
        /// oInventoryNameComboBox_SelectionChangeCommitted()
        /// =========================================
        private void oInventoryNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Inventory? inventory = oInventoryNameComboBox.SelectedItem as Inventory;

            if (inventory != null)
            {
                oInventoryNameComboBox.Text = inventory.Name;
                oInventoryQtyNumericUpDown.Value = 1;
                oInventoryWgtNumericUpDown.Value = decimal.Parse(inventory.Weight);
                oInventoryNoteTextBox.Text = inventory.Note;
            }
        }

        /// =========================================
        /// oAmmoNameComboBox_SelectionChangeCommitted()
        /// =========================================
        private void oAmmoNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Ammunition? ammunition = oAmmoNameComboBox.SelectedItem as Ammunition;

            if (ammunition != null)
            {
                oAmmoNameComboBox.Text = ammunition.Name;
                oAmmoQtyNumericUpDown.Value = decimal.Parse(ammunition.Quantity);
                oAmmoBonusTextBox.Text = "";
                oAmmoTypeComboBox.Text = "None";
                oAmmoUsedNumericUpDown.Value = 0;
            }
        }

        /// =========================================
        /// oSpellNameComboBox_SelectionChangeCommitted()
        /// =========================================
        private void oSpellNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Spell? spell = oSpellNameComboBox.SelectedItem as Spell;

            if (spell != null)
            {
                oSpellNameComboBox.Text = spell.Name;
                oSpellLevelTextBox.Text = spell.Level;
                oSpellPageTextBox.Text = spell.Page;
                oSpellSchoolComboBox.Text = spell.School;
                oSpellRitualComboBox.Text = spell.Ritual;
                oSpellCompTextBox.Text = spell.Components;
                oSpellConcenComboBox.Text = spell.Concentration;
                oSpellRangeTextBox.Text = spell.Range;
                oSpellDurationTextBox.Text = spell.Duration;
                oSpellAreaTextBox.Text = spell.Area;
                oSpellSaveTextBox.Text = spell.Save;
                oSpellDamageTextBox.Text = spell.Damage;
                oSpellDescriptionTextBox.Text = spell.Description;
                oSpellPreparedComboBox.Text = spell.Prepared;
            }
        }

        #endregion

    }
}
