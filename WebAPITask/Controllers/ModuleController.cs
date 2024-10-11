using Microsoft.AspNetCore.Mvc;
using WebAPITask.Models;
using WebAPITask.DataAccess;
using WebAPITask.Services;

namespace WebAPITask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IModuleService _moduleService;

        public ModuleController(ICourseRepository courseRepository,
            IModuleService moduleService)
        {
            _courseRepository = courseRepository;
            _moduleService = moduleService;
        }

        [HttpPost("CreateModule")]
        public IActionResult CreateModule(int courseId, string moduleTitle, int order)
        {
            var result = _moduleService.CreateModule(courseId, moduleTitle, order, 1);
            return StatusCode((int)result);
        }

        [HttpGet("GetModule")]
        public Module GetModule(int Id)
        {
            return _courseRepository.GetModuleById(Id);
        }

        [HttpDelete("DeleteModule")]
        public IActionResult DeleteModule(int userId, int moduleId)
        {
            var module = _courseRepository.GetModuleById(moduleId);

            if (userId == module.CreatedBy)
            {
                _moduleService.DeleteModule(moduleId);
                return Ok("Module was successfuly deleted");
            }
            else
            {
                return BadRequest("User has no rights for deleting this module");
            }
        }

        [HttpPatch("UpdateModule")]
        public IActionResult UpdateModule(int userId, int moduleId, string title,
            string description, string content)
        {
            var module = _courseRepository.GetModuleById(moduleId);

            if (userId == module.CreatedBy)
            {
                var result = _moduleService.UpdateModule(moduleId, title, description, content);
                return StatusCode((int)result); 
            }
            else
            {
                return BadRequest("User has no rights for updating this module");
            }
        }
    }
}
