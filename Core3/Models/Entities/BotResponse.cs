using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatterSite.Models.Entities
{
    public class BotResponse
    {
        public bool IsSuccessful { get; set; }
        public bool Detected { get; set; }
        public string Error { get; set; }
        public string Symbol { get; set; }
        public string Close { get; set; }
    }
}
