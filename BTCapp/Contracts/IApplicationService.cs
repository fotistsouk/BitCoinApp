namespace BTCapp.Contracts
{
    public interface IApplicationService
    {
        Task <GetPriceSingleQueryResult> GetPrice(GetPriceSingleQuery query, CancellationToken cancellationToken);
        Task<GetPriceMultiQueryResult> GetPrices(GetPriceMultiQuery query, CancellationToken cancellationToken);
    }
}
