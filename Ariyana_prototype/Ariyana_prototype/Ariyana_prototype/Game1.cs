using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ariyana_prototype
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sound newsound;
        GameBoard gameBoard;
        SpriteManager manageSprites = new SpriteManager();

        public static Boolean restartGame = false;
        public static Boolean audioEnabled = true;
        public static int buttonPressWait = 0;
        public static Boolean paused = false;
        public static SpriteFont FairyDustfont;
        public static SpriteFont FairyDustfontHUD;
        public static SpriteFont FairyDustfontSprite;
        public enum GameStates { TitleScreen, Playing, GameOver, Intro, SplashScreen };
        public enum TitleStates { Start, Story, Instructions, Quit };
        public static GameStates gameState = GameStates.SplashScreen;
        public static Boolean phase1Complete = false;
        public static Boolean replayGame = true;

        public static TitleStates titleState;
        public static System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private static Cinematic intro = new Cinematic();
        private static TitleScreen newTitleScreen;
        private static Cinematic introScreen;
        private static SplashScreen splashScreen;
        private static GameBoardManager gameBoardManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.Window.Title = "Ariyana : Defence of the Keep!";

            splashScreen = new SplashScreen();
            gameBoard = new GameBoard();

            Configuration singleton = Configuration.Instance;
            gameBoardManager = new GameBoardManager();
            singleton.Initialize(graphics);
            newTitleScreen = new TitleScreen();
            introScreen = new Cinematic();
            newsound = new Sound();

            GameBoardManager.Initilize();
            HUDManager.Initilize();

            gameBoard.ClearBoard();
            stopWatch.Start();

            gameState = GameStates.SplashScreen;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            splashScreen.LoadContent(this.Content, GraphicsDevice);

            FairyDustfont = Content.Load<SpriteFont>(@"Fonts/FairyDust");
            FairyDustfontHUD = Content.Load<SpriteFont>(@"Fonts/FairyDustHUD");
            FairyDustfontSprite = Content.Load<SpriteFont>(@"Fonts/FairyDustSprite");
            
            newsound.LoadContent(this.Content);
            gameBoard.LoadContent(this.Content);
            newTitleScreen.LoadContent(this.Content);

            GameBoardManager.Cursor.LoadContent(this.Content);

            Enemy[] enemyList = GameBoardManager.enemyList;
            for (int i = 0; i < enemyList.Length; i++)
            {
                 enemyList[i].LoadContent(this.Content);
            }

            Unit[] unitList = GameBoardManager.unitList;
            for (int i = 0; i < unitList.Length; i++)
            {
                unitList[i].LoadContent(this.Content);
            }

            foreach (HUDObject o in HUDManager.hudList)
            {
                o.LoadContent(this.Content);
            }

            intro.LoadContent(this.Content, GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            

        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameStates.SplashScreen:
                      
                    if (!splashScreen.getVideoPlaying())
                    {
                        splashScreen.playVideo();
                        newsound.stopSong();
                    }
                    splashScreen.Update();
                    break;

                case GameStates.Playing:
                    if (!restartGame)
                    {
                        if (!GameBoardManager.gameStarted)
                        {
                            GameBoardManager.gameStarted = true;
                            stopWatch.Restart();
                        }
                        if (!paused)
                        {
                            GameBoardManager.updateGameBoard();
                            HUDManager.updateHUDScreen(gameTime);
                        }

                        if (!(newsound.getSongPlaying()) && audioEnabled)
                        {
                            newsound.playSong();
                        }

                        if (!stopWatch.IsRunning)
                        {
                            stopWatch.Start();
                        }
                    }
                    else
                    {
                        restartGame = false;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) || (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed))
                    {
                        gameState = GameStates.TitleScreen;
                        titleState = TitleStates.Start;
                        restartGame = true;
                        paused = true;       
                        stopWatch.Stop();
                    }
                    break;

                case GameStates.TitleScreen:
                    if (TitleScreen.timeSinceStart == 0)
                    {
                        TitleScreen.timeSinceStart = (int)stopWatch.ElapsedMilliseconds / 1000;
                    }
                    newTitleScreen.Update();

                    if (!(newsound.getSongPlaying()) && audioEnabled)
                    {
                        newsound.playSong();
                    }

                    if (intro.getVideoPlaying())
                    {
                        intro.stopVideo();
                    }

                    if (newTitleScreen.exitGame)
                    {
                        Exit();
                    }
                    break;

                case GameStates.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) || (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed))
                    {
                        gameState = GameStates.TitleScreen;
                        GameBoardManager.resetGameBoard();
                        HUDManager.Initilize();
                        restartGame = true;
                        paused = false;
                        titleState = TitleStates.Start;

                        if (replayGame == true)
                        {
                            newsound.stopSong();
                            Initialize();
                            LoadContent();
                            gameState = GameStates.TitleScreen;
                            GameStats.resetStats();
                        }
                    }
                    else
                    {
                        GameBoardManager.updateGameBoard();
                        HUDManager.updateHUDScreen(gameTime);
                    }
                    break;

                case GameStates.Intro:
                    if (!intro.getVideoPlaying())
                    {
                        intro.playVideo();
                        newsound.stopSong();
                    }
                    intro.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (gameState == GameStates.TitleScreen)
            {
                newTitleScreen.Draw(spriteBatch, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height, FairyDustfont);
            }
            else if (gameState == GameStates.Playing)
            {
                gameBoard.Draw(spriteBatch);
                manageSprites.renderAllSprites(spriteBatch);
                manageSprites.renderAllHUDSprites(spriteBatch);

                if (stopWatch.ElapsedMilliseconds / 1000 <= 2)
                {
                    spriteBatch.Draw(TitleScreen.titleBars, new Rectangle(305, 490, 650, 75), new Rectangle(0, 128, 1024, 64), Color.White);
                    spriteBatch.DrawString(FairyDustfont, "Defend the Front Line", new Vector2(400, 500), Color.White);
                }

            }
            else if (gameState == GameStates.GameOver)
            {
                gameBoard.Draw(spriteBatch);
                if (replayGame)
                {
                    manageSprites.renderAllSprites(spriteBatch);
                    manageSprites.renderAllHUDSprites(spriteBatch);
                }
                else
                {
                    manageSprites.renderAllSprites(spriteBatch);
                    manageSprites.renderAllHUDSprites(spriteBatch);
                }
                if (!phase1Complete)
                {
                    spriteBatch.Draw(TitleScreen.titleBars, new Rectangle(305, 490, 650, 75), new Rectangle(0, 128, 1024, 64), Color.White);
                    spriteBatch.DrawString(FairyDustfont, "Demons are Victorious", new Vector2(425, 500), Color.White);
                    spriteBatch.DrawString(FairyDustfontHUD, "Press Start to return to Menu", new Vector2(470, 575), Color.White);
                    GameBoardManager.gameStarted = false;
                }
                else if (phase1Complete)
                {
                    spriteBatch.Draw(TitleScreen.titleBars, new Rectangle(305, 490, 650, 75), new Rectangle(0, 128, 1024, 64), Color.White);
                    spriteBatch.DrawString(FairyDustfont, "Phase 1 Complete", new Vector2(450, 500), Color.White);
                    spriteBatch.DrawString(FairyDustfontHUD, "Press Start to return to Menu", new Vector2(470, 575), Color.White);
                    GameBoardManager.gameStarted = false;
                }
                Game1.stopWatch.Stop();
            }
            else if (gameState == GameStates.Intro)
            {
                intro.Draw(spriteBatch);
            }
            else if (gameState == GameStates.SplashScreen)
            {
                splashScreen.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public GraphicsDeviceManager getGraphicsDeviceManager()
        {
            return graphics;
        }

    }

}