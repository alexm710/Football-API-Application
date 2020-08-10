using football_api_from_scratch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace football_from_scratch_client
{
    public class CRUDManager
    {
        public List<Player> players = new List<Player>();
        public Player player = new Player();

        public Team team = new Team();
        public List<Team> teams = new List<Team>();

        Uri url = new Uri("https://localhost:44310/api/players");
        Uri url2 = new Uri("https://localhost:44310/api/teams");

        static Player newTeam = new Player()
        {
            PlayerId = 8,
            PlayerName = "Sergio Aguero",
            PlayerAge = 29,
            PlayerPosition = "Striker",
            TeamName = "Manchester City"
        };

        static Player updatePlayer = new Player()
        {
            PlayerId = 7,
            PlayerName = "Sergio Aguero",
            PlayerAge = 29,
            PlayerPosition = "Striker",
            TeamName = "Manchester City"
        };
        static void Main(string[] args)
        {
            ////Async Get One Customer
            //GetPlayerAsync("3");
            //Thread.Sleep(8000);
            //Console.WriteLine($"ID: {player.PlayerId} has returned - Player's name is {player.PlayerName} \n{player.PlayerName} is {player.PlayerAge} years old. They play for {player.TeamName} and their position is {player.PlayerPosition}\n");


            //// get all customers
            //Console.WriteLine("---All Football Players---");
            //GetAllPlayersAsync();
            //Thread.Sleep(5000);
            //foreach (var item in players)
            //{
            //    Console.WriteLine(item.PlayerName);
            //};


            // Post a new Player

            //Thread.Sleep(7000);
            //Console.WriteLine("\n---Adding a new player to the database---");
            //PostPlayerAsync(newPlayer);
            //Thread.Sleep(4000);

            // Post a new player v2
            //Console.WriteLine("\n---Adding a new player to the database---");
            //var newPlayer = new Player()
            //{
            //    PlayerId = 9,
            //    PlayerName = "Mesut Ozil",
            //    PlayerAge = 30,
            //    PlayerPosition = "CAM",
            //    TeamName = "Arsenal"
            //};
            //AddPlayer(newPlayer).Wait();
            //Thread.Sleep(4000);

            //Update a Player
            //Thread.Sleep(4000);
            //UpdatePlayer(updatePlayer);
            //Thread.Sleep(7000);
            //Console.WriteLine("update successful");


            //// Delete a Player
            //Console.WriteLine("---Deleting a Player from the database---");
            //DeletePlayerAsync(5);
            //Thread.Sleep(2000);
            //Team updateTeam = new Team();
            //UpdateTeam(1, "jij", "jijij", "ijiji", 1);
        }

        public void UpdateTeamX(int id, string name, string stadium, string location, int playerid)
        {
            Team uppdateTeam = new Team()
            {
                TeamId = id,
                TeamName = name,
                TeamStadium = stadium,
                TeamLocation = location,
                PlayerId = playerid
            };

            string updatePlayerAsJson = JsonConvert.SerializeObject(player);
            var httpContent = new StringContent(updatePlayerAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.PutAsync($"{url2}/{team.TeamId}", httpContent);
                Console.WriteLine($"Update was: {httpResponse.Result.IsSuccessStatusCode}");
            }
        }

        // 2 Get methods to read a players data
        public async void GetPlayerAsync(string playerId)
        {
            using (var httpclient = new HttpClient())
            {
                var playerData = await httpclient.GetStringAsync($"{url}/{playerId}");
                player = JsonConvert.DeserializeObject<Player>(playerData);
            }
        }

        public void GetAllPlayers()
        {
            using (var httpclient = new HttpClient())
            {
                var playerData = httpclient.GetStringAsync(url);
                players = JsonConvert.DeserializeObject<List<Player>>(playerData.Result);
            }
        }

        public async void GetAllPlayersAsync()
        {
            using (var httpclient = new HttpClient())
            {
                var customerData = await httpclient.GetStringAsync(url);
                players = JsonConvert.DeserializeObject<List<Player>>(customerData);
            }
        }

        // check the player exists in db
        public bool PlayerExists(int playerId)
        {
            GetAllPlayers();
            player = null;
            player = players.Find(c => c.PlayerId == playerId);
            if (player != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // using post to add a player to db
        public void PostPlayerAsync(Player newPlayer)
        {
            // check customer does not exist
            if (!PlayerExists(newPlayer.PlayerId))
            {
                // first serialise our player to JSON
                string newPlayerAsJson = JsonConvert.SerializeObject(newPlayer, Formatting.Indented);
                // convert this to http
                var httpcontent = new StringContent(newPlayerAsJson);
                // add headers before send
                httpcontent.Headers.ContentType.MediaType = "application/json";
                httpcontent.Headers.ContentType.CharSet = "UTF-8";

                // send data
                using (var httpclient = new HttpClient())
                {
                    var httpresponse = httpclient.PostAsync(url, httpcontent);
                    Console.WriteLine($"Success status is {httpresponse.Result.IsSuccessStatusCode}");
                    if (httpresponse.Result.IsSuccessStatusCode == true)
                    {
                        Console.WriteLine($"Player added with ID {newPlayer.PlayerId}");
                        players.Add(newPlayer);
                    }
                    else
                    {
                        Console.WriteLine($"Player already exists - {newPlayer.PlayerId}");
                    }
                }
            }
        }
        public void PostPlayer(int id, string name, string position, int age, string team)
        {
            Player newPlayer = new Player()
            {
                PlayerId = id,
                PlayerName = name,
                PlayerPosition = position,
                PlayerAge = age,
                TeamName = team
            };
            string newPlayerAsJson = JsonConvert.SerializeObject(newPlayer, Formatting.Indented);

            var httpContent = new StringContent(newPlayerAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.PostAsync(url, httpContent);
                if (httpResponse.Result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Record {newPlayer.PlayerName} successfully added");
                }
            }
        }

        public async Task AddPlayer(Player player)
        {
            string newPlayerAsJson = JsonConvert.SerializeObject(player);
            var httpContent = new StringContent(newPlayerAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PostAsync(url, httpContent);
                Console.WriteLine($"Adding record was: {httpResponse.IsSuccessStatusCode}");
            }
        }

        // using put to update a players record in db
        public async void UpdatePlayerAsync(Player player)
        {
            string updatePlayerAsJson = JsonConvert.SerializeObject(player);
            var httpContent = new StringContent(updatePlayerAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.PutAsync($"{url}/{player.PlayerId}", httpContent);
                Console.WriteLine($"Update was: {httpResponse.IsSuccessStatusCode}");
            }
        }

        public void UpdatePlayer(int id, string name, string position, int age, string team)
        {
            Player player = new Player()
            {
                PlayerId = id,
                PlayerName = name,
                PlayerPosition = position,
                PlayerAge = age,
                TeamName = team
            };

            string updatePlayerAsJson = JsonConvert.SerializeObject(player);
            var httpContent = new StringContent(updatePlayerAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.PutAsync($"{url}/{player.PlayerId}", httpContent);
                Console.WriteLine($"Update was: {httpResponse.Result.IsSuccessStatusCode}");
            }
        }

        // using put to update a players record in db
        //public async void UpdatePlayerAsync(Player updatePlayer)
        //{
        //    if (PlayerExists(updatePlayer.PlayerId) == true)
        //    {
        //        string updatePlayerAsJson = JsonConvert.SerializeObject(updatePlayer);

        //        // Convert to HTTP
        //        var httpContent = new StringContent(updatePlayerAsJson);

        //        // Add headers before send
        //        httpContent.Headers.ContentType.MediaType = "application/json";
        //        httpContent.Headers.ContentType.CharSet = "UTF-8";

        //        // Send data
        //        using (var httpClient = new HttpClient())
        //        {
        //            var httpResponse = await httpClient.PutAsync($"{url}/{updatePlayer.PlayerId}", httpContent);
        //            if (httpResponse.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine($"Player record: {updatePlayer.PlayerId}, has successfully updated");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Player with ID: {updatePlayer.PlayerId} does not exist");
        //    }
        //}

        // simple delete method to delete players record
        public async void DeletePlayerAsync(int playerId)
        {
            if (PlayerExists(playerId) == true)
            {
                // Send Data
                using (var httpClient = new HttpClient())
                {
                    var httpResponse = await httpClient.DeleteAsync($"{url}/{playerId}");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Record {playerId} successfully deleted");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Player with ID: {playerId} cannot be deleted");
            }
        }

        // TEAMS CRUD

        public void UpdateTeam(int id, string name, string stadium, string location, int playerid)
        {
            Team newTeam = new Team()
            {
                TeamId = id,
                TeamName = name,
                TeamStadium = stadium,
                TeamLocation = location,
                PlayerId = playerid
            };

            string updateTeamAsJson = JsonConvert.SerializeObject(newTeam, Formatting.Indented);
            var httpContent = new StringContent(updateTeamAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.PutAsync($"{url2}/{team.TeamId}", httpContent);
                Console.WriteLine($"Update was: {httpResponse.Result.IsSuccessStatusCode}");
            }
        }


        public void GetAllTeams()
        {
            using (var httpclient = new HttpClient())
            {
                var teamData = httpclient.GetStringAsync(url2);
                teams = JsonConvert.DeserializeObject<List<Team>>(teamData.Result);
            }
        }
        public async void DeleteTeamAsync(int teamId)
        {
            if (PlayerExists(teamId) == true)
            {
                // Send Data
                using (var httpClient = new HttpClient())
                {
                    var httpResponse = await httpClient.DeleteAsync($"{url2}/{teamId}");
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Record {teamId} successfully deleted");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Player with ID: {teamId} cannot be deleted");
            }
        }
        public void PostTeam(int id, string name, string stadium, string location)
        {
            Team newTeam = new Team()
            {
                TeamId = id,
                TeamName = name,
                TeamStadium = stadium,
                TeamLocation = location
            };

            string newTeamAsJson = JsonConvert.SerializeObject(newTeam, Formatting.Indented);
            var httpContent = new StringContent(newTeamAsJson);

            httpContent.Headers.ContentType.MediaType = "application/json";
            httpContent.Headers.ContentType.CharSet = "UTF-8";

            using (var httpClient = new HttpClient())
            {
                var httpResponse = httpClient.PostAsync(url2, httpContent);
                if (httpResponse.Result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Record {newTeam.TeamName} successfully added");
                }
            }
        }
    }
}
