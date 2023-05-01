namespace KanbanAPI.Data.Entities
{
    public class Project
    {
        public Project()
        {

        }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Client Client { get; set; } = new Client();
        public Guid ClientId { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
