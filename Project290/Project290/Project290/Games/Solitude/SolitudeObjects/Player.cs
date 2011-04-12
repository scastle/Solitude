using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Collision.Shapes;
using Project290.Physics.Factories;
using Project290.Physics.Dynamics;
using Project290.Rendering;

namespace Project290.Games.Solitude.SolitudeObjects
{
    class Player : SolitudeObject
    {
        //integers tracking stats and inventory
        int oxygen, oxygenCap;
        int fuel, fuelCap;
        int lives;
        int numBombs;
        int numEMP;

        //Booleans to track upgrade levels
        bool hasGloves;
        bool hasBoots;
        bool hasENVSuit;
        bool hasSpaceSuit;
        bool hasJetpack;

        public Player(Vector2 position, World world)
            : base(position, world)
        {
            body.BodyType = BodyType.Dynamic;
            FixtureFactory.CreateRectangle(128, 256, .5f, Settings.zero, body, null);

            texture = TextureStatic.Get("solitudePlayer");

            oxygen = 100;
            oxygenCap = 100;
            fuel = 100;
            fuelCap = 100;

            hasGloves = false;
            hasBoots = false;
            hasENVSuit = false;
            hasSpaceSuit = false;
            hasJetpack = false;

        }


        public void Update()
        {
            /*
             * 1. get controller input
             * 2. check if on object: yes = jump if A pressed; no: check whether collision with object
             * 3. lay bomb or use EMP: X or Y
             * 4. is jetpack enabled? yes = calc jetpack force
             * 5. check damage from any effects
             * 
             */
        }
        public override void Draw()
        {
            
            Drawer.Draw(
                TextureStatic.Get("solitudePlayer"),
                body.Position,
                new Rectangle(0, 0, 256, 256),
                Color.White,
                body.Rotation,
                TextureStatic.GetOrigin("solitudePlayer"),
                1,
                SpriteEffects.None,
                .8f);



        }
    }
}
