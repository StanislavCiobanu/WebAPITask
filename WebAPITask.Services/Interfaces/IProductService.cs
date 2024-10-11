namespace WebAPITask.Services
{
    public interface IProductService
    {
        public void CreateProduct(string name, string description, string manufacturer);
        public void DeleteProduct(int productId);
        public void UpdateProductName(int productId, string name);
    }
}
