namespace WebAPITask.Models
{
    public class Module
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public int CourseId { get; init; }
        public int CreatedBy { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }

        public Module(int id, string title, int order, int createdBy)
        {
            Id = id;
            Title = title;
            Order = order;

            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
        }
    }
}
