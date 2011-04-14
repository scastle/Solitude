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
        Direction direction;

        public Door(Vector2 position, World world, float width, float height, float density, WallType type, Direction d)
            :base(position, world, width, height, density, type)
        {
            switch (type)
            {
                case WallType.Smooth:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorSmooth"; break;
                case WallType.HandHold:
                    textureString = /*TextureStatic.Get(*/"solitudeWallHandHold"; break;
                case WallType.Grip:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorGrip"; break;
                case WallType.Metal:
                    textureString = /*TextureStatic.Get(*/"solitudeDoorMetal"; break;
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
