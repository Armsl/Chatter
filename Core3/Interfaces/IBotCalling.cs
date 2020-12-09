using ChatterSite.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatterSite.Interfaces
{
    public interface IBotCalling
    {
        BotResponse BotDetection(string message);
    }
}
