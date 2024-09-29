using Moq;
using BTCapp.Contracts;
using BTCapp.Application.Handlers;
using BTCapp.Domain;
using Xunit;
using System;
using System.Threading;
using System.Collections.Generic;
namespace BTC_Tests
{
    public class GetPriceMultiQueryHandlerTests
    {
        public GetPriceMultiQueryHandler handler;
        public Mock<IPriceRepository> _pricerepository;
        public Mock<IUpdateBtcService> _btcservice;
        public GetPriceMultiQueryHandlerTests()
        {
            _pricerepository = new Mock<IPriceRepository>();
            _btcservice = new Mock<IUpdateBtcService>();
            handler = new GetPriceMultiQueryHandler(_pricerepository.Object, _btcservice.Object);

        }
        [Fact]
        public async void GetPricesOk()
        {
                var fakePrices = new List<Price>()
            {
                new Price (1,DateTime.Now)
            };

            // Configure the mock to return the fake data
            _pricerepository.Setup(repo => repo.GetPriceRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(fakePrices);
            var notification = new GetPriceMultiQuery(start: DateTime.Now.AddHours(-1), end: DateTime.Now);
            var result = await handler.Handle(notification, CancellationToken.None);
            Assert.NotNull(result);
            Assert.NotNull( result.Prices);

        }
        
    }
}