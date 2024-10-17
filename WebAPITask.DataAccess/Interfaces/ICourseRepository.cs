using WebAPITask.Models;

namespace WebAPITask.DataAccess
{
    public interface ICourseRepository
    {
        public List<Course> GetCourses();

        public Course GetCourse(int Id);
        public Course GetCourse(string title);

        public Module GetModuleById(int moduleId);
        public Module GetModuleByTitle(string title);

        public CourseLimitedData GetCourseLimitedData(int Id);
    }
}
