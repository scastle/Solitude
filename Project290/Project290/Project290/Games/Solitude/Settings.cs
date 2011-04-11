using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project290.Games.Solitude
{
    class Settings
    {
        /// <summary>
        /// a vector used so that you don't need to create a new vector2 as zero
        /// </summary>
        public static Vector2 zero = new Vector2(0);

        /// <summary>
        /// number of rows of rooms in the game
        /// </summary>
        public static int maxShipRows = 5;

        /// <summary>
        /// number of columns of rooms in the game
        /// </summary>
        public static int maxShipColumns = 5;


        public static Texture2D smoothWall;
        public static Texture2D handHoldWall;
        public static Texture2D gripWall;
        public static Texture2D metalWall;
        public static Texture2D hotWall;
        public static Texture2D coldWall;
        public static Texture2D spikeWall;
        public static Texture2D doorWall;
    }
}
