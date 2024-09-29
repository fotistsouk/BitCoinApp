using System.Collections.Generic;
using System.Linq;
using BTCapp.Domain;
namespace BTCapp.Contracts.Mappings
{
    

    public static class PriceMappings
    {
        public static PriceResult MapToContract(this Price price)
        {
            return new PriceResult (  price :price.CloseAmount, timepoint : price.TimePoint );
        }

        public static IEnumerable<PriceResult> MapMultiToContract(this IEnumerable<Price> prices)
        {
            return prices.Select(MapToContract).ToList();
        }
    }

}
