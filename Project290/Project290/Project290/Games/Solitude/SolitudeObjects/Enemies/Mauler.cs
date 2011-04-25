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
    class Mauler : Enemy
    {
        World world;
        bool CanSeePlayer = false;
        Vector2 targetPoint;
        Fixture fixtureInTheWay;

        public Mauler(Vector2 position, World w)
            : base(position, w, TextureStatic.Get("solitudeMauler").Width, TextureStatic.Get("solitudeMauler").Height)
        {
            health = 200;
            world = w;
            body.BodyType = BodyType.Dynamic;
            body.Position = position;
            texture = TextureStatic.Get("solitudeMauler");
            fixture = FixtureFactory.CreateCircle(texture.Width/2, 0.25f, body, Vector2.Zero);
            world.AddBody(body);
            targetPoint = body.Position;
        }

        public override void Update()
        {
            base.Update();
            CheckCanSeePlayer();
            Charge();
        }

        private void Charge()
        {
            if (CanSeePlayer)
            {
                targetPoint = SolitudeScreen.ship.Player.body.Position;
            }
            else
            {
                if (targetPoint == body.Position)
                {
                    if (fixtureInTheWay.Body.Position != Vector2.Zero)
                        targetPoint = new Vector2(fixtureInTheWay.Body.Position.X, fixtureInTheWay.Body.Position.Y - fixtureInTheWay.Shape.Radius + 20);
                }
            }
            Vector2 n = new Vector2(targetPoint.X - body.Position.X, targetPoint.Y - body.Position.Y);
            n.Normalize();
            body.ApplyForce( n * Settings.MaulerForce);
        }

        public void CheckCanSeePlayer(Vector2 point)
        {
            CanSeePlayer = true;
            RayCastCallback callback = new RayCastCallback(RayCastCallback);
            world.RayCast(callback, point, SolitudeScreen.ship.Player.body.Position);
        }

        public void CheckCanSeePlayer()
        {
            CheckCanSeePlayer(body.Position);
        }

        private float RayCastCallback(Fixture f, Vector2 point1, Vector2 point2, float fl)
        {
            if (f != SolitudeScreen.ship.Player.PlayerFixture && f.Body.UserData as string != "bullet")
            {
                fixtureInTheWay = f;
                CanSeePlayer = false;
                return 0;
            }
            return 1;
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
