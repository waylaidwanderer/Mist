using System;
using System.Threading;
using System.Windows.Forms;
using MistClient;
using SteamKit2;
using System.IO;
using SteamTrade;

namespace SteamBot
{
    public class Program
    {
        private static Mutex mutex;

        [STAThread]
        public static void Main(string[] args)
        {
            string LogDirectory = Path.Combine(Application.StartupPath, "logs");
            if (!Directory.Exists(LogDirectory))
            {
                try
                {
                    Directory.CreateDirectory(LogDirectory); // try making the cache directory
                    Console.WriteLine("Creating log directory.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to create log directory.\n{0}", ex.ToString());
                    return;
                }
            }
            Log mainLog = new Log(@"logs/Mist.log", null);
            bool created;
            mutex = new Mutex(false, "Mist-AF12AF2", out created);
            if (!created)
            {
                int pid = 0;
                try
                {
                    pid = System.Diagnostics.Process.GetProcessesByName("Mist")[0].Id;
                }
                catch
                {
                    Random random = new Random();
                    pid = random.Next(0, 9999);
                }
                mainLog = new Log(@"logs/Mist-" + pid + ".log", null);
            }
            Application.EnableVisualStyles();
            Login login = new Login();
            mainLog.Info("Launching Mist...");
            Configuration config = new Configuration();
            Configuration.BotInfo info = new Configuration.BotInfo();
            info.BotControlClass = "SteamBot.SimpleUserHandler";
            login.Show();
            login.Activate();    
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
                        new Bot(info, login.APIKey, (Bot bot, SteamID sid) => {
                                    
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
    }
}
