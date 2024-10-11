using WebAPITask.Models;

namespace WebAPITask.DataAccess
{
    public interface ICourseRepository
    {
        public List<Course> GetCourses();

        public Course GetCourse(int Id);
        public Course GetCourse(string title);

        public List<Module> GetModulesById(int courseId);

        public Module GetModuleById(int courseId, int moduleId);
        public Module GetModuleById(int moduleId);
        public Module GetModuleByOrder(int courseId, int moduleOrder);
        public Module GetModuleByTitle(string title);

    }
}
