namespace WebAPITask.TimerService
{
    public class TimerService : ITimerService
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
