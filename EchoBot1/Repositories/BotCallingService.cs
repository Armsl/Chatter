using ChatterBot.Interfaces;
using ChatterSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatterBot.Repositories
{
    public class BotCallingService : IBotCallingService
    {
        public HttpClient Client { get; }

        public BotCallingService(HttpClient _client)
        {
            Client = _client;
        }

        /// <summary>
        /// Download the CSV from the stooq server & generate a Stock object
        /// </summary>
        /// <param name="stock_code"></param>
        /// <returns></returns>
        public Stocks GetStock(string stock_code)
        {
            using (HttpResponseMessage response = Client.GetAsync($"https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv").Result)
            using (HttpContent content = response.Content)
            {
                var callResponse = content.ReadAsStringAsync().Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new ArgumentException(callResponse);
                var data = callResponse.Substring(callResponse.IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
                var processedArray = data.Split(',');
                return new Stocks()
                {
                    Symbol = processedArray[0],
                    Date = !processedArray[1].Contains("N/D") ? Convert.ToDateTime(processedArray[1]) : default,
                    Time = !processedArray[2].Contains("N/D") ? Convert.ToDateTime(processedArray[2]).TimeOfDay : default,
                    Open = !processedArray[3].Contains("N/D") ? Convert.ToDouble(processedArray[3]) : default,
                    High = !processedArray[4].Contains("N/D") ? Convert.ToDouble(processedArray[4]) : default,
                    Low = !processedArray[5].Contains("N/D") ? Convert.ToDouble(processedArray[5]) : default,
                    Close = !processedArray[6].Contains("N/D") ? Convert.ToDouble(processedArray[6]) : default,
                    Volume = !processedArray[7].Contains("N/D") ? Convert.ToDouble(processedArray[7]) : default,
                };
            }
        }
    }
}
