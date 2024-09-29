using MediatR;
namespace BTCapp.Contracts
{
    public class GetPriceSingleQuery : IRequest<GetPriceSingleQueryResult>
    {
        public DateTime Timepoint { get; set; }

        public GetPriceSingleQuery(DateTime timepoint)
        {
            this.Timepoint = timepoint;
        }
    }
}
