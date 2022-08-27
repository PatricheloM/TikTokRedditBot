using System;

namespace tiktokBot.util
{
    class randomUtil
    {
        private static Random rnd = new Random();

        public static int randomStartFrame(int videoLength, int videoDuration)
        {
            return rnd.Next(0, videoLength - videoDuration);
        }
    }
}
