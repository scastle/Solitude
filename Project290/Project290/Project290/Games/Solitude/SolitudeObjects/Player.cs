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
    public class Player : SolitudeObject
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

        /// <summary>
        /// a vector to use for updating and drawing
        /// </summary>
        Vector2 vector = new Vector2();

        /// <summary>
        /// rectangle used for drawing
        /// </summary>
        Rectangle rect;

        int width = 32, height = 64;

        public Fixture PlayerFixture;

        public Player(Vector2 position, World world)
            : base(position, world)
        {
            body.BodyType = BodyType.Dynamic;
            PlayerFixture = FixtureFactory.CreateRectangle(width, height, .05f, Settings.zero, body, null);

            texture = TextureStatic.Get("solitudePlayer");

            rect = new Rectangle(0, 0, width, height);

            oxygen = 100;
            oxygenCap = 100;
            fuel = 100;
            fuelCap = 100;

            hasGloves = false;
            hasBoots = false;
            hasENVSuit = false;
            hasSpaceSuit = false;
            hasJetpack = true;

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
            Console.WriteLine(body.LinearVelocity);
            if (hasJetpack)
            {
                vector.X = 10000 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookHorizontal);
                vector.Y = -10000 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookVertical);
                body.ApplyForce(vector);
                
            }
        }
        public override void Draw()
        {
            
            Drawer.Draw(
                TextureStatic.Get("solitudePlayer"),
                body.Position,//new Vector2(body.Position.X - width / 2, body.Position.Y - height / 2),
                rect,
                Color.White,
                body.Rotation,
                TextureStatic.GetOrigin("solitudePlayer"),
                1,
                SpriteEffects.None,
                .8f);



        }
    }
}
