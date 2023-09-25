using Microsoft.Xna.Framework;
using System;
using Pong.Ults;

namespace Pong
{
    internal static class Globals
    {
        public static readonly Pong game = new();
        //public static readonly Ult[] UltList = { new Smash(), new Teleport(), new FreeMove() };
        
        // converts degrees to radians
        public static double Radians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
