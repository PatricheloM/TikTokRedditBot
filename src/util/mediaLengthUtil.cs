using System;
using System.Diagnostics;
using System.IO;

namespace tiktokBot.util
{
    class mediaLengthUtil
    {
        public static double length(string mediaPath)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = $"ffmpeg/ffprobe.exe";
            processStartInfo.Arguments = "-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 " + mediaPath;
            processStartInfo.RedirectStandardOutput = true;
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
