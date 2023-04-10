using System;
using System.IO;
using System.Diagnostics;
using TiktokBot.Util;

namespace TiktokBot.Video
{
    static class ImageGetter
    {
        public static void GetTitleImage(string postTitle, string subredditUrl)
        {

            Process P = Process.Start($"cmd.exe", "/C cd Resources/py && python3 get_title_image.py \"" + subredditUrl + "\"");
            P.WaitForExit();

            string validDirName = StringUtil.DirectoryNameHelper(postTitle);
            Directory.CreateDirectory("images/" + validDirName);
            File.Move("Resources/py/screenshot.png", "images/" + validDirName + "/title.png");

        }

        public static void GetCommentImages(string postTitle, string subredditUrl, int quantity)
        {

            Process P = Process.Start($"cmd.exe", "/C cd Resources/py && python3 get_comment_images.py \"" + subredditUrl + "\" " + quantity);
            P.WaitForExit();

            for (int i = 0; i < quantity; i++)
            {
                string validDirName = StringUtil.DirectoryNameHelper(postTitle);
                Directory.CreateDirectory("images/" + validDirName + "/" + i);
                File.Move("Resources/py/" + Convert.ToString(0 + (i * 5))  + "screenshot.png", "images/" + validDirName + "/" + i + "/0.png");
                File.Move("Resources/py/" + Convert.ToString(1 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/1.png");
                File.Move("Resources/py/" + Convert.ToString(2 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/2.png");
                File.Move("Resources/py/" + Convert.ToString(3 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/3.png");
                File.Move("Resources/py/" + Convert.ToString(4 + (i * 5)) + "screenshot.png", "images/" + validDirName + "/" + i + "/4.png");

                File.Move("Resources/py/" + Convert.ToString(0 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/0.txt");
                File.Move("Resources/py/" + Convert.ToString(1 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/1.txt");
                File.Move("Resources/py/" + Convert.ToString(2 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/2.txt");
                File.Move("Resources/py/" + Convert.ToString(3 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/3.txt");
                File.Move("Resources/py/" + Convert.ToString(4 + (i * 5)) + ".txt", "images/" + validDirName + "/" + i + "/4.txt");
            }
        }
    }
}
