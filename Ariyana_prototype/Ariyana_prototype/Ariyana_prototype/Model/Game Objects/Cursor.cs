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
    class Cursor : GameObject
    {
        long previousSeconds = 0;
        KeyboardState inputKeyboard;
        public  int cursorX = 8;
        public  int cursorY = 9;
        public bool unitSelected = false;
        public static GameObject previousUnitSelected;
        public static GameObject selectedUnit;

        public Cursor(Vector2 position)
            : base(position)
        {
            this.position = position;
            this.movementSpeed = 2;
            objType = "Cursor";
            solid = false;
        }

        public Cursor(Vector2 position, String spriteName)
            : base(position)
        {
            this.position = position;
            this.movementSpeed = 63;
            this.spriteName = spriteName;
            textureWidth = 62;
            textureHeight = 62;
            objType = "Cursor";
            solid = false;
        }
      
        public void checkGamePieceForUnit()
        {
            int objectsSelected = 0;

            Unit [] unitList = GameBoardManager.unitList;

           for (int i =0; i<unitList.Length; i++)
           {
                if (previousUnitSelected != null)
                {
                    previousUnitSelected.objectHit = false;
                    previousUnitSelected.objectSelected = false;
                }
                if (this.BoundingBox.Intersects(unitList[i].BoundingBox) && unitList[i].solid && unitList[i].alive)
                {
                    if (inputKeyboard.IsKeyDown(Keys.R) || (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed))
                    {
                        selectedUnit = unitList[i];
                        selectedUnit.objectSelected = true;
                        objectsSelected++;
                        unitSelected = true;
                    }

                    if (GamePad.GetState(PlayerIndex.One).Triggers.Right == 1.0f )
                    {
                        if (unitList[i].health < 100)
                        {
                            unitList[i].health += 2;
                        }
                    }
                }
                else if (inputKeyboard.IsKeyDown(Keys.Q) && selectedUnit != null || (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) && selectedUnit != null)
                {
                    bool drop = true;
                    if (selectedUnit != null)
                    {
                        for (int j = 0; j < unitList.Length; j++)
                        {
                            if (unitList[i].position == unitList[j].position)
                            {
                                drop = false;
                            }
                        }
                        if (!drop)
                        {
                            selectedUnit.objectSelected = false;
                            unitSelected = false;
                            selectedUnit.position = GameBoard.getGameBoardVector(cursorX, cursorY);
                        }                      
                    }

                    if (drop)
                    {
                        selectedUnit = null;
                        previousUnitSelected = selectedUnit;
                    }
                }
            }

            if (objectsSelected > 1 && previousUnitSelected != null)
            {
                previousUnitSelected.objectSelected = false;
            }
       }
        /**
         * Update
         * Moves the cursor around the unit box 
         * Only allows a move to be carried out each 0.1 of a second
         * Locks the cursor to the gameboard grid
         */
        public override void Update()
        {
            inputKeyboard = Keyboard.GetState();

            if (Game1.stopWatch.ElapsedMilliseconds >= previousSeconds + 100 || previousSeconds == 0)
            {
                checkGamePieceForUnit();
                previousSeconds = Game1.stopWatch.ElapsedMilliseconds;

                if (inputKeyboard.IsKeyDown(Keys.W) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 1.0f))
                {
                    if (cursorY != 9)
                    {
                        cursorY -= 1;
                        position.Y = GameBoard.getGameBoardVector(cursorX, cursorY).Y;
                    }
                }
                if (inputKeyboard.IsKeyDown(Keys.A) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == -1.0f))
                {
                    if (cursorX != 0)
                    {
                        cursorX -= 1;
                        position.X = GameBoard.getGameBoardVector(cursorX, cursorY).X;
                    }
                }
                if (inputKeyboard.IsKeyDown(Keys.S) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == -1.0f))
                {
                    if (cursorY != 12)
                    {
                        cursorY += 1;
                        position.Y = GameBoard.getGameBoardVector(cursorX, cursorY).Y;
                    }
                }
                if (inputKeyboard.IsKeyDown(Keys.D) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X == 1.0f))
                {
                    if (cursorX != 14)
                    {
                        cursorX += 1;
                        position.X = GameBoard.getGameBoardVector(cursorX, cursorY).X;
                    }
                }

            }
            base.Update();
        }

        public override Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, textureWidth, textureHeight); }
        }

        public bool getUnitSelected()
        {
            return unitSelected;
        }

    }
}
