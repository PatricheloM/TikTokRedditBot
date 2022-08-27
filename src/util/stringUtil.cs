using System;
using System.Collections.Generic;
using System.IO;

namespace tiktokBot.util
{
    class stringUtil
    {

        private const string T2S_VOICE = "Microsoft David Desktop";

        public static string getT2SVoice()
        {
            return T2S_VOICE;
        }

        public static string betweenParagraph(string comment)
        {
            try
            {
                string FinalString;
                int Pos1 = comment.IndexOf("<p>") + "<p>".Length;
                int Pos2 = comment.IndexOf("</p>");
                FinalString = comment.Substring(Pos1, Pos2 - Pos1);
                return FinalString;
            }
            catch (Exception ex)
            {
                return invalidCommentUtil.getDeletedOrNullConst();
            }
        }

        // deprecated due to an ffmpeg argument that trims the output
        public static int durationSerializer(string ffmpegOutput)
        {
            string FinalString;
            int Pos1 = ffmpegOutput.IndexOf("=") + "=".Length;
            int Pos2 = ffmpegOutput.IndexOf(".");
            FinalString = ffmpegOutput.Substring(Pos1, Pos2 - Pos1);
            return Convert.ToInt32(FinalString);
        }

        public static string directoryNameHelper(string str)
        {
            string returnVal = str;
            char[] illegalChars = { '#', '>', '$', '+', '%', '<', '!', '`', '&', '*', '\'', '|', '{', '}', '?', '\"', '=', '/', ':', '\\', ' ', '@', '.' };

            foreach (var character in illegalChars)
            {
                returnVal = returnVal.Replace(character, '_');   
            }

            return returnVal;
        }

        public static List<string> getCommentsFromTxt(string postTitle)
        {
            List<string> comments = new List<string>();

            string validDirName = stringUtil.directoryNameHelper(postTitle);

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
