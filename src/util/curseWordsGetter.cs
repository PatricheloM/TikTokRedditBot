using TiktokBot.Json;
using System.Collections.Generic;

namespace TiktokBot.Util
{
    static class CurseWordsGetter
    {
        private static readonly JsonHandling<Dictionary<string, string>> Json = JsonHandling<Dictionary<string, string>>.GetCurseWordsInstance();

        public static Dictionary<string, string> GetCurseWords()
        {
            return Json.JsonObject;
        }
    }
}
