namespace BTCapp.Domain
{
    public interface IPriceRepository
    {
        Task<IEnumerable<Price>> GetPriceRangeAsync(DateTime start, DateTime end);
        Task<Price?> GetPriceByTimepointAsync(DateTime time);
        Task AddPriceAsync(Price price);
        Task AddPricesAsync(IEnumerable<Price> prices, IEnumerable<Price> oldprices);
    }
}
