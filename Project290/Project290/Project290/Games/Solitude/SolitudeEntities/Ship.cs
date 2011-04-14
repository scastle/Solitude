using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Games.Solitude.SolitudeTools;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.Physics.Collision;
using Project290.Physics.Dynamics;

namespace Project290.Games.Solitude.SolitudeEntities
{
    /// <summary>
    /// Stores information about the entire world!!! It has a matrix of all the rooms
    /// and determines when to enter/exit rooms and handles their objects' transitions
    /// into and out of the physicsworld
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// The matrix containing each room in the game
        /// </summary>
        static Room[,] rooms;

        /// <summary>
        /// the player object.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The world for all physical objects to interact
        /// </summary>
        public World PhysicalWorld;

        /// <summary>
        /// the row index of the active room
        /// </summary>
        static int r;

        /// <summary>
        /// the column index of the active room
        /// </summary>
        static int c;

        public Room GetCurrentRoom()
        {
            return rooms[r, c];
        }


        public Ship()
        {
            PhysicalWorld = new World(Microsoft.Xna.Framework.Vector2.Zero);
            Player = new Player(new Microsoft.Xna.Framework.Vector2(1200f, 600f), PhysicalWorld);
            rooms = new Room[Settings.maxShipRows, Settings.maxShipColumns];
            // initialize all rooms? replace this later once we have all the rooms
            rooms[0, 0] = new Room();

            Wall w = new Wall(new Microsoft.Xna.Framework.Vector2(950, 750), PhysicalWorld, 112, 500, 1, WallType.Smooth);
            Wall h = new Wall(new Microsoft.Xna.Framework.Vector2(25, 270), PhysicalWorld, 16, 512, 1, WallType.HandHold);
            Wall i = new Wall(new Microsoft.Xna.Framework.Vector2(200, 270), PhysicalWorld, 256, 16, 1, WallType.Grip);
            PhysicalWorld.AddBody(Player.body);
            PhysicalWorld.AddBody(w.body);
            PhysicalWorld.AddBody(h.body);
            PhysicalWorld.AddBody(i.body);

            GetCurrentRoom().Add(w);
            GetCurrentRoom().Add(h);
            GetCurrentRoom().Add(i);
            Player.body.ApplyLinearImpulse(new Microsoft.Xna.Framework.Vector2(-5000, 5000));

            r = 0;
            c = 0;
        }

        /// <summary>
        /// This will deserialize a room and add it to the ship matrix
        /// </summary>
        /// <param name="row">row of the room</param>
        /// <param name="column">column of the room</param>
        /// <param name="path">filename of the xml file</param>
        public void createRoom(int row, int column, string path)
        {
            rooms[row,column] = Serializer.DeserializeFile<Room>(path);
        }


        public void Update()
        {
            //Console.WriteLine(Player.body.Position);
            PhysicalWorld.Step(0.01f);
            Player.Update();
            GetCurrentRoom().Update();
        }
        public void Draw()
        {
            Player.Draw();
            GetCurrentRoom().Draw();
        }

    }
}
