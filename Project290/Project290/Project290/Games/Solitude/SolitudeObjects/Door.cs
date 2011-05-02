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

        float rotation;

        WallType type;

        public Door(Vector2 position, World world, float width, float height, float density, WallType type, Direction d)
            : base(position, world, width, height, density, type, d)
        {
            this.type = type;
            textureString = "solitudeWallDoor";
            if (type == WallType.Grip)
            {
                textureString = "solitudeGripDoor";
            }
            direction = d;
            switch (d)
            {
                case Direction.Up: body.Rotation = (float)(3 * Math.PI / 2); break;
                case Direction.Down: body.Rotation = (float)(Math.PI / 2); break;
                case Direction.Left: body.Rotation = (float)Math.PI; break;
                case Direction.Right: body.Rotation = 0; break;
            }
            //body.Rotation = (float)((int)d * (Math.PI / 2));

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
