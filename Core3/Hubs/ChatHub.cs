using ChatterSite.Interfaces;
using ChatterSite.Models;
using Core3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatterSite.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IBotCalling BotCalling;

        public ChatHub(IBotCalling _botCalling)
        {
            BotCalling = _botCalling;
        }

        /// <summary>
        /// Message handling logic
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
            var botResponse = BotCalling.BotDetection(message.Text);
            if (botResponse.Detected)
                if (botResponse.IsSuccessful)
                    await Clients.All.SendAsync("receiveMessage", StockBotMessage($"{botResponse.Symbol} quote is {botResponse.Close} per share"));
                else
                    await Clients.All.SendAsync("receiveMessage", StockBotMessage($"There was an error with the message received. { botResponse.Error }"));
        }

        /// <summary>
        /// Create a Bot message
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal Message StockBotMessage(string text)
        {
            return new Message
            {
                UserName = "StockBot",
                Text = text,
                CurrentTime = DateTime.Now
            };

        }
    }
}
