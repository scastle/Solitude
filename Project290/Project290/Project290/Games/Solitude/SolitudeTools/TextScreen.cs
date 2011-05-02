using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.GameElements;
using Microsoft.Xna.Framework;
using Project290.Menus;
using Project290.Clock;
using Project290.Menus.MenuDelegates;
using Project290.Rendering;
using Microsoft.Xna.Framework.Graphics;
using Project290.Inputs;
using Project290.Mathematics;
using Project290.Screens.Shared;
using Project290.Screens;

namespace Project290.Games.Solitude.SolitudeTools
{
    public class TextScreen:Screen //PauseScreen
    {
        Vector2 backgroundLength = new Vector2();
        Vector2 backgroundStart = new Vector2();


        /// <summary>
        /// This is the menu used for the pause screen.
        /// </summary>
        private PauseMenu menu;

        private string[] listOfStrings;

        /// <summary>
        /// Where to write "Paused", relative to the background
        /// </summary>
        private Vector2 textDrawPosition;

        /// <summary>
        /// The center of the word "Paused".
        /// </summary>
        private Vector2 textDrawOrigin;

        /// <summary>
        /// The position of the background.
        /// </summary>
        private tVector2 position;

        private string terminalText;

        private string textLocationHolder;

        /// <summary>
        /// Used for displaying the background.
        /// </summary>
        private HypercubeDisplay background;

        /// <summary>
        /// Lag the draw time by a split second to prevent stuff from being drawn where it shouldn't...
        /// </summary>
        private long drawLagTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseScreen"/> class.
        /// </summary>
        public TextScreen(string text)
            : base()
        {
            //Console.WriteLine("in Textscreen constructor");
            terminalText = text;
            textLocationHolder = text;
            this.textDrawPosition = new Vector2(0, -225);
            this.textDrawOrigin = FontStatic.Get("defaultFont").MeasureString(terminalText) / 2f;
            this.position = new tVector2(1920f / 2f - 2000f, 1080f / 2f);
            this.position.GoTo(1920f / 2f, 1080f / 2f, 0.3f, true);
            this.drawLagTime = DateTime.Now.Ticks + 1000000;
            //this.background = new HypercubeDisplay(
            //    new Rectangle((int)(this.position.Value.X - 584f / 2f) - 3, (int)(this.position.Value.Y - 700f / 2f) - 3, 584 + 6, 700 + 6),
            //    3,
            //    this.random,
            //    0.2f);
            if (terminalText.Length > 23)
            {
                string[] temp = terminalText.Split(' ');
                terminalText = "";
                int count = 1;
                foreach (string word in temp)
                {

                    if (terminalText.Length + word.Length < 28 * count)
                    {
                        terminalText = terminalText + word + " ";
                    }
                    else
                    {
                        count++;
                        terminalText = terminalText + "\n" + word + " ";
                    }
                }
                this.textDrawOrigin = FontStatic.Get("defaultFont").MeasureString(terminalText) / 2f;
                this.textDrawOrigin.Y -= count * 60;
            }
            
        }

        /// <summary>
        /// Quits this instance.
        /// </summary>
        public void Quit()
        {
            if (!this.FadingOut)
            {
                this.FadingOut = true;
                this.position.GoTo(1920f / 2f + 2000f, 1080f / 2f, 0.3f, true);
                GameWorld.audio.PlaySound("menuGoBack");
            }
        }

        /// <summary>
        /// Updates this instance. This makes sure that GameClock is paused,
        /// and it also updates the menu.
        /// </summary>
        public override void Update()
        {
            base.Update();
            
            //Console.WriteLine("Updating TextScreen");
            // Only pause the gameclock if the screen is not fading out.
            if (!this.FadingOut)
            {
                GameClock.Pause();
            }
            else
            {
                GameClock.Unpause();
            }

            this.position.Update();

            // If it is fading out and the pause menu is done moving, then dispose.
            if (this.FadingOut && !this.position.IsTransitioning)
            {
                GameClock.Unpause();
                this.Disposed = true;
            }

            if (GameElements.GameWorld.controller.ContainsBool(Inputs.ActionType.BButtonFirst))
                Quit();
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public override void Draw()
        {
            Drawer.DrawString(
                FontStatic.Get("defaultFont"),
                terminalText,
                this.position.Value + this.textDrawPosition,
                Color.PaleGreen,
                0f,
                this.textDrawOrigin,
                .4f,
                SpriteEffects.None,
                0.99f);
            
            // Draw the frame
            Drawer.Draw(
                TextureStatic.Get("BoxArtHolder"),
                this.position.Value,
                null,
                Color.White,
                0f,
                TextureStatic.GetOrigin("BoxArtHolder"),
                1f,
                SpriteEffects.None,
                0.9f);
            backgroundLength.X = this.position.Value.X + TextureStatic.Get("BoxArtHolder").Width / 2;
            backgroundLength.Y = this.position.Value.Y;
            backgroundStart.X = this.position.Value.X - TextureStatic.Get("BoxArtHolder").Width / 2;
            backgroundStart.Y = this.position.Value.Y;
            Drawer.DrawLine(
                backgroundStart,
                backgroundLength,
                TextureStatic.Get("BoxArtHolder").Height,
                .89f,
                Color.Black);
            
           // this.background.Draw();
        }
    }
}
