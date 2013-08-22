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
    class HUDManager
    {
        public static List<HUDObject> hudList = new List<HUDObject>();

        static SpellBookBar titleBar;
        static SpellButton spellButton;
        static int currentID =0;
        static string[] Bars = new string[6];

        public static void Initilize()
        {
            hudList = new List<HUDObject>();

            Bars[0] = "Phase 1";
            Bars[1] = "Mana : ";
            Bars[2] = "Points : ";
            Bars[3] = "Time : ";
            Bars[4] = "Experience : ";
            Bars[5] = "Battle Morale : ";

            createBars(Bars);
            createButtons();

        }

        public static void createButtons()
        {
            String[] spellButtons = new String[4];
            spellButtons[0] = "armored_swordman_attack";
            spellButtons[1] = "heal";
            spellButtons[2] = "airship_attack";
            spellButtons[3] = "shield1";

            int previousDestinationX =0;
            for (int i = 0; i < 4; i++)
            {
                spellButton = new SpellButton(new Vector2(0, 0), spellButtons[i]);
                spellButton.id = currentID +1;

                if (previousDestinationX > 0)
                {
                    spellButton.destinationX = ((previousDestinationX + spellButton.hudElementSizeX + spellButton.offsetBetweenBars));
                }
                previousDestinationX = spellButton.destinationX;

                hudList.Add(spellButton);
            }
        }

        public static void createBars(string [] Bars)
        {
            for (int i = 0; i < Bars.Length; i++)
            {
                titleBar = new SpellBookBar(new Vector2(0, 0), "PhaseBar");
                titleBar.id = i + 4;
                currentID = i + 4;
                setHudElementPosition(titleBar);
                titleBar.HUDText = Bars[i];
                titleBar.InitialHUDText = Bars[i];
                hudList.Add(titleBar);
            }
        }

        public static void setHudElementPosition(HUDObject o)
        {
            setHudElementPositionOffSetX(o);
            setHudElementPositionOffSetY(o);
        }

        public static void setHudElementPositionOffSetX(HUDObject o)
        {
            int screenWidth = 1920;
            int offsetToRightX = 60;

            o.destinationX = (screenWidth - o.hudElementSizeX) - offsetToRightX;
        }

        public static void setHudElementPositionOffSetY(HUDObject o)
        {
            foreach ( SpellBookBar obj in hudList)
            {
                if(o.id == (obj.id +1))
                {
                    o.destinationY = obj.destinationY + obj.hudElementSizeY + obj.offsetBetweenBars;
                    return;
                }
            }
        }

        public static void setSpellPositionOffSetX(HUDObject o)
        {
            foreach (SpellButton obj in hudList)
            {
                if (o.id == (obj.id + 1))
                {
                    o.destinationX = obj.destinationX + obj.hudElementSizeX + obj.offsetBetweenBars;
                    return;
                }
            }
        }

        public static void updateHUDScreen(GameTime gametime)
        {
            foreach (HUDObject obj in hudList)
            {
                if (obj.InitialHUDText.Equals("Time : "))
                {
                    obj.HUDText = obj.InitialHUDText + updateGameTime();
                }

                if (obj.InitialHUDText.Equals("Points : "))
                {
                    obj.HUDText = obj.InitialHUDText + GameStats.points;
                }

                if (obj.InitialHUDText.Equals("Experience : "))
                {
                    obj.HUDText = obj.InitialHUDText + GameStats.experience;
                }

                if (obj.InitialHUDText.Equals("Mana : "))
                {
                    obj.HUDText = obj.InitialHUDText + GameStats.mana;
                }

                if (obj.InitialHUDText.Equals("Battle Morale : "))
                {
                    obj.HUDText = obj.InitialHUDText + GameStats.getBattleMorale();
                }
            }
        }

        public static string updateGameTime()
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = Game1.stopWatch.Elapsed;

            int minutes = GameStats.minutes;
            int seconds = GameStats.seconds;
            minutes = minutes - ts.Minutes;
            seconds = seconds - ts.Seconds;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}", minutes, seconds);

            if (minutes == 0 && seconds == 0)
            {
                Game1.gameState = Game1.GameStates.GameOver;
                Game1.phase1Complete = true;
                Game1.paused = true;
            }
            return elapsedTime;
        }

    }
}
