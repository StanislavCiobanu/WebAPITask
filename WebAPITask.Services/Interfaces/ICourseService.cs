using System.Net;
using WebAPITask.Models;

namespace WebAPITask.Services
{
    public interface ICourseService
    {
        public HttpStatusCode CreateCourse(string title, int createdBy);

        public void DeleteCourse(int courseId);
        public void DeleteCourse(Course course);

        public HttpStatusCode UpdateCourse(int courseId, string title, string description,
            Discipline discipline, Difficulty difficulty);

        public void EnrollUser(int courseId, int userId);

        public void EnrollContributor(int courseId, int contributorId);
    }
}
