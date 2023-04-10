using System;
using System.Net;
using TiktokBot.Web;
using TiktokBot.Video;
using TiktokBot.Text;
using TiktokBot.Util;
using System.IO;

namespace TiktokBot
{
    class Program
    {
        static void Main()
        {
            InitUtil.Init();
            string log = "logs/" + StringUtil.DirectoryNameHelper(DateTime.Now.ToString()) + "-log.log";
            string url = SettingsGetter.GetSettings().SubredditUrlOrFileName + ".rss";
            int quantity = SettingsGetter.GetSettings().NumberOfVidsPerPost;
            int threshold;
            if (quantity < 7) threshold = quantity * 25; // due to the screenshotting method we cant calculate the threshold to be perfect
            else threshold = 175; // maximum comments per rss is 200

            try
            {
                var titleLinkPairs = RssSerializer.GetSubreddit(url);

                foreach (var pair in titleLinkPairs)
                {
                    try 
                    {
                        Console.WriteLine("\n{0}\n[{1}]\n", pair.Key, pair.Value);
                        File.AppendAllText(log, String.Format("\n{0}\n[{1}]\n", pair.Key, pair.Value));

                        if (RssSerializer.GetCommentCount(pair.Value[..pair.Value.IndexOf("?sort=top&depth=1")] + ".rss?sort=top&depth=1") < threshold)
                        {
                            Console.WriteLine("\t--- Not enough comments! Going to the next post... ---\n");
                            File.AppendAllText(log, "\t--- Not enough comments! Going to the next post... ---\n");
                            continue;
                        }

                        ImageGetter.GetTitleImage(pair.Key, pair.Value[..pair.Value.IndexOf("?sort=top&depth=1")]);
                        Console.WriteLine("\t--- Saved title image! ---\n");

                        ImageGetter.GetCommentImages(pair.Key, pair.Value, quantity);
                        Console.WriteLine("\t--- Saved comment images! ---\n");

                        TextToSpeech.SaveAllSoundFiles(pair.Key, StringUtil.GetCommentsFromTxt(pair.Key)); //rssSerializer.getComments(pair.Value, quantity));
                        Console.WriteLine("\t--- Saved audio files! ---\n");

                        VideoTrimmerBasedOfLengthOfAllAudio.SaveAllVideos(pair.Key);
                        Console.WriteLine("\t--- Saved video files! ---\n");

                        VideoMux.MuxAllVideosWithAudio(pair.Key);
                        Console.WriteLine("\t--- Saved muxed files! ---\n");

                        ImageAdder.MakeVideos(pair.Key);
                        Console.WriteLine("\t--- Saved finished files! ---\n");

                        Console.WriteLine("\t--- Finished rendering {0} videos! ---\n", quantity);
                        File.AppendAllText(log, String.Format("\t--- Finished rendering {0} videos! ---\n", quantity));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error! - {0}: {1}", ex.GetType().Name, ex.Message);
                        File.AppendAllText(log, String.Format("Error! - {0}: {1}", ex.GetType().Name, ex.Message));
                    }

                }

                Console.ReadKey();
            }
            catch (WebException webEx)
            {
                Console.WriteLine("Wrong URL or file name in settings! - {0}: {1}", webEx.GetType().Name, webEx.Message);
                File.AppendAllText(log, String.Format("Wrong URL or file name in settings! - {0}: {1}", webEx.GetType().Name, webEx.Message));
                Console.ReadKey();
            }
        }
    }
}
