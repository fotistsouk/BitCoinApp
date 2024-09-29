namespace BTCapp.Domain
{
    public class Price
    {
        public Price(float closeAmount,  DateTime timePoint)
        {
            CloseAmount = closeAmount;
            TimePoint = timePoint;
        }
        public Price(Price price1, Price price2)
        {
            CloseAmount = (price1.CloseAmount+price2.CloseAmount)/2;

            TimePoint = price1.TimePoint==price2.TimePoint ? price1.TimePoint : throw new ArgumentException();
        }
        public Price(Price price1, Price price2 , Price price3)
        {
            CloseAmount = (price1.CloseAmount + price2.CloseAmount+ price3.CloseAmount) / 3;

            TimePoint = price1.TimePoint == price2.TimePoint && price2.TimePoint == price3.TimePoint ? price1.TimePoint : throw new ArgumentException();
        }
        public int Id { get; }
        public float CloseAmount {get;}
        public DateTime TimePoint { get; }
    }
}
