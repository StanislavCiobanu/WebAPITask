namespace WebAPITask.Models
{
    public class CourseLimitedData
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
        public List<Object> Modules { get; set; }
        public List<int> Enrollments { get; set; }

        public CourseLimitedData(Course course)
        {
            Id = course.Id;

            Title = course.Title;

            CreatedBy = course.CreatedBy;
            CreatedAt = course.CreatedAt;
            UpdatedAt = course.UpdatedAt;

            Discipline = course.Discipline;
            Description = course.Description;
            Difficulty = course.Difficulty;

            Contributors = course.Contributors;       
            Enrollments = course.Enrollments;

            Modules = new List<object>();

            foreach (var module in course.Modules)
            {
                Modules.Add(new { id = module.Id, title = module.Title });
            }
        }
    }
}
