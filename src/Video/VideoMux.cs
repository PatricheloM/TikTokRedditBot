using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TiktokBot.Util;

namespace TiktokBot.Video
{
    static class VideoMux
    {
        private static void MuxVideoWithAudio(string video, string audio, string outputFilePath)
        {
            Process P = Process.Start($"Resources/ffmpeg/ffmpeg.exe", "-hide_banner -loglevel error -i " + video + " -i " + audio + " -c:v copy -c:a aac muxed/" + outputFilePath);
            P.WaitForExit();
        }

        public static void MuxAllVideosWithAudio(string title)
        {
            string validDirName = StringUtil.DirectoryNameHelper(title);
            string[] sounds = Directory.GetFiles("sounds\\" + validDirName, "*.wav");
            Directory.CreateDirectory("muxed/" + validDirName);

            int length = sounds.Length - 1;
            int counter = 0;

            foreach (var sound in sounds)
            {
                MuxVideoWithAudio("videos/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + ".mp4", sound, 
                    validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + ".mp4");
                Console.WriteLine("\tSaved mux {0}, {1} remaining.", counter, length - counter);
                counter++;
            }
        }
    }
}
