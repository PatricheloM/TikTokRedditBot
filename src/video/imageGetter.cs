using System;
using System.IO;
using System.Diagnostics;
using tiktokBot.util;

namespace tiktokBot.video
{
    class imageGetter
    {
        public static void getTitleImage(string postTitle, string subredditUrl)
        {

            Process P = Process.Start($"cmd.exe", "/C cd resources/py && python3 getTitleImage.py \"" + subredditUrl + "\"");
            P.WaitForExit();

            string validDirName = stringUtil.directoryNameHelper(postTitle);
            Directory.CreateDirectory("images/" + validDirName);
            File.Move("resources/py/screenshot.png", "images/" + validDirName + "/title.png");

        }

        public static void getCommentImages(string postTitle, string subredditUrl, int quantity)
        {

            Process P = Process.Start($"cmd.exe", "/C cd resources/py && python3 getCommentImages.py \"" + subredditUrl + "\" " + quantity);
            P.WaitForExit();

            for (int i = 0; i < quantity; i++)
            {
                string validDirName = stringUtil.directoryNameHelper(postTitle);
                Directory.CreateDirectory("images/" + validDirName + "/" + i);
                File.Move("resources/py/" + Convert.ToString(0 + (i * 5))  + "screenshot.png", "images/" + validDirName + "/" + i + "/0.png");
                File.Move("resources/py/" + Convert.ToString(1 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/1.png");
                File.Move("resources/py/" + Convert.ToString(2 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/2.png");
                File.Move("resources/py/" + Convert.ToString(3 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/3.png");
                File.Move("resources/py/" + Convert.ToString(4 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/4.png");

                File.Move("resources/py/" + Convert.ToString(0 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/0.txt");
                File.Move("resources/py/" + Convert.ToString(1 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/1.txt");
                File.Move("resources/py/" + Convert.ToString(2 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/2.txt");
                File.Move("resources/py/" + Convert.ToString(3 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/3.txt");
                File.Move("resources/py/" + Convert.ToString(4 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/4.txt");
            }
        }
    }
}
