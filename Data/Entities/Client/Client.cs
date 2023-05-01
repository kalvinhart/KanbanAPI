namespace KanbanAPI.Data.Entities
{
    public class Client
    {
        public Client() { }

        public Guid ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
