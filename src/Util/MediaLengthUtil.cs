using System;
using System.Diagnostics;
using System.IO;

namespace TiktokBot.Util
{
    static class MediaLengthUtil
    {
        public static double Length(string mediaPath)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = $"Resources/ffmpeg/ffprobe.exe",
                Arguments = "-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 " + mediaPath,
                RedirectStandardOutput = true
            };
            Process P = Process.Start(processStartInfo);
            P.StartInfo.RedirectStandardOutput = true;
            StreamReader sr = P.StandardOutput;
            string result = sr.ReadToEnd();
            P.WaitForExit();
            sr.Close();

            return Convert.ToDouble(result.Replace('.', ','));
        }
    }
}
