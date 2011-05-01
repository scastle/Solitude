using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Physics.Dynamics;
using Project290.Screens;
using Project290.Games.Solitude;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Factories;
using Project290.Games.Solitude.SolitudeTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Rendering;
using Project290.GameElements;


namespace Project290.Games.Solitude.SolitudeObjects.Items
{

    public enum PhysicsShape
    {
        Circle,
        Rectangle,
        Ellipse
    }

    /// <summary>
    /// Massless Items include all objects that do not COLLIDE with other objects
    /// Types include powerups, ammo, and upgrades, as well as fire and poison gas
    /// </summary>
    public abstract class MasslessItem : SolitudeObject
    {
        public Vector2 position;

        public MasslessItem(Texture2D t, PhysicsShape shape, Vector2 position, World w)
            : base(position, w, t.Width, t.Height)
        {
            texture = t;
            body.BodyType = BodyType.Static;
            body.Position = position;

            switch (shape)
            {
                case PhysicsShape.Rectangle:
                    fixture = FixtureFactory.CreateRectangle(
                        texture.Width,
                        texture.Height,
                        0.0f,
                        Vector2.Zero,
                        body);
                    break;
            }

            //fixture.CollisionFilter.IgnoreCollisionWith();
            
            fixture.OnCollision += new OnCollisionEventHandler(this.OnCollision);
            w.AddBody(body);

            //drawOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            //drawRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public virtual bool OnCollision(Fixture f1, Fixture f2, Physics.Dynamics.Contacts.Contact c)
        {
            f2.Body.AngularVelocity = 0f;
            f2.Body.LinearVelocity = Vector2.Zero;
            return true;
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

    public class Terminal : MasslessItem
    {
        //Shape s = 
        public bool IsShowing = false;
        bool IsColliding = false;
        DateTime lastHeldDown;
        public string text;

        public Terminal(Vector2 position, World w, string t)
            : base(TextureStatic.Get("terminal"), PhysicsShape.Rectangle, position, w)
        {
            fixture.OnCollision += new OnCollisionEventHandler(OnCollision);
            fixture.OnSeparation += new OnSeparationEventHandler(OnSeparation);
            text = t;
            
        }

        public override bool OnCollision(Fixture f1, Fixture f2, Physics.Dynamics.Contacts.Contact c)
        {
            f2.Body.AngularVelocity = 0f;
            f2.Body.LinearVelocity = Vector2.Zero;
            if (f2 == SolitudeScreen.ship.Player.PlayerFixture)
            {
                IsColliding = true;
            }
            return true;
        }

        public override void Update()
        {
            if (IsColliding && GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.BButton))
            {
                Console.WriteLine("B  button pressed");
                if (DateTime.Now - lastHeldDown > TimeSpan.FromMilliseconds(500))
                {
                    Console.WriteLine("B  button held");
                    if (IsShowing)
                    {
                        Console.WriteLine("TextScreen Showing");
                        GameWorld.screens.Play(new TextScreen(text));
                        //terminalText.Update();
                        //terminalText.Draw();
                    }
                    //else
                    //{
                        
                    //}
                    IsShowing = !IsShowing;
                }
                lastHeldDown = DateTime.Now;
            }
        }

        public void OnSeparation(Fixture f1, Fixture f2)
        {
            IsColliding = false;
        }
    }
}
