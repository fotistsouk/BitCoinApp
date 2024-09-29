namespace BTCapp.Contracts
{
    public class GetPriceMultiQueryResult
    {
        public IEnumerable<PriceResult> Prices { get; set; }

        public GetPriceMultiQueryResult(IEnumerable<PriceResult> prices)
        {
            Prices = prices;
        }
    }
    
}
