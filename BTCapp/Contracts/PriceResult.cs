namespace BTCapp.Contracts
{
    public class PriceResult
    {
        public float Price { get; set; }
        public DateTime TimePoint { get; set; }

        public PriceResult(float price, DateTime timepoint)
        {
            Price = price;
            TimePoint = timepoint;
        }
    }
}
