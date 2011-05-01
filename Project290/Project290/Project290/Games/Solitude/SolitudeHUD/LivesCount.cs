using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Dynamics;
using Project290.Physics.Factories;
using Project290.Rendering;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.Games.Solitude.SolitudeObjects.Enemies;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude;
using Project290.GameElements;

namespace Project290.Games.Solitude.SolitudeHUD
{
    public class LivesCount
    {
        /*//SpriteFont font1;
        Texture2D bombTexture;

        int live;
        string toDraw;
        Vector2 livesPosition;

        public LivesCount()
        {
            //font1 = GameWorld.content.Load<SpriteFont>("defaultFont");
            livesPosition = new Vector2(1020, 120);
            live = 3;
            toDraw = "Lives: ";
            bombTexture = TextureStatic.Get("bomb");
        }

        public void Update()
        {

            live = SolitudeScreen.ship.Player.lives;
            toDraw = "Lives: " + live.ToString();
        }

        public void Draw()
        {
            Drawer.Draw(
               TextureStatic.Get("solitudePlayer"),
               livesPosition,//new Vector2(body.Position.X - width / 2, body.Position.Y - height / 2),
               SolitudeScreen.ship.Player.drawRectangle,
               Color.White,
               0,
               drawOrigin,//TextureStatic.GetOrigin("solitudePlayer"),
               1,
               SpriteEffects.None,
               .8f);
            //Drawer.DrawString(FontStatic.Get("defaultFont"), toDraw, new Vector2(1000, 120), Color.Black, 1f, new Vector2(1020, 120), 0f, SpriteEffects.None, .9f);
            //Drawer.DrawLine(new Vector2(1000, 120), new Vector2 (1020, 120), 20, .9f, Color.Black);
        }*/
=======

namespace Project290.Games.Solitude.SolitudeHUD
{
    class LivesCount
    {
>>>>>>> b2b00a22c5d51daefddfb213ac37b0ba2768ec0b
    }
}
