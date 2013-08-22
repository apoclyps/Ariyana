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
    class Unit : GameObject
    {
        public IUnitType unitBehaviour;
        
        public void SetSortStrategy(IUnitType unitBehaviour)
        {
            this.unitBehaviour = unitBehaviour;
        }

        public Unit(Vector2 position)  : base(position)
        {
            this.position = position;
            this.movementSpeed = 2;
            this.damage = 2;
            objType = "Unit";
        }

        public Unit(Vector2 position, String spriteName) : base(position)
        {
            this.position = position;
            this.movementSpeed = 1f;
            this.spriteName = spriteName;
            this.damage = 2;
            objType = "Unit";
        }

        public override void Update()
        {
            if (!alive) return;

            unitBehaviour.moveCommand(this);

            base.Update();
        }

    }
}
