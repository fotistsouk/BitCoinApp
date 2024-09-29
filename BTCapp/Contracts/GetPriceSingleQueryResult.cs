namespace BTCapp.Contracts
{
    public class GetPriceSingleQueryResult 
    {
        public PriceResult Price { get; set; }

        public GetPriceSingleQueryResult(PriceResult price)
        {
            Price = price;
        }
    }
}
