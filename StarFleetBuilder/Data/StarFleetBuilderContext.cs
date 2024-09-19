using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarFleetBuilder.Models;

namespace StarFleetBuilder.Data
{
    public class StarFleetBuilderContext : DbContext
    {
        public StarFleetBuilderContext (DbContextOptions<StarFleetBuilderContext> options)
            : base(options)
        {
        }

        public DbSet<StarFleetBuilder.Models.Starship> Starship { get; set; } = default!;
    }
}
