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
using Project290.Games.Solitude;
using Project290.Clock;

namespace Project290.Games.Solitude.SolitudeTools
{
    public class Explosion : SolitudeObject
    { 
        private float radius;
        private float maxRadius;
        private int power;

        public Explosion(Vector2 position, World world, float radius, int power)
            :base(position, world, TextureStatic.Get("solitudeExplosion").Width, TextureStatic.Get("solitudeExplosion").Height)
        {
            body.BodyType = BodyType.Kinematic;
            body.Position = position;
            this.radius = 1f;
            maxRadius = radius;
            this.power = power;
            fixture = FixtureFactory.CreateCircle(this.maxRadius, 0f, body);
            fixture.CollisionFilter.CollidesWith = Category.None;
            texture = TextureStatic.Get("solitudeExplosion");
        }

        public override void Update()
        {


            //end if explosion has reached max size
            if (radius > maxRadius)
            {
                //objects are only damaged in the last frame to avoid doing damage thousands of times and to conserve time.

                //check to see what objects are destroyed
                foreach (SolitudeObject o in SolitudeScreen.ship.contents)
                {
                    if (o is Enemy)
                    {
                        //if the sum of their radii is greater than the distance between them i.e. if the explosion is touching the fixture
                        if (o.body.FixtureList.Last().Shape.Radius + this.radius >= (o.body.Position - this.body.Position).Length())
                        {
                            (o as Enemy).health -= power;
                        }
                    }
                }

                //check to damage player
                if (this.radius + SolitudeScreen.ship.Player.body.FixtureList.Last().Shape.Radius >= (SolitudeScreen.ship.Player.body.Position - this.body.Position).Length())
                {
                    SolitudeScreen.ship.Player.oxygen -= power;
                }

                //remove the explosion
                SolitudeScreen.ship.Destroy(this);
                SolitudeScreen.ship.Player.hasDiedRecently = false;
            }
            else //grow
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
