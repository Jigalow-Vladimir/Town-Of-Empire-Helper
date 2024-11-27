using Town_Of_Empire_Helper.Models.Entities;
using Town_Of_Empire_Helper.Models.Entities.RoleInfo;
using System.Text.Json;
using System.IO;

namespace Town_Of_Empire_Helper
{
    public static class RoleConfigurationHandler
    {
        //                                |характеристика    |роль   |значение
        public readonly static Dictionary<string, Dictionary<string, int>> Stats;
        public readonly static Dictionary<string, Dictionary<string, string>> OtherStats;
        public readonly static Dictionary<StatusType, string> Statuses;

        static RoleConfigurationHandler()
        {
            Stats = [];
            OtherStats = [];
            Statuses = [];

            LoadJsonFiles("src/stats/", Stats);
            LoadJsonFiles("src/otherStats/", OtherStats);
            LoadStatuses("src/statuses.json", Statuses);
        }

        static void LoadJsonFiles<T>(string directoryPath, Dictionary<string, T> outputDictionary)
        {
            if (!Directory.Exists(directoryPath)) return;

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                try
                {
                    string key = Path.GetFileNameWithoutExtension(file);
                    string json = File.ReadAllText(file);
                    var data = JsonSerializer.Deserialize<T>(json);
                    if (data != null)
                    {
                        outputDictionary.Add(key, data);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading file '{file}': {ex.Message}");
                }
            }
        }

        static void LoadStatuses(string filePath, Dictionary<StatusType, string> statuses)
        {
            if (!File.Exists(filePath)) return;

            try
            {
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer
                    .Deserialize<Dictionary<StatusType, string>>(json);

                if (data != null)
                    foreach (var item in data)
                    {
                        statuses.Add(item.Key, item.Value);
                    };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading statuses from '{filePath}': {ex.Message}");
            }
        }

        public static void Configurate(string name, Role role)
        {
            role.Name = name;
            ConfigurateStats(name, role);
            ConfigurateInfoStats(name, role);
            ConfigurateStatuses(role);
        }

        private static void ConfigurateInfoStats(string name, Role role)
        {
            foreach (var otherStat in OtherStats)
            {
                bool contains = otherStat.Value.ContainsKey(name);
                role.OtherStats
                    .Add(otherStat.Key,
                    new Stat<string>(
                        name: otherStat.Key,
                        value: contains ? otherStat.Value[name] : "-",
                        priority: 0,
                        endTime: null));
            }
        }

        private static void ConfigurateStats(string name, Role role)
        {
            foreach (var stat in Stats)
            {
                bool contains = stat.Value.ContainsKey(name);
                role.Stats
                    .Add(stat.Key,
                    new Stat<int>(
                        name: stat.Key,
                        value: contains ? stat.Value[name] : 0,
                        priority: 0,
                        endTime: null));
            }
        }

        private static void ConfigurateStatuses(Role role)
        {
            foreach (var status in Statuses)
                role.Statuses
                    .Add(status.Key, new Status(status.Key));
        }
    }
}
