using BTCapp.API.ApiModel;
using BTCapp.Contracts;
namespace BTCapp.API.Mappings
{
    public static class PriceMappings{

        public static BitCoinPriceResponse MapToApi(this PriceResult price)
        {
            return new BitCoinPriceResponse() { Value = price.Price, TimeOfValue = price.TimePoint };
        }

        public static IEnumerable<BitCoinPriceResponse> MapMultiToApi(this IEnumerable<PriceResult> prices)
        {
            return prices.Select(MapToApi).ToList();
        }
    }
    
}
