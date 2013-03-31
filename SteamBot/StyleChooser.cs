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
    public partial class StyleChooser : MetroForm
    {
        bool light = false;
        bool dark = false;
        public StyleChooser(string Theme)
        {
            InitializeComponent();
            Util.LoadTheme(metroStyleManager1);
            if (Theme == "Light")
                light = true;
            if (Theme == "Dark")
                dark = true;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string selection = metroComboBox1.SelectedItem.ToString();
            if (selection == "")
            {
                return;
            }
            SetStyle(selection);
            Friends.RefreshTheme();
            this.Close();
        }

        private void SetStyle(string Style)
        {
            if (light)
            {
                Friends.globalStyleManager.Theme = MetroFramework.MetroThemeStyle.Light;
                Properties.Settings.Default.Theme = "Light";
            }
            if (dark)
            {
                Friends.globalStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
                Properties.Settings.Default.Theme = "Dark";
            }
            if (Style == "Blue")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Blue;
            if (Style == "Black")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Black;
            if (Style == "Brown")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Brown;
            if (Style == "Green")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Green;
            if (Style == "Lime")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Lime;
            if (Style == "Magenta")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Magenta;
            if (Style == "Orange")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Orange;
            if (Style == "Pink")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Pink;
            if (Style == "Purple")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Purple;
            if (Style == "Red")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Red;
            if (Style == "Silver")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Silver;
            if (Style == "Teal")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Teal;
            if (Style == "White")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.White;
            if (Style == "Yellow")
                Friends.globalStyleManager.Style = MetroFramework.MetroColorStyle.Yellow;
            Properties.Settings.Default.Style = Style;
            Properties.Settings.Default.Save();
        }
    }
}
