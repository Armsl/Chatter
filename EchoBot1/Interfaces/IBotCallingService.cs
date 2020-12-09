using ChatterSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatterBot.Interfaces
{
    public interface IBotCallingService
    {
        Stocks GetStock(string stock_code);
    }
}
