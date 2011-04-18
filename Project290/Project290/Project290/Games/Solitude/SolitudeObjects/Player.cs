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
using Project290.Games.Solitude.SolitudeObjects.Items;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude;

namespace Project290.Games.Solitude.SolitudeObjects
{
    public class Player : SolitudeObject
    {
        //integers tracking stats and inventory

        //health
        public int oxygen, oxygenCap;
        int fuel, fuelCap;
        int lives;
        int numBombs;
        int numEMP;

        /// <summary>
        /// door used to enter room
        /// </summary>
        public  Door enterDoor;
        public Vector2 enterPosition;

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

        /// <summary>
        /// a reference to the wall/door the player is on.
        /// </summary>
        public Wall standingOn;

        public static int width = 33, height = 56;

        public Fixture PlayerFixture;


        public Player(Vector2 position, World world)
            : base(position, world, (float)Player.width, (float)Player.height)
        {
            body.BodyType = BodyType.Dynamic;
            PlayerFixture = FixtureFactory.CreateEllipse(width / 2, height / 2, 32, .2f, body);
            //PlayerFixture = FixtureFactory.CreateRectangle(width, height, .05f, Vector2.Zero, body, null);
            PlayerFixture.Restitution = .8f;
            texture = TextureStatic.Get("solitudePlayer");

            standingOn = null;
            onWall = false;
            jumpCounter = 0;

            oxygen = 1000;
            oxygenCap = 1000;
            fuel = 1000;
            fuelCap = 1000;
            lives = 3;
            numBombs = 10;

            hasGloves = true;
            hasBoots = false;
            hasENVSuit = false;
            hasSpaceSuit = false;
            hasJetpack = true;

        }


        public override void Update()
        {
            /*
             * 1. get controller input
             * 2. check if on object: yes = jump if A pressed; no: check whether collision with object
             * 3. lay bomb or use EMP: X or Y
             * 4. is jetpack enabled? yes = calc jetpack force
             * 5. check damage from any effects
             */

            if (!body.LinearVelocity.Equals(Vector2.Zero))
            {
                jumpCounter = 0;
                onWall = false;
                standingOn = null;
            }

            if (onWall)
            {

                if (standingOn is Door && GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.BButtonFirst))
                {
                    (standingOn as Door).Enter();
                    jumpCounter = 0;
                }
                if (GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.AButton))
                {
                    if (jumpCounter < 125)
                    {
                        jumpCounter++;
                    }else{
                        jumpCounter = 100;
                    }

                }
                else
                {
                    if (jumpCounter > 0)
                    {
                        //jump
                        vector.X =  GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveHorizontal);
                        vector.Y = -1 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveVertical);

                        

                        if (!vector.Equals(Vector2.Zero))
                        {
                            vector.Normalize();
                            body.ApplyLinearImpulse(2000 * vector * jumpCounter);
                            onWall = false;
                            standingOn = null;
                        }
                    }
                    jumpCounter = 0;
                }

            }

            if (hasJetpack && fuel > 0)
            {

                vector.X = GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookHorizontal);
                vector.Y = -1 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookVertical);
                if (!vector.Equals(Vector2.Zero))
                {
                    vector.Normalize();
                    fuel--;
                    body.ApplyForce(vector * Settings.jetPackForceMult);
                }
            }

            if (GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.XButtonFirst))
            {
                if (Settings.maxBombs > SolitudeScreen.ship.bombCount && !onWall && numBombs > 0)
                {
                    vector.X = GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveHorizontal);
                    vector.Y = -1 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveVertical);
                    if (!vector.Equals(Vector2.Zero))
                    {
                        vector.Normalize();
                        numBombs--;
                        body.ApplyForce(vector * Settings.bombForce);


                        Vector2 bombSpeed = new Vector2();
                        bombSpeed = -1 * vector * body.LinearVelocity.Length();
                        Vector2 temp = new Vector2();
                        temp = body.LinearVelocity;
                        temp.Normalize();



                        Bomb b = new Bomb(body.Position - 32 * temp, SolitudeScreen.ship.PhysicalWorld, bombSpeed);
                        SolitudeScreen.ship.contents.Add(b);
                        SolitudeScreen.ship.bombCount++;
                    }
                }
            }

            if (oxygen <= 0)
            {
                if (lives > 0)
                {
                    lives--;
                    standingOn = enterDoor;
                    body.Position = enterPosition;
                    onWall = true;
                    body.LinearVelocity = Vector2.Zero;
                    body.AngularVelocity = 0f;
                    oxygen = oxygenCap;
                }
                else
                {
                    //gameover
                }
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
