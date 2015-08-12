using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using libZPlay;

namespace BBVisNov
{
    public class SoundEff
    {
        public ZPlay SoundPlayer { get; set; }
        public uint ReferenceCount { get; set; }
        public string FileName { get; set; }

        public SoundEff(string filename)
        {
            FileName = filename;
            ReferenceCount = 0;
            SoundPlayer = new ZPlay();
        }

        public void AddRef()
        {
            if (ReferenceCount == 0)
            {
                // Load sound
                SoundPlayer.OpenFile(FileName, TStreamFormat.sfAutodetect);
            }

            ReferenceCount++;
        }

        public void RemoveRef()
        {
            if (ReferenceCount > 0)
            {
                ReferenceCount--;

                if (ReferenceCount == 0)
                {
                    // Unload sound
                    SoundPlayer.Close();
                }
            }
        }
    }
}
