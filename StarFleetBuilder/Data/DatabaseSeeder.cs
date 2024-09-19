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
            if (_context.Starship.Count() == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    var starship = await _starshipService.GetRandomStarshipAsync();
                    _context.Starship.Add(starship);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
