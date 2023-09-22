using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Sprites
{
    internal class Player : Sprite
    {
        public int points = 0;
        public readonly Side side;
        public float movementSpeed = 10;
        public int wallOffset = 10;
        private const int width = 17, height = 133;
        private readonly Keys KeyUp, KeyDown, KeyUlt;
        
        
        public Player(Side side)
        {
            this.side = side;
            Rect = new Rectangle(0, 0, width, height);
            switch (this.side) // a switch statement is the same as an if statement
            {
                case Side.Left:
                    image = game.Content.Load<Texture2D>("blauweSpeler");
                    Rect.X = wallOffset;
                    KeyUp = Keys.W; KeyDown = Keys.S;
                    break;
                case Side.Right:
                    image = game.Content.Load<Texture2D>("rodeSpeler");
                    Rect.X = game.screenRectangle.Width - width - wallOffset;
                    KeyUp = Keys.Up; KeyDown = Keys.Down;
                    break;
            }
            Rect.Y = game.screenRectangle.Center.Y - height / 2;
        }

        public override void Update()
        {
            var keyState = Keyboard.GetState();
            var blueVelocity = Vector2.Zero;
            var (sprite, border) = Collision();

            if (keyState.IsKeyDown(KeyUp)) {
                if (border != Border.TopBorder) blueVelocity.Y = 1;
            }
            if (keyState.IsKeyDown(KeyDown)) {
                if (border != Border.BottomBorder) blueVelocity.Y = -1;
            }

            if (blueVelocity != Vector2.Zero) blueVelocity.Normalize(); 
            
            Move(0, -blueVelocity.Y * movementSpeed);
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
            game._spriteBatch.Draw(image, Rect, Color.White);
        }
        
    }

    internal enum Side
    {
        Left,
        Right
    }
}
