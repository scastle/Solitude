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
    class TyTaylor : SolitudeObject
    {
        Vector2 targetPoint = Vector2.Zero;
        World world;
        public int health;
        public DateTime lastShot;

        public TyTaylor(World w, Vector2 position) :
            base(position, w, TextureStatic.Get("ty").Width, TextureStatic.Get("ty").Height)
        {
            this.health = Settings.TyHealth;
            world = w;
            lastShot = DateTime.Now;
            body.BodyType = BodyType.Dynamic;
            body.Position = position;
            texture = TextureStatic.Get("ty");
            fixture = FixtureFactory.CreateRectangle(texture.Width, texture.Height, 0.25f, Vector2.Zero, body);
            world.AddBody(body);
            targetPoint = body.Position;
        }

        public override void Update()
        {
            base.Update();
            targetPoint = SolitudeScreen.ship.Player.body.Position;
            Vector2 velocity = new Vector2(targetPoint.X - body.Position.X, targetPoint.Y - body.Position.Y);
            float magnitude = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            velocity.X = Settings.TySpeed * velocity.X / magnitude;
            velocity.Y = Settings.TySpeed * velocity.Y / magnitude;
            if (DateTime.Now - lastShot > TimeSpan.FromSeconds(Settings.TyShootRate))
            {
                lastShot = DateTime.Now;
                Shoot();
            }

        }

        public void Shoot()
        {
            Vector2 playerPosition = SolitudeScreen.ship.Player.body.Position;
            Vector2 velocity = new Vector2(playerPosition.X - body.Position.X, playerPosition.Y - body.Position.Y);
            float magnitude = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            velocity.X = Settings.BulletSpeed * velocity.X / magnitude;
            velocity.Y = Settings.BulletSpeed * velocity.Y / magnitude;

            Bullet b = new Bullet(velocity, body.Position, world, Color.Orange, fixture);
            SolitudeScreen.ship.contents.Add(b);
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
                0.1f);
        }
    }
}
