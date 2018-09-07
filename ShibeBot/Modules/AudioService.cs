using Discord;
using Discord.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibeBot.Modules
{

    public class AudioService
    {
        public IAudioClient audioClient { get; set; }
        public Process audioProcess { get; set; }
    }
    
}
