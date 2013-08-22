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
    class SpellBookBar : HUDObject
    {


        public SpellBookBar(Vector2 position)
            : base(position)
        {
            this.position = position;

            this.destinationX = 1200;
            this.destinationY = 200;

            this.hudElementSizeX = 450;
            this.hudElementSizeY = 50;
            this.offsetBetweenBars = 10;
        }

        public SpellBookBar(Vector2 position, String HUDName)
            : base(position)
        {
            this.position = position;
            this.HUDName = HUDName;
            this.destinationX = 1200;
            this.destinationY = 200;

            this.hudElementSizeX = 450;
            this.hudElementSizeY = 50;
            this.offsetBetweenBars = 10;
        }


    }
}
