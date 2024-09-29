using MediatR;
using BTCapp.Contracts;

namespace BTCapp.Application
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMediator _mediator;

        public ApplicationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<GetPriceSingleQueryResult> GetPrice(GetPriceSingleQuery query, CancellationToken cancellationToken)
        => await _mediator.Send(query, cancellationToken);
        public async Task<GetPriceMultiQueryResult> GetPrices(GetPriceMultiQuery query, CancellationToken cancellationToken)
        => await _mediator.Send(query, cancellationToken);
    }
}
