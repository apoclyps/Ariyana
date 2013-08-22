using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Ariyana_prototype
{
    class GamePiece
    {
        public static string[] PieceTypes = { "Black", "White" };

        //Grabbing images from tilesheet
        public const int PieceHeight = 40;
        public const int PieceWidth = 40;

        //Size of image on screen
        public const int PieceDisplayHeight = 63;
        public const int PieceDisplayWidth = 63;

        public const int MaxPlayablePieceIndex = 5;
        public const int EmptyPieceIndex = 6;

        private const int textureOffsetX = 1;
        private const int textureOffsetY = 1;
        private const int texturePaddingX = 1;
        private const int texturePaddingY = 1;

        private string pieceSuffix = "";

        public GamePiece()
        {
            pieceSuffix = "";
        }

        public Rectangle GetSourceRect()
        {
            int x = textureOffsetX;
            int y = textureOffsetY;

            if (pieceSuffix.Contains("White"))
                x += PieceWidth + texturePaddingX;

            y += (PieceHeight + texturePaddingY);

            return new Rectangle(x, y, PieceWidth, PieceHeight);
        }


    }
}
