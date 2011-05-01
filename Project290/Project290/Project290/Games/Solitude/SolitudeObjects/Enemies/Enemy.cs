using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Project290.Physics.Dynamics;
using Project290.Games.Solitude.SolitudeTools;

namespace Project290.Games.Solitude.SolitudeObjects.Enemies
{
    class Enemy : SolitudeObject
    {
        public int health;
        public Enemy(Vector2 position, World world, float width, float height)
            : base(position, world, width, height)
        {
        }
        public override void Draw()
        {
        }
        public override void Update()
        {
            if(health <= 0)
            {
                SolitudeScreen.ship.contents.Add(new Explosion(body.Position, SolitudeScreen.ship.PhysicalWorld,Settings.robotExpRadius, Settings.robotExpPower));
                SolitudeScreen.ship.PhysicalWorld.RemoveBody(body);
                SolitudeScreen.ship.Destroy(this);
                SolitudeScreen.ship.screen.Score += 1000;
            }
        }

    }
}
