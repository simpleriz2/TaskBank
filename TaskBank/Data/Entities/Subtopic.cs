namespace TaskBank.Data.Entities
{
    public class Subtopic
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public required ICollection<UserTask> UserTasks { get; set; }
    }
}
