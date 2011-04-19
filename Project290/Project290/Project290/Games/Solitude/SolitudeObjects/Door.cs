using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Rendering;
using Project290.Games.Solitude;
using Project290.Physics.Dynamics;

namespace Project290.Games.Solitude.SolitudeObjects
{


    public class Door : Wall
    {
        /// <summary>
        /// the side of the room the wall is on and the direction of the next room
        /// </summary>
        public Direction direction;

        string textureString;

        WallType type;

        public Door(Vector2 position, World world, float width, float height, float density, WallType type, Direction d)
            : base(position, world, width, height, density, type)
        {
            this.type = type;
            switch (type)
            {
                case WallType.Smooth:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorSmooth"; break;
                case WallType.HandHold:
                    textureString = /*TextureStatic.Get(*/"solitudeWallHandHold"; break;
                case WallType.Grip:
                    textureString = /*TextureStatic.Get(*/"solitudeWallGrip"; break;
                case WallType.Metal:
                    textureString = /*TextureStatic.Get(*/"solitudeWallMetal"; break;
                case WallType.Hot:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorHot"; break;
                case WallType.Cold:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorCold"; break;
                case WallType.Spike:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorSpike"; break;
            }
            direction = d;

            Console.WriteLine(textureString);

        }
        public void Enter()
        {
            SolitudeScreen.ship.EnterRoom(direction);
        }


        public bool OnCollision(Fixture f1, Fixture f2, Physics.Dynamics.Contacts.Contact c)
        {

            // Check if f2 is player
            if (f2 == SolitudeScreen.ship.Player.PlayerFixture && type != WallType.Smooth)
            {

                if (type == WallType.HandHold) //grab on to wall
                {
                    SolitudeScreen.ship.Player.body.LinearVelocity = Vector2.Zero;
                    SolitudeScreen.ship.Player.body.AngularVelocity = 0f;
                    SolitudeScreen.ship.Player.onWall = true;
                    SolitudeScreen.ship.Player.standingOn = (Wall)this;
                }
                else if (SolitudeScreen.ship.Player.hasGloves && type == WallType.Grip) // grab if player has gloves
                {
                    SolitudeScreen.ship.Player.body.LinearVelocity = Vector2.Zero;
                    SolitudeScreen.ship.Player.body.AngularVelocity = 0f;
                    SolitudeScreen.ship.Player.onWall = true;
                    SolitudeScreen.ship.Player.standingOn = this;
                }
                else if (SolitudeScreen.ship.Player.hasBoots && type == WallType.Metal) // grab if player has boots
                {
                    SolitudeScreen.ship.Player.body.LinearVelocity = Vector2.Zero;
                    SolitudeScreen.ship.Player.body.AngularVelocity = 0f;
                    SolitudeScreen.ship.Player.onWall = true;
                    SolitudeScreen.ship.Player.standingOn = this;
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
                    }

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
