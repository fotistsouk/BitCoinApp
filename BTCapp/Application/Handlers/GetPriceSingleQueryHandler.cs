using MediatR;
using BTCapp.Contracts;
using BTCapp.Domain;
using BTCapp.Contracts.Mappings;
namespace BTCapp.Application.Handlers
{
    public class GetPriceSingleQueryHandler :IRequestHandler<GetPriceSingleQuery , GetPriceSingleQueryResult>
    {
        private readonly IPriceRepository _priceRepository;
        private  readonly IUpdateBtcService _updatebtcService;
        public GetPriceSingleQueryHandler(IPriceRepository priceRepository, IUpdateBtcService updatebtcService)
        {
            _priceRepository = priceRepository ?? throw new ArgumentNullException(nameof(priceRepository));
            _updatebtcService = updatebtcService ?? throw new ArgumentNullException(nameof(updatebtcService));
        }
        public async Task<GetPriceSingleQueryResult> Handle(GetPriceSingleQuery query, CancellationToken cancellationToken)
        {
            DateTime roundedDownTime = new DateTime(query.Timepoint.Year, query.Timepoint.Month, query.Timepoint.Day, query.Timepoint.Hour,0,0);
            var price = await _priceRepository.GetPriceByTimepointAsync(roundedDownTime);
            if (price != null)
            {
                return new GetPriceSingleQueryResult(price.MapToContract());
            }

            Price price1 = await _updatebtcService.GetPriceFromBitStampAsync(roundedDownTime);
            Price price2 = await _updatebtcService.GetPriceFromBitfinexAsync(roundedDownTime);
            Price newprice = new Price(price1, price2);
            await _priceRepository.AddPriceAsync(newprice);
            return new GetPriceSingleQueryResult(newprice.MapToContract());
        }

    }
}
