﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Ults;

namespace Pong.Sprites
{
    public class Player : Sprite
    {
        public int points = 0;
        public readonly Side side;
        public float movementSpeed = 12;
        public readonly int wallOffset = 10;
        private const int width = 17, height = 133;
        public bool inverted = false;
        public int coinsCollected;
        public Keys KeyUp, KeyDown, KeyUlt;
        private readonly Ult ult;

        public Player(Side side, Ult ult)
        {
            this.side = side;
            this.ult = ult; ult.player = this; // kind of ugly but well too bad i guess
            Rect = new Rectangle(0, 0, width, height);
            switch (this.side)
            {
                case Side.Left:
                    image = game.Content.Load<Texture2D>("Speler1");
                    Rect.X = wallOffset;
                    KeyUp = Keys.W; KeyDown = Keys.S; KeyUlt = Keys.A;
                    break;
                case Side.Right:
                    image = game.Content.Load<Texture2D>("Speler2");
                    Rect.X = game.screenRectangle.Width - width - wallOffset;
                    KeyUp = Keys.Up; KeyDown = Keys.Down; KeyUlt = Keys.Left;
                    break;
            }
            Rect.Y = game.screenRectangle.Center.Y - height / 2;
        }

        public void collectCoin()
        {
            coinsCollected++;
        }

        public override void Update()
        {
            ult.Update();
            var keyState = Keyboard.GetState();
            var blueVelocity = Vector2.Zero;
            var (sprite, border) = Collision();
            var factor = inverted ? -1 : 1;

            if (keyState.IsKeyDown(KeyUp)) {
                if ((!inverted && border != Border.TopBorder) || (inverted && border != Border.BottomBorder)) blueVelocity.Y = 1;
            }
            if (keyState.IsKeyDown(KeyDown)) {
                if ((!inverted && border != Border.BottomBorder) || (inverted && border != Border.TopBorder)) blueVelocity.Y = -1;
            }

            blueVelocity.Y *= factor;

            if (keyState.IsKeyDown(KeyUlt))
            {
                ult.activateUlt();
            }

            if (blueVelocity != Vector2.Zero) blueVelocity.Normalize(); 
            
            Move(0, -blueVelocity.Y * movementSpeed, checkThrough: false);
        }

        public override void Draw()
        {
            Vector2 pos = new Vector2(1050, 300); Color color = new Color(255, 0, 0, 0.5f);
            if (side == Side.Left)
            {
                pos = new Vector2(250, 300);
                color = new Color(0, 0, 255, 0.5f);
            }
            game._spriteBatch.DrawString(game.Content.Load<SpriteFont>("fonts/scorefont"), (points.ToString()), pos, color);
            game._spriteBatch.Draw(image, Rect, ult.color);
            ult.Draw();
        }
        
        public void OnBallHitSideWall()
        {
            ult.OnBallHitSideWall();
        }

        public void OnTouchBall()
        {
            ult.OnTouchBall();
        }
    }

    public enum Side
    {
        Left,
        Right
    }
}
