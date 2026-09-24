using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceService.Models;
using PriceService.Services;

namespace PriceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Получает цену товара по его идентификатору в БД.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <returns>Цена товара.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrice(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Добавляет товар в БД.
        /// </summary>
        /// <param name="product">Данные о товаре.</param>
        /// <returns>Код 200.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _productService.CreateAsync(product);

            return Ok(product);
        }
    }
}
