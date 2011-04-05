using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        Room[][] rooms;

        /// <summary>
        /// the row index of the active room
        /// </summary>
        int r;

        /// <summary>
        /// the column index of the active room
        /// </summary>
        int c;
    }
}
