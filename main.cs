using System;
using System.Net;
using tiktokBot.web;
using tiktokBot.video;
using tiktokBot.text;
using tiktokBot.util;
using System.IO;

namespace tiktokBot
{
    class main
    {
        static void Main(string[] args)
        {
            initUtil.init();
            string log = "logs/" + stringUtil.directoryNameHelper(DateTime.Now.ToString()) + "-log.log";
            string url = settingsGetter.getSettings().subredditUrlOrFileName + ".rss";
            int quantity = settingsGetter.getSettings().numberOfVidsPerPost;
            int threshold;
            if (quantity < 7) threshold = quantity * 25; // due to the screenshotting method we cant calculate the threshold to be perfect
            else threshold = 175; // maximum comments per rss is 200

            try
            {
                var titleLinkPairs = rssSerializer.getSubreddit(url);

                foreach (var pair in titleLinkPairs)
                {
                    try 
                    {
                        Console.WriteLine("\n{0}\n[{1}]\n", pair.Key, pair.Value);
                        File.AppendAllText(log, String.Format("\n{0}\n[{1}]\n", pair.Key, pair.Value));

                        if (rssSerializer.getCommentCount(pair.Value.Substring(0, pair.Value.IndexOf("?sort=top&depth=1")) + ".rss?sort=top&depth=1") < threshold)
                        {
                            Console.WriteLine("\t--- Not enough comments! Going to the next post... ---\n");
                            File.AppendAllText(log, "\t--- Not enough comments! Going to the next post... ---\n");
                            continue;
                        }

                        imageGetter.getTitleImage(pair.Key, pair.Value.Substring(0, pair.Value.IndexOf("?sort=top&depth=1")));
                        Console.WriteLine("\t--- Saved title image! ---\n");

                        imageGetter.getCommentImages(pair.Key, pair.Value, quantity);
                        Console.WriteLine("\t--- Saved comment images! ---\n");

                        textToSpeech.saveAllSoundFiles(pair.Key, stringUtil.getCommentsFromTxt(pair.Key)); //rssSerializer.getComments(pair.Value, quantity));
                        Console.WriteLine("\t--- Saved audio files! ---\n");

                        videoTrimmerBasedOfLengthOfAllAudio.saveAllVideos(pair.Key);
                        Console.WriteLine("\t--- Saved video files! ---\n");

                        videoMux.muxAllVideosWithAudio(pair.Key);
                        Console.WriteLine("\t--- Saved muxed files! ---\n");

                        imageAdder.makeVideos(pair.Key, pair.Value, quantity);
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
