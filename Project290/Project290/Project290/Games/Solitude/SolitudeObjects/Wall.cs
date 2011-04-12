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


        public Wall(Vector2 position, World world, float width, float height, float density, WallType t)
            :base(position, world)
        {
            body.BodyType = BodyType.Static;
            FixtureFactory.CreateRectangle(width, height, density, Settings.zero, body, null);
            
            type = t;

            switch (type){
                case WallType.Smooth:
                    texture = TextureStatic.Get("solitudeWallSmooth");      break;
                case WallType.HandHold:
                    texture = TextureStatic.Get("solitudeWallHandHold");    break;
                case WallType.Grip:
                    texture = TextureStatic.Get("solitudeWallGrip");        break;
                case WallType.Metal:
                    texture = TextureStatic.Get("solitudeWallMetal");       break;
                case WallType.Hot:
                    texture = TextureStatic.Get("solitudeWallHot");         break;
                case WallType.Cold:
                    texture = TextureStatic.Get("solitudeWallCold");        break;
                case WallType.Spike:
                    texture = TextureStatic.Get("solitudeWallSpike");       break;
                case WallType.Door:
                    texture = TextureStatic.Get("solitudeWallDoor");        break;
            }
        }

        public void Update()
        {

        }
        public void Draw()
        {
        }
    }
}
