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
        float healthPercent;
<<<<<<< HEAD
        Vector2 pointA = new Vector2(300, 120); //start of bar
        Vector2 pointB = new Vector2(300, 120); //end of health
        Vector2 pointC = new Vector2(600, 120); //end of bar
=======
        Vector2 pointA = new Vector2(300, 112); //start of bar
        Vector2 pointB = new Vector2(300, 112); //end of health
        Vector2 pointC = new Vector2(600, 112); //end of bar
>>>>>>> b2b00a22c5d51daefddfb213ac37b0ba2768ec0b

        public HealthBar(int currentHealth, int totalHealth)
        {
            healthPercent = (float)currentHealth / (float)totalHealth;
        }

        public void Update()
        {
            healthPercent = (float)SolitudeScreen.ship.Player.oxygen / (float)SolitudeScreen.ship.Player.oxygenCap;
            pointB.X = 300 + 300 * healthPercent;
        }

        public void Draw()
        {      
            Drawer.DrawLine(pointA, pointB, 20, .9f, Color.PaleGreen);
            Drawer.DrawLine(pointB, pointC, 20, .9f, Color.Black);
        }
    }
}
