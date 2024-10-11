using System.Net;

namespace WebAPITask.Services
{
    public interface IModuleService
    {
        public HttpStatusCode CreateModule(int courseId, string moduleTitle, int order, int createdBy);
        public void DeleteModule(int moduleId);
        public HttpStatusCode UpdateModule(int moduleId, string title, string description, string content);
    }
}
