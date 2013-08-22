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
    class SplashScreen
    {
        Video splashScreenVideo;
        VideoPlayer videoPlayer;
        Texture2D videoTexture;
        Rectangle videoRectangle;
        Boolean videoPlaying = false;
        public static KeyboardState inputKeyboard;

        public Boolean getVideoPlaying()
        {
            return videoPlaying;
        }

        public SplashScreen()
        {
            videoPlayer = new VideoPlayer();
        }

        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            splashScreenVideo = Content.Load<Video>(@"Cinematics/SplashScreen");
            videoRectangle = new Rectangle(graphics.Viewport.X, graphics.Viewport.Y, graphics.Viewport.Width, graphics.Viewport.Height + 5);
        }

        public void playVideo()
        {
            videoPlayer.Play(splashScreenVideo);
            videoPlaying = true;
        }

        public void stopVideo()
        {
            videoPlayer.Stop();
            videoPlaying = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only call GetTexture if a video is playing or paused
            if (videoPlayer.State == MediaState.Playing)
            {
                videoTexture = videoPlayer.GetTexture();

                // Draw the video, if we have a texture to draw.
                if (videoTexture != null)
                {
                    spriteBatch.Draw(videoTexture, videoRectangle, Color.White);
                }
            }
            else if (videoPlayer.State == MediaState.Stopped)
            {
                //  Game1.gameState = Game1.GameStates.Playing;
            }

        }

        public virtual void Update()
        {
            if (videoPlayer.State == MediaState.Stopped)
            {
                Game1.gameState = Game1.GameStates.TitleScreen;
            }

            inputKeyboard = Keyboard.GetState();

            if (inputKeyboard.IsKeyDown(Keys.Enter) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
            {
                Game1.gameState = Game1.GameStates.TitleScreen;
                videoPlayer.Stop();
            }
        }

    }
}
