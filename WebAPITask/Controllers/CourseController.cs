using Microsoft.AspNetCore.Mvc;
using WebAPITask.Models;
using WebAPITask.DataAccess;
using WebAPITask.Services;

namespace WebAPITask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;

        public CourseController(ICourseRepository courseRepository, 
            ICourseService courseService)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
        }

        [HttpPost("CreateCourse")]
        public IActionResult CreateCourse(string title)
        {
            var result = _courseService.CreateCourse(title, 1);
            return StatusCode((int)result);
        }

        [HttpGet("GetCourse")]
        public Object GetCourse(int Id)
        {
            Course course = _courseRepository.GetCourse(Id);

            List<Object> list = new List<Object>();
            foreach (var module in course.Modules) 
            {
                list.Add(new { module.Id, module.Title});
            }

            var output = new { course.Id, course.Title, course.CreatedBy, 
                course.CreatedAt, course.UpdatedAt, list };

            return output;
        }

        [HttpDelete("DeleteCourse")]
        public IActionResult DeleteCourse(int courseId, int userId)
        {
            Course course = _courseRepository.GetCourse(courseId);

            if (course.CreatedBy == userId)
            {
                _courseService.DeleteCourse(courseId);
                return Ok("Course was successfuly deleted");
            }
            else
            {
                return Forbid("User has no rights for deleting this course");
            }
        }

        [HttpPatch("UpdateCourse")]
        public IActionResult UpdateCourse(int userId, int courseId, string title, 
            string description, Discipline discipline, Difficulty difficulty)
        {
            Course course = _courseRepository.GetCourse(courseId);

            if (course.CreatedBy == userId)
            {
                var result = _courseService.UpdateCourse(courseId, title, 
                    description, discipline, difficulty);
                return StatusCode((int)result/*, result*/);
            }
            else
            {
                return Forbid("User has no rights for updating this course");
            }
        }

        [HttpPatch("EnrollUser")]
        public IActionResult EnrollUser(int requestAuthorId, int courseId, int userId)
        {
            Course course = _courseRepository.GetCourse(courseId);

            if (course.CreatedBy == requestAuthorId)
            {
                _courseService.EnrollUser(courseId, userId);
                return Ok("User was successfully enrolled");
            }
            else
            {
                return Forbid("User has no rights for enrolling new members to course");
            }
        }

        [HttpPatch("EnrollContributor")]
        public IActionResult EnrollContributor(int userId, int courseId, int contributorId)
        {
            Course course = _courseRepository.GetCourse(courseId);

            if (course.CreatedBy == userId)
            {
                _courseService.EnrollContributor(courseId, contributorId);
                return Ok("Contributor was successfully enrolled");
            }
            else
            {
                return Forbid("User has no rights for enrolling new contributors to course");
            }
        }
    }
}
