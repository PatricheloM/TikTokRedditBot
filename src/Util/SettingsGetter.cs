using TiktokBot.Json;

namespace TiktokBot.Util
{
    static class SettingsGetter
    {
        private static readonly JsonHandling<Settings> Json = JsonHandling<Settings>.GetSettingsInstance();

        public static Settings GetSettings()
        {
            return Json.JsonObject;
        }
    }
}
