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

namespace Project290.Games.Solitude.SolitudeObjects
{
    public enum WallType
    {
        Smooth, HandHold, Grip, Metal, Hot, Cold, Spike, Door
    }


    class Wall : SolitudeObject
    {
        /// <summary>
        /// The type of wall for purposes of what happens to the player
        /// </summary>
        WallType type;

        /// <summary>
        /// used in oncollision method
        /// </summary>
        Vector2 distance = new Vector2();

        string textureString;

        Fixture fixture;
        float width, height;

        public Wall(Vector2 position, World world, float width, float height, float density, WallType t)
            :base(position, world, width, height)
        {
            body.BodyType = BodyType.Static;
            world.AddBody(body);
            fixture = FixtureFactory.CreateRectangle(width, height, density, Vector2.Zero, body, null);
            fixture.OnCollision += new OnCollisionEventHandler(OnCollision);
            type = t;
            this.width = width;
            this.height = height;

            
            drawRectangle = new Rectangle(0, 0, (int)width, (int)height);
            switch (type){
                case WallType.Smooth:
                    textureString = /*TextureStatic.Get(*/"solitudeWallSmooth";      break;
                case WallType.HandHold:
                    textureString = /*TextureStatic.Get(*/"solitudeWallHandHold";    break;
                case WallType.Grip:
                    textureString = /*TextureStatic.Get(*/"solitudeWallGrip";        break;
                case WallType.Metal:
                    textureString = /*TextureStatic.Get(*/"solitudeWallMetal";       break;
                case WallType.Hot:
                    textureString = /*TextureStatic.Get(*/"solitudeWallHot";         break;
                case WallType.Cold:
                    textureString = /*TextureStatic.Get(*/"solitudeWallCold";        break;
                case WallType.Spike:
                    textureString = /*TextureStatic.Get(*/"solitudeWallSpike";       break;
                case WallType.Door:
                    textureString = /*TextureStatic.Get(*/"solitudeWallDoor";        break;
            }
        }

        public void Update()
        {

        }

        public bool OnCollision(Fixture f1, Fixture f2, Physics.Dynamics.Contacts.Contact c)
        {
            Console.WriteLine("COLLISION");
            // Check if f2 is player
            if (f2 == SolitudeScreen.ship.Player.PlayerFixture && type != WallType.Smooth)
            {

                if (type == WallType.HandHold) //grab on to wall
                {
                    SolitudeScreen.ship.Player.body.LinearVelocity = Vector2.Zero;
                    SolitudeScreen.ship.Player.body.AngularVelocity = 0f;
                }
                else if (SolitudeScreen.ship.Player.hasGloves && type == WallType.Grip) // grab if player has gloves
                {
                    SolitudeScreen.ship.Player.body.LinearVelocity = Vector2.Zero;
                    SolitudeScreen.ship.Player.body.AngularVelocity = 0f;
                }
                else if (SolitudeScreen.ship.Player.hasBoots && type == WallType.Metal) // grab if player has boots
                {
                    SolitudeScreen.ship.Player.body.LinearVelocity = Vector2.Zero;
                    SolitudeScreen.ship.Player.body.AngularVelocity = 0f;
                }
                else //otherwise
                {
                    switch (type) //switch because player info is irrelevant
                    {
                        case WallType.Cold:
                            break;
                        case WallType.Hot:
                            break;
                        case WallType.Spike:
                            break;
                        case WallType.Door:
                            break;
                    }

                }

                distance.X = Math.Abs(f2.Body.Position.X - f1.Body.Position.X);
                distance.Y = Math.Abs(f2.Body.Position.Y - f1.Body.Position.Y);

                switch (type)
                {
                    


                    case WallType.Smooth: 
                        // ball is above or below wall
                        if (distance.X > distance.Y)
                        {
                            //f1.Body.LinearVelocity = new Vector2(f1.Body.LinearVelocity.X, f1.Body.LinearVelocity.Y * -1);
                            f1.Body.LinearVelocity = Vector2.Zero;
                        }
                        // ball is to left or right of wall
                        else
                        {
                        }
                        break;

                }
            }
            // Check if f2 is other item (ie block)
            return true;
        }

        public override void Draw()
        {
            Drawer.Draw(
                TextureStatic.Get(textureString),
                body.Position,//new Vector2(body.Position.X - width / 2, body.Position.Y - height / 2),
                drawRectangle,
                Color.White,
                body.Rotation,
                drawOrigin,//TextureStatic.GetOrigin("solitudeWallHandHold"),
                1,
                SpriteEffects.None,
                .8f);

        }
    }
}
