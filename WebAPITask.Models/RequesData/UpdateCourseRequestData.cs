namespace WebAPITask.Models
{
    public class UpdateCourseRequestData
    {
        public int userId { get; set; }
        public int courseId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Discipline discipline { get; set; }
        public Difficulty difficulty { get; set; }
    }
}
