using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using LevelsRanksApi;
using Microsoft.Extensions.Logging;

namespace LevelsRanksModuleNameReward
{
    [MinimumApiVersion(80)]
    public class LevelsRanksModuleNameReward : BasePlugin
    {
        public override string ModuleName => "[LR] Module - NameReward";
        public override string ModuleVersion => "1.0";
        public override string ModuleAuthor => "ABKAM";

        private readonly PluginCapability<ILevelsRanksApi> _levelsRanksApiCapability = new("levels_ranks");
        private ILevelsRanksApi? _levelsRanksApi;
        private NameRewardSettings? _settings;

        public override void OnAllPluginsLoaded(bool hotReload)
        {
            base.OnAllPluginsLoaded(hotReload);

            var configPath = Path.Combine(Application.RootDirectory, "configs/plugins/LevelsRanks", "settings_namereward.json");
            _settings = ConfigLoader<NameRewardSettings>.Load(configPath);

            _levelsRanksApi = _levelsRanksApiCapability.Get();
            if (_levelsRanksApi == null)
            {
                Server.PrintToConsole("LevelsRanks API is currently unavailable.");
                return;
            }

            RegisterListener<Listeners.OnClientConnected>(OnClientConnected);
            RegisterEventHandler<EventRoundStart>(OnRoundStart);
        }

        private void OnClientConnected(int playerSlot)
        {
            var player = Utilities.GetPlayerFromSlot(playerSlot);
            if (player != null && !player.IsBot)
            {
                var steamId = player.SteamID.ToString();
                var playerName = player.PlayerName;

                if (playerName.Contains(_settings?.Website!))
                {
                    _levelsRanksApi?.SetExperienceMultiplier(steamId, _settings!.ExperienceMultiplier);
                }
            }
        }
        private HookResult OnRoundStart(EventRoundStart eventRoundStart, GameEventInfo gameEventInfo)
        {
            if (_settings!.SendRoundStartMessages)
            {
                foreach (var player in Utilities.GetPlayers().Where(p => p.AuthorizedSteamID != null))
                {
                    var playerName = player.PlayerName;

                    if (playerName.Contains(_settings.Website!))
                    {
                        player.PrintToChat(ReplaceColorPlaceholders(Localizer["message.pattern.match", _settings.Website!, _settings.ExperienceMultiplier]));
                    }
                    else
                    {
                        player.PrintToChat(ReplaceColorPlaceholders(Localizer["message.pattern.no_match", _settings.Website!, _settings.ExperienceMultiplier]));
                    }
                }
            }

            return HookResult.Continue;
        }

        public string ReplaceColorPlaceholders(string message)
        {
            if (message.Contains('{'))
            {
                var modifiedValue = message;
                foreach (var field in typeof(ChatColors).GetFields())
                {
                    var pattern = $"{{{field.Name}}}";
                    if (message.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                        modifiedValue = modifiedValue.Replace(pattern, field.GetValue(null)?.ToString(),
                            StringComparison.OrdinalIgnoreCase);
                }

                return modifiedValue;
            }

            return message;
        } 
        [ConsoleCommand("css_lvl_reload", "Reloads the configuration files")]
        public void ReloadConfigsCommand(CCSPlayerController? player, CommandInfo command)
        {
            if (player == null)
            {
                try
                {
                    var configPath = Path.Combine(Application.RootDirectory, "configs/plugins/LevelsRanks", "settings_namereward.json");
                    _settings = ConfigLoader<NameRewardSettings>.Load(configPath);
                }
                catch (Exception ex)
                {
                    Logger.LogInformation($"Error reloading configuration: {ex.Message}");
                }
            }
        }    
    }
}