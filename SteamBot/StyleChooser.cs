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
            Util.LoadTheme(this, this.Controls);
            metroComboBox1.SelectedIndex = 0;
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
            this.Close();
        }

        private void SetStyle(string style)
        {
            if (light)
            {
                Friends.GlobalStyleManager.Theme = MetroFramework.MetroThemeStyle.Light;
                Properties.Settings.Default.Theme = "Light";
            }
            if (dark)
            {
                Friends.GlobalStyleManager.Theme = MetroFramework.MetroThemeStyle.Dark;
                Properties.Settings.Default.Theme = "Dark";
            }
            if (style == "Blue")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Blue;
            if (style == "Black")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Black;
            if (style == "Brown")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Brown;
            if (style == "Green")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Green;
            if (style == "Lime")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Lime;
            if (style == "Magenta")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Magenta;
            if (style == "Orange")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Orange;
            if (style == "Pink")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Pink;
            if (style == "Purple")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Purple;
            if (style == "Red")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Red;
            if (style == "Silver")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Silver;
            if (style == "Teal")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Teal;
            if (style == "White")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.White;
            if (style == "Yellow")
                Friends.GlobalStyleManager.Style = MetroFramework.MetroColorStyle.Yellow;
            Properties.Settings.Default.Style = style;
            Properties.Settings.Default.Save();
            Util.LoadTheme(this, this.Controls);
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selection = metroComboBox1.SelectedItem.ToString();
            SetStyle(selection);
        }
    }
}
