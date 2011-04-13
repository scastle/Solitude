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
        public Vector2 drawOrigin;
        public Rectangle drawRectangle;
        public Body body;
        public Fixture fixture;

        public SolitudeObject(Vector2 position, World world, float width, float height)
        {
            
            body = BodyFactory.CreateBody(world, position);
            drawOrigin = new Vector2(width / 2f, height / 2f);
            drawRectangle = new Rectangle(0, 0, (int)width, (int)height);
        }

        public SolitudeObject(Vector2 position, World world, Shape shape, float width, float height)
        {
            body = BodyFactory.CreateBody(world, position);
            fixture = new Fixture(body, shape);
            fixture.OnCollision += new OnCollisionEventHandler(OnCollision);
            drawOrigin = new Vector2(width, height);
            drawRectangle = new Rectangle(0, 0, (int)width, (int)height);
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
