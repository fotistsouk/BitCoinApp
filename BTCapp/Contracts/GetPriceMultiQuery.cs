using MediatR;
namespace BTCapp.Contracts
{
    public class GetPriceMultiQuery : IRequest<GetPriceMultiQueryResult>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public GetPriceMultiQuery(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
