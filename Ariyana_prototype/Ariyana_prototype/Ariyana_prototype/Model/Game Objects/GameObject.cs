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
    class GameObject
    {
        public Vector2 position;
        public Texture2D spriteIndex;
        public string spriteName = "";
        public float scale = 0.25f;
        public float speed = 0.0f;
        public bool alive = true;
        public bool solid = true;
        public int id = 0;
        public bool objectHit = false;
        public string objType = "GameObject";
        public float movementSpeed = 0.15f;
        public int health = 100;
        public int damage = 1;
        public int rowX = 0;
        public int columnY = 0;
        public bool objectSelected = false;
        public int textureWidth=62;
        public int textureHeight=62;
        public Texture2D healthTexture;

        public GameObject()
        {
            this.position = new Vector2(0, 0);
           
        }

        public GameObject(Vector2 position)
        {
            this.position = position;
        }

        public virtual void LoadContent(ContentManager content)
        {
            spriteIndex = content.Load<Texture2D>("Sprites/" + this.spriteName);
            healthTexture = content.Load<Texture2D>("HUD/HealthTexture");
        }

        public virtual void Update()
        {

        }

        public virtual Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, textureWidth, textureHeight); }
        }

    }
}
