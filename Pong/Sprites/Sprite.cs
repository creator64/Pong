using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Sprites
{
    public abstract class Sprite
    {
        public Rectangle Rect;
        public Texture2D image;
        private float visibility = 1f;
        protected readonly Pong game = Globals.game;

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

        protected void Move(double x, double y, bool checkThrough = true)
        {
            // TODO: prevent going through objects
            var newPos = new Rectangle(Rect.X + (int)x, Rect.Y + (int)y, Rect.Width, Rect.Height);

            if (!checkThrough)
            {
                Rect.X = newPos.X; Rect.Y = newPos.Y;
                return;
            }
            
            // check if there is a sprite between the two positions
            foreach (var sprite in game.ObjectList)
            {
                if (sprite == this) continue;
                if ((sprite.Rect.Left < Rect.Right && sprite.Rect.Right > newPos.Left) ||
                    (sprite.Rect.Right > Rect.Left && sprite.Rect.Left < newPos.Right))
                {
                    if (Rect.Top <= sprite.Rect.Bottom &&
                    Rect.Bottom >= sprite.Rect.Top)
                    {
                        //Debug.WriteLine("fixing move size" + sprite.image);
                        if (x < 0) Rect.X -= (Rect.Left - sprite.Rect.Right);
                        else Rect.X += (sprite.Rect.Left - Rect.Right);
                        
                        Rect.Y = newPos.Y;
                        return;
                    }
                }
                
                // this is only for x, to lazy to implement y as going through the paddle in y is not really realistic in the game
            }
            Rect.X = newPos.X; Rect.Y = newPos.Y;
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
