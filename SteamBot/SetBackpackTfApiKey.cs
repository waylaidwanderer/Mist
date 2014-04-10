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
    public partial class SetBackpackTfApiKey : MetroForm
    {
        public SetBackpackTfApiKey()
        {
            InitializeComponent();
            Util.LoadTheme(metroStyleManager1);
            text_apikey.Text = Properties.Settings.Default.backpackTfApiKey;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.backpackTfApiKey = text_apikey.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
