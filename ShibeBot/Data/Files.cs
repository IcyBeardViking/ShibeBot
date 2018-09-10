using System;
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
            static readonly List<string> allPictures;
            static readonly List<string> sleepyPictures;
            static readonly List<string> excitedPictures;
            static readonly List<string> angeryPictures;
            static readonly List<string> happyPictures;
            static readonly List<string> heckPictures;
            static readonly List<string> memePictures;
            static readonly List<string> wowPictures;

            public static readonly string pictureFolder = @"shibepics";
            public static readonly string pictureSleepySubFolder = @"\sleepy\";
            public static readonly string pictureExcitedSubFolder = @"\excited\";
            public static readonly string pictureAngerySubFolder = @"\angery\";
            public static readonly string pictureHappySubFolder = @"\happy\";
            public static readonly string pictureHeckSubFolder = @"\heck\";
            public static readonly string pictureMemeSubFolder = @"\meme\";
            public static readonly string pictureWowSubFolder = @"\wow\";

            static Pictures()
            {
                sleepyPictures  = new List<string>(Directory.GetFiles(pictureFolder + pictureSleepySubFolder));
                excitedPictures = new List<string>(Directory.GetFiles(pictureFolder + pictureExcitedSubFolder));
                angeryPictures  = new List<string>(Directory.GetFiles(pictureFolder + pictureAngerySubFolder));
                happyPictures   = new List<string>(Directory.GetFiles(pictureFolder + pictureHappySubFolder));
                heckPictures    = new List<string>(Directory.GetFiles(pictureFolder + pictureHeckSubFolder));
                memePictures    = new List<string>(Directory.GetFiles(pictureFolder + pictureMemeSubFolder));
                wowPictures     = new List<string>(Directory.GetFiles(pictureFolder + pictureWowSubFolder));
                

                allPictures = new List<string>(sleepyPictures);
                allPictures.AddRange(excitedPictures);
                allPictures.AddRange(angeryPictures);
                allPictures.AddRange(happyPictures);
                allPictures.AddRange(heckPictures);
                allPictures.AddRange(memePictures);
                allPictures.AddRange(wowPictures);
            }

            public static string getShibePath(int index = -1, ShibeType type = ShibeType.ALL)
            {
                string returnedPath = "";

                List<string> list = new List<string>();

                switch (type)
                {
                    case ShibeType.SLEEPY:
                        list = new List<string>(sleepyPictures);
                        break;
                    case ShibeType.EXCITED:
                        list = new List<string>(excitedPictures);
                        break;
                    case ShibeType.ALL:
                        list = new List<string>(allPictures);
                        break;
                    case ShibeType.ANGERY:
                        list = new List<string>(angeryPictures);
                        break;
                    case ShibeType.HAPPY:
                        list = new List<string>(happyPictures);
                        break;
                    case ShibeType.HECK:
                        list = new List<string>(heckPictures);
                        break;
                    case ShibeType.MEME:
                        list = new List<string>(memePictures);
                        break;
                    case ShibeType.WOW:
                        list = new List<string>(wowPictures);
                        break;
                    default:
                        list = new List<string>(allPictures);
                        break;
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
