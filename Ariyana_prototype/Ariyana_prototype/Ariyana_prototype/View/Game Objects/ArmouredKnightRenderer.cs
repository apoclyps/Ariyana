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
    class ArmouredKnightRenderer : SpriteRenderer
    {
        GameObject armouredKnight;
        public int spriteTileX = 0;
        public int spriteTileY = 0;

        public ArmouredKnightRenderer(GameObject armouredKnight)
        {
            this.armouredKnight = armouredKnight;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (armouredKnight.alive && armouredKnight.spriteIndex !=null)
            {

                Vector2 center = new Vector2(armouredKnight.spriteIndex.Width / 2, armouredKnight.spriteIndex.Height / 2);
                if (armouredKnight.objectSelected)
                {
                    spriteBatch.Draw(armouredKnight.spriteIndex, new Rectangle((int)armouredKnight.position.X, (int)armouredKnight.position.Y, GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight), new Rectangle(spriteTileX, spriteTileY, 63, 63), Color.FromNonPremultiplied(75, 75, 75, 100));
                }
                else
                {
                    spriteBatch.Draw(armouredKnight.spriteIndex, new Rectangle((int)armouredKnight.position.X, (int)armouredKnight.position.Y, GamePiece.PieceDisplayWidth, GamePiece.PieceDisplayHeight), new Rectangle(spriteTileX, spriteTileY, 63, 63), Color.AliceBlue);
                }

                // Displaying health bar
                Rectangle healthRectangle = new Rectangle((int)armouredKnight.position.X + 5, (int)armouredKnight.position.Y, armouredKnight.healthTexture.Width, armouredKnight.healthTexture.Height);

                spriteBatch.Draw(armouredKnight.healthTexture, healthRectangle, Color.Gray);

                float visibleWidth = armouredKnight.health / 2;
                healthRectangle = new Rectangle((int)armouredKnight.position.X + 5, (int)armouredKnight.position.Y, (int)(visibleWidth), armouredKnight.healthTexture.Height);

                spriteBatch.Draw(armouredKnight.healthTexture, healthRectangle, Color.White);
                spriteBatch.DrawString(Game1.FairyDustfontSprite, armouredKnight.id.ToString(), new Vector2(healthRectangle.X + (healthRectangle.Width / 2), healthRectangle.Y - 5), Color.Black);
            }
        }
    }
}
