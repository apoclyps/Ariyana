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
    class TitleScreen
    {
        int titleFPS = 0;
        int titleSelected = 0;
        long previousSeconds = 0;
        Texture2D titleScreen;
        public static Texture2D titleBars;
        Texture2D instructionScreen;
        Texture2D storyScreen;
        Texture2D logo;
        KeyboardState inputKeyboard;
        public bool exitGame = false;
        public bool displayStory = false;
        public bool displayInstruction = false;
        public static int timeSinceStart = 0;
        public static System.Diagnostics.Stopwatch TitleStopWatch = new System.Diagnostics.Stopwatch();

        string[] Titles = { "Start", "Story", "Instructions", "Quit" };

        public TitleScreen()
        {
            TitleStopWatch.Start();
        }

        public void LoadContent(ContentManager Content)
        {
            titleScreen = Content.Load<Texture2D>(@"Textures/CastleScreenBW");
            titleBars = Content.Load<Texture2D>(@"Textures/titles");
            instructionScreen = Content.Load<Texture2D>(@"Textures/instructions");
            storyScreen = Content.Load<Texture2D>(@"Textures/story");
            logo = Content.Load<Texture2D>(@"Textures/AriyanaLogo"); 
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight, SpriteFont FairyDustfont)
        {
            DrawTitleScreen(spriteBatch, screenWidth, screenHeight, FairyDustfont);
            
            if (displayStory)
            {
                spriteBatch.Draw(storyScreen, new Rectangle(375, 170, 1200, 900), Color.White);
            }
            if (displayInstruction)
            {
                spriteBatch.Draw(instructionScreen, new Rectangle(375, 170, 1200, 900), Color.White);
            }
        }

        public virtual void Update()
        {
            if (!Game1.paused )
            {
                Titles[0] = "Start";
            }
            else
            {
                Titles[0] = "Resume";
            }
            
            if (Game1.restartGame && !Game1.paused)
            {
                Titles[0] = "New Game";
            }
 
            CycleTitleScreen();
        }


        public void DrawTitleScreen(SpriteBatch spriteBatch, int screenWidth, int screenHeight, SpriteFont FairyDustfont)
        {
            spriteBatch.Draw(titleScreen, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            int centerX = (int)((screenWidth) - (screenWidth * 0.45)); //45
            int centerY = (int)(screenHeight * 0.08);                   // // Size of the box
            int yOffset = 0; // distance between boxes
            int incrementOffset = 0;
            int yPaddingFromTop = (int)((screenHeight) - (screenHeight * 0.50)); // Distance from the top

            if (screenHeight == (1080 - 30) || screenHeight == 1080)
            {
                incrementOffset = 90;
            }
            else if (screenHeight == 600)
            {
                incrementOffset = 45;
            }

            for (int i = 0; i < 4; i++)
            {
                int DestinationRectX = ((screenWidth / 2) - (centerX / 2));
                int DestinationRectY = yPaddingFromTop + yOffset;

                Rectangle destinationRect = new Rectangle(DestinationRectX, DestinationRectY, centerX, centerY);
                Rectangle sourceRect;

                if (titleSelected == i)
                {
                    sourceRect = new Rectangle(0, 128, 1024, 64);
                }
                else
                {
                    sourceRect = new Rectangle(0, 0, 1024, 64);
                }

                spriteBatch.Draw(titleBars, destinationRect, sourceRect, Color.White);

                spriteBatch.DrawString(FairyDustfont, Titles[i], new Vector2((screenWidth / 2) - 50, DestinationRectY + 10), Color.White);

                yOffset += incrementOffset;
            }
        }

        /**
         * CycleTitleScreen  
         * Accepts input and moves the selected menu item up or down
         * Triggers state changes whenever button is pressed as specific menu items
         */
        public void CycleTitleScreen()
        {
            if (TitleStopWatch.ElapsedMilliseconds >= previousSeconds + 25 || previousSeconds == 0)
            {
                inputKeyboard = Keyboard.GetState();

                titleFPS += 1;
                // Toggle up and down the menu
                if (titleFPS >= 5)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.S) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == -1.0f) || GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                    {
                        titleSelected++;
                        Game1.titleState++;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 1.0f) || GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                    {
                        titleSelected--;
                        Game1.titleState--;
                    }

                    if (titleSelected >= 4)
                    {
                        titleSelected = 0;
                        Game1.titleState = Game1.TitleStates.Start;
                    }
                    else if (titleSelected < 0)
                    {
                        titleSelected = 3;
                        Game1.titleState = Game1.TitleStates.Quit;
                    }
                    titleFPS = 0;

                    if (Game1.titleState == Game1.TitleStates.Story && displayStory == false && (Keyboard.GetState().IsKeyDown(Keys.Enter) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)))
                    {
                        displayStory = true;
                    }
                    else if (Game1.titleState == Game1.TitleStates.Story && displayStory == true && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Delete) || Keyboard.GetState().IsKeyDown(Keys.Enter)))
                    {
                        displayStory = false;
                    }

                    if (Game1.titleState == Game1.TitleStates.Instructions && displayInstruction == false && (Keyboard.GetState().IsKeyDown(Keys.Enter) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)))
                    {
                        displayInstruction = true;
                    }
                    else if (Game1.titleState == Game1.TitleStates.Instructions && displayInstruction == true && (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Delete) || Keyboard.GetState().IsKeyDown(Keys.Enter)))
                    {
                        displayInstruction = false;
                    }
                }
                previousSeconds = TitleStopWatch.ElapsedMilliseconds;
            }

            // Trigger events when a button is pressed at menu item
            switch (Game1.titleState)
            {
                case Game1.TitleStates.Start:
                    if (TitleStopWatch.ElapsedMilliseconds >= timeSinceStart + 3000 || Game1.paused)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Space) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
                        {
                            if (!Game1.paused)
                            {
                                Game1.gameState = Game1.GameStates.Intro;
                            }
                            else
                            {
                                Game1.gameState = Game1.GameStates.Playing;
                                Game1.stopWatch.Start();
                            }
                            Game1.paused = false;
                        }
                    }
                    break;

                case Game1.TitleStates.Story:

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) || (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed))
                    {
                        Game1.gameState = Game1.GameStates.TitleScreen;
                    }
                    break;

                case Game1.TitleStates.Instructions:

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) || (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed))
                    {
                        Game1.gameState = Game1.GameStates.TitleScreen;
                    }
                    break;
                case Game1.TitleStates.Quit:

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Escape) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
                    {
                        exitGame = true;
                    }
                    break;
            }
        }

    }
}
