using System.IO;
using Newtonsoft.Json;

namespace TiktokBot.Json
{
    class JsonHandling<T>
    {
        public T JsonObject { get; set; }

        private static readonly JsonHandling<T> InstanceSettings = new JsonHandling<T>("settings");

        private static readonly JsonHandling<T> InstanceCurseWords = new JsonHandling<T>("cursewords");

        public static JsonHandling<T> GetSettingsInstance()
        {
            return InstanceSettings;
        }

        public static JsonHandling<T> GetCurseWordsInstance()
        {
            return InstanceCurseWords;
        }

        private JsonHandling(string jsonName)
        {
            string json;
            using (StreamReader r = new StreamReader("Resources/" + jsonName + ".json"))
            {
                json = r.ReadToEnd();
            }
            JsonObject = JsonConvert.DeserializeObject<T>(json);
        }
    }
}
