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
    class Enemy : GameObject
    {
        KeyboardState inputKeyboard;
        KeyboardState previousKeyboard;
        MouseState inputMouse;
        MouseState previousMouse;
        public IEnemyType enemyBehaviour;

        public void SetSortStrategy(IEnemyType enemyBehaviour)
        {
            this.enemyBehaviour = enemyBehaviour;
        }

        public Enemy(Vector2 position)  : base(position)
        {
            this.position = position;
            this.objType = "Enemy";
            this.movementSpeed = movementSpeed + 0.65f;
        }

        public Enemy(Vector2 position, String spriteName) : base(position)
        {
            this.position = position;
            this.spriteName = spriteName;
            this.movementSpeed = movementSpeed + 0.65f;
            this.objType = "Enemy";
        }

        public override void Update()
        {
            if (!alive) return;

            inputMouse = Mouse.GetState();
            inputKeyboard = Keyboard.GetState();
            
            enemyBehaviour.moveCommand(this);

            if (inputKeyboard.IsKeyDown(Keys.Up) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y == 1.0f))
            {
                position.Y -= movementSpeed+1;
            }
            if (inputKeyboard.IsKeyDown(Keys.Left) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == -1.0f))
            {
                position.X -= movementSpeed + 1;
            }
            if (inputKeyboard.IsKeyDown(Keys.Down) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y == -1.0f))
            {
                position.Y += movementSpeed + 1;
            }
            if (inputKeyboard.IsKeyDown(Keys.Right) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 1.0f))
            {
                position.X += movementSpeed + 1;
            }

            previousKeyboard = inputKeyboard;
            previousMouse = inputMouse;

            base.Update();
        }

        public override Rectangle BoundingBox
        {
            get { return new Rectangle((int)position.X, (int)position.Y, textureWidth, textureHeight); }
        }
    }
}
