using WebAPITask.DataAccess;
using WebAPITask.Models;

namespace WebAPITask.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void CreateCourse(string title, int createdBy)
        {
            ValidateCourseTitle(title);

            var data = _courseRepository.GetCourses();

            Course course = new Course(CountNextCourseId(), title, createdBy);
            _courseRepository.GetCourses().Add(course);
        }

        private int CountNextCourseId()
        {
            int highestId = 0;

            foreach (var course in _courseRepository.GetCourses())
            {
                if (course.Id >= highestId)
                {
                    highestId = course.Id + 1;
                }
            }

            return highestId;
        }

        private void ValidateCourseTitle(string title)
        {
            if (_courseRepository.GetCourse(title) != null)
            {
                throw new Exception("Course with this name allready exists");
            }
        }

        public void DeleteCourse(int courseId)
        {
            Course course = _courseRepository.GetCourse(courseId);
            _courseRepository.GetCourses().Remove(course);
        }
        public void DeleteCourse(Course course)
        {
            _courseRepository.GetCourses().Remove(course);
        }

        public void UpdateCourse(int courseId, string title, string description,
            Discipline discipline, Difficulty difficulty)
        {
            ValidateCourseTitle(title);

            Course course = _courseRepository.GetCourse(courseId);

            course.Title = title;
            course.Description = description;
            course.Discipline = discipline;
            course.Difficulty = difficulty;
            course.UpdatedAt = DateTime.Now;
        }

        public void EnrollUser(int courseId, int userId)
        {
            _courseRepository.GetCourse(courseId).Enrollments.Add(userId);
        }

        public void EnrollContributor(int courseId, int contributorId)
        {
            _courseRepository.GetCourse(courseId).Contributors.Add(contributorId);
        }

        public void AddModuleToCourse(Course course, Module module)
        {
            if (module.Order < course.Modules.Count())
            {
                course.Modules.Insert(module.Order - 1, module /*new Tuple<int, string>(module.Id, module.Title)*/);
            }
            else
            {
                course.Modules.Insert(course.Modules.Count() - 1, module /*new Tuple<int, string>(module.Id, module.Title)*/);
            }

        }
        public void AddModuleToCourse(int courseId, Module module)
        {
            var course = _courseRepository.GetCourse(courseId);

            if (module.Order - 1 < course.Modules.Count())
            {
                course.Modules.Insert(module.Order - 1, module /*new Tuple<int, string>(module.Id, module.Title)*/);
            }
            else
            {
                course.Modules.Insert(course.Modules.Count(), module /* new Tuple<int, string>(module.Id, module.Title)*/);
            }
        }

        public void RemoveModuleFromCourse(int moduleId)
        {
            var course = _courseRepository.GetCourses()
                .Where(c => c.Modules.Where(m => m.Id == moduleId) != null).FirstOrDefault();

            course.Modules.Remove(course.Modules.Where(m => m.Id == moduleId).FirstOrDefault());
        }
    }
}
