using System.Threading.Tasks;
using StarFleetBuilder.Models;
using StarFleetBuilder.Services;

namespace StarFleetBuilder.Data
{
    public class DatabaseSeeder
    {
        private readonly StarFleetBuilderContext _context;
        private readonly StarshipService _starshipService;

        public DatabaseSeeder(StarFleetBuilderContext context, StarshipService starshipService)
        {
            _context = context;
            _starshipService = starshipService;
        }

        public async Task SeedDatabaseAsync()
        {
            await SeedStarshipsAsync();
            await SeedUserAsync();
        }

        private async Task SeedStarshipsAsync()
        {
            if (_context.Starship.Count() == 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    var starship = await _starshipService.GetRandomStarshipAsync();
                    AddCost(starship);
                    _context.Starship.Add(starship);
                }
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUserAsync()
        {
            if (_context.User.Count() == 0)
            {
                var user = new User
                {
                    Name = "Jedi Knight",
                    Credits = 1000000
                };
                _context.User.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddCost(Starship starship)
        {
            Random random = new Random();
            var cost = random.Next(245433, 3434564).ToString();
            starship.CostInCredits = cost;
        }
    }
}
