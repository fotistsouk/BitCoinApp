using MediatR;
using BTCapp.Contracts;
using BTCapp.Domain;
using BTCapp.Contracts.Mappings;
namespace BTCapp.Application.Handlers
{
    public class GetPriceMultiQueryHandler : IRequestHandler<GetPriceMultiQuery, GetPriceMultiQueryResult>
    {
        private readonly IPriceRepository _priceRepository;
        private readonly IUpdateBtcService _updatebtcService;
        public GetPriceMultiQueryHandler(IPriceRepository priceRepository, IUpdateBtcService updatebtcService)
        {
            _priceRepository = priceRepository ?? throw new ArgumentNullException(nameof(priceRepository));
            _updatebtcService = updatebtcService ?? throw new ArgumentNullException(nameof(updatebtcService));
        }
        public async Task<GetPriceMultiQueryResult> Handle(GetPriceMultiQuery query, CancellationToken cancellationToken)
        {

            DateTime start = query.Start;
            DateTime end = query.End;
            TimeSpan range = (end - start);
            int hours = (int)range.TotalHours;

            IEnumerable<Price> prices = await _priceRepository.GetPriceRangeAsync(query.Start, query.End);
            //check if we have entire range in database
            if (prices != null && prices.Count() >= hours)
            {
                //do a mapping here 
                var a = prices.MapMultiToContract();
                return new GetPriceMultiQueryResult(prices: prices.MapMultiToContract());
            }

            IEnumerable<Price> prices1 = await _updatebtcService.GetPriceFromBitStampManyAsync(start,end);
            IEnumerable<Price> prices2 = await _updatebtcService.GetPriceFromBitfinexManyAsync(start,end);
            if (prices1.Count() < prices2.Count())
                prices2 = prices1.Take(prices1.Count());
            if (prices2.Count() < prices2.Count())
                prices1 = prices1.Take(prices2.Count());
            var newprices = prices1.Zip(prices2, (p1, p2) => new Price(p1, p2)).ToList();
            await _priceRepository.AddPricesAsync(newprices, prices);
            return new GetPriceMultiQueryResult(prices: newprices.MapMultiToContract());
        }

    }
}
