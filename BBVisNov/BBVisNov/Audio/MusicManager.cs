using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using libZPlay;

namespace BBVisNov
{
    public class MusicManager
    {
        ZPlay backgroundMusicPlayer;
        string currentBackgroundMusic;

        public MusicManager()
        {
            backgroundMusicPlayer = new ZPlay();
            currentBackgroundMusic = "";
        }

        public void PlayBackgroundMusic(string music)
        {
            if (currentBackgroundMusic != music)
            {
                currentBackgroundMusic = music;

                backgroundMusicPlayer.StopPlayback();
                backgroundMusicPlayer.Close();

                backgroundMusicPlayer.OpenFile(currentBackgroundMusic, TStreamFormat.sfAutodetect);
                backgroundMusicPlayer.StartPlayback();
            }
        }

        public void StopBackgroundMusic()
        {
            backgroundMusicPlayer.StopPlayback();
            backgroundMusicPlayer.Close();

            currentBackgroundMusic = "";
        }

        public void Update()
        {
            if (currentBackgroundMusic != "")
            {
                TStreamStatus status = new TStreamStatus();

                backgroundMusicPlayer.GetStatus(ref status);

                if (!status.fPlay)
                {
                    backgroundMusicPlayer.StartPlayback();
                }
            }
        }
    }
}
