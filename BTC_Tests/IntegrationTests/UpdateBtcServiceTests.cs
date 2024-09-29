using Xunit;
using BTCapp.Infrastructure;
using System.Net.Http;
using System;
namespace BTC_Tests
{
    public class UpdateBtcServiceTests
    {
        public HttpClient _httpClient;
        public UpdateBtcServiceTests()
        {
            _httpClient = new HttpClient();
        }
        [Fact]
        public async void GetPriceFromBitStampAsyncOldOk()
        {
            var service = new UpdateBtcService(_httpClient);
            DateTime time = new DateTime(2022, 01, 01, 1, 0, 0);
            await service.GetPriceFromBitStampAsync( time);
        }
        [Fact]
        public async void GetPriceFromBitStampAsyncNowOk()
        {
            var service = new UpdateBtcService(_httpClient);
            DateTime time = DateTime.Now;
            await service.GetPriceFromBitStampAsync(time);
        }

        [Fact] 
        public async void GetPriceFromBitfinexAsyncOldOk()
        {
            var service = new UpdateBtcService(_httpClient);
            DateTime time = new DateTime(2022,01,01,1,0,0);
            await service.GetPriceFromBitfinexAsync(time);
        }
        [Fact] 
        public async void GetPriceFromBitfinexAsyncNowOk()
        {
            var service = new UpdateBtcService(_httpClient);
            DateTime time = DateTime.Now.AddDays(-1);
            await service.GetPriceFromBitfinexAsync(time);
        }

        [Fact] 
        public async void GetPriceFromBitfinexAsyncMultiOk()
        {
            var service = new UpdateBtcService(_httpClient);
            DateTime start = DateTime.Now.AddDays(-1);
            DateTime end = DateTime.Now;
            await service.GetPriceFromBitfinexManyAsync(start,end);
        }

        [Fact] 
        public async void GetPriceFromBitStampManyAsyncOk()
        {
            var service = new UpdateBtcService(_httpClient);
            DateTime start = DateTime.Now.AddDays(-1);
            DateTime end = DateTime.Now;
            await service.GetPriceFromBitStampManyAsync(start, end);
        }
    }
}