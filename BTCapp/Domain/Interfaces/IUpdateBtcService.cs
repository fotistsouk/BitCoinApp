namespace BTCapp.Domain
{
    public interface IUpdateBtcService
    {
        Task<Price> GetPriceFromBitStampAsync(DateTime time);
        Task<Price> GetPriceFromBitfinexAsync(DateTime time);
        Task<IEnumerable<Price>> GetPriceFromBitfinexManyAsync(DateTime start, DateTime end);
        Task<IEnumerable<Price>> GetPriceFromBitStampManyAsync(DateTime start, DateTime end);
    }
}
