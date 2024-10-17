using System.Net;
using WebAPITask.DataAccess;
using WebAPITask.Models;

namespace WebAPITask.Services
{
    public class ModuleService : IModuleService
    {
        private readonly ICourseRepository _courseRepository;

        public ModuleService(ICourseRepository courseRepository, ICourseService courseService)
        {
            _courseRepository = courseRepository;
        }

        public Module CreateModule(int courseId, string moduleTitle, int order, int createdBy)
        {
            ValidateModuleTitle(moduleTitle);

            var module = new Module(CountNextModuleId(), moduleTitle, order, createdBy);

            var course = _courseRepository.GetCourse(courseId);
            var modules = course.Modules;

            foreach (var mod in modules)
            {
                if (mod.Order >= module.Order && mod != module)
                {
                    mod.Order++;
                }
            }

            return module;
        }

        public void DeleteModules(List<Module> modules)
        {
            foreach (var moduleData in modules)
            {
                DeleteModule(moduleData.Id);
            }
        }

        public void DeleteModule(int moduleId)
        {
            var course = _courseRepository.GetCourses().Where(c => c.Modules.Where(m => m.Id == moduleId).FirstOrDefault() != null).FirstOrDefault();
            var module = course.Modules.Where(m => m.Id == moduleId).FirstOrDefault();

            var courseModules = course.Modules;

            foreach (var mod in courseModules)
            {
                if (mod.Order > module.Order)
                {
                    mod.Order--;
                }
            }

            courseModules.Remove(module);
        }

        public HttpStatusCode UpdateModule(int moduleId, string title, string description, string content)
        {
            ValidateModuleTitle(title);

            var module = _courseRepository.GetModuleById(moduleId); 

            module.Title = title;
            module.Description = description;
            module.Content = content;

            module.UpdatedAt = DateTime.Now;

            return HttpStatusCode.OK;
        }

        private int CountNextModuleId()
        {
            int highestId = 0;

            foreach (var course in _courseRepository.GetCourses())  //_moduleRepository.GetModules())
            {
                foreach (var module in course.Modules)
                {

                    if (module.Id >= highestId)
                    {
                        highestId = module.Id + 1;
                    }
                }
            }

            return highestId;
        }

        private void ValidateModuleTitle(string title)
        {
            if (_courseRepository.GetModuleByTitle(title) != null)  //_moduleRepository.GetModuleByTitle(title) != null)
            {
                throw new Exception("Module with this name allready exists");
            }
        }
    }
}
