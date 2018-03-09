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
            [JsonProperty(PropertyName = "Префикс Плагина (Введите none для его удаления)")]
            public string Prefix = "Welcome";
            
            [JsonProperty(PropertyName = "Сообщения приветствия", ObjectCreationHandling = ObjectCreationHandling.Replace)]
            public List<string> WelcomeMessages = new List<string>
                                           {
                                               "<color=#FFC040>{player}</color> только что подключился - glhf!",
                                               "<color=#FFC040>{player}</color> только что подключился. Прячьте серу!",
                                               "<color=#FFC040>{player}</color> подключился. Все сделайте вид что заняты!",
                                               "<color=#FFC040>{player}</color> подключился. Лучше оставить свой лут дома!",
                                               "<color=#FFC040>{player}</color> подключился. Лучше пока не выходить из дома!",
                                               "<color=#FFC040>{player}</color> подключился. Вечерника окончена(",
                                               "<color=#FFC040>{player}</color> пришел на нашу вечеринку.",
                                               "<color=#FFC040>{player}</color> только что появился на сервере.",
                                               "<color=#FFC040>{player}</color> объявился на сервере.",
                                               "<color=#FFC040>{player}</color> показался на сервере.",
                                               "<color=#FFC040>{player}</color> явил себя. Все на колени!",
                                               "<color=#FFC040>{player}</color> пришел, чтобы надрать всем задницы!",
                                               "Где же <color=#FFC040>{player}</color>? На сервере!",
                                               "Добро пожаловать, <color=#FFC040>{player}</color>. Мы вас ждали ( ͡° ͜ʖ ͡°)",
                                               "Ну вот и <color=#FFC040>{player}</color>, которого мы так долго ждали.",
                                               "Добро пожаловать, <color=#FFC040>{player}</color>. Надеемся, что вы принесли с собой банку тушенки?",
                                               "Мужайтесь! <color=#FFC040>{player}</color> только что подключился!",
                                               "Ну все! Сушите весла! <color=#FFC040>{player}</color> подключился!",
                                               "Это птица! Это самолет! А, не важно. Это всего лишь <color=#FFC040>{player}</color>...",
                                               "Тссс... Тихо... <color=#FFC040>{player}</color> подключился..."
                                           };
            
            [JsonProperty(PropertyName = "Сообщения о выходе", ObjectCreationHandling = ObjectCreationHandling.Replace)]
            public List<string> ExitMessages = new List<string>
                                           {
                                               "<color=#FFC040>{NAME}</color> ушел. А ведь так все хорошо начиналось(",
                                               "<color=#FFC040>{NAME}</color> ушел. Можно выходить на улицу!",
                                               "<color=#FFC040>{NAME}</color> ушел. Не вздумайте его рейдить!",
                                               "<color=#FFC040>{NAME}</color> ушел... за подмогой!",
                                               "<color=#FFC040>{NAME}</color> не выдержал и ушел.",
                                               "<color=#FFC040>{NAME}</color> нас покинул.",
                                               "<color=#FFC040>{NAME}</color> устал и пошел спать.",
                                               "<color=#FFC040>{NAME}</color> ушел, но обещал вернуться.",
                                               "Мама сказала, что хватит. <color=#FFC040>{NAME}</color> ушел спать.",
                                               "Ну наконец-то <color=#FFC040>{NAME}</color> свалил с сервера."
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
            string msg = _config.WelcomeMessages[Random.Range(0, _config.WelcomeMessages.Count)]
                .Replace("{player}", player.displayName)
                .Replace("{NAME}", player.displayName);
            
            if (_config.Prefix.ToLower().Contains("none"))
                Server.Broadcast(msg);
            else Server.Broadcast(msg, _config.Prefix);
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private void OnPlayerDisconnected(BasePlayer player, string reason)
        {
            string msg = _config.ExitMessages[Random.Range(0, _config.ExitMessages.Count)]
                .Replace("{player}", player.displayName)
                .Replace("{NAME}", player.displayName);
            
            if (_config.Prefix.ToLower().Contains("none"))
                Server.Broadcast(msg);
            else Server.Broadcast(msg, _config.Prefix);
        }
        
        #endregion
    }
}
