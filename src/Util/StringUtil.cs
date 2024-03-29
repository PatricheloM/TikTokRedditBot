﻿using System;
using System.Collections.Generic;
using System.IO;

namespace TiktokBot.Util
{
    static class StringUtil
    {

        private const string T2S_VOICE = "Microsoft David Desktop";

        public static string GetT2SVoice()
        {
            return T2S_VOICE;
        }

        public static string BetweenParagraph(string comment)
        {
            try
            {
                string FinalString;
                int Pos1 = comment.IndexOf("<p>") + "<p>".Length;
                int Pos2 = comment.IndexOf("</p>");
                FinalString = comment.Substring(Pos1, Pos2 - Pos1);
                return FinalString;
            }
            catch (Exception)
            {
                return InvalidCommentUtil.GetDeletedOrNullConst();
            }
        }

        // deprecated due to an ffmpeg argument that trims the output
        public static int DurationSerializer(string ffmpegOutput)
        {
            string FinalString;
            int Pos1 = ffmpegOutput.IndexOf("=") + "=".Length;
            int Pos2 = ffmpegOutput.IndexOf(".");
            FinalString = ffmpegOutput[Pos1..Pos2];
            return Convert.ToInt32(FinalString);
        }

        public static string DirectoryNameHelper(string str)
        {
            string returnVal = str;
            char[] illegalChars = { '#', '>', '$', '+', '%', '<', '!', '`', '&', '*', '\'', '|', '{', '}', '?', '\"', '=', '/', ':', '\\', ' ', '@', '.' };

            foreach (var character in illegalChars)
            {
                returnVal = returnVal.Replace(character, '_');   
            }

            return returnVal;
        }

        public static List<string> GetCommentsFromTxt(string postTitle)
        {
            List<string> comments = new List<string>();

            string validDirName = DirectoryNameHelper(postTitle);

            DirectoryInfo dinfo = new DirectoryInfo("images/" + validDirName + "/");
            FileInfo[] files = dinfo.GetFiles("*.txt", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
            {
                StreamReader sr = file.OpenText();
                comments.Add(sr.ReadToEnd());
            }
            

            return comments;
        }
    }
}
