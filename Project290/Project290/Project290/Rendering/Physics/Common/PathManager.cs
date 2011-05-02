﻿//////__     __               _                 _     _               _   
//////\ \   / /              | |               | |   | |             | |  
////// \ \_/ /__  _   _   ___| |__   ___  _   _| | __| |  _ __   ___ | |_ 
//////  \   / _ \| | | | / __| '_ \ / _ \| | | | |/ _` | | '_ \ / _ \| __|
//////   | | (_) | |_| | \__ \ | | | (_) | |_| | | (_| | | | | | (_) | |_ 
//////   |_|\___/ \__,_| |___/_| |_|\___/ \__,_|_|\__,_| |_| |_|\___/ \__|
//////      _                              _   _     _     _ 
//////     | |                            | | | |   (_)   | |
//////  ___| |__   __ _ _ __   __ _  ___  | |_| |__  _ ___| |
////// / __| '_ \ / _` | '_ \ / _` |/ _ \ | __| '_ \| / __| |
//////| (__| | | | (_| | | | | (_| |  __/ | |_| | | | \__ \_|
////// \___|_| |_|\__,_|_| |_|\__, |\___|  \__|_| |_|_|___(_)
//////                         __/ |                         
//////                        |___/                          

using System;
using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;

namespace FarseerPhysics.Factories
{
    /// <summary>
    /// An easy to use manager for creating paths.
    /// </summary>
    public static class PathManager
    {
        public enum LinkType
        {
            Revolute,
            Slider
        }

        // Contributed by Matthew Bettcher

        /// <summary>
        /// Convert a path into a set of edges and attaches them to the specified body.
        /// Note: use only for static edges.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="body">The body.</param>
        /// <param name="subdivisions">The subdivisions.</param>
        public static void ConvertPathToEdges(Path path, Body body, int subdivisions)
        {
            List<Vector2> verts = path.GetVertices(subdivisions);

            for (int i = 1; i < verts.Count; i++)
            {
                body.CreateFixture(new PolygonShape(PolygonTools.CreateEdge(verts[i], verts[i - 1])), 0);
            }

            if (path.Closed)
            {
                body.CreateFixture(new PolygonShape(PolygonTools.CreateEdge(verts[verts.Count - 1], verts[0])), 0);
            }
        }

        /// <summary>
        /// Convert a closed path into a polygon.
        /// Convex decomposition is automatically performed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="body">The body.</param>
        /// <param name="density">The density.</param>
        /// <param name="subdivisions">The subdivisions.</param>
        public static void ConvertPathToPolygon(Path path, Body body, float density, int subdivisions)
        {
            if (!path.Closed)
                throw new Exception("The path must be closed to convert to a polygon.");

            List<Vector2> verts = path.GetVertices(subdivisions);

            List<Vertices> decomposedVerts = EarclipDecomposer.ConvexPartition(new Vertices(verts));
            // List<Vertices> decomposedVerts = BayazitDecomposer.ConvexPartition(new Vertices(verts));

            foreach (var item in decomposedVerts)
            {
                body.CreateFixture(new PolygonShape(item), density);
            }
        }

        /// <summary>
        /// Duplicates the given Body along the given path for approximatly the given copies.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="path">The path.</param>
        /// <param name="shapes">The shapes.</param>
        /// <param name="type">The type.</param>
        /// <param name="copies">The copies.</param>
        /// <returns></returns>
        public static List<Body> EvenlyDistibuteShapesAlongPath(World world, Path path, IEnumerable<Shape> shapes,
                                                                BodyType type, int copies, float density)
        {
            List<Vector3> centers = path.SubdivideEvenly(copies);
            List<Body> bodyList = new List<Body>();

            Body b;

            for (int i = 0; i < centers.Count; i++)
            {
                b = world.CreateBody();
                // copy the type from original body
                b.BodyType = type;
                b.Position = new Vector2(centers[i].X, centers[i].Y);
                b.Rotation = centers[i].Z;

                foreach (Shape shape in shapes)
                {
                    b.CreateFixture(shape, density);
                }

                bodyList.Add(b);
            }

            return bodyList;
        }

        /// <summary>
        /// Duplicates the given Body along the given path for approximatly the given copies.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="path">The path.</param>
        /// <param name="shape">The shape.</param>
        /// <param name="type">The type.</param>
        /// <param name="copies">The copies.</param>
        /// <param name="density">The density.</param>
        /// <returns></returns>
        public static List<Body> EvenlyDistibuteShapesAlongPath(World world, Path path, Shape shape, BodyType type,
                                                                int copies, float density)
        {
            List<Shape> shapes = new List<Shape>(1);
            shapes.Add(shape);

            return EvenlyDistibuteShapesAlongPath(world, path, shapes, type, copies, density);
        }

        /// <summary>
        /// Moves the body on the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="body">The body.</param>
        /// <param name="time">The time.</param>
        /// <param name="strength">The strength.</param>
        /// <param name="timeStep">The time step.</param>
        public static void MoveBodyOnPath(Path path, Body body, float time, float strength, float timeStep)
        {
            Vector2 destination = path.GetPosition(time);
            Vector2 positionDelta = body.Position - destination;
            Vector2 velocity = (positionDelta / timeStep) * strength;

            body.LinearVelocity = -velocity;
        }

        /// <summary>
        /// Attaches the bodies with revolute joints.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="bodies">The bodies.</param>
        /// <param name="localAnchorA">The local anchor A.</param>
        /// <param name="localAnchorB">The local anchor B.</param>
        /// <param name="connectFirstAndLast">if set to <c>true</c> [connect first and last].</param>
        /// <param name="collideConnected">if set to <c>true</c> [collide connected].</param>
        public static void AttachBodiesWithRevoluteJoint(World world, List<Body> bodies, Vector2 localAnchorA,
                                                         Vector2 localAnchorB, bool connectFirstAndLast,
                                                         bool collideConnected)
        {
            for (int i = 1; i < bodies.Count; i++)
            {
                RevoluteJoint joint = new RevoluteJoint(bodies[i], bodies[i - 1], localAnchorA, localAnchorB);
                joint.CollideConnected = collideConnected;
                world.AddJoint(joint);
            }

            if (connectFirstAndLast)
            {
                RevoluteJoint lastjoint = new RevoluteJoint(bodies[0], bodies[bodies.Count - 1], localAnchorA,
                                                            localAnchorB);
                lastjoint.CollideConnected = collideConnected;
                world.AddJoint(lastjoint);
            }
        }

        /// <summary>
        /// Attaches the bodies with revolute joints.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="bodies">The bodies.</param>
        /// <param name="localAnchorA">The local anchor A.</param>
        /// <param name="localAnchorB">The local anchor B.</param>
        /// <param name="connectFirstAndLast">if set to <c>true</c> [connect first and last].</param>
        /// <param name="collideConnected">if set to <c>true</c> [collide connected].</param>
        public static void AttachBodiesWithSliderJoint(World world, List<Body> bodies, Vector2 localAnchorA,
                                                         Vector2 localAnchorB, bool connectFirstAndLast,
                                                         bool collideConnected, float minLength, float maxLength)
        {
            for (int i = 1; i < bodies.Count; i++)
            {
                SliderJoint joint = new SliderJoint(bodies[i], bodies[i - 1], localAnchorA, localAnchorB, minLength, maxLength);
                joint.CollideConnected = collideConnected;
                world.AddJoint(joint);
            }

            if (connectFirstAndLast)
            {
                SliderJoint lastjoint = new SliderJoint(bodies[0], bodies[bodies.Count - 1], localAnchorA, localAnchorB, minLength, maxLength);
                lastjoint.CollideConnected = collideConnected;
                world.AddJoint(lastjoint);
            }
        }
    }
}