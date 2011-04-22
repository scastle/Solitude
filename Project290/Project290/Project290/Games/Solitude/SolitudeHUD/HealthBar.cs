using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project290.Physics.Dynamics;
using Project290.Physics.Factories;
using Project290.Rendering;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.Games.Solitude.SolitudeObjects.Enemies;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude;

namespace Project290.Games.Solitude.SolitudeHUD
{
    public class HealthBar
    {
        Texture2D texture;
        int healthPercent;
        Vector2 midPoint;
        Rectangle drawRectangle;
        float width;

        public HealthBar(int currentHealth, int totalHealth)
        {
            //texture = TextureStatic.Get("healthbar");
            //midPoint = new Vector2(300, 200);
            healthPercent = currentHealth / totalHealth;
            drawRectangle = new Rectangle(200, 200, healthPercent, 33);
        }

        public void Update(int pointX, int pointY)
        {
            healthPercent = SolitudeScreen.ship.Player.oxygen / SolitudeScreen.ship.Player.oxygenCap;
        }

        public void Draw()
        {
            //Drawer.Draw(
            //    texture,
            //    new Vector2(200, 200),
            //    drawRectangle,
            //    Color.White,
            //    1f,
            //    midPoint,
            //    1, //draw to the scale the ratio of radius to its texture
            //    SpriteEffects.None,
            //    .8f);
            
            Drawer.DrawLine(new Vector2(300, 112), new Vector2(300 + healthPercent*200, 112), 20, .8f, Color.PaleGreen);
        }
    }
}
