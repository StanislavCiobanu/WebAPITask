using Microsoft.AspNetCore.Mvc;
using WebAPITask.Services;

namespace WebAPITask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(string name, string description, string Manufacturer)
        {
            _productService.CreateProduct(name, description, Manufacturer);
            return Ok("Product was successfully created");
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productId)
        {
            if (productId >= 0)
            {
                _productService.DeleteProduct(productId);
                return Ok("Product was successfully deleted");
            }
            else
            {
                return Conflict("Wrong identifier");
            }
        }

        [HttpPatch("UpdateProductName")]
        public IActionResult UpdateProductName(int productId, string name)
        {
            if (productId >= 0)
            {
                _productService.UpdateProductName(productId, name);
                return Ok("Product was successfully updated");
            }
            else
            {
                return Conflict("Wrong identifier");
            }
        }

    }
}
