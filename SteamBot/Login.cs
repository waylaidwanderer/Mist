using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using SteamBot;

namespace MistClient
{
    public partial class Login : Form
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public string Username;
        public string Password;
        public string APIKey;
        public static bool LoginClicked = false;
        public bool wrongAPI = false;
        Log log;

        public Login(Log log)
        {
            InitializeComponent();
            this.Text = "Login - Mist v" + Friends.mist_ver;
            MakePortable(Properties.Settings.Default);
            this.log = log;
        }

        private static void MakePortable(ApplicationSettingsBase settings)
        {
            var portableSettingsProvider =
                new PortableSettingsProvider("Mist.settings");
            settings.Providers.Add(portableSettingsProvider);
            foreach (System.Configuration.SettingsProperty prop in settings.Properties)
                prop.Provider = portableSettingsProvider;
            settings.Reload();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            text_username.Text = text_username.Text.Trim();
            text_api.Text = text_api.Text.Trim();
            if (text_username.Text != "" && text_password.Text != "" && text_api.Text != "")
            {
                Properties.Settings.Default.apiKey = text_api.Text;
                Properties.Settings.Default.Username = text_username.Text;
                if (check_remember.Checked)
                    Properties.Settings.Default.Password = Encrypt(text_password.Text);
                else
                    Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
                Username = text_username.Text;
                Password = text_password.Text;
                APIKey = text_api.Text;
                LoginClicked = true;
                text_username.Enabled = false;
                text_password.Enabled = false;
                text_api.Enabled = false;
                button_login.Enabled = false;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            updatechecker.RunWorkerAsync();
            label4.ForeColor = Color.Blue;
            label4.Font =
            new Font
            (
            label4.Font,
            FontStyle.Underline
            );
            if (Properties.Settings.Default.Username != "")
                text_username.Text = Properties.Settings.Default.Username;
            if (Properties.Settings.Default.apiKey != "")
                text_api.Text = Properties.Settings.Default.apiKey;
            if (Properties.Settings.Default.Password != "")
            {
                text_password.Text = Decrypt(Properties.Settings.Default.Password);
                check_remember.Checked = true;
            }
        }

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (wrongAPI)
            {
                // Starts a new instance of the program itself
                var filename = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.Diagnostics.Process.Start(filename);
            }
            else
            {
                Application.Exit();
                Environment.Exit(0);
            }
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            ToolTip APIToolTip = new ToolTip();
            APIToolTip.ToolTipIcon = ToolTipIcon.Info;
            APIToolTip.IsBalloon = true;
            APIToolTip.ShowAlways = true;
            APIToolTip.ToolTipTitle = "What's this?";
            APIToolTip.SetToolTip(label4, "Don't have an API key? Click me!");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            string message = "Would you like to be taken to http://steamcommunity.com/dev/apikey to get an API key? Click No to simply close this message box.";
            DialogResult choice = MessageBox.Show(new Form() { TopMost = true }, message,
                            "Steam API Key",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1);
            switch (choice)
            {
                case DialogResult.Yes:
                    System.Diagnostics.Process.Start("http://steamcommunity.com/dev/apikey");
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void updatechecker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = "http://www.thectscommunity.com/dev/update_check.php";
            string response = Util.HTTPRequest(url);
            if (response != "")
            {
                string latestVer = Util.ParseBetween(response, "<version>", "</version>");
                if (Properties.Settings.Default.SkipUpdate && Properties.Settings.Default.SkippedVersion != latestVer.Trim())
                {
                    Properties.Settings.Default.SkipUpdate = false;
                    Properties.Settings.Default.Save();
                    string changelog = Util.ParseBetween(response, "<changelog>", "</changelog>");
                    Updater updater = new Updater(latestVer, changelog, log);
                    updater.ShowDialog();
                    updater.Activate();
                }
                else if (!Properties.Settings.Default.SkipUpdate && latestVer.Trim() != Friends.mist_ver)
                {
                    string changelog = Util.ParseBetween(response, "<changelog>", "</changelog>");
                    Updater updater = new Updater(latestVer, changelog, log);
                    updater.ShowDialog();
                    updater.Activate();
                }
                else
                {

                }
            }
        }
    }
}
