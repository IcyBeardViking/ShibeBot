﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibeBot.Data
{
    public enum ShibeType
    {
        SLEEPY,
        EXCITED,
        ALL,
        ANGERY,
        HAPPY,
        HECK,
        MEME,
        WOW
    }

    public static class Files
    {
        public static class Audio
        {
            public  static readonly string audioFolder = @"songs\";
            private static readonly string command = "-hide_banner -i \"{0}{1}\" -ac 2 -f s16le -ar 48000 pipe:1";
            private static readonly string commandForDevice = @"-hide_banner -loglevel panic -f dshow -i audio=@device_cm_{33D9A762-90C8-11D0-BD43-00A0C911CE86}\wave_{C75B83D1-78B9-4C41-95FD-709A9A8BDE6B} -ac 2 -f s16le -ar 48000 pipe:1";

            //Audio files
            public static readonly string FBI    = @"FBI.mp3";
            public static readonly string USSR   = @"USSR.mp3";
            public static readonly string arf    = @"arf.mp3";
            public static readonly string oof    = @"oof.mp3";
            public static readonly string bork   = @"bork.mp3";
            public static readonly string bork2  = @"bork2.mp3";
            public static readonly string newtoy = @"newtoy.mp3";
            public static readonly string nani   = @"nani.mp3";
            public static readonly string omae   = @"omae.mp3";
            public static readonly string omaenani   = @"omaenani.mp3";
            public static readonly string cutelaugh  = @"cutelaugh.mp3";
            public static readonly string despasito  = @"despacito.mp3";
            public static readonly string despasito2 = @"despacito2.mp3";
            public static readonly string despasito3 = @"despacito3.mp3";
            public static readonly string cheekibreeki = @"Cheeki_Breeki_Hardbass.mp3";
            public static readonly string hardcheekibreeki = @"AH NU CHEEKI BREEKI IV DAMKE.mp3";
            

            // returns a full ffmpeg command attribute list
            public static string getCommand(string file)
            {
                file += ".mp3";

                if (File.Exists(audioFolder + file))
                    return String.Format(command, audioFolder, file);
                else throw new FileNotFoundException("*No such file*");
            }
        }

        public static class Pictures
        {
            public static readonly string pictureFolder = @"shibepics\";

            public static string getShibePath(int index = -1, ShibeType type = ShibeType.ALL)
            {
                string returnedPath = "";

                List<string> list = new List<string>();

                if (type != ShibeType.ALL)
                {
                    list = new List<string>(Directory.GetFiles(pictureFolder + type.ToString().ToLower()));
                }
                else
                {
                    list = new List<string>(Directory.GetFiles(pictureFolder, "*.*", SearchOption.AllDirectories));
                }

                if (index > -1)
                {
                    if (index >= list.Count)
                        index = list.Count - 1;
                        returnedPath = list[index];
                }
                else
                    returnedPath = list[randomShibeNumber(list.Count)];
                

                return returnedPath;
            }

            private static int randomShibeNumber(int max)
            {
                Random rnd = new Random();

                return rnd.Next(0, max - 1);
            }
        }
    }
}
