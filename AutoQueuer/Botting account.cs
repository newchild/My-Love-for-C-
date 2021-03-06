﻿using System;
using System.Collections.Generic;
using System.Linq;
using PVPNetConnect;
using PVPNetConnect.RiotObjects.Platform.Game;
using PVPNetConnect.RiotObjects.Platform.Game.Message;
using PVPNetConnect.RiotObjects.Platform.Matchmaking;
using PVPNetConnect.RiotObjects.Platform.Statistics;
using PVPNetConnect.RiotObjects.Platform.Clientfacade.Domain;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using PVPNetConnect.RiotObjects.Team.Dto;

namespace AutoQueuer
{
    class Botting_account
    {
        private double IP = 0;
        private bool firstTimeInLobby = true;
        private bool firstTimeInQueuePop = true;
        private bool HasLaunchedGame = false;
        private bool firstTimeInCustom = true;
        private Process exeProcess;
        private double Level = 0;
        private double XP = 0;
        private LoginDataPacket LoginPacket;
        private string username = "";
        private string GameStatus = "IDLE";
        private PVPNetConnection connection = new PVPNetConnection();
        private string user;
        private string p;
        public Botting_account(string username1, string password)
        {
            username = username1;
            connection.Connect(username, password, Region.EUW, "5.5.3");
            connection.OnLogin += connection_OnLogin;
            connection.OnError += connection_OnError;
            connection.OnMessageReceived += connection_OnMessageReceived;
        }

        void connection_OnError(object sender, Error error)
        {
            GameStatus = error.Message;
        }

        

        void connection_OnLogin(object sender, string username, string ipAddress)
        {
            GameStatus = "Logging in...";
            connect(sender);
        }

        async void connect(object data)
        {
            GameStatus = "Connecting...";
            LoginPacket = await connection.GetLoginDataPacketForUser();
            if (LoginPacket.AllSummonerData == null)
            {
                MessageBox.Show("Failed to start bot @" + username + ". Please login and create a summoner first.");
                return;
            }
            Level = LoginPacket.AllSummonerData.SummonerLevel.Level;
            XP =  LoginPacket.AllSummonerData.SummonerLevel.ExpToNextLevel;
            IP = LoginPacket.IpBalance;
            PlayerDTO player = await connection.CreatePlayer();
            if (LoginPacket.ReconnectInfo != null && LoginPacket.ReconnectInfo.Game != null)
            {
                connection_OnMessageReceived(data, (object)LoginPacket.ReconnectInfo.PlayerCredentials);
            }
            else
                connection_OnMessageReceived(data, (object)new EndOfGameStats());
        }

        private void LaunchGame(PlayerCredentialsDto CurrentGame)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.WorkingDirectory = @"C:\Riot Games\League of Legends\" ;
            startInfo.FileName = "League of Legends.exe";
            startInfo.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + CurrentGame.ServerIp + " " +
                CurrentGame.ServerPort + " " + CurrentGame.EncryptionKey + " " + CurrentGame.SummonerId + "\"";
            GameStatus = "Ingame";
            new Thread(() =>
            {
                exeProcess = Process.Start(startInfo);
                while (exeProcess.MainWindowHandle == IntPtr.Zero) { }
                Thread.Sleep(1000);
            }).Start();
        }

        public async void connection_OnMessageReceived(object sender, object message)
        {
            
            if (message is GameDTO)
            {
                
                GameDTO game = message as GameDTO;
                MessageBox.Show(game.GameState);
                switch (game.GameState)
                {
                    case "CHAMP_SELECT":
                        firstTimeInCustom = true;
                        firstTimeInQueuePop = true;
                        if (firstTimeInLobby)
                        {
                            firstTimeInLobby = false;
                            GameStatus = "In Champion Select";
                            await connection.SetClientReceivedGameMessage(game.Id, "CHAMP_SELECT_CLIENT");
                            await connection.SelectChampion(Enums.championToId("ANNIE"));
                            await connection.ChampionSelectCompleted();
                        }

                         break;

                    case "POST_CHAMP_SELECT":
                        firstTimeInLobby = false;
                        GameStatus = "Post Champ Select";
                        break;
                    case "PRE_CHAMP_SELECT":
                        GameStatus = "Pre Champ Select";
                        break;
                    case "GAME_START_CLIENT":
                       GameStatus = "Game client ran";
                        break;
                    case "GameClientConnectedToServer":
                       GameStatus = "Client connected to the server";
                        break;
                    case "IN_QUEUE":
                        GameStatus = "In Queue";
                        break;
                    case "TERMINATED":
                        GameStatus = "Re-entering queue";
                        firstTimeInQueuePop = true;
                        break;
                    case "JOINING_CHAMP_SELECT":
                        if (firstTimeInQueuePop)
                        {
                            GameStatus = "Queue popped";
                            if (game.StatusOfParticipants.ToString().Contains("1"))
                            {
                                GameStatus = "Accepted Queue";
                                firstTimeInQueuePop = false;
                                firstTimeInLobby = true;
                                await connection.AcceptPoppedGame(true);
                            }
                        }
                        break;
                }
            }
            else if (message is PlayerCredentialsDto)
            {
                PlayerCredentialsDto dto = message as PlayerCredentialsDto;
                if (!HasLaunchedGame)
                {
                    HasLaunchedGame = true;
                    new Thread((ThreadStart)(() =>
                    {
                        LaunchGame(dto);
                        Thread.Sleep(3000);
                    })).Start();
                }
            }
            else if (!(message is GameNotification) && !(message is SearchingForMatchNotification))
            {
                if (message is EndOfGameStats)
                {
					
                    var x = message as EndOfGameStats;
                    MatchMakerParams matchParams = new MatchMakerParams();
                   
                    
                        matchParams.BotDifficulty = "MEDIUM";
                    
                    
                    matchParams.QueueIds = new Int32[1] {33};
                    SearchingForMatchNotification m = await connection.AttachToQueue(matchParams);
					
                    if (m.PlayerJoinFailures == null)
                    {
                        GameStatus = "In Queue";
                    }
                        
                    else
                    {
						
                        GameStatus = "Queue failed";
						
                    }
                    
                }
                else
                {
                    if (message.ToString().Contains("EndOfGameStats"))
                    {
                        EndOfGameStats eog = new EndOfGameStats();
                        connection_OnMessageReceived(sender, eog);
                        exeProcess.Kill();
                        LoginPacket = await this.connection.GetLoginDataPacketForUser();
                        Level = LoginPacket.AllSummonerData.SummonerLevel.Level;
                        XP = LoginPacket.AllSummonerData.SummonerLevel.ExpToNextLevel;
                        IP = LoginPacket.IpBalance;
                    }
                }
            }
        }
        public string getUserInfo()
        {
            return  "User: " + username + " Level: " + Level + " IP: " + IP + " XP for next Level: " + XP + " Status: " + GameStatus;
        }



        
    }
}
