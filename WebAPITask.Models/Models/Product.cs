namespace WebAPITask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }

        public Product(int id, string name, string description, string manufacturer)
        {
            Id = id;
            Name = name;
            Description = description;
            Manufacturer = manufacturer;
        }
    }
}
