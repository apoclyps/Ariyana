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
    class HUDRenderer
    {
        HUDObject HUDSprite;

        public HUDRenderer()
        {
            HUDSprite = new HUDObject();
        }

        public HUDRenderer(HUDObject HUDSprite)
        {
            this.HUDSprite = HUDSprite;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!HUDSprite.alive) return;

            Vector2 center = new Vector2(HUDSprite.HUDIndex.Width / 2, HUDSprite.HUDIndex.Height / 2);
            spriteBatch.Draw(HUDSprite.HUDIndex, HUDSprite.position, Color.AliceBlue);

        }

    }
}
