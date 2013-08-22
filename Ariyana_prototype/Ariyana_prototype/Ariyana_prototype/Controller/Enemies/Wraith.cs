using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ariyana_prototype
{
    class Wraith : IEnemyType
    {

        /**
         * moveCommand  
         * All Enemies are set to use the MoveCommand by default
         * Moves the enemy 1 pixel down the gameboard unless it collides with a unit
         * @param     Enemy wraith
         * @return    none
         */
        public override void moveCommand(Enemy wraith)
        {
            if (wraith.alive && wraith.solid)
            {
                wraith.position.Y += wraith.movementSpeed;

                Unit[] unitList = GameBoardManager.unitList;

                for (int i = 0; i < unitList.Length; i++)
                {
                    if (wraith.BoundingBox.Intersects(unitList[i].BoundingBox) && (unitList[i].solid) && unitList[i].alive)
                    {
                        unitList[i].objectHit = true;
                        unitList[i].solid = true;
                        unitList[i].unitBehaviour.attackCommand(unitList[i]);

                        if (unitList[i].alive)
                        {
                            wraith.enemyBehaviour.attackCommand(wraith);
                            wraith.position.Y -= wraith.movementSpeed;
                        }
                    }
                }
            }
        }

        /**
        * attackCommand  
        * Called when a enemy collides with a unit
        * Changes to attack that enemy and only that enemy
        * Reduces unit health by the amount of damage it inflicts
        * If unit health is zero then sets unit to dead
        * @param     Enemy wraith
        * @return    none
        */
        public override void attackCommand(Enemy wraith)
        {
            Unit [] unitList = GameBoardManager.unitList;

            for( int i =0; i < unitList.Length; i++) 
            {
                if (wraith.alive)
                {
                    if (wraith.BoundingBox.Intersects(unitList[i].BoundingBox))
                    {
                        unitList[i].objectHit = true;
                        unitList[i].health -= wraith.damage;

                        if (unitList[i].health == 0)
                        {
                            unitList[i].alive = false;
                            unitList[i].solid = false;
                            unitList[i].objectHit = false;
                        }
                        if (wraith.health == 0)
                        {
                            wraith.alive = false;
                            wraith.solid = false;
                            wraith.objectHit = false;
                            GameStats.points += 10;
                        }
                        break;
                    }
                }
            }
        }

        // Not yet implemented
        public override void spawnCommand()
        {

        }

        public override void haltCommand()
        {

        }

        public override void waitCommand()
        {

        }

        public override void dieCommand()
        {

        }
    }
}
