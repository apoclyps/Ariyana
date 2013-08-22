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
    class SpellButton : HUDObject
    {
        public SpellButton(Vector2 position)
            : base(position)
        {
            this.position = position;

            this.destinationX = 1400;
            this.destinationY = 550;
            this.objType = "HUD";
            this.hudElementSizeX = 64;
            this.hudElementSizeY = 64;
            this.offsetBetweenBars = 30;
        }

        public SpellButton(Vector2 position, String HUDName)
            : base(position)
        {
            this.position = position;
            this.HUDName = HUDName;

            this.destinationX = 1400;
            this.destinationY = 650;
            this.objType = "HUD";
            this.hudElementSizeX = 75;
            this.hudElementSizeY = 75;
            this.offsetBetweenBars = 45;
        }


    }
}
