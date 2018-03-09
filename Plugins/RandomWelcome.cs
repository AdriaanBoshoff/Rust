using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("RandomWelcome", "Iv Misticos", "1.0.0")]
    [Description("Random Welcome for players")]
    class RandomWelcome : RustPlugin
    {
        #region Configuration
        
        private Configuration _config;

        private class Configuration
        {
            [JsonProperty(PropertyName = "Plugin Prefix (None to remove it)")]
            public string Prefix = "Welcome";
            
            [JsonProperty(PropertyName = "Welcome Messages", ObjectCreationHandling = ObjectCreationHandling.Replace)]
            public List<string> Messages = new List<string>
                                           {
                                               "{player} только что подключился - glhf!",
                                               "{player} только что подключился. Прячьте серу!",
                                               "{player} подключился. Все сделайте вид что заняты!",
                                               "{player} подключился. Лучше оставить свой лут дома!",
                                               "{player} подключился. Лучше пока не выходить из дома!",
                                               "{player} подключился. Вечерника окончена(",
                                               "{player} пришел на нашу вечеринку.",
                                               "{player} только что появился на сервере.",
                                               "{player} объявился на сервере.",
                                               "{player} показался на сервере.",
                                               "{player} явил себя. Все на колени!",
                                               "{player} пришел, чтобы надрать всем задницы!",
                                               "Где же {player}? На сервере!",
                                               "Добро пожаловать, {player}. Мы вас ждали ( ͡° ͜ʖ ͡°)",
                                               "Ну вот и {player}, которого мы так долго ждали.",
                                               "Добро пожаловать, {player}. Надеемся, что вы принесли с собой банку тушенки?",
                                               "Мужайтесь! {player} только что подключился!",
                                               "Ну все! Сушите весла! {player} подключился!",
                                               "Это птица! Это самолет! А, не важно. Это всего лишь {player}...",
                                               "Тссс... Тихо... {player} подключился..."
                                           };
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            try
            {
                _config = Config.ReadObject<Configuration>();
            }
            catch
            {
                LoadDefaultConfig();
            }
            SaveConfig();
        }

        protected override void LoadDefaultConfig() => _config = new Configuration();

        protected override void SaveConfig() => Config.WriteObject(_config);
        
        #endregion
        
        #region Oxide Hooks
        
        // ReSharper disable once UnusedParameter.Local
        // ReSharper disable once UnusedMember.Local
        private void OnPlayerInit(BasePlayer player)
        {
            string msg = _config.Messages[Random.Range(0, _config.Messages.Count)].Replace("{player}", player.displayName);
            if (_config.Prefix.ToLower().Contains("none"))
                Server.Broadcast(msg);
            else Server.Broadcast(msg, _config.Prefix);
        }
        
        #endregion
    }
}
