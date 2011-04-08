using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Project290.Games.Solitude.SolitudeObjects
{
    abstract class SolitudeObject
    {
        Texture2D texture;

        public SolitudeObject()
        {
        }

        void Update();

        void Draw()
        {

        }
    }
}
