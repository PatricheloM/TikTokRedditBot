using System.Speech.Synthesis;
using System.Collections.Generic;
using System.IO;
using tiktokBot.util;
using System;

namespace tiktokBot.text
{
    class textToSpeech
    {
        private const int BREAK_VALUE = 1;

        private static void saveSoundFile(string filePath, params string[] texts)
        {
            SpeechSynthesizer reader = new SpeechSynthesizer();
            reader.SetOutputToWaveFile("sounds/" + filePath);
            reader.SelectVoice(stringUtil.getT2SVoice());
            PromptBuilder builder = new PromptBuilder(new System.Globalization.CultureInfo("en-US"));
            foreach (var text in texts)
            {
                builder.AppendText(text);
                builder.AppendBreak(new TimeSpan(0, 0, BREAK_VALUE));
            }
            reader.Speak(builder);
            reader.Dispose();
        }

        public static void saveAllSoundFiles(string postTitle, List<string> commentList)
        {
            string validDirName = stringUtil.directoryNameHelper(postTitle);
            Directory.CreateDirectory("sounds/" + validDirName);
            for (int i = 0; i < commentList.Count; i = i + 5)
            {
                saveSoundFile(validDirName + "/" + i / 5 + ".wav", postTitle, commentList[i], commentList[i + 1], commentList[i + 2], commentList[i + 3], commentList[i + 4]);
                Console.WriteLine("\tSaved sound {0}, {1} remaining.", i / 5, (commentList.Count / 5) - 1 - (i / 5));
            }
        }

        public static List<double> getImageCoordinates(string postTitle, List<string> commentList)
        {
            List<double> coordinates = new List<double>();

            string validDirName = stringUtil.directoryNameHelper(postTitle);
            Directory.CreateDirectory("sounds/" + validDirName);
            for (int i = 0; i < commentList.Count; i = i + 5)
            {
                double current = 0;

                Directory.CreateDirectory("sounds/" + validDirName + "/raw_" + (i / 5));
                saveSoundFile(validDirName + "/raw_" + (i / 5) + "/0.wav", postTitle);
                coordinates.Add(0);

                current = current + mediaLengthUtil.length("sounds/" + validDirName + "/raw_" + (i / 5) + "/0.wav");
                saveSoundFile(validDirName + "/raw_" + (i / 5) + "/1.wav", commentList[i]);
                coordinates.Add(current - BREAK_VALUE);
                coordinates.Add(current);

                current = current + mediaLengthUtil.length("sounds/" + validDirName + "/raw_" + (i / 5) + "/1.wav");
                saveSoundFile(validDirName + "/raw_" + (i / 5) + "/2.wav", commentList[i + 1]);
                coordinates.Add(current - BREAK_VALUE);
                coordinates.Add(current);

                current = current + mediaLengthUtil.length("sounds/" + validDirName + "/raw_" + (i / 5) + "/2.wav");
                saveSoundFile(validDirName + "/raw_" + (i / 5) + "/3.wav", commentList[i + 2]);
                coordinates.Add(current - BREAK_VALUE);
                coordinates.Add(current);

                current = current + mediaLengthUtil.length("sounds/" + validDirName + "/raw_" + (i / 5) + "/3.wav");
                saveSoundFile(validDirName + "/raw_" + (i / 5) + "/4.wav", commentList[i + 3]);
                coordinates.Add(current - BREAK_VALUE);
                coordinates.Add(current);

                current = current + mediaLengthUtil.length("sounds/" + validDirName + "/raw_" + (i / 5) + "/4.wav");
                coordinates.Add(current - BREAK_VALUE);
                coordinates.Add(current);

                coordinates.Add(mediaLengthUtil.length("sounds/" + validDirName +  "/" + (i / 5) + ".wav") - BREAK_VALUE);
            }

            return coordinates;
        }
    }
}
