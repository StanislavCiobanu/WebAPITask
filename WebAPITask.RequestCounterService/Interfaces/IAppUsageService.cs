namespace WebAPITask.RequestCounterServices
{
    public interface IAppUsageService
    {
        public void IncreaseCount();
        public int GetCount();
    }
}
