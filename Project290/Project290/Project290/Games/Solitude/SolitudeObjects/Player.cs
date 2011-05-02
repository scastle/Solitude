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
using Project290.Games.Solitude.SolitudeHUD;

namespace Project290.Games.Solitude.SolitudeObjects
{
    public class Player : SolitudeObject
    {
        //integers tracking stats and inventory

        //health
        public int oxygen, oxygenCap;
        public int fuel, fuelCap;
        public int lives;
        int numBombs;

        /// <summary>
        /// the door used to enter room, so we can reset if the player dies
        /// </summary>
        public  Door enterDoor;
        public Vector2 enterPosition;

        public HealthBar hpBar;
        public FuelBar fBar;
        //public LivesCount lCnt;

        public bool hasUsedJetPack;
        private int JetPackState;

        public bool onWall;

        public bool hasDiedRecently;

        //Booleans to track upgrade levels
        public bool hasGloves;
        public bool hasBoots;
        public bool hasENVSuit;
        public bool hasSpaceSuit;
        public bool hasJetpack;

        float rotation;

        Vector2 arrowbodyPosition;
        Vector2 arrowheadPosition;

        int jumpCounter;

        /// <summary>
        /// a vector to use for updating and drawing
        /// </summary>
        Vector2 vector = new Vector2();

        /// <summary>
        /// a reference to the wall/door the player is on.
        /// </summary>
        public SolitudeObject standingOn;

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

            enterPosition = new Vector2(900, 830);

            Reset();

            hpBar = new HealthBar(oxygen, oxygenCap);
            fBar = new FuelBar(fuel, fuelCap);
            //lCnt = new LivesCount();

        }
        public void Reset()
        {
            hasGloves = true;
            hasBoots = false;
            hasENVSuit = false;
            hasSpaceSuit = false;
            hasJetpack = true;
            hasUsedJetPack = false;

            JetPackState = 1;

            enterPosition.X = 900;
            enterPosition.Y = 830;

            oxygen = 1000;
            oxygenCap = 1000;
            fuel = 10000;
            fuelCap = 10000;
            lives = 0;
            numBombs = 10;


            hasDiedRecently = false;

            standingOn = null;
            onWall = true;
            jumpCounter = 0;
        }

        public override void Update()
        {
            //update health and fuel bars
            hpBar.Update();
            fBar.Update();

            //if moving, make sure not on wall
            if (!body.LinearVelocity.Equals(Vector2.Zero))
            {
                jumpCounter = 0;
                onWall = false;
                standingOn = null;
            }
            if (onWall)
            {
                //get input
                vector.X =  GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveHorizontal);
                vector.Y = -1 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveVertical);

                //if B pressed on door, enter next room
                if (standingOn is Door && GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.BButtonFirst))
                {
                    (standingOn as Door).Enter();
                    jumpCounter = 0;
                }

                //if the A button is held down
                if (GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.AButton))
                {
                    //power up jump
                    if (jumpCounter < 125)
                    {
                        jumpCounter++;
                    }else{
                        jumpCounter = 100;
                    }
                    // Draw arrow
                    vector.Normalize();
                    int arrowDistance = 50;
                    
                    rotation = (float)Math.Atan(vector.Y / vector.X) - (float)Math.PI / 2;
                    if (vector.X >= 0)
                        rotation += (float)Math.PI;

                    arrowbodyPosition = new Vector2(
                        body.Position.X + arrowDistance * vector.X,
                        body.Position.Y + arrowDistance * vector.Y);
                    arrowheadPosition = new Vector2(
                        arrowbodyPosition.X + vector.X * (jumpCounter + 15),
                        arrowbodyPosition.Y + vector.Y * (jumpCounter + 15));
                }
                else //A is not pressed
                {
                    if (jumpCounter > 0)
                    {
                        //jump
                        if (!vector.Equals(Vector2.Zero))
                        {
                            vector.Normalize();
                            body.ApplyLinearImpulse(2000 * vector * (jumpCounter + 25));
                            onWall = false;
                            standingOn = null;
                        }
                    }
                    jumpCounter = 0;
                }

            }

            //update jetpack
            if (hasJetpack && fuel > 0)
            {

                vector.X = GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookHorizontal);
                vector.Y = -1 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.LookVertical);
                if (!vector.Equals(Vector2.Zero))
                {
                    vector.Normalize();
                    fuel--;
                    body.ApplyForce(vector * Settings.jetPackForceMult);
                    hasUsedJetPack = true;
                    if (JetPackState < 100)
                        JetPackState++;
                }
            }

            //set a bomb
            if (GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.XButtonFirst))
            {
                if (Settings.maxBombs > SolitudeScreen.ship.bombCount && !onWall && numBombs > 0)
                {
                    vector.X = GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveHorizontal);
                    vector.Y = -1 * GameElements.GameWorld.controller.ContainsFloat(Inputs.ActionType.MoveVertical);
                    
                    //if the player is holding a direction
                    if (!vector.Equals(Vector2.Zero))
                    {
                        vector.Normalize();
                        //numBombs--;
                        body.ApplyForce(vector * Settings.bombForce);

                        //calculate the bomb's speed
                        Vector2 bombSpeed = new Vector2();
                        bombSpeed = -1 * vector * body.LinearVelocity.Length();

                        Bomb b = new Bomb(body.Position - 50 * vector, SolitudeScreen.ship.PhysicalWorld, bombSpeed);
                        SolitudeScreen.ship.contents.Add(b);
                        SolitudeScreen.ship.bombCount++;
                    }
                }
            }

            if (oxygen <= 0 )
            {
                if (lives > 0 /*&& !hasDiedRecently*/)
                {
                    //die, and place player back at start of room
                    lives--;
                    standingOn = enterDoor;
                    body.Position = enterPosition;
                    onWall = true;
                    body.LinearVelocity = Vector2.Zero;
                    body.AngularVelocity = 0f;
                    oxygen = oxygenCap;
                    hasDiedRecently = true;
                    SolitudeScreen.ship.ResetRoom();
                }
                else
                {
                    SolitudeScreen.ship.screen.GameOver();
                    //SolitudeScreen.ship.endGameFlag = true;
                    //gameover
                }
            }


        }

        Color arrowColor;
        Rectangle arrowbodyRectangle;
        Rectangle arrowheadRectangle = new Rectangle(
                    0,
                    0,
                    21,
                    21);
        Vector2 arrowbodyOrigin = new Vector2(4, 0);
        Vector2 arrowheadOrigin = new Vector2(10, 0);

        public override void Draw()
        {
            hpBar.Draw();
            fBar.Draw();


            if (hasUsedJetPack)
            {
                hasUsedJetPack = false;
                for (int x = 0; x < 2; x++)
                    Drawer.Draw(
                        TextureStatic.Get("jetpackFlame"),
                        body.Position,
                        drawRectangle,
                        Color.White,
                        body.Rotation,
                        drawOrigin,
                        1f,
                        SpriteEffects.None,
                        .7f);
            }
            else
            {
                if(!hasJetpack)
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
                else
                    Drawer.Draw(
                    TextureStatic.Get("solitudePlayerJetpack"),
                    body.Position,//new Vector2(body.Position.X - width / 2, body.Position.Y - height / 2),
                    drawRectangle,
                    Color.White,
                    body.Rotation,
                    drawOrigin,//TextureStatic.GetOrigin("solitudePlayer"),
                    1,
                    SpriteEffects.None,
                    .8f);
            }
            for (int x = 0; x < lives; x++)
            {
                Drawer.Draw(
                    TextureStatic.Get("playerLives"),
                    new Vector2(1700 - x * 30, 137),//new Vector2(body.Position.X - width / 2, body.Position.Y - height / 2),
                    new Rectangle(0, 0, ((int) width) / 2, ((int) height) / 2),
                    Color.White,
                    0,
                    drawOrigin,//TextureStatic.GetOrigin("solitudePlayer"),
                    1,
                    SpriteEffects.None,
                    .9f);
            }
            //draw the arrow
            if (jumpCounter > 0)
            {
                arrowColor = new Color(255, 255 - jumpCounter * 2, 0); 
                arrowbodyRectangle = new Rectangle(
                     0,
                     0,
                     7,
                     jumpCounter);
                Drawer.Draw(
                    TextureStatic.Get("arrow-body"),
                    arrowbodyPosition,
                    arrowbodyRectangle,
                    arrowColor,
                    (float)Math.PI + rotation,
                    arrowbodyOrigin,
                    1,
                    SpriteEffects.None,
                    1f);
                Drawer.Draw(
                    TextureStatic.Get("arrow"),
                    arrowheadPosition,
                    arrowheadRectangle,
                    arrowColor,
                    rotation,
                    arrowheadOrigin,
                    1,
                    SpriteEffects.None,
                    1f);
            }



        }
    }
}
