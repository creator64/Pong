using Microsoft.Xna.Framework;
using System;
using Pong.Ults;

namespace Pong
{
    internal static class Globals
    {
        public static Ult[] UltList = { new Smash(), new Teleport(), new FreeMove() };
        public static readonly Game1 game = new Game1();
        
        // converts degrees to radians
        public static double Radians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
