using Microsoft.AspNetCore.Mvc;
using WebAPITask.Configuration;
using WebAPITask.Models;

namespace WebAPITask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDataController : ControllerBase
    {
        private readonly ITestDataService _testDataService;
        public TestDataController(ITestDataService dataService) { _testDataService = dataService; }

        [HttpGet("GetTestData")]
        public List<Course> GetTestData()
        {
            return _testDataService.GetTestData();
        }
    }
}
