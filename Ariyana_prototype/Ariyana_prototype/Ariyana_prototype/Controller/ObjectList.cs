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

namespace Ariyana
{
    class ObjectList
    {

        public static List<GameObject> objList = new List<GameObject>();

        public static void Initilize()
        {

            Enemy Grue = new Enemy(new Vector2(0, 0));
            //Enemy Grue2 = new Enemy(new Vector2(5, 5));
            objList.Add(Grue);
            //objList.Add(Grue2);

            /*
            for (int i = 0; i < 64; i++)
            {
                GameObject obj = new Enemy(new Vector2(0, 0));
                obj.alive = false;
                objList.Add(obj);
            }
             * */

           /* objList.Add(new Wall(new Vector2(50, 50)));
            objList.Add(new Wall(new Vector2(60, 50)));
            objList.Add(new Wall(new Vector2(70, 50)));
            objList.Add(new Wall(new Vector2(80, 50)));

            objList.Add(new Man(new Vector2(450, 450)));
            objList.Add(new Cursor(new Vector2(450, 450)));
            */
        }

        public static void Reset()
        {
            foreach (GameObject obj in objList)
            {
                obj.alive = false;
            }
        }
    }
}
