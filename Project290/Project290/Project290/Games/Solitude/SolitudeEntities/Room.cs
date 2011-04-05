using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project290.Games.Solitude.SolitudeEntities
{

    /// <summary>
    /// Contains all information about the objects in a room.
    /// Stores type, and initial positions (velocities if necessary)
    /// Each room is loaded at startup, and its contents will be put into the physics simulation when
    /// the ship class calls the Enter() method. 
    /// </summary>
    class Room
    {
        /// <summary>
        /// List of ALL objects that need to be put into the room
        /// </summary>
        List<Object> contents;

        
    }
}
