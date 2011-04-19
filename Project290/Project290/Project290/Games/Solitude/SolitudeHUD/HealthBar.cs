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

        public HealthBar(int currentHealth, int totalHealth)
        {
            texture = TextureStatic.Get("healthbar");
            midPoint = new Vector2(300, 200);
            healthPercent = currentHealth / totalHealth;
            drawRectangle = new Rectangle(0, 0, (int)healthPercent, (int)50);
        }

        public void Update()
        {
            healthPercent = SolitudeScreen.ship.Player.oxygen / SolitudeScreen.ship.Player.oxygenCap;
            midPoint.X = 200 + healthPercent;
        }

        public void Draw()
        {
            Drawer.Draw(
                texture,
                new Vector2(200, 200),
                drawRectangle,
                Color.White,
                1f,
                midPoint,
                1, //draw to the scale the ratio of radius to its texture
                SpriteEffects.None,
                .8f);
        }
    }
}
