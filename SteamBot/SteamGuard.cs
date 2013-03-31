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
    public partial class SteamGuard : MetroForm
    {
        public static string AuthCode;
        public bool submitted = false;

        public SteamGuard(string title = null)
        {
            InitializeComponent();
            if (title != null)
                label1.Text = title;
            metroStyleManager1.Theme = Friends.globalStyleManager.Theme;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (AuthCode != "")
            {
                AuthCode = text_auth.Text;
                submitted = true;
                this.Close();
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
    }
}
