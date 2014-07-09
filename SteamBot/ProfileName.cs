using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MistClient
{
    public partial class ProfileName : MetroForm
    {
        SteamBot.Bot bot;

        public ProfileName(SteamBot.Bot bot)
        {
            InitializeComponent();
            this.bot = bot;
            Util.LoadTheme(this, this.Controls);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (text_newprofile.Text != "")
            {                
                bot.SteamFriends.SetPersonaName(text_newprofile.Text.Trim());
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
