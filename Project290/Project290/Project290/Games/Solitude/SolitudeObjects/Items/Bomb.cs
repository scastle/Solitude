using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Dynamics;
using Project290.Physics.Factories;
using Project290.Rendering;
using Project290.Games.Solitude.SolitudeTools;

namespace Project290.Games.Solitude.SolitudeObjects.Items
{
    class Bomb : SolitudeObject
    {

        private DateTime age;

        public Bomb(Vector2 position, World world, Vector2 velocity)
            : base(position, world, TextureStatic.Get("bomb").Width, TextureStatic.Get("bomb").Height)
        {
            body.BodyType = BodyType.Dynamic;
            texture = TextureStatic.Get("bomb");
            fixture = FixtureFactory.CreateCircle(TextureStatic.Get("bomb").Width / 2, 1, body);
            
            age = DateTime.Now;
            world.AddBody(body);
            body.LinearVelocity = velocity;
        }

        public override void Update()
        {
            if (DateTime.Now - age > TimeSpan.FromSeconds(Settings.BombTimer))
            {
                //explode
                SolitudeScreen.ship.contents.Add(new Explosion(body.Position, body.World, 256f, 200));
                SolitudeScreen.ship.Destroy(this);
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
                1f,
                SpriteEffects.None,
                .5f);
        }
    }
}
