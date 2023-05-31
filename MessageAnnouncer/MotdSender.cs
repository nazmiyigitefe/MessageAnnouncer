using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Players.Connections.Events;

namespace MessageAnnouncer
{
    public class MotdSender : IEventListener<UnturnedPlayerConnectedEvent>
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageFiller _filler;

        public MotdSender(IConfiguration configuration, IMessageFiller messageFiller)
        {
            _configuration = configuration;
            _filler = messageFiller;
        }
        
        public async Task HandleEventAsync(object? sender, UnturnedPlayerConnectedEvent @event)
        {
            foreach (var section in _configuration.GetSection("motd").GetChildren().ToList())
            {
                var text = section.GetValue<string>("text");
                var color = section.GetValue<string>("color");
                await @event.Player.PrintMessageAsync(_filler.FillMessage(text), ColorTranslator.FromHtml(color));
            }
        }
    }
}