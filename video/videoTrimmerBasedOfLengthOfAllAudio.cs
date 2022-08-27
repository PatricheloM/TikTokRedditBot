using System;
using System.IO;
using System.Linq;
using tiktokBot.util;


namespace tiktokBot.video
{
    class videoTrimmerBasedOfLengthOfAllAudio
    {
        public static void saveAllVideos(string title)
        {
            string validDirName = stringUtil.directoryNameHelper(title);
            string[] sounds = Directory.GetFiles("sounds\\" + validDirName, "*.wav");
            Directory.CreateDirectory("videos/" + validDirName);

            int length = sounds.Length - 1;
            int counter = 0;

            foreach (var sound in sounds)
            {
                backgroundVideoTrimmer.trimVideo(settingsGetter.getSettings().backgroundVideoPath, 
                    validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + ".mp4", 
                    randomUtil.randomStartFrame(Convert.ToInt32(mediaLengthUtil.length(settingsGetter.getSettings().backgroundVideoPath)),
                    Convert.ToInt32(mediaLengthUtil.length(sound))), Convert.ToInt32(mediaLengthUtil.length(sound)));
                Console.WriteLine("\tSaved video {0}, {1} remaining.", counter, length - counter);
                counter++;
            }
        }
    }
}
