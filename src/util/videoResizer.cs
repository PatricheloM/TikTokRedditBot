using System.Diagnostics;

namespace TiktokBot.Util
{
    static class VideoResizer
    {
        public static void ResizeVideo(string input, string output)
        {
            Process P = Process.Start($"Ffmpeg/ffmpeg.exe", "-hide_banner -loglevel error -i " + input + " -vf scale=720:1280 " + output);
            P.WaitForExit();
        }
    }
}
