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

            return starshipDetailsObject;
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
