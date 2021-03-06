﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Games.Solitude.SolitudeTools;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.Games.Solitude.SolitudeObjects.Items;
using Project290.Physics.Collision;
using Project290.Physics.Dynamics;
using Microsoft.Xna.Framework;
using Project290.Audio;
using Project290.GameElements;

using System.Xml;
using Microsoft.Xna.Framework.Content;


namespace Project290.Games.Solitude.SolitudeEntities
{
    /// <summary>
    /// Stores information about the entire world!!! It controls EVERYTHING
    /// and determines when to enter/exit rooms and handles their objects' transitions
    /// into and out of the physicsworld
    /// </summary>
    public class Ship
    {

        Vector2 utilVector = new Vector2();
        /// <summary>
        /// time of last enemy's death to boost score multiplier.
        /// </summary>
        public DateTime lastEnemyDied;

        DateTime songTimer;

        public SolitudeScreen screen;

        static string[] songs = {"eerie1", "heartbeat1", "bad", "solitudePiano"};
        static int[] songLengths = { 66, 50, 158, 75 };
        int songIndex;

        /// <summary>
        /// a class that contains info about how to construct a room for loading from files.
        /// an instance represents one object in a room
        /// </summary>
        public class ObjectListItem
        {
            /// <summary>
            /// class name of the object to be created
            /// </summary>
            public string type;
 
            public Vector2 position;
            public Vector2 dimensions;

            /// <summary>
            /// Additional information that should be used to build certain types of objects
            /// </summary>
            public List<string> moreInfo;
        }

        DateTime playUntil;

        /// <summary>
        /// the player object.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The world for all physical objects to interact
        /// </summary>
        public World PhysicalWorld;


        /// <summary>
        /// distinguishes between normal rooms, rooms with terminals, unfound terminals, and boss fights, etc.
        /// </summary>
        public int[,] roomStatus;

        /// <summary>
        /// the row index of the active room
        /// </summary>
        public static int r;

        /// <summary>
        /// the column index of the active room
        /// </summary>
        public static int c;

        /// <summary>
        /// A list of objects in the room
        /// </summary>
        public List<SolitudeObject> contents;

        public bool bossFight;

        public Terminal term;

        public Random random;

        /// <summary>
        /// smooth walls bound every room
        /// </summary>
        static List<Wall> border = new List<Wall>();

        private List<SolitudeObject> toKill;

        /// <summary>
        /// number of bombs currently active
        /// </summary>
        public int bombCount;

        

        public Ship(SolitudeScreen s)
        {
            screen = s;
            toKill = new List<SolitudeObject>();
            
            roomStatus = new int[Settings.maxShipRows, Settings.maxShipColumns];

            random = new Random();
            //create the world, player, and boundaries
            PhysicalWorld = new World(Vector2.Zero);
            Player = new Player(new Vector2(900f, 830f), PhysicalWorld);
      
            border.Add(new Wall(new Vector2(192, 540), PhysicalWorld, 32, 1080, 1f, WallType.Smooth, Direction.Left));
            border.Add(new Wall(new Vector2(960, 108), PhysicalWorld, 1920, 64, 1f, WallType.Smooth, Direction.Left));
            border.Add(new Wall(new Vector2(1727, 540), PhysicalWorld, 32, 1080, 1f, WallType.Smooth, Direction.Left));
            border.Add(new Wall(new Vector2(960, 971), PhysicalWorld, 1920, 32, 1f, WallType.Smooth, Direction.Left));
            //term = new Terminal(new Vector2(500, 500), PhysicalWorld, "Derp Derp Derp Derp Derp Dfffffffferp Desdfrp Derp Derp Dffffferp Derp Derp Deffffffffffrp Derp Derp Deffrp Derp Derp Derp Derp");
            

            foreach (Wall j in border){
                PhysicalWorld.AddBody(j.body);
            }
            contents = new List<SolitudeObject>();

        }

        public void Reset()
        {
            bossFight = false;
            //get rid of last rooms objects
            foreach (SolitudeObject o in contents)
            {
                PhysicalWorld.RemoveBody(o.body);
            }
          
            PhysicalWorld.RemoveBody(Player.body);
            PhysicalWorld.Step(.1f);
            contents.Clear();
            toKill.Clear();

            
            bombCount = 0;
            //Player.Reset();
            Player = new Player(new Vector2(900, 830), PhysicalWorld);
            r = 5; c = 5;

            //load the first room
            List<ObjectListItem> read;
            read = Serializer.DeserializeFile<List<ObjectListItem>>(GameElements.GameWorld.content.RootDirectory + @"/Solitude/Rooms/room-5-5.xml");
            
            CreateObjects(read);
        }

        /// <summary>
        /// lets ship know to destroy an object (such as bomb or power up)
        /// </summary>
        /// <param name="o"></param>
        public void Destroy(SolitudeObject o)
        {
            toKill.Add(o);
        }

        /// <summary>
        /// Creates a wall based on information from an ObjectListItem o
        /// </summary>
        /// <param name="o">the information on the wall</param>
        private void ItemIsWall(ObjectListItem o)
        {
            
                        WallType t;
                        Direction d = Direction.Left;
                        if (o.dimensions.X != 32) // Up or down
                        {
                            utilVector.X = o.dimensions.Y;
                            utilVector.Y = o.dimensions.X;
                            o.dimensions = utilVector;
                            d = Direction.Up;
                        }
                        string[] s = o.moreInfo.ToArray();
                        switch(s[0]){
                            case "HandHold":
                                t = WallType.HandHold; break;
                            case "Grip":
                                t = WallType.Grip; break;
                            case "Smooth":
                                t = WallType.Smooth; break;
                            case "Metal":
                                t = WallType.Metal; break;
                            case "Fire":
                                t = WallType.Hot; break;
                            case "Cold":
                                t = WallType.Cold; break;
                            case "Spike":
                                t = WallType.Spike; break;
                            default:    t = WallType.Smooth; break;
                        }
                        Wall w = new Wall(o.position, PhysicalWorld, o.dimensions.X, o.dimensions.Y, 1, t, d);
                        contents.Add(w);
        }
        private void ItemIsDoor(ObjectListItem o)
        {
                        WallType t;
                        Direction d;
                        string[] s = o.moreInfo.ToArray();
                        switch(s[0]){
                            case "HandHold":
                                t = WallType.HandHold; break;
                            case "Grip":
                                t = WallType.Grip; break;
                            case "Smooth":
                                t = WallType.Smooth; break;
                            case "Metal":
                                t = WallType.Metal; break;
                            case "Hot":
                                t = WallType.Hot; break;
                            case "Cold":
                                t = WallType.Cold; break;
                            case "Spike":
                                t = WallType.Spike; break;
                            default:    t = WallType.Smooth; break;
                        }
                        switch (s[1])
                        {
                            case "Up":
                                d = Direction.Up; break;
                            case "Down":
                                d = Direction.Down; break;
                            case "Left":
                                d = Direction.Left; break;
                            default:
                                d = Direction.Right; break;
                        }
                        o.dimensions = new Vector2(32, 200);
            Door door = new Door(o.position, PhysicalWorld, o.dimensions.X, o.dimensions.Y, 1, t, d);
            contents.Add(door);
        }
        private void ItemIsTy(ObjectListItem o)
        {
            SolitudeObjects.Enemies.TyTaylor t = new SolitudeObjects.Enemies.TyTaylor(PhysicalWorld, o.position);
            contents.Add(t);
            
            bossFight = true;

            GameWorld.audio.StopSong();
            GameWorld.audio.SongPlay("breakbeat", true);
        }
        private void ItemIsSentinel(ObjectListItem o)
        {
            string[] s = o.moreInfo.ToArray();
            int i;
            switch (s[0])
            {
                case "3": i = 3; break;
                case "4": i = 4; break;
                default: i = 5; break;
            }
            SolitudeObjects.Enemies.Sentinel sn = new SolitudeObjects.Enemies.Sentinel(o.position, o.dimensions, PhysicalWorld, i);
            contents.Add(sn);
        }
        private void ItemIsFighter(ObjectListItem o)
        {
            SolitudeObjects.Enemies.Fighter f = new SolitudeObjects.Enemies.Fighter(o.position, PhysicalWorld);
            contents.Add(f);
        }
        private void ItemIsMauler(ObjectListItem o)
        {
            SolitudeObjects.Enemies.Mauler m = new SolitudeObjects.Enemies.Mauler(o.position, PhysicalWorld);
            contents.Add(m);
        }
        private void ItemIsTerminal(ObjectListItem o)
        {
            string[] s = o.moreInfo.ToArray();
            Terminal t = new Terminal(o.position, PhysicalWorld, s[0]);
            contents.Add(t);
        }

        /// <summary>
        /// reloads the current room when the player dies.
        /// </summary>
        public void ResetRoom()
        {
            foreach(SolitudeObject o in contents)
                toKill.Add(o);

            //read the next room's file
            string s = GameElements.GameWorld.content.RootDirectory + @"/Solitude/Rooms/room-";
            s += r + "-" + c + ".xml";
            List<ObjectListItem> read;
            read = Serializer.DeserializeFile<List<ObjectListItem>>(s);

            //create the objects
            CreateObjects(read);
        }

        /// <summary>
        /// take the list of objects in the room and actually create them
        /// </summary>
        /// <param name="items"></param>
        public void CreateObjects(List<ObjectListItem> items)
        {
            foreach (ObjectListItem o in items)
            {
                switch (o.type)
                {
                    case "Wall":
                        ItemIsWall(o);
                        break;
                    case "Door":
                        ItemIsDoor(o);
                        break;
                    case "Sentinel":
                        ItemIsSentinel(o);
                        break;
                    case "Fighter":
                        ItemIsFighter(o);
                        break;
                    case "Mauler":
                        ItemIsMauler(o);
                        break;
                    case "Terminal":
                        ItemIsTerminal(o);
                        break;
                    case "Ty":
                        ItemIsTy(o);
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        public void EnterRoom(Direction d)
        {
            if (bossFight)
            {            
                bossFight = false;
                GameWorld.audio.StopSong();
                songIndex = random.Next(0, 4);
                playUntil = DateTime.Now + TimeSpan.FromSeconds(songLengths[songIndex]);
                GameWorld.audio.SongPlay(songs[songIndex], false);
            }

            //get rid of last rooms objects
            foreach (SolitudeObject o in contents)
            {
                PhysicalWorld.RemoveBody(o.body);
            }
            contents.Clear();

            //find out where the next room is
            switch (d)
            {
                case Direction.Up:
                    c--; break;
                case Direction.Down:
                    c++; break;

                case Direction.Right:
                    r++; break;
                case Direction.Left:
                    r--; break;
            }
            bossFight = false;

            //read the next room's file
            string s = GameElements.GameWorld.content.RootDirectory + @"/Solitude/Rooms/room-";
            s += r + "-" + c +".xml";
            List<ObjectListItem> read;
            read = Serializer.DeserializeFile<List<ObjectListItem>>(s);

            //create the objects
            CreateObjects(read);

            //place the player
            foreach (SolitudeObject o in contents)
            {
                if (o is Door)
                {
                    Door dr = o as Door;
                    if ((int)dr.direction == -(int)d) //If the door is linking to the room we just left
                    {
                        Player.body.Rotation = 0;
                        switch (dr.direction)
                        {

                            case Direction.Down:
                                utilVector.X = 960;
                                utilVector.Y = 894;
                                Player.body.Position = utilVector;
                                break;
                            case Direction.Up:
                                utilVector.X = 960;
                                utilVector.Y = 186;
                                Player.body.Position = utilVector;
                                break;

                            case Direction.Left:
                                utilVector.X = dr.body.Position.X + dr.drawOrigin.X + Player.drawOrigin.X;
                                utilVector.Y = dr.body.Position.Y; 
                                Player.body.Position = utilVector;
                                break;
                            case Direction.Right:
                                utilVector.X = dr.body.Position.X - dr.drawOrigin.X - Player.drawOrigin.X;
                                utilVector.Y = dr.body.Position.Y;
                                Player.body.Position = utilVector;
                                break;
                        }
                        Player.onWall = true;
                        Player.standingOn = dr;
                        Player.enterDoor = dr;
                        Player.enterPosition = Player.body.Position;
                    }

                }
            }
            bombCount = 0;
        }

        public void Update()
        {
            //remove bodies to be killed from the world
            foreach (SolitudeObject o in toKill)
            {
                if (o is Bomb)
                    bombCount--;
                PhysicalWorld.RemoveBody(o.body);
            }
            //step world (actually removes the bodies)
            PhysicalWorld.Step(0.01f);

            //now it is safe to remove the objects
            foreach (SolitudeObject o in toKill)
            {
                contents.Remove(o);
            }
            toKill.Clear();

            contents.ForEach(i => i.Update());
            Player.Update();

            if (DateTime.Now > playUntil && !bossFight)
            {
                    //Random Song Number
                    songIndex = random.Next(0, 4);
                    playUntil = DateTime.Now + TimeSpan.FromSeconds(songLengths[songIndex]);
                    GameWorld.audio.SongPlay(songs[songIndex], false);
            }


        }
        public void Draw()
        {
            foreach (Wall j in border)
            {
                j.Draw();
            }
            Player.Draw();
            contents.ForEach(i => i.Draw());
        }

    }
}
