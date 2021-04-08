using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Players.Connections.Events;
using SDG.Unturned;
using UnityEngine;

namespace ChatAnnoyer
{
    public class MOTDSender : IEventListener<UnturnedPlayerConnectedEvent>
    {
        private readonly IConfiguration m_Configuration;
        private readonly IMessageFiller m_Filler;

        public MOTDSender(IConfiguration configuration, IMessageFiller messageFiller)
        {
            m_Configuration = configuration;
            m_Filler = messageFiller;
        }
        
        public async Task HandleEventAsync(object? sender, UnturnedPlayerConnectedEvent @event)
        {
            foreach (var section in m_Configuration.GetSection("motd").GetChildren().ToList())
            {
                var text = section.GetValue<string>("text");
                var colour = section.GetValue<string>("colour");
                await @event.Player.PrintMessageAsync(m_Filler.FillMessage(text), ColorTranslator.FromHtml(colour));
            }
        }
    }
}