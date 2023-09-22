using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Screens;
using Pong.Sprites;
using System.Collections.Generic;

namespace Pong
{
    public class Game1 : Game
    {
        public string StateScreen = "menu";
        public readonly GraphicsDeviceManager Graphics;
        public SpriteBatch _spriteBatch;
        public List<Sprite> SpriteList;
        public Rectangle screenRectangle;
        private Ball BallSprite;
        private Player PlayerLeft;
        private Player PlayerRight;
        MenuScreen menuScreen;
        /*Rectangle blueRectangle;
        Rectangle redRectangle;
        Texture2D oldBal;
        Vector2 bluePos;
        Vector2 blueVelocity;
        Vector2 redPos;
        Vector2 redVelocity;
        Texture2D blauweSpeler;
        Texture2D rodeSpeler;
        public string keyW = "keyW/Upblue";
        public string keyS = "keyS/Downblue";
        public float timepassed;
        public double maxAngle = 45;
        public int bluePoints;
        public int redPoints;
        public float movementSpeed;
        double speed, angle;
        Rectangle balRectangle;*/

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            screenRectangle = new Rectangle(0, 0, 1400, 800);
            
            Graphics.PreferredBackBufferWidth = screenRectangle.Width;
            Graphics.PreferredBackBufferHeight = screenRectangle.Height;
            Graphics.ApplyChanges();
            

            /*balRectangle.Width = 20;
            balRectangle.Height = 20;
            balRectangle.X = screenRectangle.Width / 2 - balRectangle.Width / 2;
            balRectangle.Y = screenRectangle.Height / 2 - balRectangle.Height / 2;
            speed = 10;
            angle = 40;

            blueRectangle.Width = 17;
            blueRectangle.Height = 133;
            blueRectangle.X = 10;
            blueRectangle.Y = (int)bluePos.Y;
            movementSpeed = 10;

            redRectangle.Width = 17;
            redRectangle.Height = 133;
            redRectangle.X = 1370;
            redRectangle.Y = (int)redPos.Y;*/

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            menuScreen = new MenuScreen(Content, Graphics);
            
            BallSprite = new Ball(new Vector2(screenRectangle.Width / 2 - Ball.size / 2, screenRectangle.Height / 2 - Ball.size / 2));
            PlayerLeft = new Player(Side.Left);
            PlayerRight = new Player(Side.Right);
            SpriteList = new List<Sprite>(){PlayerLeft, PlayerRight, BallSprite};
            
            /*oldBal = Content.Load<Texture2D>("bal");
            blauweSpeler = Content.Load<Texture2D>("blauweSpeler");
            rodeSpeler = Content.Load<Texture2D>("rodeSpeler");*/
        }

        public void OnHitSideWall(Border border)
        {
            if (border == Border.LeftBorder | border == Border.RightBorder)
            {
                if (border == Border.LeftBorder)
                    PlayerRight.points++;
                else PlayerLeft.points++;
                
                BallSprite.MoveTo(screenRectangle.Width / 2 - Ball.size / 2, screenRectangle.Height / 2 - Ball.size / 2);
                BallSprite.speed = 10;
            }
        }

        private void updateGame(GameTime gameTime)
        {
            foreach (var sprite in SpriteList)
            {
                sprite.Update();
            }

            /*int xSpeed = (int)(speed * Math.Cos(Globals.Radians(angle)));
            int ySpeed = (int)(speed * Math.Sin(Globals.Radians(angle)));
            balRectangle.X += xSpeed;
            balRectangle.Y += ySpeed;

            if (balRectangle.Bottom >= screenRectangle.Bottom)
            {
                angle = 360 - angle;
            }
            if (balRectangle.Right >= screenRectangle.Right)
            {
                balRectangle.X = screenRectangle.Width / 2 - balRectangle.Width / 2;
                balRectangle.Y = screenRectangle.Height / 2 - balRectangle.Height / 2;
                bluePoints++;
                speed = 10;
            }
            if (balRectangle.Top <= screenRectangle.Top)
            {
                angle = 360 - angle;
            }
            if (balRectangle.Left <= screenRectangle.Left)
            {
                balRectangle.X = screenRectangle.Width / 2 - balRectangle.Width / 2;
                balRectangle.Y = screenRectangle.Height / 2 - balRectangle.Height / 2;
                redPoints++;
                speed = 10;
            }

            if (balRectangle.Right >= redRectangle.Left && balRectangle.Top >= redRectangle.Top && balRectangle.Bottom <= redRectangle.Bottom)
            {
                angle = 180 - (((balRectangle.Center.Y - redRectangle.Center.Y) / (redRectangle.Height * .5)) * maxAngle);
                speed += .3;
            }
            if (balRectangle.Left <= blueRectangle.Right && balRectangle.Top >= blueRectangle.Top && balRectangle.Bottom <= blueRectangle.Bottom)
            {
                angle = (((balRectangle.Center.Y - blueRectangle.Center.Y) / (blueRectangle.Height * .5)) * maxAngle);
                speed += .3;            
            }

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W)) {

                blueVelocity.Y = 1;
                if (blueRectangle.Top <= screenRectangle.Top)
                {
                    blueVelocity.Y = 0;
                }

            }

            if (keyState.IsKeyDown(Keys.S)) {
                blueVelocity.Y = -1;
                if (blueRectangle.Bottom >= screenRectangle.Bottom)
                {
                    blueVelocity.Y = 0;
                }

            }

            if (blueVelocity != Vector2.Zero) 
            { 
                blueVelocity.Normalize(); 
            }


            
            
          

            bluePos.Y -= blueVelocity.Y * movementSpeed;

            blueVelocity = Vector2.Zero;

            blueRectangle.Y = (int)bluePos.Y;



            if (keyState.IsKeyDown(Keys.Up))
            {
                redVelocity.Y = 1;
                if (redRectangle.Top <= screenRectangle.Top)
                {
                    redVelocity.Y = 0;
                }
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                redVelocity.Y = -1;
                if (redRectangle.Bottom >= screenRectangle.Bottom)
                {
                    redVelocity.Y = 0;
                }

            }

            if (redVelocity != Vector2.Zero)
            {
                redVelocity.Normalize();
            }






            redPos.Y -= redVelocity.Y * movementSpeed;

            redVelocity = Vector2.Zero;

            redRectangle.Y = (int)redPos.Y;*/


            base.Update(gameTime);
        }

        private void DrawGame(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            _spriteBatch.Begin();
            
            foreach (var sprite in SpriteList)
            {
                sprite.Draw();
            }
            
            /*_spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/scorefont"), (bluePoints.ToString()), new Vector2(250, 300), new Color(0, 0, 255, 0.5f));
            _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/scorefont"), (redPoints.ToString()), new Vector2(1050, 300), new Color(255, 0, 0, 0.5f));*/

            /*_spriteBatch.Draw(oldBal, balRectangle, Color.White);
            _spriteBatch.Draw(blauweSpeler, blueRectangle , Color.White);
            _spriteBatch.Draw(rodeSpeler, redRectangle, Color.White);*/

            _spriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // this switch statement is the same as an if-else statement
            switch (StateScreen)
            {
                case "menu":
                    menuScreen.Update(gameTime);
                    break;
                default:
                    updateGame(gameTime);
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            switch (StateScreen)
            {
                case "menu":
                    menuScreen.Draw(gameTime);
                    break;
                default:
                    DrawGame(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}