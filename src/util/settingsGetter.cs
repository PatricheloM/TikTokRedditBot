using tiktokBot.json;

namespace tiktokBot.util
{
    class settingsGetter
    {
        private static jsonHandling<settings> json = jsonHandling<settings>.getSettingsInstance();

        public static settings getSettings()
        {
            return json.jsonObject;
        }
    }
}
