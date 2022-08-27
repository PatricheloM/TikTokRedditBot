using System.Diagnostics;

namespace tiktokBot.util
{
    class videoResizer
    {
        public static void resizeVideo(string input, string output)
        {
            Process P = Process.Start($"ffmpeg/ffmpeg.exe", "-hide_banner -loglevel error -i " + input + " -vf scale=720:1280 " + output);
            P.WaitForExit();
        }
    }
}
