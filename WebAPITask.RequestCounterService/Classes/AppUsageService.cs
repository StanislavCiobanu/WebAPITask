namespace WebAPITask.RequestCounterServices
{
    public class AppUsageService : IAppUsageService
    {
        private int _count = 0;

        public void IncreaseCount()
        {
            _count++;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
