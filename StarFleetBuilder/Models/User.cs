namespace StarFleetBuilder.Models
{
    // Quick user model for scaffolding
    // This model is used for money management in the application
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Credits { get; set; }
    }
}
