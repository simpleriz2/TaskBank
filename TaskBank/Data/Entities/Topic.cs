namespace TaskBank.Data.Entities
{
    public class Topic
    {
        public int Id { get; set; }

        public int LevelId { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public required ICollection<Subtopic> Subtopics { get; set; }
    }
}
