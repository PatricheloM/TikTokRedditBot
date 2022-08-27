using tiktokBot.json;
using System.Collections.Generic;

namespace tiktokBot.util
{
    class curseWordsGetter
    {
        private static jsonHandling<Dictionary<string, string>> json = jsonHandling<Dictionary<string, string>>.getCurseWordsInstance();

        public static Dictionary<string, string> getCurseWords()
        {
            return json.jsonObject;
        }
    }
}
