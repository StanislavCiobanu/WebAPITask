using Microsoft.AspNetCore.Mvc;
using WebAPITask.Models;
using WebAPITask.DataAccess;
using WebAPITask.Services;
//using MediatR;

namespace WebAPITask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        private readonly IModuleService _moduleService;
        //private readonly IMediator _mediator;

        public CourseController(/*IMediator mediator,*/ ICourseRepository courseRepository,
            ICourseService courseService, IModuleService moduleService)
        {
            // _mediator = mediator;
            _courseRepository = courseRepository;
            _courseService = courseService;
            _moduleService = moduleService; 
        } 

        [HttpPost("CreateCourse")]
        public IActionResult CreateCourse(string title)
        {
            _courseService.CreateCourse(title, 1);
            return Ok("Course was successfully created");
        }

        [HttpGet("GetCourse")]
        public Object GetCourse(int Id)
        {
            return _courseRepository.GetCourseLimitedData(Id);
        }

        [HttpDelete("DeleteCourse")]
        public IActionResult DeleteCourse(int courseId, int userId)
        {
            Course course = _courseRepository.GetCourse(courseId);

            if (course.CreatedBy == userId)
            {
                _moduleService.DeleteModules(course.Modules);
                _courseService.DeleteCourse(courseId);
                return Ok("Course was successfuly deleted");
            }
            else
            {
                return Forbid("User has no rights for deleting this course");
            }
        }

        [HttpPatch("UpdateCourse")]
        public IActionResult UpdateCourse([FromBody] UpdateCourseRequestData data)
        {
            Course course = _courseRepository.GetCourse(data.courseId);

            if (course.CreatedBy == data.userId)
            {
                _courseService.UpdateCourse(data.courseId,
                    data.title, data.description, data.discipline, data.difficulty);

                return Ok();
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
