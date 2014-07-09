using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MetroFramework.Forms;

namespace MistClient
{
    public partial class AddFriend : MetroForm
    {
        SteamBot.Bot bot;

        public AddFriend(SteamBot.Bot bot)
        {
            InitializeComponent();
            this.bot = bot;
            Util.LoadTheme(this, this.Controls);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (text_profile.Text == "" || Regex.IsMatch(text_profile.Text, "^[A-Za-z]$"))
            {
                MetroFramework.MetroMessageBox.Show(this, "The SteamID64 is invalid. It cannot be blank or contain letters.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            else
            {
                SteamKit2.SteamID id = Convert.ToUInt64(text_profile.Text);
                bot.SteamFriends.AddFriend(id);
                this.Close();
                MetroFramework.MetroMessageBox.Show(this, "Friend added successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
