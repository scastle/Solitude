using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Physics;
using Project290.Physics.Common;
using Project290.Physics.Collision;
using Project290.Physics.Dynamics;
using Project290.Physics.Dynamics.Contacts;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Factories;
using Project290.Physics.Common.PolygonManipulation;
using Microsoft.Xna.Framework;

namespace Project290.Games.Solitude.SolitudeObjects
{
    class PhysicsItem
    {
        public PhysicsItem()
        {
            Body b = new Body(GameElements.GameWorld.screens.OfType<SolitudeScreen>().First().PhysicalWorld);
            b.BodyType = BodyType.Dynamic;
            CircleShape s = new CircleShape();
            Fixture f = new Fixture(b, s);

            f.OnCollision += new OnCollisionEventHandler(OnCollision);
        }

        public bool OnCollision(Fixture f1, Fixture f2, Contact c)
        {
            return true;
        }
        
    }
}
