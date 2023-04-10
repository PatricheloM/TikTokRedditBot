using System;

namespace TiktokBot.Util
{
    static class RandomUtil
    {
        private static readonly Random Rnd = new Random();

        public static int RandomStartFrame(int videoLength, int videoDuration)
        {
            return Rnd.Next(0, videoLength - videoDuration);
        }
    }
}
