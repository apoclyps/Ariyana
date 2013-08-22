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
    class InputController
    {

        public static MouseState inputMouse;
        public static KeyboardState inputKeyboard;
          public static Cursor cursor = new Cursor(new Vector2(600, 600), "Cursor");
          long previousSeconds = 0;

        public InputController()
        {

        }

        public static Cursor getCursor()
        {
            return cursor;
        }

        /**
         * Update
         * Updates the cursor on the gameboard
         */
        public void Update()
        {
            inputKeyboard = Keyboard.GetState();

            if (Game1.stopWatch.ElapsedMilliseconds >= previousSeconds + 100 || previousSeconds == 0)
            {
                previousSeconds = Game1.stopWatch.ElapsedMilliseconds;

                if (inputKeyboard.IsKeyDown(Keys.W) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 1.0f))
                {
                    if (cursor.cursorY != 8)
                    {
                        cursor.cursorY -= 1;
                        cursor.position.Y = GameBoard.getGameBoardVector(cursor.cursorX, cursor.cursorY).Y;
                    }
                }
                if (inputKeyboard.IsKeyDown(Keys.A) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == -1.0f))
                {
                    if (cursor.cursorX != 0)
                    {
                        cursor.cursorX -= 1;
                        cursor.position.X = GameBoard.getGameBoardVector(cursor.cursorX, cursor.cursorY).X;
                    }
                }
                if (inputKeyboard.IsKeyDown(Keys.S) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == -1.0f))
                {
                    if (cursor.cursorY != 12)
                    {
                        cursor.cursorY += 1;
                        cursor.position.Y = GameBoard.getGameBoardVector(cursor.cursorX, cursor.cursorY).Y;
                    }
                }
                if (inputKeyboard.IsKeyDown(Keys.D) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 1.0f))
                {
                    if (cursor.cursorX != 12)
                    {
                        cursor.cursorX += 1;
                        cursor.position.X = GameBoard.getGameBoardVector(cursor.cursorX, cursor.cursorY).X;
                    }
                }
            }        
        }

      
    }
}
