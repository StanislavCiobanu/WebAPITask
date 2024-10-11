using WebAPITask.Models;

namespace WebAPITask.Configuration
{
    public interface ITestDataService
    {
        public List<Course> GetTestData();
    }
}
