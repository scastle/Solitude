﻿using System;
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
using Project290.Clock;
using Project290.Games.Solitude.SolitudeTools;

namespace Project290.Games.Solitude.SolitudeObjects.Enemies
{
    class TyTaylor : Enemy
    {
        Vector2 targetPoint = Vector2.Zero;
        World world;
        //public int health;
        public long lastShot;

        public TyTaylor(World w, Vector2 position) :
            base(position, w, TextureStatic.Get("ty").Width, TextureStatic.Get("ty").Height)
        {
            health = Settings.TyHealth;
            world = w;
            lastShot = GameClock.Now;
            body.BodyType = BodyType.Dynamic;
            body.Position = position;
            texture = TextureStatic.Get("ty");
            fixture = FixtureFactory.CreateRectangle(texture.Width, texture.Height, 0.25f, Vector2.Zero, body);
            world.AddBody(body);
            targetPoint = body.Position;
        }

        public override void Update()
        {
            if (health <= 0)
            {
                SolitudeScreen.ship.bossFight = false;
                GameElements.GameWorld.audio.StopSong();
                SolitudeScreen.ship.contents.Add(new Explosion(body.Position, SolitudeScreen.ship.PhysicalWorld, Settings.robotExpRadius, Settings.robotExpPower));
                SolitudeScreen.ship.PhysicalWorld.RemoveBody(body);
                SolitudeScreen.ship.Destroy(this);

                SolitudeScreen.ship.screen.Score += (uint)(10000);
                SolitudeScreen.ship.lastEnemyDied = DateTime.Now;
            }
            //base.Update();
            targetPoint = SolitudeScreen.ship.Player.body.Position;
            Vector2 velocity = new Vector2(targetPoint.X - body.Position.X, targetPoint.Y - body.Position.Y);
            float magnitude = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            velocity.X = Settings.TySpeed * velocity.X / magnitude;
            velocity.Y = Settings.TySpeed * velocity.Y / magnitude;
            if (GameClock.Now - lastShot > 10000000 * (Settings.TyShootRate))
            {
                lastShot = GameClock.Now;
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
