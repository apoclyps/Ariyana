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
    class ArmouredKnight : IUnitType
    {
        KeyboardState inputKeyboard;


        /**
        * marchForward  
        * Called from moveCommand
        * Moves unit forward 1 pixel unless it collides with another unit
        * If collision occurs then unit is moved back to its previous vector2 position on the gameboard
        * Checked three times for each square for all available locations
        */
        public void marchForward(Unit armouredKnight)
        {
            if (armouredKnight.alive && armouredKnight.solid)
            {
                Vector2 knightMoveTo = GameBoard.getGameBoardVector(armouredKnight.rowX, 9);

                // Check to see which of the 4 Y grids that the unit is in
                if (armouredKnight.position.Y >= knightMoveTo.Y)
                {
                    armouredKnight.position.Y -= armouredKnight.movementSpeed;
                    // Checks all available vector 2 positions behind a unit for a free location
                    for (int i = 0; i < 3; i++)
                    {
                        if (armouredKnight.BoundingBox.Intersects(GameBoard.getGameBoardRectangle(armouredKnight.rowX, 9 + i)) && !(armouredKnight.BoundingBox.Intersects(GameBoard.getGameBoardRectangle(armouredKnight.rowX, 9))))
                        {
                            armouredKnight.columnY = 9 + i;

                        }
                    }
                }
                else if (armouredKnight.position.Y < knightMoveTo.Y - 1)
                {
                    armouredKnight.position.Y += armouredKnight.movementSpeed;
                }
            }
        }

        /**
        * marchBackwards  
        * USED FOR TESTING ONLY
        * Moves the unit towards the bottom of the board
        */
        public void marchBackwards(Unit armouredKnight)
        {
            Vector2 knightMoveTo = GameBoard.getGameBoardVector(armouredKnight.rowX, 12);

            // Check to see which of the 4 Y grids that the unit is in
            if (armouredKnight.position.Y <= knightMoveTo.Y)
            {
                armouredKnight.position.Y += armouredKnight.movementSpeed;
                for (int i = 0; i < 3; i++)
                {
                    if (armouredKnight.BoundingBox.Intersects(GameBoard.getGameBoardRectangle(armouredKnight.rowX, 12 - i)) && !(armouredKnight.BoundingBox.Intersects(GameBoard.getGameBoardRectangle(armouredKnight.rowX, 9))))
                    {
                        armouredKnight.columnY = 12- i;
                    }
                }
            }
            else if (armouredKnight.position.Y > knightMoveTo.Y +1)
            {
                armouredKnight.position.Y -= armouredKnight.movementSpeed;
            }
        }


        /**
        * checkForFight 
        * Collision detection for units on the gameboard
         * Checks each enemy is not colliding with the unit
         * If enemies are colliding then the unit attacks that unit
         * Units can attack multiple units per check
        */
        public static void checkForFight(Unit armouredKnight)
        {
            Enemy[] enemyList = GameBoardManager.enemyList;

            for (int i = 0; i < enemyList.Length; i++)
            {
                if (armouredKnight.BoundingBox.Intersects(enemyList[i].BoundingBox) && (enemyList[i].solid) && enemyList[i].alive)
                {
                    enemyList[i].objectHit = true;

                    if (enemyList[i].alive)
                    {
                        armouredKnight.unitBehaviour.attackCommand(armouredKnight);
                        armouredKnight.position.Y += armouredKnight.movementSpeed;
                    }
                }
                else if (enemyList[i].BoundingBox.Intersects(armouredKnight.BoundingBox) && (enemyList[i].solid))
                {
                    enemyList[i].objectHit = false;
                    armouredKnight.position.Y += armouredKnight.movementSpeed;
                }
            }
        }

        /**
        * checkForUnitCollision 
        * Collision detection for units v units on the gameboard
        * Checks each unit is not colliding with another unit
        */
        public static void checkForUnitCollision(Unit armouredKnight)
        {
            Unit[] unitList = GameBoardManager.unitList;

            for (int i = 0; i < unitList.Length; i++)
            {
                if (armouredKnight.alive && unitList[i].alive && armouredKnight.id != unitList[i].id)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (armouredKnight.BoundingBox.Intersects(GameBoard.getGameBoardRectangle(armouredKnight.rowX, 9 + j)) && !(armouredKnight.BoundingBox.Intersects(GameBoard.getGameBoardRectangle(armouredKnight.rowX, 9))))
                        {
                            armouredKnight.columnY = 9 + j;
                            armouredKnight.position.Y = GameBoard.getGameBoardRectangle(armouredKnight.rowX, 9 + j).Y;
                        }
                    }
                }
            }
        }


        /**
        * moveCommand 
        * default state for unit
        * moves foward if not at end of collision box for units
        * if intersects enemy then enters attack state.
        */
        public override void moveCommand(Unit armouredKnight)
        {
            inputKeyboard = Keyboard.GetState();

            marchForward(armouredKnight);
            
            if (inputKeyboard.IsKeyDown(Keys.B))
            {
                marchBackwards(armouredKnight);
            }

            checkForFight(armouredKnight);
        }


        /**
        * attackCommand 
         * When unit collides with enemy
         * Unit attacks enemy and deals damage to it
         * If enemy has no health then enemy dies
        */
        public override void attackCommand(Unit armouredKnight)
        {
            Enemy[] enemyList = GameBoardManager.enemyList;

            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemyList[i].alive)
                {
                    if (armouredKnight.BoundingBox.Intersects(enemyList[i].BoundingBox))
                    {
                        enemyList[i].objectHit = true;
                        enemyList[i].health -= armouredKnight.damage;
                        
                        if (enemyList[i].health == 0)
                        {
                            enemyList[i].alive = false;
                            enemyList[i].objectHit = false;
                            enemyList[i].solid = false;
                            GameStats.points += 10;
                            GameStats.experience += 1;
                            GameStats.mana += 5;
                        }

                        if (armouredKnight.health == 0)
                        {
                            armouredKnight.alive = false;
                            armouredKnight.solid = false;
                            armouredKnight.objectHit = false;
                        }
                    }
                }
            }
        }

        // Not yet implemented
        public override void haltCommand()
        {

        }

        public override void waitCommand()
        {

        }

        public override void dieCommand()
        {

        }

        public override void spawnCommand()
        {

        }
    }
}
