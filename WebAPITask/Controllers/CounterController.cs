using Microsoft.AspNetCore.Mvc;
using WebAPITask.RequestCounterServices;

namespace WebAPITask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly IRequestCounterService _counterService;
        private readonly IAppUsageService _usageService;

        public CounterController(IRequestCounterService counterService, 
            IAppUsageService usageService)
        {
            _counterService = counterService;
            _usageService = usageService;
        }

        [HttpGet("stats")]
        public Object GetStats()
        {
            return new {AllRequests = _usageService.GetCount(), 
                UsagesPerRequest = _counterService.GetCount() };
        }
    }
}