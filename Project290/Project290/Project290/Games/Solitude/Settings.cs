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
        public static int maxShipRows = 10;

        /// <summary>
        /// number of columns of rooms in the game
        /// </summary>
        public static int maxShipColumns =10;

        /// <summary>
        /// a constant governing jetpack strength.
        /// </summary>
        public static int jetPackForceMult = 100000;

        /// <summary>
        /// The amount a damage a punch does
        /// </summary>
        public static int PunchDamage = 50;

        /// <summary>
        /// The amount of damage a bullet does
        /// </summary>
        public static int BulletDamage = 100;

        /// <summary>
        /// the rate at which a mauler charges
        /// </summary>
        public static int MaulerForce = 100000;

        /// <summary>
        /// the time (in seconds) for a bomb to explode
        /// </summary>
        public static int BombTimer = 2;

        /// <summary>
        /// force applied when a bomb is used
        /// </summary>
        public static int bombForce =1000000;

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
    
        /// <summary>
        /// width of the explosion caused by an enemy dying.
        /// </summary>
        public static int robotExpRadius = 128;

        /// <summary>
        /// damage dealt by enemy's explosion
        /// </summary>
        public static int robotExpPower = 100;

        /// <summary>
        /// width of the explosion caused by a bomb
        /// </summary>
        public static int bombExpRadius = 256;

        /// <summary>
        /// damage dealt by a bomb's explosion
        /// </summary>
        public static int bombExpPower = 200;

        /// <summary>
        /// health of a sentinel
        /// </summary>
        public static int sentinelHealth = 300;

        /// <summary>
        /// health of a mauler
        /// </summary>
        public static int maulerHealth = 200;

        /// <summary>
        /// health of a fighter
        /// </summary>
        public static int fighterHealth = 500;
    }
}
