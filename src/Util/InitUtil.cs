using System;
using System.IO;

namespace TiktokBot.Util
{
    static class InitUtil
    {
        public static void Init()
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
