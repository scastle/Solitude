using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Screens;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude.SolitudeObjects;

namespace Project290.Games.Solitude
{
    /// <summary>
    /// Object of the whole game.
    /// contains the ship, player, and objects, and governs interactions
    /// </summary>
    class SolitudeScreen : GameScreen
    {
        /// <summary>
        /// ship contains all information about levels of the game
        /// </summary>
        Ship ship;

        /// <summary>
        /// the player object.
        /// </summary>
        Player player;

        /// <summary>
        /// a list of all objects in the current room
        /// </summary>
        List<Object> activeObjects;


        public SolitudeScreen(int scoreBoardIndex)
            :base(scoreBoardIndex)
        {
            //todo: everything
            



            this.Reset();
        }

        internal override void Reset()
        {
            //todo: reset

            base.Reset();
        }

    }
}
