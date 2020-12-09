using ChatterSite.Interfaces;
using ChatterSite.Models;
using ChatterSite.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatterSite.Repositories
{
    public class BotCalling : IBotCalling
    {
        private HttpClient client { get; set; }

        public BotCalling(HttpClient _client)
        {
            client = _client;
        }

        /// <summary>
        /// Detects if the message contains the shortcut that allows the Bot to do his magic
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BotResponse BotDetection(string message)
        {
            try
            {
                if (message.ToLower().Contains("/stock="))
                {
                    string code = message.Replace("/stock=", "");
                    using (HttpResponseMessage response = client.GetAsync($"http://localhost:3978/api/Bot/GetStock?stock_code={code}").Result)
                    using (HttpContent content = response.Content)
                    {
                        string serviceResponse = content.ReadAsStringAsync().Result;
                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            return new BotResponse { Detected = true, IsSuccessful = false, Error = response.StatusCode.ToString() };

                        var stock = JsonConvert.DeserializeObject<Stocks>(serviceResponse);
                        return new BotResponse { Detected = true, IsSuccessful = true, Symbol = stock.Symbol, Close = stock.Close.ToString() };
                    }
                }
                return new BotResponse { Detected = false };
            }
            catch (Exception ex)
            {
                return new BotResponse { Detected = true, IsSuccessful = false, Error = ex.Message };
            }
        }
    }
}
