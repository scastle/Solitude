using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Physics.Dynamics;
using Project290.Screens;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.GameElements;

namespace Project290.Games.Solitude
{
    /// <summary>
    /// Object of the whole game.
    /// contains the ship, player, and objects, and governs interactions
    /// </summary>
    public class SolitudeScreen : GameScreen
    {
        /// <summary>
        /// ship contains all information about levels of the game
        /// </summary>
        public static Ship ship;

        public SolitudeScreen(int scoreBoardIndex)
            :base(scoreBoardIndex)
        {
            //todo: everything
            ship = new Ship();
          //  this.Reset();
        }

        public override void Update()
        {
            ship.Update();
        }

        internal override void Reset()
        {
            //todo: reset
            base.Reset();
        }

        public override void Draw()
        {
            ship.Draw();
            base.Draw();
        }


        internal override void GameOver()
        {
            base.GameOver();
        }
    }
}
