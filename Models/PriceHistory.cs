namespace PriceService.Models
{
    /// <summary>
    /// История изменения цен.
    /// </summary>
    public class PriceHistory
    {
        /// <summary>
        /// Идентификатор операции.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор товара в БД.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Старая цена товара.
        /// </summary>
        public decimal OldPrice { get; set; }
        /// <summary>
        /// Новая цена товара.
        /// </summary>
        public decimal NewPrice { get; set; }
        /// <summary>
        /// Дата и время операции.
        /// </summary>
        public DateTime ChangedAt { get; set; }
    }
}
