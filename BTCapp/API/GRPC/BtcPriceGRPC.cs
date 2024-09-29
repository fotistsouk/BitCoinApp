using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using BTCapp.Contracts;
namespace BTCapp.API.GRPC
{
    public class BtcPriceGRPC : BtcPriceService.BtcPriceServiceBase
    {
        private readonly ILogger<BtcPriceGRPC> _logger;
        public IApplicationService _appservice;

        public BtcPriceGRPC(ILogger<BtcPriceGRPC> logger, IApplicationService appservice)
        {
            _logger = logger;
            _appservice = appservice;
        }

        // Override the GetPrice method to implement the logic
        public override async Task<GetPriceResponse> GetPrice(GetPriceRequest request, ServerCallContext context)
        {
            // Parse the date from the request
            DateTime parsedDate;
            if (!DateTime.TryParse(request.Date, out parsedDate))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid date format. Please use YYYY-MM-DD."));
            }

            // Validate the hour
            if (request.Hour < 0 || request.Hour > 23)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid hour. Must be between 0 and 23."));
            }

            var query = new GetPriceSingleQuery(parsedDate);
            GetPriceSingleQueryResult queryresponse = await _appservice.GetPrice(query, CancellationToken.None);
            // Return the response containing the float value and the combined DateTime
            var grpcresponse = new GetPriceResponse
            {
                Value = queryresponse.Price.Price,
                DateTime = Timestamp.FromDateTime(queryresponse.Price.TimePoint)  // Convert to UTC
            };
            return grpcresponse;
            // Log the operation

        }
    }
}
