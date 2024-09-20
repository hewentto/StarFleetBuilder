using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarFleetBuilder.Models;

namespace StarFleetBuilder.Services
{
    public class StarshipService
    {
        private readonly HttpClient client = new HttpClient();
        private const string apiUrl = "https://swapi.dev/api/starships/";

        public async Task<Starship> GetRandomStarshipAsync()
        {
            var response = await client.GetStringAsync(apiUrl);
            var starshipList = JsonConvert.DeserializeObject<StarshipList>(response);

            var random = new Random();
            var index = random.Next(starshipList.Results.Count);
            var starship = starshipList.Results[index];

            var starshipDetails = await client.GetStringAsync(starship.Url);
            var starshipDetailsObject = JsonConvert.DeserializeObject<Starship>(starshipDetails);

            AddCost(starshipDetailsObject);

            return starshipDetailsObject;
        }

        public async Task<Starship> GetStarshipByUrlAsync(string url)
        {
            var response = await client.GetStringAsync(url);
            var starship = JsonConvert.DeserializeObject<Starship>(response);
            AddCost(starship);
            return starship;
        }

        private void AddCost(Starship starship)
        {
            Random random = new Random();
            var cost = random.Next(245433, 3434564).ToString();
            starship.CostInCredits = cost;
        }
    }

    public class StarshipList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Starship> Results { get; set; }
    }
}
