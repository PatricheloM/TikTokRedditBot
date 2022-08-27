using System;
using System.IO;

namespace tiktokBot.util
{
    class initUtil
    {
        public static void init()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Directory.CreateDirectory("videos/");
            Directory.CreateDirectory("images/");
            Directory.CreateDirectory("done/");
            Directory.CreateDirectory("muxed/");
            Directory.CreateDirectory("sounds/");
            Directory.CreateDirectory("logs/");
        }
    }
}
