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
        /// number of rows of rooms in the game
        /// </summary>
        public static int maxShipRows = 5;

        /// <summary>
        /// number of columns of rooms in the game
        /// </summary>
        public static int maxShipColumns = 5;

        /// <summary>
        /// a constant governing jetpack strength.
        /// </summary>
        public static int jetPackForceMult = 30000;

        /// <summary>
        /// The amount a damage a punch does
        /// </summary>
        public static int PunchDamage = 50;

        /// <summary>
        /// The amount of damage a bullet does
        /// </summary>
        public static int BulletDamage = 50;

        /// <summary>
        /// the time (in seconds) for a bomb to explode
        /// </summary>
        public static int BombTimer = 5;

        /// <summary>
        /// force applied when a bomb is used
        /// </summary>
        public static int bombForce = 1500000;

        /// <summary>
        /// maximum number of bombs at a time
        /// </summary>
        public static int maxBombs = 3;

        /// <summary>
        /// The speed of a bullet
        /// </summary>
        public static int BulletSpeed = 400;

        /// <summary>
        /// Time (in seconds) between shots for the Sentinel
        /// </summary>
        public static int SentinelShootRate = 2;

        /// <summary>
        /// The velocity that that Fighter moves
        /// </summary>
        public static int FighterSpeed = 100;
    }
}
