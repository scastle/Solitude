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
    public class FuelBar
    {
        float fuelPercent;
        Vector2 pointA = new Vector2(600, 112); //start of bar
        Vector2 pointB = new Vector2(600, 112); //end of health
        Vector2 pointC = new Vector2(900, 112); //end of bar

        public FuelBar(int currentFuel, int totalFuel)
        {
            fuelPercent = (float)currentFuel / (float)totalFuel;
        }

        public void Update()
        {
            fuelPercent = (float)SolitudeScreen.ship.Player.fuel / (float)SolitudeScreen.ship.Player.fuelCap;
            pointB.X = 600 + 300 * fuelPercent;
        }

        public void Draw()
        {
            Drawer.DrawLine(pointA, pointB, 20, .9f, Color.Orange);
            Drawer.DrawLine(pointB, pointC, 20, .9f, Color.Black);
        }
    }
}
