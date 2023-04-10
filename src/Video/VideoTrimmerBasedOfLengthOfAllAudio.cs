using System;
using System.IO;
using System.Linq;
using TiktokBot.Util;


namespace TiktokBot.Video
{
    static class VideoTrimmerBasedOfLengthOfAllAudio
    {
        public static void SaveAllVideos(string title)
        {
            string validDirName = StringUtil.DirectoryNameHelper(title);
            string[] sounds = Directory.GetFiles("sounds\\" + validDirName, "*.wav");
            Directory.CreateDirectory("videos/" + validDirName);

            int length = sounds.Length - 1;
            int counter = 0;

            foreach (var sound in sounds)
            {
                BackgroundVideoTrimmer.TrimVideo(SettingsGetter.GetSettings().BackgroundVideoPath, 
                    validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + ".mp4", 
                    RandomUtil.RandomStartFrame(Convert.ToInt32(MediaLengthUtil.Length(SettingsGetter.GetSettings().BackgroundVideoPath)),
                    Convert.ToInt32(MediaLengthUtil.Length(sound))), Convert.ToInt32(MediaLengthUtil.Length(sound)));
                Console.WriteLine("\tSaved video {0}, {1} remaining.", counter, length - counter);
                counter++;
            }
        }
    }
}
