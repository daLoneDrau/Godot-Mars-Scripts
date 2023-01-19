using Godot;
using System;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Map
{
    public class MarsLocation
    {
        /// <summary>
        /// The location's coordinates.
        /// </summary>
        /// <value></value>
        public Vector2 Location { get; set; }
        /// <summary>
        /// A flag indicating whether a location has been explored.
        /// </summary>
        /// <value></value>
        public bool IsExplored { get; set; }
        /// <summary>
        /// Creates a new MarsLocation.
        /// </summary>
        /// <param name="loc"></param>
        public MarsLocation(Vector2 loc)
        {
            Location = loc;
        }
        /// <summary>
        /// Creates a new MarsLocation.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public MarsLocation(int x, int y)
        {
            Location = new Vector2(x, y);
        }
        /// <summary>
        /// Sets the location's coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Set(int x, int y)
        {
            Location = new Vector2(x, y);
            IsExplored = false;
        }
    }
}