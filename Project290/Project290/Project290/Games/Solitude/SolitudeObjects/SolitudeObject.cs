using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Factories;
using Project290.Physics.Dynamics;
using Microsoft.Xna.Framework;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Dynamics.Contacts;

namespace Project290.Games.Solitude.SolitudeObjects
{

    public abstract class SolitudeObject
    {
        public Texture2D texture;
        
        public Body body;
        public Fixture fixture;

        public SolitudeObject(Vector2 position, World world)
        {
            
            body = BodyFactory.CreateBody(world, position);
        }

        public SolitudeObject(Vector2 position, World world, Shape shape)
        {
            body = BodyFactory.CreateBody(world, position);
            fixture = new Fixture(body, shape);
            fixture.OnCollision += new OnCollisionEventHandler(OnCollision);
        }

        public void Update() 
        {
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact c)
        {
            return true;
        }

        abstract public void Draw();

    }
}
