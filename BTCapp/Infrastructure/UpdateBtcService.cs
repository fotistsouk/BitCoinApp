using System.Text.Json;
using BTCapp.Domain;
using System.Globalization;
namespace BTCapp.Infrastructure
{
    public class UpdateBtcService  : IUpdateBtcService 
    {
        private readonly HttpClient _httpClient;

        public UpdateBtcService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Price> GetPriceFromBitStampAsync(DateTime time)
        {
            string baseurl = "https://www.bitstamp.net/api/v2/ohlc/btcusd/?step=3600&";
            long unixTimestamp = ((DateTimeOffset)time).ToUnixTimeSeconds();
            var url = baseurl+ "limit=1&start="+unixTimestamp.ToString();
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var price = jsonDocument.RootElement
                                    .GetProperty("data")
                                    .GetProperty("ohlc")
                                    .EnumerateArray()
                                    .First().GetProperty("close");
            var close = float.Parse(price.GetString(), CultureInfo.InvariantCulture);
            return new Price((float)close, time);

        }
        public async Task<IEnumerable<Price>> GetPriceFromBitStampManyAsync(DateTime start, DateTime end)
        {
            string baseurl = "https://www.bitstamp.net/api/v2/ohlc/btcusd/?step=3600&";
            long unixTimestampstart = ((DateTimeOffset)start).ToUnixTimeSeconds();
            long unixTimestampend = ((DateTimeOffset)end).ToUnixTimeSeconds();
            TimeSpan range = (end - start);
            int hours = (int)range.TotalHours;
            if (hours > 1000)
                hours = 1000;//common API limit
            var url = baseurl + "limit="+ hours.ToString()+"&start=" + unixTimestampstart.ToString()+ "&end="+ unixTimestampend.ToString();  
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var prices = jsonDocument.RootElement
                                    .GetProperty("data")
                                    .GetProperty("ohlc").EnumerateArray().Select(
                x => new Price(float.Parse(x.GetProperty("close").GetString(), CultureInfo.InvariantCulture),
                ToDateTime.SecondsToDateTime(Double.Parse(x.GetProperty("timestamp").GetString()))
                )
                ) .ToList();
            //var price2=price.(x =>
            //                        new Price(
            //                            float.Parse(x.GetProperty("close").GetString(), CultureInfo.InvariantCulture),
            //                            ToDateTime.MilliToDateTime(x.EnumerateArray().ElementAt(2).GetInt64())
            //                            )
            //                        )
            //                        .ToList();
            return prices;

        }
        public async Task<Price> GetPriceFromBitfinexAsync(DateTime time)
        {
            string baseurl = "https://api-pub.bitfinex.com/v2/candles/trade:1h:tBTCUSD/hist?start=";
            long unixTimestamp = ((DateTimeOffset)time).ToUnixTimeMilliseconds();
            long unixTimestampend = unixTimestamp + 3600000;
            var url = baseurl + unixTimestamp.ToString()+ "&end=" + unixTimestampend.ToString() + "&limit=1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var close = jsonDocument.RootElement
                                    .EnumerateArray()
                                    .First().EnumerateArray().ElementAt(2).GetDecimal();
            return new Price((float)close, time);
        }
        public async Task<IEnumerable<Price>> GetPriceFromBitfinexManyAsync(DateTime start,DateTime end)
        {
            string baseurl = "https://api-pub.bitfinex.com/v2/candles/trade:1h:tBTCUSD/hist?start=";
            long unixTimestamp = ((DateTimeOffset)start).ToUnixTimeMilliseconds();
            long unixTimestampend = ((DateTimeOffset)end).ToUnixTimeMilliseconds();
            var url = baseurl + unixTimestamp.ToString() + "&end=" + unixTimestampend.ToString() + "&limit=1000"; //common API limit 
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(content);
            var prices = jsonDocument.RootElement
                                    .EnumerateArray().Select(x => 
                                    new Price(
                                        (float)x.EnumerateArray().ElementAt(2).GetDecimal(),
                                        ToDateTime.MilliToDateTime(x.EnumerateArray().ElementAt(0).GetDouble())
                                        )
                                    )
                                    .ToList();
            prices.Reverse();
            return prices;
        }
        private static class ToDateTime
        {
            public static DateTime MilliToDateTime(double milliseconds)
            {
                return new DateTime(1970, 1, 1).AddMilliseconds(milliseconds);
            }
            public static DateTime SecondsToDateTime(double seconds)
            {
                return new DateTime(1970, 1, 1).AddSeconds(seconds);
            }
        }
    }
    

}

