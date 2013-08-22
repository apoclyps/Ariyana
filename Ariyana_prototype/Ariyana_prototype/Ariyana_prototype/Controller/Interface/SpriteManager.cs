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
    class SpriteManager
    {
        SpriteRenderer renderSprite;
        HUDRenderer renderHUD;

        public void renderEnemies(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameBoardManager.enemyList.Length; i++)
            {
                if (GameBoardManager.enemyList[i].alive && GameBoardManager.enemyList[i].spriteIndex != null)
                {
                    renderSprite = new WraithRenderer(GameBoardManager.enemyList[i]);
                    renderSprite.Draw(spriteBatch);
                }
            }
        }

        public void renderUnits(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameBoardManager.unitList.Length; i++)
            {
                if (GameBoardManager.unitList[i].alive && GameBoardManager.unitList[i] != null)
                {
                    renderSprite = new ArmouredKnightRenderer(GameBoardManager.unitList[i]);
                    renderSprite.Draw(spriteBatch);
                }
            }
        }

        public void renderAllSprites(SpriteBatch spriteBatch)
        {
            renderEnemies(spriteBatch);
            renderUnits(spriteBatch);

            renderSprite = new CursorRenderer(GameBoardManager.Cursor);
            renderSprite.Draw(spriteBatch);
        }

        public void renderAllHUDSprites(SpriteBatch spriteBatch)
        {
            foreach (HUDObject obj in HUDManager.hudList)
            {
                if (obj.alive)
                {
                    switch (obj.HUDName)
                    {
                        case "PhaseBar":
                            renderHUD = new PhaseBarRenderer(obj);
                            renderHUD.Draw(spriteBatch);
                            break;
                        case "armored_swordman_attack":
                            renderHUD = new SpellButtonRenderer(obj);
                            renderHUD.Draw(spriteBatch);
                            break;
                        case "heal":
                            renderHUD = new SpellButtonRenderer(obj);
                            renderHUD.Draw(spriteBatch);
                            break;
                        case "airship_attack":
                            renderHUD = new SpellButtonRenderer(obj);
                            renderHUD.Draw(spriteBatch);
                            break;
                        case "shield1":
                            renderHUD = new SpellButtonRenderer(obj);
                            renderHUD.Draw(spriteBatch);
                            break;

                    }
                }
            }
        }

    }
}
