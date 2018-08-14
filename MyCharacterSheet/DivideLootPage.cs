using MyCharacterSheet.Divide_Loot;
using MyCharacterSheet.Utility;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyCharacterSheet
{
    public partial class DivideLootPage : Form
    {

        #region Constants

        public const int CURRENCIES = 5;

        #endregion

        #region Members

        private List<Player>   players = new List<Player>();

        #endregion

        #region Constructor

        /// =========================================
        /// DivideLootPage()
        /// =========================================
        public DivideLootPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// =========================================
        /// ShowPane()
        /// =========================================
        public void ShowPane()
        {
            defaultValues();
            ShowDialog();
        }

        /// =========================================
        /// defaultValues()
        /// =========================================
        private void defaultValues()
        {
            oPlayerInput.Text = "0";
            oCopperInput.Text = "0";
            oSilverInput.Text = "0";
            oElectrumInput.Text = "0";
            oGoldInput.Text = "0";
            oPlatinumInput.Text = "0";
        }

        /// =========================================
        /// fillList()
        /// =========================================
        private void fillList()
        {
            foreach (Player player in players)
            {
                int index = oDivideLootGrid.Rows.Add();
                DataGridViewRow row = oDivideLootGrid.Rows[index];

                row.Cells[PlayerName.Index].Value = player.Name;
                row.Cells[TotalValue.Index].Value = player.Total;
                row.Cells[CopperValue.Index].Value = player.Copper;
                row.Cells[SilverValue.Index].Value = player.Silver;
                row.Cells[ElectrumValue.Index].Value = player.Electrum;
                row.Cells[GoldValue.Index].Value = player.Gold;
                row.Cells[PlatinumValue.Index].Value = player.Platinum;
            }
        }

        /// =========================================
        /// parsePlayers()
        /// =========================================
        private void parsePlayers()
        {
            int numPlayers;

            numPlayers = int.Parse(oPlayerInput.Text);
            players.Clear();
            for (int i = 0; i < numPlayers; i++)
                players.Add(new Player("Player " + (i + 1)));
        }

        /// =========================================
        /// parseLoot()
        /// =========================================
        private Player parseLoot()
        {
            int cp, sp, ep, gp, pp;
            Player loot;

            cp = int.Parse(oCopperInput.Text);
            sp = int.Parse(oSilverInput.Text);
            ep = int.Parse(oElectrumInput.Text);
            gp = int.Parse(oGoldInput.Text);
            pp = int.Parse(oPlatinumInput.Text);

            loot = new Player(cp, sp, ep, gp, pp);

            return loot;
        }

        /// =========================================
        /// distribute()
        /// =========================================
        private void distribute(Player loot)
        {
            bool end = false;
            double maxValue = loot.Total/players.Count;

            if (players.Count > 0)
            {
                for (int i = 0; i < CURRENCIES; i++)
                {
                    while (loot.currency[i] > 0)
                    {
                        for (int j = 0; j < players.Count && !end; j++)
                        {
                            if (loot.currency[i] < 1)
                            {
                                end = true;
                            }
                            else if (players[j].Total < maxValue)
                            {
                                players[j].currency[i]++;
                                loot.currency[i]--;
                            }
                        }
                        end = false;
                    }
                }
            }
        }

        #endregion

        #region Events

        /// =========================================
        /// btnReset_Click()
        /// =========================================
        private void btnReset_Click(object sender, EventArgs e)
        {
            Sounds.ButtonClick();
            defaultValues();
            oDivideLootGrid.Rows.Clear();
        }

        /// =========================================
        /// btnDivideLoot_Click()
        /// =========================================
        private void btnDivideLoot_Click(object sender, EventArgs e)
        {
            Player loot;

            Sounds.ButtonClick();
            oDivideLootGrid.Rows.Clear();

            parsePlayers();
            loot = parseLoot();

            distribute(loot);

            fillList();
        }

        /// =========================================
        /// oPlayerInput_Leave()
        /// =========================================
        private void oPlayerInput_Leave(object sender, EventArgs e)
        {
            int result;

            if (!int.TryParse(oPlayerInput.Text, out result))
            {
                oPlayerInput.Text = "0";
            }
        }

        /// =========================================
        /// oCopperInput_Leave()
        /// =========================================
        private void oCopperInput_Leave(object sender, EventArgs e)
        {
            int result;

            if (!int.TryParse(oCopperInput.Text, out result))
            {
                oCopperInput.Text = "0";
            }
        }

        /// =========================================
        /// oSilverInput_Leave()
        /// =========================================
        private void oSilverInput_Leave(object sender, EventArgs e)
        {
            int result;

            if (!int.TryParse(oSilverInput.Text, out result))
            {
                oSilverInput.Text = "0";
            }
        }

        /// =========================================
        /// oElectrumInput_Leave()
        /// =========================================
        private void oElectrumInput_Leave(object sender, EventArgs e)
        {
            int result;

            if (!int.TryParse(oElectrumInput.Text, out result))
            {
                oElectrumInput.Text = "0";
            }
        }

        /// =========================================
        /// oGoldInput_Leave()
        /// =========================================
        private void oGoldInput_Leave(object sender, EventArgs e)
        {
            int result;

            if (!int.TryParse(oGoldInput.Text, out result))
            {
                oGoldInput.Text = "0";
            }
        }

        /// =========================================
        /// oPlatinumInput_Leave()
        /// =========================================
        private void oPlatinumInput_Leave(object sender, EventArgs e)
        {
            int result;

            if (!int.TryParse(oPlatinumInput.Text, out result))
            {
                oPlatinumInput.Text = "0";
            }
        }

        /// =========================================
        /// oPlayerInput_Enter()
        /// =========================================
        private void oPlayerInput_Enter(object sender, EventArgs e)
        {
            if (oPlayerInput.Text.Equals("0"))
            {
                oPlayerInput.Text = "";
            }
        }

        /// =========================================
        /// oCopperInput_Enter()
        /// =========================================
        private void oCopperInput_Enter(object sender, EventArgs e)
        {
            if (oCopperInput.Text.Equals("0"))
            {
                oCopperInput.Text = "";
            }
        }

        /// =========================================
        /// oSilverInput_Enter()
        /// =========================================
        private void oSilverInput_Enter(object sender, EventArgs e)
        {
            if (oSilverInput.Text.Equals("0"))
            {
                oSilverInput.Text = "";
            }
        }

        /// =========================================
        /// oElectrumInput_Enter()
        /// =========================================
        private void oElectrumInput_Enter(object sender, EventArgs e)
        {
            if (oElectrumInput.Text.Equals("0"))
            {
                oElectrumInput.Text = "";
            }
        }

        /// =========================================
        /// oGoldInput_Enter()
        /// =========================================
        private void oGoldInput_Enter(object sender, EventArgs e)
        {
            if (oGoldInput.Text.Equals("0"))
            {
                oGoldInput.Text = "";
            }
        }

        /// =========================================
        /// oPlatinumInput_Enter()
        /// =========================================
        private void oPlatinumInput_Enter(object sender, EventArgs e)
        {
            if (oPlatinumInput.Text.Equals("0"))
            {
                oPlatinumInput.Text = "";
            }
        }

        /// =========================================
        /// DivideLootPage_KeyDown()
        /// =========================================
        private void DivideLootPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        #endregion

    }
}
