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
    class SpellButtonRenderer : HUDRenderer
    {
        SpellButton spellbutton;

        public SpellButtonRenderer(HUDObject spellbutton)
        {
            this.spellbutton = (SpellButton)spellbutton;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (spellbutton.HUDIndex != null)
            {
                Vector2 center = new Vector2(spellbutton.HUDIndex.Width / 2, spellbutton.HUDIndex.Height / 2);

                int destinationX = spellbutton.destinationX;
                int destinationY = spellbutton.destinationY;

                int phasebarSizeX = spellbutton.hudElementSizeX;
                int phasebarSizeY = spellbutton.hudElementSizeY;

                Vector2 SpellButtonSize = new Vector2(phasebarSizeX, phasebarSizeY);
                Rectangle destination = new Rectangle(destinationX, destinationY, (int)phasebarSizeX + 15, (int)phasebarSizeY + 15);
                Rectangle source = new Rectangle(0, 0, 64, 64);

                spriteBatch.Draw(spellbutton.HUDIndex, destination, source, Color.White);
                spriteBatch.DrawString(Game1.FairyDustfontHUD, this.spellbutton.HUDText, new Vector2(destinationX + (int)(phasebarSizeX / 3), destinationY + (int)(phasebarSizeY / 6)), Color.White);
            }
        }

    }
}
