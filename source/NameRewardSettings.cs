using System.Text.Json;

namespace LevelsRanksModuleNameReward
{
    public static class ConfigLoader<T> where T : new()
    {
        public static T Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var defaultSettings = new T();
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                File.WriteAllText(filePath,
                    JsonSerializer.Serialize(defaultSettings, new JsonSerializerOptions { WriteIndented = true }));
                return defaultSettings;
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json) ?? new T();
        }
    }
}

