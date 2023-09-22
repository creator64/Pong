using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Sprites
{
    public abstract class Sprite
    {
        public Rectangle Rect;
        public Texture2D image;
        private float visibility = 1f;
        protected readonly Game1 game = Globals.game;

        public (Sprite sprite, Border border) Collision(List<Sprite> otherSprites = null)
        {
            if (Rect.Bottom >= game.screenRectangle.Bottom) return (null, Border.BottomBorder);
            if (Rect.Right >= game.screenRectangle.Right) return (null, Border.RightBorder);
            if (Rect.Top <= game.screenRectangle.Top) return (null, Border.TopBorder);
            if (Rect.Left <= game.screenRectangle.Left) return (null, Border.LeftBorder);
            
            if (otherSprites == null) return (null, Border.None); 
            foreach (Sprite sprite in otherSprites)
            {
                if (sprite == this) continue;
                Rectangle otherRect = sprite.Rect;
                if (Rect.Left <= otherRect.Right && Rect.Right >= otherRect.Left && Rect.Top <= otherRect.Bottom &&
                    Rect.Bottom >= otherRect.Top)
                {
                    return (sprite, Border.None);
                }
            }

            return (null, Border.None);
        }

        public void Move(double x, double y)
        {
            Rect.X += (int)x;
            Rect.Y += (int)y;
        }

        public void MoveTo(double x, double y)
        {
            Rect.X = (int)x;
            Rect.Y = (int)y;
        }

        public abstract void Update();

        public abstract void Draw();
    }

    public enum Border
    {
        LeftBorder,
        RightBorder,
        TopBorder,
        BottomBorder,
        None
    }
}
