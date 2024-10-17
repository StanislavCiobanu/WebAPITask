using System.Net;
using WebAPITask.Models;

namespace WebAPITask.Services
{
    public interface ICourseService
    {
        public void CreateCourse(string title, int createdBy);

        public void DeleteCourse(int courseId);
        public void DeleteCourse(Course course);

        public void UpdateCourse(int courseId, string title, string description,
            Discipline discipline, Difficulty difficulty);

        public void EnrollUser(int courseId, int userId);

        public void EnrollContributor(int courseId, int contributorId);

        public void AddModuleToCourse(int courseId, Module module);

        public void RemoveModuleFromCourse(int moduleId);
    }
}
