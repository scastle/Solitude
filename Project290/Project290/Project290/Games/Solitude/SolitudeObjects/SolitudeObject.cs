using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Factories;
using Project290.Physics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project290.Games.Solitude.SolitudeObjects
{

    abstract class SolitudeObject
    {
        public Texture2D texture;
        
        public Body body;


        public SolitudeObject(Vector2 position, World world)
        {
            
            body = BodyFactory.CreateBody(world, position);
        }


        public void Update() 
        {
        }

        abstract public void Draw();

    }
}
