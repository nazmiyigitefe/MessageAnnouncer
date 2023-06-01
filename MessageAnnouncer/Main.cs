using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenMod.API.Plugins;
using OpenMod.UnityEngine.Extensions;
using OpenMod.Unturned.Plugins;
using SDG.Unturned;
using Action = System.Action;

[assembly:
    PluginMetadata("Nuage.MessageAnnouncer", Author = "Nuage",
        Website = "https://github.com/nazmiyigitefe/MessageAnnouncer/")]

namespace MessageAnnouncer
{
    public class MessageAnnouncerPlugin : OpenModUnturnedPlugin
    {
        private readonly IConfiguration _configuration;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IMessageFiller _filler;
        
        public MessageAnnouncerPlugin(IServiceProvider serviceProvider, IConfiguration configuration, IMessageFiller messageFiller) : base(serviceProvider)
        {
            _configuration = configuration;
            _cancellationTokenSource = new CancellationTokenSource();
            _filler = messageFiller;
        }

        protected override async UniTask OnLoadAsync()
        { 
            await Task.Run(new Action(() => Annoyer()), _cancellationTokenSource.Token);
        }

        protected override UniTask OnUnloadAsync()
        {
            _cancellationTokenSource.Cancel();
            return base.OnUnloadAsync();
        }

        private async UniTask Annoyer()
        {
            while (true)
            {
                for (int i = 0; i < _configuration.GetSection("broadcasts").GetChildren().ToList().Count; i++)
                {
                    var text = _configuration.GetSection("broadcasts").GetChildren().ToList()[i].GetValue<string>("text");
                    var color = _configuration.GetSection("broadcasts").GetChildren().ToList()[i].GetValue<string>("color");
                    var imageURL = _configuration.GetSection("broadcasts").GetChildren().ToList()[i].GetValue<string>("imageURL");
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    if (Level.isLoaded)
                    {
                        await UniTask.SwitchToMainThread();
                        ChatManager.serverSendMessage(_filler.FillMessage(text), ColorTranslator.FromHtml(color).ToUnityColor(), null, null, EChatMode.SAY, imageURL, true);
                        await UniTask.SwitchToThreadPool();
                    }
                    await Task.Delay(_configuration.GetValue<int>("seconds_between_messages") * 1000);
                }
            }
        }
    }
}
