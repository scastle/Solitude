using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RecordRobot.Screens;

namespace RecordRobot
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Gets the stack of screens used in the game.
        /// </summary>
        /// <value>The screens.</value>
        public static ScreenContainer screens { get; private set; }

        public static bool GamePaused;

        public static SpriteFont Font;
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static TextReader tr;
        public static Random rand;
        public static DateTime Time;
        public static bool ExitStatus;
        public GameScreen MainGame { get; private set; }

        public static Level CurrentLevel;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 480;
            Content.RootDirectory = "Content";
            CurrentLevel = new Level();
            rand = new Random();
            ExitStatus = false;
            GamePaused = false;
            MainGame = new GameScreen();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Time = DateTime.Now;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures.Robot = this.Content.Load<Texture2D>("Images\\robot-normal");
            Textures.RobotLeft = this.Content.Load<Texture2D>("Images\\robot-left");
            Textures.RobotRight = this.Content.Load<Texture2D>("Images\\robot-right");
            Textures.RobotInvincible = this.Content.Load<Texture2D>("Images\\robot-invincible");
            Textures.RobotFlashingLeft = this.Content.Load<Texture2D>("Images\\robot-left-dead");
            Textures.RobotFlashingRight = this.Content.Load<Texture2D>("Images\\robot-right-dead");
            Textures.RobotDead = this.Content.Load<Texture2D>("Images\\robot-dead");
            Textures.mazepath = this.Content.Load<Texture2D>("Images\\maze-path");
            Textures.mazewall = this.Content.Load<Texture2D>("Images\\maze-wall");
            Textures.RedRecord = this.Content.Load<Texture2D>("Images\\record-red");
            Textures.OrangeRecord = this.Content.Load<Texture2D>("Images\\record-orange");
            Textures.GreyRecord = this.Content.Load<Texture2D>("Images\\record-grey");
            Textures.BlueRecord = this.Content.Load<Texture2D>("Images\\record-blue");
            Textures.GreenRecord = this.Content.Load<Texture2D>("Images\\record-green");
            Textures.VioletRecord = this.Content.Load<Texture2D>("Images\\record-violet");
            Textures.YellowRecord = this.Content.Load<Texture2D>("Images\\record-yellow");
            Textures.RobotWin = this.Content.Load<Texture2D>("Images\\robot-win");
            Textures.InfobarBackground = new Texture2D(GraphicsDevice, InfoBar.Width, InfoBar.Height);

            Font = Content.Load<SpriteFont>("Font1");

            Color[] colors = new Color[InfoBar.Width * InfoBar.Height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.Black;
            }
            Textures.InfobarBackground.SetData(colors);

            //load the maze file and instantiate the grid
            TextReader read = new StreamReader(Content.RootDirectory + "\\TextFiles\\testmaze.txt");
            string input = null;
            int r = 0;
            int l = 0;
            int [,,] map = new int [5,17,21];
            while ((input = read.ReadLine()) != null)
            {
                if (input.Substring(0, 1).Equals("=")) //start a new level
                {
                    l++;
                    r = 0;
                }
                else
                {
                    for (int c = 0; c < input.Length; c++)
                    {
                        if (input.Substring(c, 1).Equals("1"))
                        {
                            map[l, r, c] = 1;
                        }
                        else
                        {
                            map[l, r, c] = 0;
                        }
                    }
                    r++;
                }
                
                
            }
            Maze.LoadMaze(map);

            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public static void ExitGame()
        {
            ExitStatus = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (ExitStatus)
                this.Exit();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MovingObjects.MovingObjectManager.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            MainGame.Draw();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
