using Microsoft.AspNetCore.Mvc;
using PriceService.Models;
using StackExchange.Redis;

namespace PriceService.Services
{
    public class ProductService
    {
        private readonly AppDbContext _db;
        private readonly IDatabase _redis;

        public ProductService(
            AppDbContext db,
            IConnectionMultiplexer redis)
        {
            _db = db;
            _redis = redis.GetDatabase();
        }

        /// <summary>
        /// Получает цену товара по его идентификатору в БД.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <returns>Цена товара.</returns>
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        /// <summary>
        /// Добавляет товар в БД.
        /// </summary>
        /// <param name="product">Данные о товаре.</param>
        /// <returns>Созданный товар.</returns>
        public async Task<Product> CreateAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return product;
        }
    }
}
