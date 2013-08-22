using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ariyana_prototype
{
    class Sound
    {

        SoundEffect soundEffect;
        private SoundEffectInstance Sound1Instance;
        public bool songstart = false;
        public bool soundInstanceCreated = false;

        public Sound()
        {
  
        }

        public bool getSongPlaying()
        {
            return this.songstart;
        }

        public void LoadContent(ContentManager Content)
        {
            soundEffect = Content.Load<SoundEffect>(@"Audio/SisteViator");
        }


        public virtual void Update()
        {
            playSong();
        }


        public void stopSong()
        {
            if (songstart)
            {
                Sound1Instance.Stop();
                songstart = false;
            }
        }

        public void playSong()
        {
            if (soundInstanceCreated == false)
            {
                Sound1Instance = soundEffect.CreateInstance();
                Sound1Instance.IsLooped = true;
                soundInstanceCreated = true;
                Sound1Instance.Volume = 0.050f;
            }

            if (!songstart)
            {
                try
                {
                    Sound1Instance.Play();
                    songstart = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Audio Error " + e);
                    songstart = false;
                }
            }  
        }
    }
}
