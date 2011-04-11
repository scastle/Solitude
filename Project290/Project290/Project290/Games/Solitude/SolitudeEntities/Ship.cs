using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Games.Solitude.SolitudeTools;

namespace Project290.Games.Solitude.SolitudeEntities
{
    /// <summary>
    /// Stores information about the entire world!!! It has a matrix of all the rooms
    /// and determines when to enter/exit rooms and handles their objects' transitions
    /// into and out of the physicsworld
    /// </summary>
    class Ship
    {
        /// <summary>
        /// The matrix containing each room in the game
        /// </summary>
        Room[,] rooms;

        /// <summary>
        /// the row index of the active room
        /// </summary>
        int r;

        /// <summary>
        /// the column index of the active room
        /// </summary>
        int c;


        public Ship()
        {
            rooms = new Room[Settings.maxShipRows, Settings.maxShipColumns];
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

            rooms[r,c].Update();
        }
        public void Draw()
        {

            rooms[r,c].Draw();
        }

    }
}
