using System.IO;
using Newtonsoft.Json;

namespace AutomationExerciseTests.Utils
{
    public class TestConfiguration
    {
        public string ApiBaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
    }

    public static class ConfigurationManager
    {
        public static TestConfiguration GetConfiguration()
        {
            var configJson = File.ReadAllText("config.json");
            return JsonConvert.DeserializeObject<TestConfiguration>(configJson);
        }
    }
}
