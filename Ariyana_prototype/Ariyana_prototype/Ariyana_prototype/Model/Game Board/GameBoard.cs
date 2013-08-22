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
    class GameBoard
    {
        Random rand = new Random();
        Vector2 Resolution;
        Rectangle EmptyPiece = new Rectangle(1, 247, 40, 40);
  
        //Width and Height of the board
        public const int GameBoardWidth = 15;
        public const int GameBoardHeight = 13 ;
        const int gameboardDisplayOriginX = 95;
        const int gameboardDisplayOriginY = 130;

        public static Vector2 gameboardSize = new Vector2();
        public static Vector2 gameBoardDisplayOrigin = new Vector2(gameboardDisplayOriginX, gameboardDisplayOriginY);
        public Texture2D playingPieces;
        public Texture2D background;
        public Texture2D castle;
        private GamePiece[,] boardSquares = new GamePiece[GameBoardWidth, GameBoardHeight];
        public static Rectangle[,] gameBoard = new Rectangle[GameBoardWidth, GameBoardHeight];
        public static bool[,] locationOccupied = new bool[GameBoardWidth, GameBoardHeight];


        public static void calculateGameBoard()
        {

            for (int i = 0; i < GameBoardWidth; i++)
            {
                for (int j = 0; j < GameBoardHeight; j++)
                {
                    gameBoard[i, j] = new Rectangle((gameboardDisplayOriginX + (GamePiece.PieceDisplayWidth * i)), (gameboardDisplayOriginY +(GamePiece.PieceDisplayWidth * j)), GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight);
                    locationOccupied[i, j] = false;
                }
            }
        }

        /**
         * getGameBoardVector 
         * accepts the row and column of where a object currently is and returns a vector 2 position
         */
        public static Vector2 getGameBoardVector(int X, int Y)
        {
            if (X >= GameBoardWidth)
            {
                X = GameBoardWidth - 1;
            }
            if (Y>= GameBoardHeight)
            {
                Y = GameBoardHeight -1;
            }

            return new Vector2(gameBoard[X, Y].X, gameBoard[X, Y].Y);
        }

        /**
         * getGameBoardRectangle 
         * accepts the row and column of where a object currently is and returns a rectangle for collision checking
         */
        public static Rectangle getGameBoardRectangle(int X, int Y)
        {
            if (X >= GameBoardWidth)
            {
                X = GameBoardWidth - 1;
            }
            if (Y >= GameBoardHeight)
            {
                Y = GameBoardHeight - 1;
            }

            return new Rectangle(gameBoard[X, Y].X, gameBoard[X, Y].Y, GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight);
        }

        public GameBoard()
        {
            ClearBoard();
            gameboardSize.X = ((GameBoardWidth-1) * GamePiece.PieceDisplayWidth);
            gameboardSize.Y = ((GameBoardHeight-1) * GamePiece.PieceDisplayHeight);
            calculateGameBoard();
        }

        public void ClearBoard()
        {
            for (int x = 0; x < GameBoardWidth; x++)
                for (int y = 0; y < GameBoardHeight; y++)
                    boardSquares[x, y] = new GamePiece();
        }

        public Rectangle GetSourceRect(int x, int y)
        {
            return boardSquares[x, y].GetSourceRect();
        }

        public void LoadContent(ContentManager Content)
        {
            playingPieces = Content.Load<Texture2D>(@"Textures\Tile_Sheet");
            background = Content.Load<Texture2D>(@"Textures/Background");
            castle = Content.Load<Texture2D>(@"Textures/Castle");

            Configuration singleton = Configuration.Instance;
            Resolution = singleton.getResolution();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, (int)Resolution.X ,(int)Resolution.Y), Color.White);

            for (int x = 0; x < GameBoard.GameBoardWidth; x++)
                for (int y = 0; y < GameBoard.GameBoardHeight; y++)
                {
                    // Controls the position of the board
                    int pixelX = (int)gameBoardDisplayOrigin.X + (x * GamePiece.PieceDisplayWidth);
                    int pixelY = (int)gameBoardDisplayOrigin.Y + (y * GamePiece.PieceDisplayHeight);

                    if (((x + y) % 2) == 0)
                    {
                        spriteBatch.Draw(playingPieces, new Rectangle(pixelX, pixelY, GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight), EmptyPiece, Color.FromNonPremultiplied(255, 255, 255, 90));
                    }
                    else if (((x + y) % 2) == 1)
                    {
                        spriteBatch.Draw(playingPieces, new Rectangle(pixelX, pixelY, GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight), EmptyPiece, Color.FromNonPremultiplied(75, 75, 75, 100));
                    }
                    else
                    {
                        spriteBatch.Draw(playingPieces, new Rectangle(pixelX, pixelY, GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight), EmptyPiece, Color.FromNonPremultiplied(255, 255, 255, 100));
                    }     
                }
        }

        // Returns the size of the gameboard.
        public static Rectangle BoundingBox
        {
            get { return new Rectangle((int)gameboardDisplayOriginX, (int)gameboardDisplayOriginY, (int)gameboardSize.X, (int)gameboardSize.Y); }
        }

    }
}
