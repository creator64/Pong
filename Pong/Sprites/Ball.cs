using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pong.Sprites
{
    public class Ball: Sprite
    {
        public double speed = 10;
        public double angle, maxAngle = 45;
        public static int size = 20;
        public Player lastPlayerTouched;
        public Color mask = Color.White;

        public Ball(Vector2 pos)
        {
            setRandomAngle();
            image = game.Content.Load<Texture2D>("bal");
            Rect = new Rectangle((int)pos.X, (int)pos.Y, size, size);
        }

        private void setRandomAngle()
        {
            angle = new Random().NextDouble() * maxAngle; // a random number between 0 and maxAngle (in this case 45)
        }

        private void ReverseAngle() { angle = 360 - angle; }

        private void handleCollision()
        {
            var (spriteCollidedWith, border) = Collision(game.ObjectList.Concat(game.CoinList).ToList()); // a list of both coins and (the other) sprites
            
            // collision with top and bottom
            if (border == Border.TopBorder | border == Border.BottomBorder) { ReverseAngle(); }
            
            // collision with a paddle of a player
            if (spriteCollidedWith != null && spriteCollidedWith.GetType() == typeof(Player))
            {
                var player = (Player) spriteCollidedWith;
                lastPlayerTouched = player;
                
                if (player.side == Side.Right)
                    angle = 180 - (((Rect.Center.Y - player.Rect.Center.Y) / (player.Rect.Height * .5)) * maxAngle);
                else if (player.side == Side.Left)
                    angle = (((Rect.Center.Y - player.Rect.Center.Y) / (player.Rect.Height * .5)) * maxAngle);
                
                speed += 4 / speed;
                player.OnTouchBall();
            }

            if (spriteCollidedWith != null && spriteCollidedWith.GetType() == typeof(Coin))
            {
                var coin = (Coin) spriteCollidedWith;
                coin.getCollected();
            }
            
            // collision with the side walls
            if (border == Border.LeftBorder | border == Border.RightBorder)
            {
                game.OnBallHitSideWall(border);
            }
        }
        
        public override void Update()
        {
            var x = (speed * Math.Cos(Globals.Radians(angle)));
            var y = (speed * Math.Sin(Globals.Radians(angle)));
            Move(x, y);

            handleCollision();
        }

        public override void Draw()
        {
            game._spriteBatch.Draw(image, Rect, mask);
        }
    }
}

