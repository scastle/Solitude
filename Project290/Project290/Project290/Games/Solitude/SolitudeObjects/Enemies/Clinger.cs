using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Project290.Physics;

namespace Project290.Games.Solitude.SolitudeObjects.Enemies
{
    /// <summary>
    /// Moves along walls. Damages player if contact is made
    /// </summary>
    class Clinger //: SolitudeObject
    {
        /// <summary>
        /// The health of the Clinger
        /// </summary>
        public int Health = 100;
        public Vector2 Position;

        private Wall clingWall;

        /// <summary>
        /// The direction the Clinger is moving
        /// </summary>
        private Direction direction;

        public Clinger(Wall w)
        {
            clingWall = w;
        }

        public void Update()
        {
            
        }
        public void Draw()
        {
        }
    }
}
