﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Rendering;
using Project290.Screens.Title;
using Project290.Menus.MenuDelegates;

namespace Project290.GameElements
{
    /// <summary>
    /// Use this for loading anything and everything. This is its own class for organization and nothing more.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// Loads all the shared and start/title/pause screen stuff.
        /// </summary>
        public static void LoadShared()
        {
            // Box art Textures
            TextureStatic.Load("BoxArtGame1", @"Shared\BoxArt\BoxArtGame1");
            TextureStatic.Load("BoxArtGame2", @"Shared\BoxArt\BoxArtGame2");
            TextureStatic.Load("BoxArtGame3", @"Shared\BoxArt\BoxArtGame3");
            TextureStatic.Load("BoxArtGame4", @"Shared\BoxArt\BoxArtGame4");
            TextureStatic.Load("BoxArtGame5", @"Shared\BoxArt\BoxArtGame5");
            /////////////////////////////////////////////////////
            TextureStatic.Load("BoxArtSolitude", @"Shared\BoxArt\BoxArtSolitude");
            /////////////////////////////////////////////////////
            TextureStatic.Load("BoxArtHolder", @"Shared\BoxArt\BoxArtHolder");
            
            // Other Shared Textures
            TextureStatic.Load("TitleNameBorder", @"Shared\Art\titleNameBorder");
            TextureStatic.Load("Blank", @"Shared\Art\Blank");
            TextureStatic.Load("tileSafeCheck", @"Shared\Art\tileSafeCheck");
            TextureStatic.Load("particle1", @"DefaultBackground\particle1");
            TextureStatic.Load("checkers", @"DefaultBackground\checkers");
            TextureStatic.Load("colorSwirl", @"DefaultBackground\colorSwirl");
            TextureStatic.Load("sampleInstructions", @"Shared\Art\sampleInstructions");
            /////////////////////////////////////////////////////
            TextureStatic.Load("SolitudeInstructions", @"Shared\Art\SolitudeInstructions");
            /////////////////////////////////////////////////////
            TextureStatic.Load("instructionBorder", @"Shared\Art\instructionBorder");
            TextureStatic.Load("gradient", @"Shared\Art\Gradient");

            // Audio
            GameWorld.audio.LoadSound("boxArtScroll", @"Shared\Sounds\boxArtScroll");
            GameWorld.audio.LoadSound("menuClick", @"Shared\Sounds\menuClick");
            GameWorld.audio.LoadSound("menuGoBack", @"Shared\Sounds\menuGoBack");
            GameWorld.audio.LoadSound("menuScrollDown", @"Shared\Sounds\menuScrollDown");
            GameWorld.audio.LoadSound("menuScrollUp", @"Shared\Sounds\menuScrollUp");
            GameWorld.audio.LoadSound("volumeControlDown", @"Shared\Sounds\volumeControlDown");
            GameWorld.audio.LoadSound("volumeControlUp", @"Shared\Sounds\volumeControlUp");

            // Fonts
            FontStatic.Load("defaultFont", @"Shared\Fonts\defaultFont");
            FontStatic.Load("controllerFont", @"Shared\Fonts\controllerFont");
        }

        /// <summary>
        /// Loads the content of a game. There will be one of these methods per mini game
        /// </summary>
        public static void LoadSolitudeContent()
        {
            //load the walls
            TextureStatic.Load("solitudeWallSmooth", @"Solitude\Art\wall-smooth");
            TextureStatic.Load("solitudeWallHandHold", @"Solitude\Art\wall-handhold");
            TextureStatic.Load("solitudeWallGrip", @"Solitude\Art\wall-grip");
            TextureStatic.Load("solitudeWallMetal", @"Solitude\Art\wall-metal");
            TextureStatic.Load("solitudeWallHot", @"Solitude\Art\wall-hot");
            TextureStatic.Load("solitudeWallCold", @"Solitude\Art\wall-cold");
            TextureStatic.Load("solitudeWallSpike", @"Solitude\Art\wall-spike");
            TextureStatic.Load("solitudeWallDoor", @"Solitude\Art\wall-door");
            TextureStatic.Load("bullet", @"Solitude\Art\bullet");
            TextureStatic.Load("sentinel", @"Solitude\Art\sentinel");

            //load the player
            TextureStatic.Load("solitudePlayer", @"Solitude\Art\player");
            // TODO: load all Textures.

            // TODO: load all Audio.
            GameWorld.audio.LoadSong("eerie1", @"Solitude\Music\Eerie1");
            GameWorld.audio.LoadSong("heartbeat1", @"Solitude\Music\heartbeat1");

            // TODO: load all Fonts, and anything else.
        }

        /// <summary>
        /// Loads the game info.
        /// </summary>
        public static void LoadGameInfo()
        {
            int scoreBoardIndex = 0;

            GameInfoCollection.GameInfos.Add(new GameInfo(
                "Snake Death",
                "BoxArtGame1",
                "Embark on an epic journey across distant\ngalaxies to defeat the evil space overlord.\nFifteen increasingly difficult levels await you,\nand your dexterity, intellect, and bladder will\nbe vigorously challenged.",
                "By Ty Taylor",
                "sampleInstructions",
                scoreBoardIndex,
                new LaunchStupidGameDelegate(scoreBoardIndex++)));

            GameInfoCollection.GameInfos.Add(new GameInfo(
                "Hypercube Arcade",
                "BoxArtGame2",
                "You will not win!",
                "By Ty Taylor and Marc Buchner",
                "sampleInstructions",
                scoreBoardIndex,
                new LaunchStupidGameDelegate(scoreBoardIndex++)));
            
            GameInfoCollection.GameInfos.Add(new GameInfo(
                "Game 3",
                "BoxArtGame3",
                "Ahhh! The fractals are everywhere!",
                "By Ty Taylor and Samuel L. Jackson",
                "sampleInstructions",
                scoreBoardIndex,
                new LaunchStupidGameDelegate(scoreBoardIndex++)));

            GameInfoCollection.GameInfos.Add(new GameInfo(
                "Game 4",
                "BoxArtGame4",
                "The cake is a fib.",
                "By Ty Taylor and Leeroy Jenkins",
                "sampleInstructions",
                scoreBoardIndex,
                new LaunchStupidGameDelegate(scoreBoardIndex++)));

            GameInfoCollection.GameInfos.Add(new GameInfo(
                "Game 5",
                "BoxArtGame5",
                "The worst game ever made.",
                "By Ty Taylor, M. C. Escher, and some other guy",
                "sampleInstructions",
                scoreBoardIndex,
                new LaunchStupidGameDelegate(scoreBoardIndex++)));

            GameInfoCollection.GameInfos.Add(new GameInfo(
                "Solitude",
                "BoxArtSolitude",
                "The best game ever made.",
                "By Michael Oswalt, Michael Robertson, Steffen Castle, and Paul An",
                "SolitudeInstructions",
                scoreBoardIndex,
                new LaunchSolitudeDelegate(scoreBoardIndex++)));

            // Must be last!
            GameInfoCollection.GameInfos.Add(new GameInfo(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                scoreBoardIndex,
                null));
        }
    }
}
