using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using SDG.Unturned;
using UnityEngine;
using Color = UnityEngine.Color;

[assembly: PluginMetadata("Charterino.ChatAnnoyer")]
namespace ChatAnnoyer
{
    public class ChatAnnoyerPlugin : OpenModUnturnedPlugin
    {
        private readonly IConfiguration m_Configuration;
        private CancellationTokenSource m_CancellationTokenSource;
        private readonly IMessageFiller m_Filler;
        
        public ChatAnnoyerPlugin(IServiceProvider serviceProvider, IConfiguration configuration, IMessageFiller messageFiller) : base(serviceProvider)
        {
            m_Configuration = configuration;
            m_CancellationTokenSource = new CancellationTokenSource();
            m_Filler = messageFiller;
        }

        protected override async UniTask OnLoadAsync()
        {
            await Task.Run(() => Annoyer().WithCancellation(m_CancellationTokenSource.Token));
        }

        protected override UniTask OnUnloadAsync()
        {
            m_CancellationTokenSource.Cancel();
            return base.OnUnloadAsync();
        }

        private async UniTask Annoyer()
        {
            while (true)
            {
                for (int i = 0; i < m_Configuration.GetSection("broadcasts").GetChildren().ToList().Count; i++)
                {
                    var text = m_Configuration.GetSection("broadcasts").GetChildren().ToList()[i].GetValue<string>("text");
                    var colour = m_Configuration.GetSection("broadcasts").GetChildren().ToList()[i].GetValue<string>("colour");
                    var image_url = m_Configuration.GetSection("broadcasts").GetChildren().ToList()[i].GetValue<string>("image_url");
                    m_CancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (Level.isLoaded)
                    {
                        await UniTask.SwitchToMainThread();
                        ChatManager.serverSendMessage(m_Filler.FillMessage(text), ColourTranslator.GetColourFromName(colour, Color.white), null, null, EChatMode.SAY, image_url, true);
                        await UniTask.SwitchToThreadPool();
                    }
                    await Task.Delay(m_Configuration.GetValue<int>("seconds_between_messages") * 1000);
                }
            }
        }
    }
}