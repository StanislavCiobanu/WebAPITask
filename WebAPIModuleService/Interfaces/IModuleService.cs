using System.Net;
using WebAPITask.Models;

namespace WebAPITask.Services
{
    public interface IModuleService
    {
        public Module CreateModule(int courseId, string moduleTitle, int order, int createdBy);
        public void DeleteModule(int moduleId);
        public HttpStatusCode UpdateModule(int moduleId, string title, string description, string content);
        public void DeleteModules(List<Module> modules);

    }
}
