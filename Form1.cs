using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace GodGameManager
{
    public partial class Form1 : Form
    {
        private readonly string supabaseUrl = "https://qjusboguowpyamitokjh.supabase.co";
        private readonly string supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InFqdXNib2d1b3dweWFtaXRva2poIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjAwNjU5MDIsImV4cCI6MjA3NTY0MTkwMn0._AMM8U_rwX6RLAe1ACIvdC-047nnykVaQvnfcAuXZ0Q";

        public Form1()
        {
            InitializeComponent();
            LoadGames();
        }
        private async void LoadGames()
        {
            await LoadGamesAsync();
        }

        private async Task LoadGamesAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", supabaseKey);
                client.DefaultRequestHeaders.Add("apikey", supabaseKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(supabaseUrl + "/rest/v1/Games?select=*");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var games = JsonConvert.DeserializeObject<List<Games>>(json);

                    listBox1.Items.Clear();
                    foreach (var game in games)
                    {
                        listBox1.Items.Add($"{game.Title} - {game.Developer}");
                    }
                }
                else
                {
                    MessageBox.Show("데이터를 불러오지 못했습니다: " + response.StatusCode);
                }
            }
        }
        public class Games
        {

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("developer")]
            public string Developer { get; set; }

            [JsonProperty("genre")]
            public string Genre { get; set; }
        }
    }
}

