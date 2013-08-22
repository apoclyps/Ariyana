using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ariyana_prototype
{
    class GameStats
    {

        public static int points = 0;
        public static int experience = 0;
        public static int mana = 0;
        public static string battleMorale;
        public static int minutes = 2;
        public static int seconds = 59;

        public static void resetStats()
        {
            points = 0;
            experience = 0;
            mana = 0;
            battleMorale = getBattleMorale();
            minutes = 2;
            seconds = 59;
        }

        public static string getBattleMorale()
        {
            int numberOfLivingUnits=0;

            for (int i = 0; i < GameBoardManager.unitList.Length; i++)
            {
                if(GameBoardManager.unitList[i].alive)
                {
                    numberOfLivingUnits +=1;
                }
            }

            if (numberOfLivingUnits >= 13 && numberOfLivingUnits <= GameBoardManager.unitList.Length)
            {
                battleMorale = "High";
            }
            else if (numberOfLivingUnits >= 7 && numberOfLivingUnits < 13)
            {
                battleMorale = "Medium";
            }
            else if (numberOfLivingUnits< 7)
            {
                battleMorale = "Low";
            }

            return battleMorale;
        }

    }
}
