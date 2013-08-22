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
    class WraithRenderer : SpriteRenderer
    {
        GameObject Wraith;

        public WraithRenderer(GameObject Wraith)
        {
            this.Wraith = Wraith;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {   
            if (Wraith.alive && Wraith.spriteIndex != null)
            {
                Vector2 center = new Vector2(Wraith.spriteIndex.Width / 2, Wraith.spriteIndex.Height / 2);
                spriteBatch.Draw(Wraith.spriteIndex, Wraith.position, Color.AliceBlue);

                // Displaying health bar
                Rectangle healthRectangle = new Rectangle((int)Wraith.position.X, (int)Wraith.position.Y, Wraith.healthTexture.Width, Wraith.healthTexture.Height);
                spriteBatch.Draw(Wraith.healthTexture, healthRectangle, Color.Gray);

                float visibleWidth = Wraith.health / 2;
                healthRectangle = new Rectangle((int)Wraith.position.X, (int)Wraith.position.Y, (int)(visibleWidth), Wraith.healthTexture.Height);
                spriteBatch.Draw(Wraith.healthTexture, healthRectangle, Color.Black);

            }

        }
    }
}
