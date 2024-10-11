namespace WebAPITask.RequestCounterServices
{
    public interface IRequestCounterService
    {
        public void IncreaseCount();
        public int GetCount();
    }
}