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
    class GameBoardManager
    {
        public static Cursor Cursor;
        static InputController inputController = new InputController();
        public static int currentHighestID = 0;
        static long previousSeconds = 0;
        public static Unit[] unitList = new Unit[15];
        public static Enemy[] enemyList = new Enemy[15];
        public static bool gameStarted = false;
        public static Random randomNumber = new Random();
        public static Vector2 previousEnemyVector = new Vector2(16, 16);

        public static void Initilize()
        {
            Cursor = new Cursor(GameBoard.getGameBoardVector(8,10), "Cursor");
            Cursor.id = currentHighestID;

            for (int i = 0; i < unitList.Length; i++)
            {
                Unit Knight = new Unit(GameBoard.getGameBoardVector(i, 12), "ArmouredKnight");
                Knight.rowX = i;
                Knight.columnY = 12;
                currentHighestID += 1;
                Knight.id = currentHighestID;

                Knight.unitBehaviour = new ArmouredKnight();
                unitList[i] = Knight;
            }
            
            for (int i = 0; i < enemyList.Length; i++)
            {
                Enemy Wraith = new Enemy(GameBoard.getGameBoardVector(i, 0), "Wraith");
                Wraith.rowX = i;
                Wraith.columnY = 12;
                Wraith.id = currentHighestID;
                currentHighestID += 1;
                Wraith.enemyBehaviour = new Wraith();
                enemyList[i] = Wraith;
                int randomSpawnX = randomNumber.Next(0, 15);
                enemyList[i].position = GameBoard.getGameBoardVector(randomSpawnX, 0);
            }

            for (int i = 0; i < 10; i++)
            {
                enemyList[i + 5].alive = false;
                enemyList[i + 5].solid = false;
            }
         }

        public static void resetGameBoard()
        {
            try
            {
                inputController = new InputController();
                currentHighestID = 0;
                previousSeconds = 0;
                unitList = new Unit[15];
                enemyList = new Enemy[15];
                gameStarted = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception" + e);
            }
        }

        public static void spawnWraithToLocation()
        {
            int randomLocation = randomNumber.Next(0, 15);

            Enemy Wraith = new Enemy(GameBoard.getGameBoardVector(randomLocation, 0), "Wraith");
            Wraith.rowX = randomLocation;
            Wraith.columnY = 0;
            Wraith.id = randomLocation + 10;
            Wraith.enemyBehaviour = new Wraith();
        }

         public static void generateEnemies()
        {
            Random randomNumber = new Random();
            int randomSpawn = randomNumber.Next(1, 6);
            int randomSpawnX;
            
            if (Game1.stopWatch.ElapsedMilliseconds /1000 >= previousSeconds + 1)
            {
                previousSeconds = Game1.stopWatch.ElapsedMilliseconds / 1000;

                for (int i = 0; i < enemyList.Length; i++)
                {
                    if (enemyList[i].alive == false && enemyList[i].solid == false)
                    {
                        enemyList[i].alive = true;
                        enemyList[i].solid = true;
                        enemyList[i].health = 100;
                        do
                        {
                            randomSpawnX = randomNumber.Next(0, 15);
                            enemyList[i].position = GameBoard.getGameBoardVector(randomSpawnX, 0);
                        } while (previousEnemyVector == enemyList[i].position);
                        
                        previousEnemyVector = enemyList[i].position;
                        i = enemyList.Length;
                    }
                }
            }
        }

        // TESTING ONLY
        public static void constrainToBox(GameObject gameObject)
        {
            float minX = 0.00f;
            float maxX = 300f;
            float minY = 0.00f;
            float maxY = 300f;

            gameObject.position.X = (int)MathHelper.Clamp((float)gameObject.position.X, minX, maxX);
            gameObject.position.Y = (int)MathHelper.Clamp((float)gameObject.position.X, minY, maxY);

            gameObject.position.X += 1;
            gameObject.position.Y += 1;
        }

        public static void UnitCollisionWithEnemy(GameObject gameObject)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                if (gameObject.objType != enemyList[i].objType && gameObject.alive && enemyList[i].alive)
                {
                    if ((gameObject.BoundingBox.Intersects(enemyList[i].BoundingBox)) && (gameObject.solid && enemyList[i].solid))
                    {
                        enemyList[i].objectHit = true;
                    }
                }
            }
        }

        public static void EnemyCollisionWithUnit(GameObject gameObject)
        {
            for (int i = 0; i < unitList.Length; i++)
            {
                if (gameObject.objType != unitList[i].objType && gameObject.alive && unitList[i].alive)
                {
                    if ((gameObject.BoundingBox.Intersects(enemyList[i].BoundingBox)) && (gameObject.solid && unitList[i].solid))
                    {
                        unitList[i].objectHit = true;
                    }
                }
            }
        }

        // Confines units to the unit box allowing them only to move up 4 places on the grid
        public static void confineToUnitBox(GameObject o)
        {
            // X Axis
            if (o.position.X >= GameBoard.gameboardSize.X + GameBoard.gameBoardDisplayOrigin.X)
            {
                o.position.X = (GameBoard.gameboardSize.X + GameBoard.gameBoardDisplayOrigin.X) - 1;
            }
            else if (o.position.X <= GameBoard.gameBoardDisplayOrigin.X)
            {
                o.position.X = GameBoard.gameBoardDisplayOrigin.X ;
            }

            // Y Axis
            if (o.position.Y >=  GameBoard.getGameBoardVector(0,12).Y)
            {
                o.position.Y = GameBoard.getGameBoardVector(0, 12).Y; 
            }
            else if (o.position.Y <= GameBoard.getGameBoardVector(0, 9).Y)
            {
                o.position.Y = GameBoard.getGameBoardVector(0, 9).Y;
            }
        }

        // Locks game object to the gameboard
        // Check if enemy intersects the end of the gameboard, if so ends game
        public static void confineToGameBoard(GameObject o)
        {
            // X Axis
            if (o.alive && o.solid)
            {

                if (o.position.X >= GameBoard.gameboardSize.X + GameBoard.gameBoardDisplayOrigin.X)
                {
                    o.position.X = (GameBoard.gameboardSize.X + GameBoard.gameBoardDisplayOrigin.X);
                }
                else if (o.position.X <= GameBoard.gameBoardDisplayOrigin.X)
                {
                    o.position.X = GameBoard.gameBoardDisplayOrigin.X;
                }

                // Y Axis
                if (o.position.Y >= GameBoard.gameboardSize.Y + GameBoard.gameBoardDisplayOrigin.Y)
                {
                    o.position.Y = (GameBoard.gameboardSize.Y + GameBoard.gameBoardDisplayOrigin.Y);
                    if (o.objType.Equals("Enemy") && o.alive && o.solid)
                    {
                        Game1.gameState = Game1.GameStates.GameOver;
                    }
                }
                else if (o.position.Y <= GameBoard.gameBoardDisplayOrigin.Y)
                {
                    o.position.Y = GameBoard.gameBoardDisplayOrigin.Y;
                }
            }
        }

        public static void UnitCollisionWithUnit(GameObject gameObject)
        {
            for (int i = 0; i < unitList.Length; i++)
            {
                if (gameObject.alive && unitList[i].alive && gameObject.id != unitList[i].id)
                {
                    if ((gameObject.BoundingBox.Intersects(unitList[i].BoundingBox)) && (gameObject.solid && unitList[i].solid))
                    {
                        if (gameObject.columnY == unitList[i].columnY)
                        {
                            gameObject.position.Y = GameBoard.getGameBoardVector(unitList[i].rowX, unitList[i].columnY + 1).Y;
                        }
                    }
                }
            }
        }

        public static void EnemyCollisionWithEnemy(GameObject gameObject)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                if (gameObject.alive && enemyList[i].alive && gameObject.id != enemyList[i].id)
                {
                    if ((gameObject.BoundingBox.Intersects(enemyList[i].BoundingBox)) && (gameObject.solid && enemyList[i].solid))
                    {
                        if (gameObject.columnY == enemyList[i].columnY)
                        {
                            gameObject.position.Y = GameBoard.getGameBoardVector(enemyList[i].rowX, unitList[i].columnY - 1).Y;
                        }
                    }
                }
            }
        }

        // Checks if game object collides with a unit
        public static void collision(GameObject gameObject)
        {
            for (int i = 0; i < unitList.Length; i++)
            {
                // If game object that is checking for a collision is not checking against itself then
                if (gameObject.alive && unitList[i].alive && gameObject.id != unitList[i].id && gameObject.columnY == unitList[i].columnY)
                {
                        // Unit is alive, not the same object and in the same row
                        if ((gameObject.BoundingBox.Intersects(unitList[i].BoundingBox)) && (gameObject.solid && unitList[i].solid))
                        {
                            if (gameObject.columnY >= unitList[i].columnY)
                            {
                                gameObject.position.Y += 1;
                            }
                        }
                }
                confineToUnitBox(unitList[i]);
            }
        }

        public static void collisionWithEnemy(GameObject gameObject)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                // If game object that is checking for a collision is not checking against itself then
                if (gameObject.alive && enemyList[i].alive && gameObject.id != enemyList[i].id && gameObject.columnY == enemyList[i].columnY)
                {
                    // Unit is alive, not the same object and in the same row
                    if ((gameObject.BoundingBox.Intersects(enemyList[i].BoundingBox)) && (gameObject.solid && enemyList[i].solid))
                    {
                        if (gameObject.columnY <= enemyList[i].columnY)
                        {
                            gameObject.position.Y -= 1;
                        }
                    }
                }
            }
        }

        // Checks to see if all units are dead
        // if all units are dead then game ends
        public static void checkUnitsAlive()
        {
            int unitsDead = 0;

            for (int i = 0; i < unitList.Length; i++)
            {
                if (!unitList[i].alive)
                {
                    unitsDead += 1;
                }
            }

            if (unitsDead == unitList.Length)
            {
                Game1.gameState = Game1.GameStates.GameOver;
            }
        }

        /**
         * updateGameBoard  
         * Updates the cursor on screen
         * checks if units are alive
         * Generates new enemies every 1.5 seconds
         * Spawns enemy if button Y is pressed and reduces mana
         * Updates Cursor
         * Check unit positions and move, collisions with enemies and other units
         * Check enemy positions and move, collisions with units and other enemies
         */
        public static void updateGameBoard()
        {
            inputController.Update();
            checkUnitsAlive();

            if (Game1.stopWatch.ElapsedMilliseconds >= previousSeconds + (1500))
            {
                generateEnemies();
            }

            if (GameStats.mana >= 50 && GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            {
                GameStats.mana -= 50;

                    for (int i = 0; i < unitList.Length; i++)
                    {
                        if (!unitList[i].alive)
                        {
                            unitList[i].alive = true;
                            unitList[i].solid = true;
                            unitList[i].health = 100;
                            unitList[i].position = Cursor.position;
                            i = unitList.Length;
                        }
                    }
            }

            GameBoardManager.Cursor.Update();

            for (int i = 0; i < unitList.Length; i++)
            {
                if (unitList[i].alive && unitList[i] != null)
                {
                    confineToUnitBox(unitList[i]);
                    
                    unitList[i].Update();
                    UnitCollisionWithEnemy(unitList[i]);
                    collision(unitList[i]);
                }
            }

            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemyList[i].alive && enemyList[i] !=null)
                {
                    confineToGameBoard(enemyList[i]);
                    enemyList[i].Update();
                }
            }
        }


        }
    }

