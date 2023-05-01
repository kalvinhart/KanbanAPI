using KanbanAPI.Enums;
using TaskStatus = KanbanAPI.Enums.TaskStatus;

namespace KanbanAPI.Data.Entities
{
    public class TaskItem
    {
        public TaskItem()
        {

        }

        public Guid TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public TaskStatus Status { get; set; } = TaskStatus.Open;
        public DateTime? Deadline { get; set; }
        public Project Project { get; set; } = new Project();
        public Guid ProjectId { get; set; }
        public Client Client { get; set; } = new Client();
        public Guid ClientId { get; set; }
        public List<User> Asignees { get; set; } = new List<User>();
    }
}
