// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.11.1

using System;
using System.Threading.Tasks;
using ChatterBot.Interfaces;
using ChatterSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;

namespace ChatterBot.Controllers
{
    // This ASP Controller is created to handle a request. Dependency Injection will provide the Adapter and IBot
    // implementation at runtime. Multiple different IBot implementations running at different endpoints can be
    // achieved by specifying a more specific type for the bot constructor argument.
    //[Route("api/messages")]
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotFrameworkHttpAdapter Adapter;
        private readonly IBot Bot;
        private IBotCallingService BotCalling;

        public BotController(IBotFrameworkHttpAdapter adapter, IBot bot, IBotCallingService botCalling)
        //public BotController(IBotCallingService botCalling)
        {
            Adapter = adapter;
            Bot = bot;
            BotCalling = botCalling;
        }

        [HttpPost, HttpGet]
        public async Task PostAsync()
        {
            // Delegate the processing of the HTTP POST to the adapter.
            // The adapter will invoke the bot.
            await Adapter.ProcessAsync(Request, Response, Bot);
        }

        // <summary>
        // Bot Controller
        // </summary>
        // <param name = "stock_code" ></ param >
        // < returns ></ returns >
        [HttpGet]
        [Route("GetStock")]
        public ActionResult<Stocks> GetStock(string stock_code)
        {
            try
            {
                var result = BotCalling.GetStock(stock_code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
