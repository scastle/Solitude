using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Dynamics;
using Project290.Physics.Factories;
using Project290.Rendering;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.Games.Solitude.SolitudeObjects.Enemies;

namespace Project290.Games.Solitude.SolitudeTools
{
    public class Explosion : SolitudeObject
    {
        private DateTime start;
        private float radius;
        private float maxRadius;
        private int power;
        private Fixture fixture;
        private bool hasDamagedPlayer;

        public Explosion(Vector2 position, World world, float radius, int power)
            :base(position, world, TextureStatic.Get("solitudeExplosion").Width, TextureStatic.Get("solitudeExplosion").Height)
        {
            hasDamagedPlayer = false;
            body.BodyType = BodyType.Kinematic;
            body.Position = position;
            start = DateTime.Now;
            this.radius = 1f;
            maxRadius = radius;
            this.power = power;
            fixture = FixtureFactory.CreateCircle(this.maxRadius, 0f, body);
            fixture.CollisionFilter.CollidesWith = Category.None;
            texture = TextureStatic.Get("solitudeExplosion");
        }

        public override void Update()
        {
            foreach(SolitudeObject o in SolitudeScreen.ship.contents)
            {
                    if (o is Enemy)
                    {
                        //if the sum of their radii is less than the distance between them i.e. if the explosion is touching the fixture
                        if (o.body.FixtureList.Last().Shape.Radius + this.radius <= (o.body.Position.Length() - this.body.Position.Length()))
                        {
                                (o as Enemy).health-= power;
                        }
                    }
            }
            if (this.radius >= (SolitudeScreen.ship.Player.body.Position.Length() - this.body.Position.Length()) && !hasDamagedPlayer)
            {
                SolitudeScreen.ship.Player.oxygen -= power;
                Console.WriteLine("Player radius {0}, Explosion Radius {1}, Distance between them", SolitudeScreen.ship.Player.body.FixtureList.Last().Shape.Radius, this.radius);
                hasDamagedPlayer = true;
                Console.WriteLine("Total radius is {0} is <= {1}", SolitudeScreen.ship.Player.body.FixtureList.Last().Shape.Radius + this.radius,
                    (SolitudeScreen.ship.Player.body.Position.Length() - this.body.Position.Length()));
            }
            if (radius > maxRadius)
            {
                SolitudeScreen.ship.Destroy(this);
                hasDamagedPlayer = false;
            }
            else
            {
                //Console.WriteLine("Max Radius is {0}", maxRadius);
                //Console.WriteLine("Radius is {0}", radius);
                radius += radius * .1f;
            }
        }

        public override void Draw()
        {
            Drawer.Draw(
                texture,
                body.Position,
                drawRectangle,
                Color.White,
                body.Rotation,
                drawOrigin,
                radius/(texture.Width/2), //draw to the scale the ratio of radius to its texture
                SpriteEffects.None,
                .8f);
        }

    }
}
