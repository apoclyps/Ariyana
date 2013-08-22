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
    class SpriteRenderer
    {
        GameObject Sprite;

        public SpriteRenderer()
        {
            Sprite = new GameObject();
        }

        public SpriteRenderer(GameObject Sprite)
        {
            this.Sprite = Sprite;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!Sprite.alive) return;

            Vector2 center = new Vector2(Sprite.spriteIndex.Width / 2, Sprite.spriteIndex.Height / 2);

            spriteBatch.Draw(Sprite.spriteIndex, Sprite.position, Color.AliceBlue);
        }

    }
}
