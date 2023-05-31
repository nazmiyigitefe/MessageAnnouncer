using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenMod.API.Ioc;
using SDG.Unturned;

namespace MessageAnnouncer
{
    [ServiceImplementation(Lifetime = ServiceLifetime.Scoped)]
    public class MessageFiller : IMessageFiller
    {
        private readonly ILogger _logger;
        
        public MessageFiller(ILogger<MessageFiller> logger)
        {
            _logger = logger;
        }
        
        public Dictionary<string, Func<string>> Placeholders = new Dictionary<string, Func<string>>()
        {
            {"ONLINEPLAYERS", () => Provider.clients.Count.ToString()},
            {"MAXPLAYERS", () => Provider.maxPlayers.ToString()},
            {"SERVERNAME", () => Provider.serverName},
        };
        
        public string FillMessage(string message)
        {
            var placeholders = new List<string>();
            for (int i = 0; i < message.Length; i++)
            {
                var character = message[i];
                if (i == message.Length - 1) continue;
                if (character == '{')
                {
                    if (message[i + 1] == '{')
                    {
                        i++;
                        continue;
                    }
                    var cut = message.Substring(i + 1);
                    placeholders.Add(cut.Substring(0, cut.IndexOf("}", StringComparison.Ordinal)));
                }
            }

            foreach (var placeholder in placeholders)
            {
                if (!Placeholders.ContainsKey(placeholder))
                {
                    _logger.LogWarning($"{{{placeholder}}} is not a registered placeholder.");
                    continue;
                }

                message = message.Replace($"{{{placeholder}}}", Placeholders[placeholder]());
            }
            return message;
        }
    }
}