namespace WebAPITask.Models
{
    public class Course
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public int CreatedBy { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; set; }
        public Discipline Discipline { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
        public List<int> Contributors { get; set; }
        public List<Module> Modules { get; set; }
        public List<int> Enrollments { get; set; }

        public Course(int id, string title, int createdBy)
        {
            Id = id;
            Title = title;
            Modules = new List<Module>();

            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;

            Contributors = new List<int>();
            Enrollments = new List<int>();
        }
    }
}
