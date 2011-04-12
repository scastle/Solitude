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
        Ship ship;

        /// <summary>
        /// the player object.
        /// </summary>
        Player player;

        /// <summary>
        /// a list of all objects in the current room
        /// </summary>
        List<Object> activeObjects;
        Room r;
        

        public SolitudeScreen(int scoreBoardIndex)
            :base(scoreBoardIndex)
        {
            //todo: everything
            PhysicalWorld = new Physics.Dynamics.World(Microsoft.Xna.Framework.Vector2.Zero);

            player = new Player(new Microsoft.Xna.Framework.Vector2(300f, 300f), PhysicalWorld);
            r = new Room();
            //r.Add(player);
            r.Add(new Wall(new Microsoft.Xna.Framework.Vector2(400f, 400f), PhysicalWorld, 200f, 100f, 1f, WallType.HandHold));

            this.Reset();
        }

        public World getWorld()
        {
            return PhysicalWorld;
        }

        public override void Update()
        {
            r.Update();

        }

        internal override void Reset()
        {
            //todo: reset

            base.Reset();
        }

        public override void Draw()
        {
            
            base.Draw();
            player.Draw();
            r.Draw();

        }


        internal override void GameOver()
        {
            base.GameOver();
        }
    }
}
