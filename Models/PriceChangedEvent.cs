namespace PriceService.Models
{
    /// <summary>
    /// Событие изменения цены товара.
    /// </summary>
    public class PriceChangedEvent
    {
        /// <summary>
        /// Идентификатор товара в основной БД.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Старая цена.
        /// </summary>
        public decimal OldPrice { get; set; }
        /// <summary>
        /// Новая цена.
        /// </summary>
        public decimal NewPrice { get; set; }
    }
}
