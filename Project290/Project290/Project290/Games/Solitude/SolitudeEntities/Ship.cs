using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project290.Games.Solitude.SolitudeTools;
using Project290.Games.Solitude.SolitudeObjects;
using Project290.Physics.Collision;
using Project290.Physics.Dynamics;
using Microsoft.Xna.Framework;

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

        /// <summary>
        /// the player object.
        /// </summary>
        public Player Player;

        /// <summary>
        /// The world for all physical objects to interact
        /// </summary>
        public World PhysicalWorld;

        /// <summary>
        /// the row index of the active room
        /// </summary>
        static int r;

        /// <summary>
        /// the column index of the active room
        /// </summary>
        static int c;

        /// <summary>
        /// A list of objects in the room
        /// </summary>
        public List<SolitudeObject> contents;

        /// <summary>
        /// smooth walls bound every room
        /// </summary>
        static List<Wall> border = new List<Wall>();


        public Ship()
        {
            //create the world, player, and boundaries
            PhysicalWorld = new World(Vector2.Zero);
            Player = new Player(new Vector2(1200f, 600f), PhysicalWorld);
            
            border.Add(new Wall(Vector2.Zero, PhysicalWorld, 32, 2080, 1f, WallType.Smooth));
            border.Add(new Wall(Vector2.Zero, PhysicalWorld, 4920, 32, 1f, WallType.Smooth));
            border.Add(new Wall(new Vector2(0, 1048), PhysicalWorld, 4920, 32, 1f, WallType.Smooth));
            border.Add(new Wall(new Vector2(1920, 0), PhysicalWorld, 32, 2080, 1f, WallType.Smooth));

            foreach (Wall j in border){
                PhysicalWorld.AddBody(j.body);
            }


            /* was testing writing
             * 
            List<ObjectListItem> ol = new List<ObjectListItem>();
            
            ObjectListItem ob = new ObjectListItem();
            ob.type = "Wall";
            ob.position = Vector2.Zero;
            ob.dimensions = new Vector2(500, 500);
            ob.moreInfo = new List<string>();
            ob.moreInfo.Add("HandHold");
            ol.Add(ob);

            ob.type = "Door";
            ob.position = new Vector2(1700, 300);
            ob.dimensions = new Vector2(50, 200);
            ob.moreInfo = new List<string>();
            ob.moreInfo.Add("HandHold");
            ob.moreInfo.Add("Left");
            ol.Add(ob);
            */
            //Serializer.SerializeFile(GameElements.GameWorld.content.RootDirectory + @"/Solitude/Rooms/testObject.xml", ol);


            //load the first room
            List<ObjectListItem> read;
            read = Serializer.DeserializeFile<List<ObjectListItem>>(GameElements.GameWorld.content.RootDirectory + @"/Solitude/Rooms/room-0-0.xml");

            contents = new List<SolitudeObject>();
            CreateObjects(read);

        }

        /// <summary>
        /// This will deserialize a room and add it to the ship matrix
        /// </summary>
        /// <param name="row">row of the room</param>
        /// <param name="column">column of the room</param>
        /// <param name="path">filename of the xml file</param>
        public void createRoom(int row, int column, string path)
        {
            List<ObjectListItem> l = Serializer.DeserializeFile<List<ObjectListItem>>(path);
            //create them
        }


        /// <summary>
        /// Creates a wall based on information from an ObjectListItem o
        /// </summary>
        /// <param name="o">the information on the wall</param>
        private void ItemIsWall(ObjectListItem o)
        {
            
                        WallType t;
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
                        Wall w = new Wall(o.position, PhysicalWorld, o.dimensions.X, o.dimensions.Y, 1, t);
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
            Door door = new Door(o.position, PhysicalWorld, o.dimensions.X, o.dimensions.Y, 1, t, d);
            contents.Add(door);
        }

        private void ItemIsSentinel(ObjectListItem o)
        {
            string[] s = o.moreInfo.ToArray();
            int i;
            switch (s[0])
            {
                case "3": i = 3; break;
                default: i = 5; break;
            }
            SolitudeObjects.Enemies.Sentinel sn = new SolitudeObjects.Enemies.Sentinel(o.position, o.dimensions, PhysicalWorld, i);
            contents.Add(sn);
        }

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
                }
            }
        }

        /// <summary>
        /// old version... being updated
        /// </summary>
        /// <param name="d"></param>
        public void EnterRoom(Direction d)
        {
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

            //read the next room's file
            string s = GameElements.GameWorld.content.RootDirectory + @"/Solitude/Rooms/room-";
            s += r + "-" + c + ".xml";
            List<ObjectListItem> read;
            read = Serializer.DeserializeFile<List<ObjectListItem>>(s);

            //create the objects
            CreateObjects(read);
        }




        public void Update()
        {
            PhysicalWorld.Step(0.01f);
            Player.Update();
            
            contents.ForEach(i => i.Update());
            //foreach (SolitudeObject m in contents)
            //    m.Update();
        }
        public void Draw()
        {
            foreach (Wall j in border)
            {
                j.Draw();
            }
            Player.Draw();
            foreach (SolitudeObject m in contents)
                m.Draw();
        }

    }
}
