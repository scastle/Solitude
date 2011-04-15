using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Physics.Dynamics;
using Project290.Screens;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.GameElements;
using Microsoft.Xna.Framework;

namespace Project290.Games.Solitude
{

    public enum Direction
    {
        Up=-1, Down=1, Left=-2, Right=2
    }

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

        Rectangle screenSize = new Rectangle(0, 0, 1920, 1080);

        public override void Draw()
        {
            Rendering.Drawer.DrawRectangle(screenSize, 1920f, 0, Color.LightBlue);
            ship.Draw();
            base.Draw();
        }


        internal override void GameOver()
        {
            base.GameOver();
        }
    }
}
