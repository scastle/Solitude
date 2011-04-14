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

        public bool onWall;

        //Booleans to track upgrade levels
        public bool hasGloves;
        public bool hasBoots;
        public bool hasENVSuit;
        public bool hasSpaceSuit;
        public bool hasJetpack;

        int jumpCounter;

        /// <summary>
        /// a vector to use for updating and drawing
        /// </summary>
        Vector2 vector = new Vector2();



        public static int width = 32, height = 64;

        public Fixture PlayerFixture;

        public Player(Vector2 position, World world)
            : base(position, world, (float)Player.width, (float)Player.height)
        {
            body.BodyType = BodyType.Dynamic;
            PlayerFixture = FixtureFactory.CreateRectangle(width, height, .05f, Vector2.Zero, body, null);

            texture = TextureStatic.Get("solitudePlayer");

            onWall = false;
            jumpCounter = 0;

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

            if (!body.LinearVelocity.Equals(Vector2.Zero))
            {
                jumpCounter = 0;
                onWall = false;
            }

            if (onWall)
            {
                if (GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.AButton))
                {
                    if (jumpCounter < 500000)
                        jumpCounter++;
                }
                else
                {
                    if (jumpCounter > 0)
                    {
                        //jump
                        vector.X = jumpCounter * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveHorizontal);
                        vector.Y = -1 * jumpCounter * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveVertical);
                        body.ApplyLinearImpulse(1000 * vector);
                        jumpCounter = 0;
                        onWall = false;
                    }
                }

            }

            //Console.WriteLine(body.LinearVelocity);
            if (hasJetpack)
            {

                vector.X = Settings.jetPackForceMult * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookHorizontal);
                vector.Y = -1 * Settings.jetPackForceMult * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookVertical);
                body.ApplyForce(vector);
                
            }
        }
        public override void Draw()
        {
            
            Drawer.Draw(
                TextureStatic.Get("solitudePlayer"),
                body.Position,//new Vector2(body.Position.X - width / 2, body.Position.Y - height / 2),
                drawRectangle,
                Color.White,
                body.Rotation,
                drawOrigin,//TextureStatic.GetOrigin("solitudePlayer"),
                1,
                SpriteEffects.None,
                .8f);



        }
    }
}
