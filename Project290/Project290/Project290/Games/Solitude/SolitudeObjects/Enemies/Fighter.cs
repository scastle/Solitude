using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Physics.Dynamics;
using Project290.Screens;
using Project290.Games.Solitude;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Rendering;

namespace Project290.Games.Solitude.SolitudeObjects.Enemies
{
    class Fighter : SolitudeObject
    {
        World world;
        DateTime lastShot;

        public Fighter(Vector2 position, World w)
            : base(position, w, TextureStatic.Get("fighter").Width, TextureStatic.Get("fighter").Height)
        {
            world = w;
            lastShot = DateTime.Now;
            world = w;
            body.BodyType = BodyType.Dynamic;
            body.Position = position;
            texture = TextureStatic.Get("fighter");
            fixture = FixtureFactory.CreateRectangle(texture.Width, texture.Height, 0.25f, Vector2.Zero, body);
            world.AddBody(body);
        }

        public override void Update()
        {
            if (DateTime.Now - lastShot > TimeSpan.FromSeconds(Settings.SentinelShootRate))
            {
                lastShot = DateTime.Now;
                Shoot();
            }
        }

        public override void  Draw()
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
                0.1f);
        }

        public void Shoot()
        {
            Vector2 playerPosition = SolitudeScreen.ship.Player.body.Position;
            Vector2 velocity = new Vector2(playerPosition.X - body.Position.X, playerPosition.Y - body.Position.Y);
            float magnitude = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            velocity.X = Settings.BulletSpeed * velocity.X / magnitude;
            velocity.Y = Settings.BulletSpeed * velocity.Y / magnitude;

            Bullet b = new Bullet(velocity, body.Position, world, Color.Red, this.fixture);
            SolitudeScreen.ship.contents.Add(b);
        }
    }
}
