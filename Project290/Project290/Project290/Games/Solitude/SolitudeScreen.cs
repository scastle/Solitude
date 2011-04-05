using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Screens;
using Project290.Games.Solitude.SolitudeEntities;
using Project290.Games.Solitude.SolitudeObjects;

namespace Project290.Games.Solitude
{
    /// <summary>
    /// Object of the whole game.
    /// contains the ship, player, and objects, and governs interactions
    /// </summary>
    class SolitudeScreen : GameScreen
    {
        Ship ship;
        Player player;
        List<Object> activeObjects;

    }
}
