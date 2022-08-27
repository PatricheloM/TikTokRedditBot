using System.IO;
using Newtonsoft.Json;

namespace tiktokBot.json
{
    class settings
    {
        public string backgroundVideoPath { get; set; }
        public string subredditUrlOrFileName { get; set; }
        public int numberOfVidsPerPost { get; set; }
    }

    class jsonHandling<T>
    {
        public T jsonObject { get; set; }

        private static jsonHandling<T> instanceSettings = new jsonHandling<T>("settings");

        private static jsonHandling<T> instanceCurseWords = new jsonHandling<T>("cursewords");

        public static jsonHandling<T> getSettingsInstance()
        {
            return instanceSettings;
        }

        public static jsonHandling<T> getCurseWordsInstance()
        {
            return instanceCurseWords;
        }

        private jsonHandling(string jsonName)
        {
            string json;
            using (StreamReader r = new StreamReader("resources/" + jsonName + ".json"))
            {
                json = r.ReadToEnd();
            }
            jsonObject = JsonConvert.DeserializeObject<T>(json);
        }
    }
}
