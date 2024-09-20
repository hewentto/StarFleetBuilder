using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StarFleetBuilder.Models
{
    public class Starship
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Model { get; set; }

        public string? Manufacturer { get; set; }

        public string? StarshipClass { get; set; }

        public string? Length { get; set; }

        public string? Crew { get; set; }

        public string? Passengers { get; set; }

        public string? HyperdriveRating { get; set; }

        public string? MGLT { get; set; }

        public string? CargoCapacity { get; set; }

        public string? Consumables { get; set; }
        [DisplayName("Cost")]
        public string? CostInCredits { get; set; }

        public string? MaxAtmospheringSpeed { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }

        public string? Url { get; set; }
    }
}
