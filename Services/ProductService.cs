using PriceService.Models;
using StackExchange.Redis;
using System.Text.Json;

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
        /// Получает цену товара по его идентификатору.
        /// </summary>
        /// <remarks>Если товар есть в кэше, возвращается значение.
        /// Если нет, значение получается из БД и добавляется в кэш.</remarks>
        /// <param name="id">Идентификатор товара.</param>
        /// <returns>Цена товара.</returns>
        public async Task<Product?> GetByIdAsync(int id)
        {
            var key = "product:" + id;
            var cashedValue = await _redis.StringGetAsync(key);

            if (cashedValue != RedisValue.Null)
            {
                return JsonSerializer.Deserialize<Product>(cashedValue.ToString());
            }

            var product = await _db.Products.FindAsync(id);

            if (product is null)
                return null;

            await _redis.StringSetAsync(
                key, 
                JsonSerializer.Serialize(product),
                TimeSpan.FromMinutes(5));

            return product;
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
