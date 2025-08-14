namespace TaskBank.Data.Entities
{
    public class UserTask
    {
        public int Id { get; set; }

        public int SubtopicId { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
