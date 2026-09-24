namespace PriceService.Models
{
    /// <summary>
    /// Товар.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Название товара.
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Цена товара.
        /// </summary>
        public decimal Price { get; set; }
    }
}
