using WebAPITask.Models;
using WebAPITask.DataAccess;
using System.Net;

namespace WebAPITask.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public HttpStatusCode CreateCourse(string title, int createdBy)
        {
            var result = ValidateCourseTitle(title);

            if (result == HttpStatusCode.Conflict)
            {
                return result;
            }

            var data = _courseRepository.GetCourses();

            Course course = new Course(CountNextCourseId(), title, createdBy);
            _courseRepository.GetCourses().Add(course);

            return HttpStatusCode.Created;
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

        private HttpStatusCode ValidateCourseTitle(string title)
        {
            if (_courseRepository.GetCourse(title) != null)
            {
                return HttpStatusCode.Conflict;
            }

            return HttpStatusCode.Continue;
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

        public HttpStatusCode UpdateCourse(int courseId, string title, string description,
            Discipline discipline, Difficulty difficulty)
        {
            var result = ValidateCourseTitle(title);

            if (result == HttpStatusCode.Conflict)
            {
                return result;
            }

            Course course = _courseRepository.GetCourse(courseId);

            course.Title = title;
            course.Description = description;
            course.Discipline = discipline;
            course.Difficulty = difficulty;
            course.UpdatedAt = DateTime.Now;

            return HttpStatusCode.OK;
        }

        public void EnrollUser(int courseId, int userId)
        {
            Course course = _courseRepository.GetCourse(courseId);
            course.Enrollments.Add(userId);
        }

        public void EnrollContributor(int courseId, int contributorId)
        {
            Course course = _courseRepository.GetCourse(courseId);
            course.Contributors.Add(contributorId);
        }
    }
}
