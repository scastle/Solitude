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


    }
}
