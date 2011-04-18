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

namespace Project290.Games.Solitude.SolitudeTools
{
    public class Explosion : SolitudeObject
    {
        private DateTime start;
        private float radius;
        private float maxRadius;
        private int power;
        private Fixture fixture;

        public Explosion(Vector2 position, World world, float radius, int power)
            :base(position, world, TextureStatic.Get("solitudeExplosion").Width, TextureStatic.Get("solitudeExplosion").Height)
        {
            body.BodyType = BodyType.Kinematic;
            body.Position = position;
            start = DateTime.Now;
            this.radius = .001f;
            maxRadius = radius;
            this.power = power;
            fixture = FixtureFactory.CreateCircle(this.radius, 0f, body);
            fixture.CollisionFilter.CollidesWith = Category.None;
            texture = TextureStatic.Get("solitudeExplosion");
        }

        public override void Update()
        {
            fixture.Shape.Radius += .1f;
            
        }
        public override void Draw()
        {
            Drawer.Draw(
                texture,
                body.Position,
                drawRectangle,
                Color.Black,
                body.Rotation,
                drawOrigin,
                radius,
                SpriteEffects.None,
                .8f);
        }

    }
}
