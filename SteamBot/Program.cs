using System;
using System.Threading;
using System.Windows.Forms;
using MistClient;
using SteamKit2;
using System.IO;
using SteamTrade;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace SteamBot
{
    public class Program
    {
        private static Mutex mutex;

        [STAThread]
        public static void Main(string[] args)
        {   
            Application.EnableVisualStyles();
            string path = Path.Combine(Environment.ExpandEnvironmentVariables("%systemroot%"), @"Microsoft.NET\Framework\v4.0.30319\System.Core.dll");
            if (!File.Exists(path))
            {
                DialogResult userChoice = MessageBox.Show("It seems that you don't have .NET 4.0 installed. Would you like to be directed to the download page? " 
                                                            + "If you are sure you do have .NET 4.0 installed, click No to simply close this message box. "
                                                            + "Click Cancel to close Mist.\n\n"
                                                            + ".NET 4.0 can be found at http://www.microsoft.com/en-ca/download/details.aspx?id=17851",
                                                            ".NET 4.0 Not Detected",
                                                            MessageBoxButtons.YesNoCancel,
                                                            MessageBoxIcon.Hand,
                                                            MessageBoxDefaultButton.Button1);
                if (userChoice == DialogResult.Yes)
                {             
                    System.Diagnostics.Process.Start("http://www.microsoft.com/en-ca/download/details.aspx?id=17851");
                    Environment.Exit(0);
                    return;
                }
                else if (userChoice == DialogResult.No)
                {

                }
                else if (userChoice == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                    return;
                }
            }        
            CleanUp();
            string LogDirectory = Path.Combine(Application.StartupPath, "logs");
            if (!Directory.Exists(LogDirectory))
            {
                try
                {
                    Directory.CreateDirectory(LogDirectory); // try making the log directory
                    Console.WriteLine("Creating log directory.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to create log directory.\n{0}", ex.ToString());
                    return;
                }
            }
            Log mainLog;
            bool created;
            mutex = new Mutex(false, "Mist-AF12AF2", out created);
            if (!created)
            {
                int pid = 0;
                try
                {
                    Random random = new Random();
                    pid = random.Next(0, 9999);
                    pid += System.Diagnostics.Process.GetProcessesByName("Mist")[0].Id;
                }
                catch
                {
                    Random random = new Random();
                    pid = random.Next(0, 9999);
                }
                mainLog = new Log(@"logs/Mist-" + pid + ".log", "Mist", Log.LogLevel.Debug);
            }
            else
            {
                mainLog = new Log(@"logs/Mist.log", "Mist", Log.LogLevel.Debug);
            }
            
            Login login = new Login(mainLog);
            mainLog.Info("Launching Mist...");
            Configuration config = new Configuration();
            Configuration.BotInfo info = new Configuration.BotInfo();
            info.BotControlClass = "SteamBot.SimpleUserHandler";
            new Thread(() =>
            {
                while (!Login.LoginClicked)
                {
                    
                }
                int crashes = 0;
                while (crashes < 100)
                {
                    try
                    {
                        new Bot(info, mainLog, login.APIKey, (Bot bot, SteamID sid) => {
                                    
                            return (SteamBot.UserHandler)System.Activator.CreateInstance(Type.GetType(bot.BotControlClass), new object[] { bot, sid });  
                        }, login, false);

                    }
                    catch (Exception e)
                    {
                        mainLog.Error("Error With Bot: " + e);
                        crashes++;
                    }
                }
            }).Start();
            Application.Run(login);
        }

        static void CleanUp()
        {
            int max = 5;
            for (int count = 0; count < max; count++)
            {
                try
                {
                    foreach (var file in Directory.GetFiles(Application.StartupPath))
                    {
                        if (file.EndsWith(".old") || file.EndsWith(".PendingOverwrite") || file.EndsWith(".tmp"))
                        {
                            Console.WriteLine("Deleting {0}...", file);
                            File.Delete(file);
                        }
                    }
                    foreach (var file in Directory.GetFiles(Path.Combine(Application.StartupPath, "lib")))
                    {
                        if (file.EndsWith(".old") || file.EndsWith(".PendingOverwrite") || file.EndsWith(".tmp"))
                        {
                            Console.WriteLine("Deleting {0}...", file);
                            File.Delete(file);
                        }
                    }
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
