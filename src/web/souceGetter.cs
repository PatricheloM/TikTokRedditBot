using System.Net;

namespace tiktokBot.web
{
    class sourceGetter
    {
        protected static string dowloadSource(string url)
        {
            WebClient client = new WebClient();
            string returnVal = client.DownloadString(url);
            client.Dispose();

            return returnVal;
        }
    }
}
