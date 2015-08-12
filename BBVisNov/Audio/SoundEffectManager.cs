using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using libZPlay;

namespace BBVisNov
{
    public class SoundEffectManager
    {
        Dictionary<string, SoundEff> soundEffects;

        public SoundEffectManager()
        {
            soundEffects = new Dictionary<string, SoundEff>();
        }

        public void LoadSoundEff(string filename)
        {
            if (!soundEffects.ContainsKey(filename))
            {
                soundEffects.Add(filename, new SoundEff(filename));
            }

            soundEffects[filename].AddRef();
        }

        public void UnloadSoundEff(string filename)
        {
            if (soundEffects.ContainsKey(filename))
            {
                soundEffects[filename].RemoveRef();

                if (soundEffects[filename].ReferenceCount == 0)
                {
                    soundEffects.Remove(filename);
                }
            }
        }

        public void PlaySoundEff(string filename)
        {
            if (soundEffects.ContainsKey(filename))
            {
                soundEffects[filename].SoundPlayer.StartPlayback();
            }
        }

        public void Update()
        {
        }
    }
}
