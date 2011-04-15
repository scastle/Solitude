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
    public class Room
    {
        /// <summary>
        /// List of ALL objects that need to be put into the room
        /// </summary>
        public List<SolitudeObject> contents;
        

        public Room()
        {
            contents = new List<SolitudeObject>();
        }
        public void Add(SolitudeObject item)
        {
            contents.Add(item);
        }


        /// <summary>
        /// Updates the objects in the room, only called when player is in room
        /// </summary>
        public void Update()
        {
            contents.ForEach(i => i.Update());
        }
        public void Draw()
        {
            contents.ForEach(i => i.Draw());
        }

    }



}
