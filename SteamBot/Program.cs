using System;
using System.Threading;
using System.Windows.Forms;
using MistClient;
using SteamKit2;
using SteamTrade;

namespace SteamBot
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Login login = new Login();
            Log mainLog = new Log(@"MistLog.txt", null);
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
