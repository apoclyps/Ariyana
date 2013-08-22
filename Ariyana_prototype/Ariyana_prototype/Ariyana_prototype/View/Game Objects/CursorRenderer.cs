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
    class CursorRenderer : SpriteRenderer
    {
        Cursor Cursor;

        public CursorRenderer(GameObject cursor)
        {
            this.Cursor = (Cursor)cursor;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (Cursor.spriteIndex != null)
            {
                Vector2 center = new Vector2(Cursor.spriteIndex.Width / 2, Cursor.spriteIndex.Height / 2);

                if (Cursor.getUnitSelected() == true)
                {
                    spriteBatch.Draw(Cursor.spriteIndex, Cursor.position, Color.AliceBlue);
                }
                else
                {
                    spriteBatch.Draw(Cursor.spriteIndex, Cursor.position, Color.AliceBlue);
                }
            }
        }
    }
}
