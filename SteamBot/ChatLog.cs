using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MistClient
{
    public partial class ChatLog : Form
    {
        string sid;

        public ChatLog(string name, string sid, string log = "No chat log exists for this user.")
        {
            InitializeComponent();
            if (log == "")
            {
                log = "No chat log exists for this user.";
            }
            this.label1.Text = "";
            this.Text = "Chat Log Between You and " + name;
            this.textBox1.AppendText(log);
            this.sid = sid;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.label1_Enter(sender, e);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void label1_Enter(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.label1_Click(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void clearChatLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "logs");
            string file = Path.Combine(path, sid + ".txt");
            if (File.Exists(file))
            {
                File.WriteAllText(file, null);
                textBox1.Clear();
                textBox1.Text = "No chat log exists for this user.";
            }
        }
    }
}
