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
            Course course = _courses.Where(c => c.Id == Id).FirstOrDefault();

            return course;
        }

        public Course GetCourse(string title)
        {
            Course course = _courses.Where(c => c.Title == title).FirstOrDefault();

            return course;
        }

        public Module GetModuleById(int moduleId)
        {
            var course = _courses.Where(c => c.Modules.Where(m => m.Id == moduleId) != null).FirstOrDefault();
            var module = course.Modules.Where(m => m.Id == moduleId).FirstOrDefault();

            return module;
        }

        public Module GetModuleByTitle(string title)
        {
            var course = _courses.Where(c => c.Modules.Where(m => m.Title == title) != null).FirstOrDefault();
            var module = course.Modules.Where(m => m.Title == title).FirstOrDefault();

            return module;
        }

        public CourseLimitedData GetCourseLimitedData(int Id)
        {
            return new CourseLimitedData(GetCourse(Id));
        }

    }
}
