using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TiktokBot.Text;
using TiktokBot.Util;

namespace TiktokBot.Video
{
    static class ImageAdder
    {
        private static void AddImage(string videoPath, string imagePath, double start, double stop, string outputPath)
        {
            Process P = Process.Start($"Resources/ffmpeg/ffmpeg.exe", "-hide_banner -loglevel error -i " + videoPath + " -i " + imagePath + " -filter_complex \"[0:v][1:v] overlay = 20:350:enable = 'between(t," + Convert.ToString(Convert.ToDouble(start)).Replace(',', '.') + "," + Convert.ToString(Convert.ToDouble(stop)).Replace(',', '.') + ")\'\" -pix_fmt yuv420p -c:a copy done/" + outputPath);
            P.WaitForExit();
        }

        public static void MakeVideos(string title)
        {
            string validDirName = StringUtil.DirectoryNameHelper(title);
            string[] sounds = Directory.GetFiles("sounds\\" + validDirName, "*.wav");
            Directory.CreateDirectory("done/" + validDirName);

            int length = sounds.Length - 1;
            int counter = 0;

            List<double> coordinates = TextToSpeech.GetImageCoordinates(title, StringUtil.GetCommentsFromTxt(title));

            foreach (var sound in sounds)
            {
                AddImage("muxed/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + ".mp4",
                    "images/" + validDirName + "/title.png", 
                    coordinates[0 + (counter * 12)], coordinates[1 + (counter * 12)], validDirName + "/" + "temp0.mp4");

                AddImage("done/" + validDirName + "/" + "temp0.mp4",
                    "images/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + "/0.png",
                    coordinates[2 + (counter * 12)], coordinates[3 + (counter * 12)], validDirName + "/" + "temp1.mp4");

                AddImage("done/" + validDirName + "/" + "temp1.mp4",
                    "images/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + "/1.png",
                    coordinates[4 + (counter * 12)], coordinates[5 + (counter * 12)], validDirName + "/" + "temp2.mp4");

                AddImage("done/" + validDirName + "/" + "temp2.mp4",
                    "images/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + "/2.png",
                    coordinates[6 + (counter * 12)], coordinates[7 + (counter * 12)], validDirName + "/" + "temp3.mp4");

                AddImage("done/" + validDirName + "/" + "temp3.mp4",
                    "images/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + "/3.png",
                    coordinates[8 + (counter * 12)], coordinates[9 + (counter * 12)], validDirName + "/" + "temp4.mp4");

                AddImage("done/" + validDirName + "/" + "temp4.mp4",
                    "images/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + "/4.png",
                    coordinates[10 + (counter * 12)], coordinates[11 + (counter * 12)], validDirName + "/" + "temp5.mp4");

                VideoResizer.ResizeVideo("done/" + validDirName + "/" + "temp5.mp4", "done/" + validDirName + "/" + sound.Split("\\").AsQueryable().Last().Split(".")[0] + ".mp4");

                File.Delete("done/" + validDirName + "/" + "temp0.mp4");
                File.Delete("done/" + validDirName + "/" + "temp1.mp4");
                File.Delete("done/" + validDirName + "/" + "temp2.mp4");
                File.Delete("done/" + validDirName + "/" + "temp3.mp4");
                File.Delete("done/" + validDirName + "/" + "temp4.mp4"); 
                File.Delete("done/" + validDirName + "/" + "temp5.mp4");

                Console.WriteLine("\tSaved final video {0}, {1} remaining.", counter, length - counter);
                counter++;
            }
        }
    }
}
