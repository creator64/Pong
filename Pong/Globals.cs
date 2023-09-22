using Microsoft.Xna.Framework;
using Pong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    internal static class Globals
    {
        public static Color[] PaddleList = { Color.Blue, Color.Red, Color.Black, Color.Orange, Color.Purple };
        public static Game1 game = new Game1();
        
        // converts degrees to radians
        public static double Radians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
