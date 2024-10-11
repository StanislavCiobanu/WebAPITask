using WebAPITask.Models;

namespace WebAPITask.DataAccess
{
    public class CourseRepository : ICourseRepository
    {
        private List<Course> _courses;

        public CourseRepository() { _courses = new List<Course>(0); }
        public CourseRepository(List<Course> courses) { _courses = courses; }

        public List<Course> GetCourses()
        {
            return _courses;
        }

        public Course GetCourse(int Id)
        {
            Course course = _courses.Find(c => c.Id == Id);

            return course;
        }
        public Course GetCourse(string title)
        {
            Course course = _courses.Find(c => c.Title == title);

            return course;
        }

        public List<Module> GetModulesById(int courseId)
        {
            return _courses.Find(c => c.Id == courseId).Modules;
        }

        public Module GetModuleById(int courseId, int moduleId)
        {
            Course course = _courses.Find(c => c.Id == courseId);

            Module module = course.Modules.Find(m => m.Id == moduleId);

            return module;
        }
        public Module GetModuleById(int moduleId)
        {
            Course course = _courses.Find(c => c.Modules.Find(m => m.Id == moduleId) != null);

            Module module = course.Modules.Find(m => m.Id == moduleId);

            return module;
        }
        public Module GetModuleByOrder(int courseId, int moduleOrder)
        {
            Course course = _courses.Find(c => c.Id == courseId);

            Module module = course.Modules[moduleOrder - 1];

            return module;
        }
        public Module GetModuleByTitle(string title)
        {
            Course course = _courses.Find(c => c.Modules.Find(m => m.Title == title) == null);

            Module module = course.Modules.Find(m => m.Title == title);

            return module;
        }
    }
}
