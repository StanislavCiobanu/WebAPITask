using Microsoft.Extensions.Options;
using WebAPITask.Models;

namespace WebAPITask.Configuration
{
    public class TestDataService : ITestDataService
    {
        private readonly TestDataOptions _options;

        public TestDataService(IOptions<TestDataOptions> options)
        {
            _options = options.Value;
        }

        public List<Course> GetTestData()
        {
            return _options.Courses;
        }
    }
}
