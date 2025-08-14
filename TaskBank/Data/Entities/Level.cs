namespace TaskBank.Data.Entities
{
    public class Level
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public required ICollection<Topic> Topics { get; set; }
    }
}
