using System;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Windows.Forms;
using MistClient;

using SteamKit2;
using SteamTrade;
using System.Media;
using ToastNotifications;
using SteamKit2.Internal;
using SteamBot.SteamGroups;

namespace SteamBot
{
    public class Bot
    {
        public string BotControlClass;
        // If the bot is logged in fully or not.  This is only set
        // when it is.
        public bool IsLoggedIn = false;

        // The response to all chat messages sent to it.
        public string ChatResponse;

        // A list of SteamIDs that this bot recognizes as admins.
        public ulong[] Admins;
        public SteamFriends SteamFriends;
        public SteamClient SteamClient;
        public SteamTrading SteamTrade;
        public SteamUser SteamUser;
        public SteamGameCoordinator SteamGameCoordinator;
        public ClientPlayerNicknameListHandler SteamNicknames;
        // The current trade; if the bot is not in a trade, this is
        // null.
        public Trade CurrentTrade;

        public bool IsDebugMode = false;

        // The log for the bot.  This logs with the bot's display name.
        public Log log;
        
        public delegate UserHandler UserHandlerCreator(Bot bot, SteamID id);
        public UserHandlerCreator CreateHandler;
        Dictionary<ulong, UserHandler> userHandlers = new Dictionary<ulong, UserHandler>();

        public List<SteamID> friends = new List<SteamID>();

        // List of Steam groups the bot is in.
        private readonly List<SteamID> groups = new List<SteamID>();

        // The Steam Web API key.
        public string apiKey;

        // The prefix put in the front of the bot's display name.
        //string DisplayNamePrefix;

        // Log level to use for this bot
        Log.LogLevel LogLevel;

        // The number, in milliseconds, between polls for the trade.
        int TradePollingInterval;

        public string sessionId;
        public string token;

        SteamUser.LogOnDetails logOnDetails;

        TradeManager tradeManager;
        public CookieContainer botCookies;

        public string MyLoginKey;

        bool hasrun = false;
        public bool otherAccepted = false;
        public static string DisplayName = "[unknown]";
        public Login main;

        Friends showFriends;

        public static string MachineAuthData;

        Dictionary<ulong, string> PlayerNicknames = new Dictionary<ulong, string>();

        public Bot(Configuration.BotInfo config, Log log, string apiKey, UserHandlerCreator handlerCreator, Login _login, bool debug = false)
        {
            this.main = _login;
            logOnDetails = new SteamUser.LogOnDetails
            {
                Username = _login.Username,
                Password = _login.Password
            };
            ChatResponse = "";
            TradePollingInterval = 500;
            Admins = new ulong[1];
            Admins[0] = 0;
            this.apiKey = apiKey;
            try
            {
                LogLevel = (Log.LogLevel)Enum.Parse(typeof(Log.LogLevel), "Debug", true);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid LogLevel provided in configuration. Defaulting to 'INFO'");
                LogLevel = Log.LogLevel.Info;
            }
            this.log = log;
            CreateHandler = handlerCreator;
            BotControlClass = "SteamBot.SimpleUserHandler";

            // Hacking around https
            ServicePointManager.ServerCertificateValidationCallback += SteamWeb.ValidateRemoteCertificate;

            log.Debug ("Initializing Steam account...");
            main.Invoke((Action)(() =>
            {
                main.label_status.Text = "Initializing Steam account...";
            }));
            SteamClient = new SteamClient();
            SteamClient.AddHandler(new ClientPlayerNicknameListHandler());
            SteamTrade = SteamClient.GetHandler<SteamTrading>();
            SteamUser = SteamClient.GetHandler<SteamUser>();
            SteamFriends = SteamClient.GetHandler<SteamFriends>();
            SteamGameCoordinator = SteamClient.GetHandler<SteamGameCoordinator>();
            SteamNicknames = SteamClient.GetHandler<ClientPlayerNicknameListHandler>();     
            log.Info ("Connecting...");
            main.Invoke((Action)(() =>
            {
                main.label_status.Text = "Connecting to Steam...";
            }));
            SteamClient.Connect();
            
            Thread CallbackThread = new Thread(() => // Callback Handling
            {
                while (true)
                {
                    CallbackMsg msg = SteamClient.WaitForCallback(true);

                    HandleSteamMessage(msg);
                }
            }); 
            
            CallbackThread.Start();
            CallbackThread.Join();
            log.Success("Done loading account!");
            main.Invoke((Action)(() =>
            {
                main.label_status.Text = "Done loading account!";
            }));
        }

        /// <summary>
        /// Creates a new trade with the given partner.
        /// </summary>
        /// <returns>
        /// <c>true</c>, if trade was opened,
        /// <c>false</c> if there is another trade that must be closed first.
        /// </returns>
        public bool OpenTrade (SteamID other)
        {
            if (CurrentTrade != null)
                return false;

            SteamTrade.Trade(other);

            return true;
        }

        /// <summary>
        /// Closes the current active trade.
        /// </summary>
        public void CloseTrade() 
        {
            if (CurrentTrade == null)
                return;

            UnsubscribeTrade (GetUserHandler (CurrentTrade.OtherSID), CurrentTrade);

            tradeManager.StopTrade ();

            CurrentTrade = null;
        }

        void OnTradeTimeout(object sender, EventArgs args) 
        {
            // ignore event params and just null out the trade.
            GetUserHandler (CurrentTrade.OtherSID).OnTradeTimeout();
        }

        void OnTradeEnded (object sender, EventArgs e)
        {
            CloseTrade();
        }        

        bool HandleTradeSessionStart (SteamID other)
        {
            if (CurrentTrade != null)
                return false;

            try
            {
                tradeManager.InitializeTrade(SteamUser.SteamID, other);
                CurrentTrade = tradeManager.CreateTrade (SteamUser.SteamID, other);
                CurrentTrade.OnClose += CloseTrade;
                SubscribeTrade(CurrentTrade, GetUserHandler(other));
                tradeManager.StartTradeThread(CurrentTrade);
                return true;
            }
            catch (SteamTrade.Exceptions.InventoryFetchException)
            {
                CurrentTrade = null;
                return false;
            }
        }

        void HandleSteamMessage (CallbackMsg msg)
        {
            log.Debug(msg.ToString());
            msg.Handle<SteamGameCoordinator.MessageCallback>(callback =>
            {                
                Console.WriteLine(callback.EMsg);
            });

            msg.Handle<ClientPlayerNicknameListHandler.ClientPlayerNicknameListCallback>(callback =>
            {
                foreach (var player in callback.Nicknames)
                {
                    PlayerNicknames.Add(player.steamid, player.nickname);
                }
            });

            #region Login
            msg.Handle<SteamClient.ConnectedCallback> (callback =>
            {
                log.Debug ("Connection Callback: " + callback.Result);

                if (callback.Result == EResult.OK)
                {
                    UserLogOn();
                }
                else
                {
                    log.Error ("Failed to connect to Steam Community, trying again...");
                    main.Invoke((Action)(() =>
                    {
                        main.label_status.Text = "Failed to connect to Steam Community, trying again...";
                    }));
                    SteamClient.Connect ();
                }

            });

            msg.Handle<SteamUser.LoggedOnCallback> (callback =>
            {
                log.Debug ("Logged On Callback: " + callback.Result);

                if (callback.Result == EResult.OK)
                {
                    MyLoginKey = callback.WebAPIUserNonce;
                    main.Invoke((Action)(() =>
                    {
                        main.label_status.Text = "Logging in to Steam...";
                        log.Info("Logging in to Steam...");
                    }));
                }

                if (callback.Result != EResult.OK)
                {
                    log.Error ("Login Error: " + callback.Result);
                    main.Invoke((Action)(() =>
                    {
                        main.label_status.Text = "Login Error: " + callback.Result;
                    }));
                }
                
                if (callback.Result == EResult.InvalidPassword)
                {
                    MetroFramework.MetroMessageBox.Show(main, "Your password is incorrect. Please try again.",
                                    "Invalid Password",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                    main.wrongAPI = true;
                    main.Invoke((Action)(main.Close));
                    return;
                }

                if (callback.Result == EResult.AccountLogonDenied)
                {
                    log.Interface ("This account is protected by Steam Guard.  Enter the authentication code sent to the proper email: ");
                    SteamGuard SteamGuard = new SteamGuard();
                    SteamGuard.ShowDialog();
                    logOnDetails.AuthCode = SteamGuard.AuthCode;
                    main.Invoke((Action)(() =>
                    {
                        main.label_status.Text = "Logging in...";
                    }));
                }

                if (callback.Result == EResult.InvalidLoginAuthCode)
                {
                    log.Interface("An Invalid Authorization Code was provided.  Enter the authentication code sent to the proper email: ");
                    SteamGuard SteamGuard = new SteamGuard("An Invalid Authorization Code was provided.\nEnter the authentication code sent to the proper email: ");
                    SteamGuard.ShowDialog();
                    logOnDetails.AuthCode = SteamGuard.AuthCode;
                    main.Invoke((Action)(() =>
                    {
                        main.label_status.Text = "Logging in...";
                    }));
                }
            });

            msg.Handle<SteamUser.LoginKeyCallback> (callback =>
            {
                log.Debug("Handling LoginKeyCallback...");                
                while (true)
                {
                    try
                    {
                        log.Info("About to authenticate...");
                        main.Invoke((Action)(() =>
                        {
                            main.label_status.Text = "Authenticating...";
                        }));
                        bool authd = false;
                        try
                        {
                            authd = SteamWeb.Authenticate(callback, SteamClient, out sessionId, out token, MyLoginKey);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error on authentication:\n" + ex);
                        }
                        if (authd)
                        {
                            log.Success("User authenticated!");
                            main.Invoke((Action)(() =>
                            {
                                main.label_status.Text = "User authenticated!";
                            }));
                            tradeManager = new TradeManager(apiKey, sessionId, token);
                            tradeManager.SetTradeTimeLimits(0, 0, TradePollingInterval);
                            tradeManager.OnTimeout += OnTradeTimeout;
                            break;
                        }
                        else
                        {
                            log.Warn("Authentication failed, retrying in 2s...");
                            main.Invoke((Action)(() =>
                            {
                                main.label_status.Text = "Authentication failed, retrying in 2s...";
                            }));
                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.ToString());
                    }
                }                

                SteamFriends.SetPersonaName (SteamFriends.GetFriendPersonaName(SteamUser.SteamID));
                SteamFriends.SetPersonaState (EPersonaState.Online);
                log.Success ("Account Logged In Completely!");
                main.Invoke((Action)(() =>
                {
                    main.label_status.Text = "Logged in completely!";
                }));                
                botCookies = new CookieContainer();
                botCookies.SetCookies(new Uri("http://steamcommunity.com"), string.Format("steamLogin={0}", token));
                botCookies.SetCookies(new Uri("http://steamcommunity.com"), string.Format("sessionid={0}", sessionId));
                GenericInventory.SetCookie(botCookies, SteamUser.SteamID);

                IsLoggedIn = true;                
                try
                {
                    main.Invoke((Action)(main.Hide));
                }
                catch (Exception)
                {
                    Environment.Exit(1);
                }                

                new Thread(() =>
                {                    
                    CDNCache.Initialize();
                    #if !DEBUG
                    ConnectToGC(13540830642081628378);
                    System.Threading.Thread.Sleep(2000);
                    ConnectToGC(0);
                    #endif
                    while (true)
                    {
                        if (showFriends != null)
                        {
                            var numFriendsDisplayed = showFriends.GetNumFriendsDisplayed();
                            var numSteamFriendCount = SteamFriends.GetFriendCount();
                            if (numFriendsDisplayed != -1 && numFriendsDisplayed != ListFriends.Get().Count)
                            {                                
                                LoadFriends();
                                showFriends.UpdateFriendsHTML();
                            }
                            System.Threading.Thread.Sleep(10000);
                        }
                        
                    }
                }).Start();
            });

            // handle a special JobCallback differently than the others
            if (msg.IsType<SteamClient.JobCallback<SteamUser.UpdateMachineAuthCallback>>())
            {
                msg.Handle<SteamClient.JobCallback<SteamUser.UpdateMachineAuthCallback>>(
                    jobCallback => OnUpdateMachineAuthCallback(jobCallback.Callback, jobCallback.JobID)
                );
            }
            #endregion

            msg.Handle<SteamUser.AccountInfoCallback>(callback =>
            {
                DisplayName = callback.PersonaName;
            });

            #region Friends
            msg.Handle<SteamFriends.FriendsListCallback>(callback =>
            {
                foreach (SteamFriends.FriendsListCallback.Friend friend in callback.FriendList)
                {
                    if (friend.SteamID.AccountType == EAccountType.Clan)
                    {

                    }
                    else
                    {
                        if (!friends.Contains(friend.SteamID))
                        {
                            new Thread(() =>
                            {
                                main.Invoke((Action)(() =>
                                {
                                    if (showFriends == null && friend.Relationship == EFriendRelationship.RequestRecipient)
                                    {
                                        log.Info(SteamFriends.GetFriendPersonaName(friend.SteamID) + " has added you.");
                                        friends.Add(friend.SteamID);
                                        string name = SteamFriends.GetFriendPersonaName(friend.SteamID);
                                        string status = SteamFriends.GetFriendPersonaState(friend.SteamID).ToString();
                                        if (!ListFriendRequests.Find(friend.SteamID))
                                        {
                                            ListFriendRequests.Add(name, friend.SteamID, status);
                                        }
                                    }
                                    if (showFriends != null && friend.Relationship == EFriendRelationship.RequestRecipient)
                                    {
                                        log.Info(SteamFriends.GetFriendPersonaName(friend.SteamID) + " has added you.");
                                        friends.Add(friend.SteamID);
                                        string name = SteamFriends.GetFriendPersonaName(friend.SteamID);
                                        string status = SteamFriends.GetFriendPersonaState(friend.SteamID).ToString();
                                        if (!ListFriendRequests.Find(friend.SteamID))
                                        {
                                            try
                                            {
                                                ListFriendRequests.Add(name, friend.SteamID, status);                                              
                                                log.Info("Notifying you that " + SteamFriends.GetFriendPersonaName(friend.SteamID) + " has added you.");
                                                int duration = 5;
                                                FormAnimator.AnimationMethod animationMethod = FormAnimator.AnimationMethod.Slide;
                                                FormAnimator.AnimationDirection animationDirection = FormAnimator.AnimationDirection.Up;
                                                Notification toastNotification = new Notification(name, Util.GetColorFromPersonaState(this, friend.SteamID), "has sent you a friend request.", duration, animationMethod, animationDirection);
                                                toastNotification.Show();
                                                try
                                                {
                                                    string soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                                                    string soundFile = Path.Combine(soundsFolder + "trade_message.wav");
                                                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFile))
                                                    {
                                                        player.Play();
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                }
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Friends list hasn't loaded yet...");
                                            }
                                        }
                                    }
                                }));
                            }).Start();
                        }
                        else
                        {
                            if (friend.Relationship == EFriendRelationship.None)
                            {
                                friends.Remove(friend.SteamID);
                                GetUserHandler(friend.SteamID).OnFriendRemove();
                                RemoveUserHandler(friend.SteamID);
                            }
                        }
                    }                    
                }
                LoadFriends();
            });

            msg.Handle<SteamFriends.PersonaStateCallback>(callback =>
            {
                var status = callback.State;
                var sid = callback.FriendID;
                ListFriends.UpdateStatus(sid, status.ToString());
                ListFriends.UpdateName(sid, SteamFriends.GetFriendPersonaName(sid));
                GetUserHandler(sid).UpdatePersonaState();                
                if (showFriends != null)
                {
                    showFriends.UpdateState();
                    showFriends.UpdateFriendHTML(sid);
                }
            });


            msg.Handle<SteamFriends.FriendMsgCallback>(callback =>
            {
                EChatEntryType type = callback.EntryType;

                if (type == EChatEntryType.Typing)
                {
                    var name = SteamFriends.GetFriendPersonaName(callback.Sender);
                    GetUserHandler(callback.Sender).SetChatStatus(name + " is typing...");
                    System.Threading.Thread.Sleep(30000);
                    GetUserHandler(callback.Sender).SetChatStatus("");
                }                

                if (type == EChatEntryType.ChatMsg)
                {
                    //log.Info (String.Format ("Chat Message from {0}: {1}",
                    //                     SteamFriends.GetFriendPersonaName (callback.Sender),
                    //                     callback.Message
                    //));
                    GetUserHandler(callback.Sender).SetChatStatus("");
                    GetUserHandler(callback.Sender).OnMessage(callback.Message, type);
                }
            });

            msg.Handle<SteamFriends.ChatMsgCallback>(callback =>
            {
                Console.WriteLine(SteamFriends.GetFriendPersonaName(callback.ChatterID) + ": " + callback.Message);
            });
            #endregion

            #region Trading
            msg.Handle<SteamTrading.SessionStartCallback>(callback =>
            {
                bool started = HandleTradeSessionStart(callback.OtherClient);

                if (!started)
                    log.Error("Could not start the trade session.");
                else
                    log.Debug("SteamTrading.SessionStartCallback handled successfully. Trade Opened.");
            });

            msg.Handle<SteamTrading.TradeProposedCallback>(callback =>
            {
                try
                {
                    tradeManager.InitializeTrade(SteamUser.SteamID, callback.OtherClient);
                }
                catch
                {
                    SteamTrade.RespondToTrade(callback.TradeID, false);
                    return;
                }

                //if (CurrentTrade == null && GetUserHandler (callback.OtherClient).OnTradeRequest ())
                if (CurrentTrade == null)
                    GetUserHandler(callback.OtherClient).SendTradeState(callback.TradeID);
                else
                    SteamTrade.RespondToTrade(callback.TradeID, false);
            });

            msg.Handle<SteamTrading.TradeResultCallback>(callback =>
            {
                //log.Debug ("Trade Status: " + callback.Response);

                if (callback.Response == EEconTradeResponse.Accepted)
                {
                    //log.Info ("Trade Accepted!");
                }
                else if (callback.Response == EEconTradeResponse.Cancel ||
                    callback.Response == EEconTradeResponse.ConnectionFailed ||
                    callback.Response == EEconTradeResponse.Declined ||
                    callback.Response == EEconTradeResponse.AlreadyHasTradeRequest ||
                    callback.Response == EEconTradeResponse.AlreadyTrading ||
                    callback.Response == EEconTradeResponse.TargetAlreadyTrading ||
                    callback.Response == EEconTradeResponse.NoResponse ||
                    callback.Response == EEconTradeResponse.TooSoon ||
                    callback.Response == EEconTradeResponse.TradeBannedInitiator ||
                    callback.Response == EEconTradeResponse.TradeBannedTarget ||
                    callback.Response == EEconTradeResponse.NotLoggedIn)
                {
                    if (callback.Response == EEconTradeResponse.Cancel)
                        TradeResponse(callback.OtherClient, "had asked to trade with you, but has cancelled their request.");
                    if (callback.Response == EEconTradeResponse.ConnectionFailed)
                        TradeResponse(callback.OtherClient, "Lost connection to Steam. Reconnecting as soon as possible...");
                    if (callback.Response == EEconTradeResponse.Declined)
                        TradeResponse(callback.OtherClient, "has declined your trade request.");
                    if (callback.Response == EEconTradeResponse.AlreadyHasTradeRequest)
                        TradeResponse(callback.OtherClient, "An error has occurred in sending the trade request.");
                    if (callback.Response == EEconTradeResponse.AlreadyTrading)
                        TradeResponse(callback.OtherClient, "You are already in a trade so you cannot trade someone else.");
                    if (callback.Response == EEconTradeResponse.TargetAlreadyTrading)
                        TradeResponse(callback.OtherClient, "You cannot trade the other user because they are already in trade with someone else.");
                    if (callback.Response == EEconTradeResponse.NoResponse)
                        TradeResponse(callback.OtherClient, "did not respond to the trade request.");
                    if (callback.Response == EEconTradeResponse.TooSoon)
                        TradeResponse(callback.OtherClient, "It is too soon to send a new trade request. Try again later.");
                    if (callback.Response == EEconTradeResponse.TradeBannedInitiator)
                        TradeResponse(callback.OtherClient, "You are trade-banned and cannot trade.");
                    if (callback.Response == EEconTradeResponse.TradeBannedTarget)
                        TradeResponse(callback.OtherClient, "You cannot trade with this person because they are trade-banned.");
                    if (callback.Response == EEconTradeResponse.NotLoggedIn)
                        TradeResponse(callback.OtherClient, "Trade failed to initialize because you are not logged in.");
                    CloseTrade();
                }

            });
            #endregion

            #region Disconnect
            msg.Handle<SteamUser.LoggedOffCallback> (callback =>
            {
                IsLoggedIn = false;
                log.Warn ("Logged Off: " + callback.Result);
            });

            msg.Handle<SteamClient.DisconnectedCallback> (callback =>
            {
                IsLoggedIn = false;
                CloseTrade ();
                log.Warn ("Disconnected from Steam Network!");
                main.Invoke((Action)(() =>
                {
                    main.label_status.Text = "Disconnected from Steam Network! Retrying...";
                }));
                SteamClient.Connect ();
                main.Invoke((Action)(() =>
                {
                    main.label_status.Text = "Connecting to Steam...";
                }));
            });
            #endregion

            if (!hasrun && IsLoggedIn)
            {
                Thread main = new Thread(GUI);
                main.Start();
                hasrun = true;
            }
        }

        void TradeResponse(SteamID _sid, string message)
        {
            GetUserHandler(_sid).SendTradeError(message);
        }

        void GUI()
        {
            main.Invoke(new MethodInvoker(delegate()
            {
                showFriends = new Friends(this, Bot.DisplayName);
                showFriends.Show();
                showFriends.Activate();
                LoadFriends();            
            }));
        }

        public void LoadFriends()
        {            
            ListFriends.Clear();
            var steamListFriends = new List<SteamID>();
            Console.WriteLine("Loading all friends...");
            for (int count = 0; count < SteamFriends.GetFriendCount(); count++)
            {
                steamListFriends.Add(SteamFriends.GetFriendByIndex(count));
                Thread.Sleep(25);
            }
            for (int count = 0; count < steamListFriends.Count; count++)
            {
                var friendID = steamListFriends[count];
                if (SteamFriends.GetFriendRelationship(friendID) == EFriendRelationship.Friend)
                {
                    var friendName = SteamFriends.GetFriendPersonaName(friendID);
                    var friendNickname = PlayerNicknames.ContainsKey(friendID) ? "(" + PlayerNicknames[friendID] + ")" : "";
                    var friendState = SteamFriends.GetFriendPersonaState(friendID).ToString();
                    ListFriends.Add(friendName, friendID, friendNickname, friendState);
                }                
            }
            foreach (var item in ListFriends.Get())
            {
                if (ListFriendRequests.Find(item.SID))
                {
                    Console.WriteLine("Found friend {0} in list of friend requests, so let's remove the user.", item.Name);
                    // Not a friend request, so let's remove it
                    ListFriendRequests.Remove(item.SID);
                }
            }
            foreach (var item in ListFriendRequests.Get())
            {
                if (item.Name == "[unknown]")
                {
                    string name = SteamFriends.GetFriendPersonaName(item.SteamID);
                    ListFriendRequests.Remove(item.SteamID);
                    ListFriendRequests.Add(name, item.SteamID);
                }
                if (item.Name == "")
                {
                    string name = SteamFriends.GetFriendPersonaName(item.SteamID);
                    ListFriendRequests.Remove(item.SteamID);
                    ListFriendRequests.Add(name, item.SteamID);
                }
            }
            Console.WriteLine("Done! {0} friends.", ListFriends.Get().Count);
        }

        public void NewChat(SteamID sid)
        {
            Console.WriteLine("Opening chat.");
            GetUserHandler(sid).OpenChat(sid);
        }

        public static void Print(object sender)
        {
            var old_color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(sender);
            Console.ForegroundColor = old_color;
        }

        public void ConnectToGC(ulong appId)
        {
            var playMsg = new ClientMsgProtobuf<CMsgClientGamesPlayed>(
                EMsg.ClientGamesPlayedWithDataBlob);
            var game = new CMsgClientGamesPlayed.GamePlayed
            {
                game_id = new GameID(appId),
                game_extra_info = "Mist - Portable Steam Client",                
            };            

            playMsg.Body.games_played.Add(game);
            SteamClient.Send(playMsg);
        }

        public void DisconnectFromGC()
        {
            var deregMsg = new ClientMsgProtobuf<CMsgClientDeregisterWithServer>(
                EMsg.ClientDeregisterWithServer);

            deregMsg.Body.eservertype = 42;
            deregMsg.Body.app_id = 0;

            SteamClient.Send(deregMsg);

            ConnectToGC(0);
        }

        void UserLogOn()
        {
            // get sentry file which has the machine hw info saved 
            // from when a steam guard code was entered
            FileInfo fi = new FileInfo(String.Format("{0}/sentryfiles/{1}.sentryfile", Application.StartupPath, logOnDetails.Username));

            if (fi.Exists && fi.Length > 0)
                logOnDetails.SentryFileHash = SHAHash(File.ReadAllBytes(fi.FullName));
            else
                logOnDetails.SentryFileHash = null;

            SteamUser.LogOn(logOnDetails);
        }

        UserHandler GetUserHandler (SteamID sid)
        {
            if (!userHandlers.ContainsKey (sid))
            {
                userHandlers [sid.ConvertToUInt64 ()] = CreateHandler (this, sid);
            }
            return userHandlers [sid.ConvertToUInt64 ()];
        }

        void RemoveUserHandler(SteamID sid)
        {
            if (userHandlers.ContainsKey(sid))
            {
                userHandlers.Remove(sid);
            }
        }

        static byte [] SHAHash (byte[] input)
        {
            SHA1Managed sha = new SHA1Managed();
            
            byte[] output = sha.ComputeHash( input );
            
            sha.Clear();
            
            return output;
        }

        void OnUpdateMachineAuthCallback (SteamUser.UpdateMachineAuthCallback machineAuth, JobID jobId)
        {
            byte[] hash = SHAHash (machineAuth.Data);

            StringBuilder sb = new StringBuilder();
            for (int count = 0; count < hash.Length; count++)
            {
                sb.Append(hash[count]);
            }

            MachineAuthData = sb.ToString();

            Directory.CreateDirectory(Application.StartupPath + "/sentryfiles/");
            File.WriteAllBytes (String.Format ("{0}/sentryfiles/{1}.sentryfile", Application.StartupPath, logOnDetails.Username), machineAuth.Data);
            
            var authResponse = new SteamUser.MachineAuthDetails
            {
                BytesWritten = machineAuth.BytesToWrite,
                FileName = machineAuth.FileName,
                FileSize = machineAuth.BytesToWrite,
                Offset = machineAuth.Offset,
                
                SentryFileHash = hash, // should be the sha1 hash of the sentry file we just wrote
                
                OneTimePassword = machineAuth.OneTimePassword, // not sure on this one yet, since we've had no examples of steam using OTPs
                
                LastError = 0, // result from win32 GetLastError
                Result = EResult.OK, // if everything went okay, otherwise ~who knows~
                
                JobID = jobId, // so we respond to the correct server job
            };
            
            // send off our response
            SteamUser.SendMachineAuthResponse (authResponse);
        }

        /// <summary>
        /// Subscribes all listeners of this to the trade.
        /// </summary>
        public void SubscribeTrade (Trade trade, UserHandler handler)
        {
            trade.OnSuccess += handler.OnTradeSuccess;
            trade.OnClose += handler.OnTradeClose;
            trade.OnError += handler.OnTradeError;
            //trade.OnTimeout += OnTradeTimeout;
            trade.OnAfterInit += handler.OnTradeInit;
            trade.OnUserAddItem += handler.OnTradeAddItem;
            trade.OnUserRemoveItem += handler.OnTradeRemoveItem;
            trade.OnMessage += handler.OnTradeMessage;
            trade.OnUserSetReady += handler.OnTradeReadyHandler;
            trade.OnUserAccept += handler.OnTradeAcceptHandler;
        }
        
        /// <summary>
        /// Unsubscribes all listeners of this from the current trade.
        /// </summary>
        public void UnsubscribeTrade (UserHandler handler, Trade trade)
        {
            trade.OnSuccess -= handler.OnTradeSuccess;
            trade.OnClose -= handler.OnTradeClose;
            trade.OnError -= handler.OnTradeError;
            //Trade.OnTimeout -= OnTradeTimeout;
            trade.OnAfterInit -= handler.OnTradeInit;
            trade.OnUserAddItem -= handler.OnTradeAddItem;
            trade.OnUserRemoveItem -= handler.OnTradeRemoveItem;
            trade.OnMessage -= handler.OnTradeMessage;
            trade.OnUserSetReady -= handler.OnTradeReadyHandler;
            trade.OnUserAccept -= handler.OnTradeAcceptHandler;
        }

        #region Group Methods

        /// <summary>
        /// Accepts the invite to a Steam Group
        /// </summary>
        /// <param name="group">SteamID of the group to accept the invite from.</param>
        private void AcceptGroupInvite(SteamID group)
        {
            var AcceptInvite = new ClientMsg<CMsgGroupInviteAction>((int)EMsg.ClientAcknowledgeClanInvite);

            AcceptInvite.Body.GroupID = group.ConvertToUInt64();
            AcceptInvite.Body.AcceptInvite = true;

            this.SteamClient.Send(AcceptInvite);

        }

        /// <summary>
        /// Declines the invite to a Steam Group
        /// </summary>
        /// <param name="group">SteamID of the group to decline the invite from.</param>
        private void DeclineGroupInvite(SteamID group)
        {
            var DeclineInvite = new ClientMsg<CMsgGroupInviteAction>((int)EMsg.ClientAcknowledgeClanInvite);

            DeclineInvite.Body.GroupID = group.ConvertToUInt64();
            DeclineInvite.Body.AcceptInvite = false;

            this.SteamClient.Send(DeclineInvite);
        }

        /// <summary>
        /// Invites a use to the specified Steam Group
        /// </summary>
        /// <param name="user">SteamID of the user to invite.</param>
        /// <param name="groupId">SteamID of the group to invite the user to.</param>
        public void InviteUserToGroup(SteamID user, SteamID groupId)
        {
            var InviteUser = new ClientMsg<CMsgInviteUserToGroup>((int)EMsg.ClientInviteUserToClan);

            InviteUser.Body.GroupID = groupId.ConvertToUInt64();
            InviteUser.Body.Invitee = user.ConvertToUInt64();
            InviteUser.Body.UnknownInfo = true;

            this.SteamClient.Send(InviteUser);
        }
        #endregion
    }
}