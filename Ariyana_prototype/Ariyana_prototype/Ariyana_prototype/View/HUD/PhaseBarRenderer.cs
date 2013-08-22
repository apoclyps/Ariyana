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
    class PhaseBarRenderer : HUDRenderer
    {
        SpellBookBar phaseBar;

        public PhaseBarRenderer(HUDObject phaseBar)
        {
            this.phaseBar = (SpellBookBar)phaseBar;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (phaseBar.HUDIndex != null)
            {
                Vector2 center = new Vector2(phaseBar.HUDIndex.Width / 2, phaseBar.HUDIndex.Height / 2);

                int destinationX = phaseBar.destinationX;
                int destinationY = phaseBar.destinationY;

                int phasebarSizeX = phaseBar.hudElementSizeX;
                int phasebarSizeY = phaseBar.hudElementSizeY;

                Vector2 PhasebarSize = new Vector2(phasebarSizeX, phasebarSizeY);
                Rectangle destination = new Rectangle(destinationX, destinationY, (int)PhasebarSize.X, (int)PhasebarSize.Y);
                Rectangle source = new Rectangle(0, 0, 1001, 68);

                spriteBatch.Draw(phaseBar.HUDIndex, destination, source, Color.White);
                spriteBatch.DrawString(Game1.FairyDustfontHUD, this.phaseBar.HUDText, new Vector2(destinationX + (int)(phasebarSizeX / 3), destinationY + (int)(phasebarSizeY / 6)), Color.White);
            }
        }

    }
}
