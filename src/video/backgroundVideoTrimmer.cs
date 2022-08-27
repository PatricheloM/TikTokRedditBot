using System.Diagnostics;

namespace tiktokBot.video
{
    class backgroundVideoTrimmer
    {
        public static void trimVideo(string inputFilePath, string outputFilePath, int startTimeInSecs, int timeInSecs)
        {
            Process P =  Process.Start($"ffmpeg/ffmpeg.exe", "-hide_banner -loglevel error -ss " + startTimeInSecs + " -t " + timeInSecs.ToString() + " -i " + inputFilePath + " -filter:v \"crop=607:1080:658:0, fps=30\" videos/" + outputFilePath);
            P.WaitForExit();
        }
    }
}
