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
    /// <summary>
    /// This enemy moves back and forth in set path or semi-random directions. 
    /// If the player draws near, it attacks
    /// </summary>
    class Sentinel : SolitudeObject
    {
        /// <summary>
        /// The health of the Sentinel
        /// </summary>
        int Health = 150;
        private World world;
        int patrolRate;
        private DateTime lastShot;
        private DateTime lastTurn;
        Vector2 speed;

        public Sentinel(Vector2 position, Vector2 velocity, World w, int rate)
            : base(position, w, TextureStatic.Get("sentinel").Width, TextureStatic.Get("sentinel").Height)
        {
            patrolRate = rate;
            lastShot = DateTime.Now;
            lastTurn = lastShot;
            world = w;
            body.BodyType = BodyType.Dynamic;
            speed = velocity;
            body.LinearVelocity = velocity;
            body.Position = position;
            texture = TextureStatic.Get("sentinel");
            fixture = FixtureFactory.CreateRectangle(texture.Width, texture.Height, 0.25f, Vector2.Zero, body);
            world.AddBody(body);
        }

        override public void Update()
        {
            if(DateTime.Now  - lastTurn > TimeSpan.FromSeconds(patrolRate)){
                lastTurn = DateTime.Now;
                speed *= -1;
                body.LinearVelocity = speed;
            }
            if (DateTime.Now - lastShot > TimeSpan.FromSeconds(Settings.SentinelShootRate))
            {
                lastShot = DateTime.Now;
                Shoot();
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

    public class Bullet : SolitudeObject
    {
        public Body BulletBody;
        public Color BulletColor;


        private Fixture senderFixture;
        private World world;

        public Bullet(Vector2 velocity, Vector2 position, World w, Color color, Fixture sender)
            : base(position, w, 15f, 15f)
        {
            senderFixture = sender;
            world = w;
            BulletColor = color;
            body.BodyType = BodyType.Dynamic;
            body.IsBullet = true;
            body.LinearVelocity = velocity;
            body.Position = position;
            texture = TextureStatic.Get("bullet");
            fixture = FixtureFactory.CreateCircle(7f, 1, body);
            body.Mass = 0;
            fixture.Body.UserData = "bullet";
            body.Inertia = 0;
            body.Torque = 0;
            fixture.CollisionFilter.IgnoreCollisionWith(senderFixture);

            fixture.BeforeCollision += new BeforeCollisionEventHandler(BeforeCollision);
            fixture.OnCollision += new OnCollisionEventHandler(OnCollision);

            w.AddBody(body);
        }

        public override void Draw()
        {
            Drawer.Draw(
                texture,
                body.Position,
                drawRectangle,
                BulletColor,
                body.Rotation,
                drawOrigin,
                1f,
                SpriteEffects.None,
                0.1f);
        }

        public bool BeforeCollision(Fixture f1, Fixture f2)
        {
            if (f2 != senderFixture)
            {
                if (f2 == SolitudeScreen.ship.Player.PlayerFixture)
                {
                    SolitudeScreen.ship.Player.oxygen -= Settings.BulletDamage;
                }
                world.RemoveBody(body);
                SolitudeScreen.ship.contents.Remove(this);
            }
            return true;
        }

        public new bool OnCollision(Fixture f1, Fixture f2, Physics.Dynamics.Contacts.Contact c)
        {
            if (f2 != senderFixture)
            {
                if (f2 == SolitudeScreen.ship.Player.PlayerFixture)
                {
                    SolitudeScreen.ship.Player.oxygen -= Settings.BulletDamage;
                }
                world.RemoveBody(body);
                SolitudeScreen.ship.contents.Remove(this);
            }
            return true;
        }
    }
}
