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
    class HUDObject
    {
        public Vector2 position;
        public float rotation = 0.0f;
        public Texture2D HUDIndex;
        public string HUDName = "";
        public string InitialHUDText = "";
        public string HUDText = "";
        public string objType = "HUD";

        public bool alive = true;
        public Rectangle areaRect;
        public bool solid = false;
        public int id = 0;

        public int textureWidth = 63;
        public int textureHeight = 63;

        // Used for positioning
        public int destinationX = 0;
        public int destinationY = 0;

        public int hudElementSizeX = 0;
        public int hudElementSizeY = 0;
        public int offsetBetweenBars = 10;

        //Constructor
        public HUDObject()
        {
            this.position = new Vector2(0, 0);
        }

        public HUDObject(Vector2 position)
        {
            this.position = position;
        }

        public virtual void LoadContent(ContentManager content)
        {
            HUDIndex = content.Load<Texture2D>("HUD/" + this.HUDName);
            this.areaRect = HUDIndex.Bounds; 
        }

        public virtual void Update()
        {
            
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, textureWidth, textureHeight); }
        }

    }
}
