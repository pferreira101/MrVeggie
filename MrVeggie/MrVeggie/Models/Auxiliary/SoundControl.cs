using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary
{
    public static class SoundControl
    {
        private static bool soundOn = false;

        public static bool GetSoundState()
        {
            return soundOn;
        }

        public static bool ChangeSoundState()
        {
            soundOn = !soundOn;
            return soundOn;
        }
    }
}
