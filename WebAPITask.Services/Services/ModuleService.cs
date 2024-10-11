using System.Net;
using WebAPITask.DataAccess;
using WebAPITask.Models;

namespace WebAPITask.Services
{
    public class ModuleService : IModuleService
    {
        private readonly ICourseRepository _courseRepository;

        public ModuleService(ICourseRepository courseRepository) 
        {
            _courseRepository = courseRepository;
        }

        public HttpStatusCode CreateModule(int courseId, string moduleTitle, int order, int createdBy)
        {
            var result = ValidateModuleTitle(moduleTitle);

            if (result == HttpStatusCode.Conflict)
            {
                return result;
            }

            var modules = _courseRepository.GetCourse(courseId).Modules;
            
            var module = new Module(CountNextModuleId(), moduleTitle, order, createdBy);

            modules.Add(module);

            UpdateModulesOrders(modules);

            return HttpStatusCode.OK;
        }

        public void DeleteModule( int moduleId)
        {
            Course course = _courseRepository.GetCourses().Find(c => c.Modules.Find(m => m.Id == moduleId) != null);
            var module = _courseRepository.GetModuleById(moduleId);

            course.Modules.Remove(module);
        }

        public HttpStatusCode UpdateModule(int moduleId, string title, string description, string content)
        {
            var result = ValidateModuleTitle(title);

            if (result == HttpStatusCode.Conflict)
            {
                return result;
            }

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

            foreach (var course in _courseRepository.GetCourses())
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

        private HttpStatusCode ValidateModuleTitle(string title)
        {
            if (_courseRepository.GetModuleByTitle(title) != null)
            {
                return HttpStatusCode.Conflict;
            }

        return HttpStatusCode.Continue;
        }

        private void UpdateModulesOrders(List<Module> modules)
        {
            foreach (var m in modules)
            {
                m.Order = modules.FindIndex(mod => mod == m) + 1;
            }
        }
    }
}
