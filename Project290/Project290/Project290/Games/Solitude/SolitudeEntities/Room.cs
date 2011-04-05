using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Games.Solitude.SolitudeObjects;

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
        List<SolitudeObject> contents;


        public Room()
        {

        }
        /// <summary>
        /// Updates the objects in the room, only called when player is in room
        /// </summary>
        public void Update()
        {
            foreach (SolitudeObject m in contents)
                m.Update();
        }
        public void Draw()
        {
        }

    }



}
