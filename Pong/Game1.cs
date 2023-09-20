using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Rectangle screenRectangle;
        Rectangle blueRectangle;
        Rectangle redRectangle;
        Texture2D bal;
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

        Rectangle balRectangle;
        double speed, angle;
       

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()

        {
            screenRectangle = new Rectangle(0, 0, 1400, 800);

            balRectangle.Width = 20;
            balRectangle.Height = 20;
            balRectangle.X = screenRectangle.Width / 2 - balRectangle.Width / 2;
            balRectangle.Y = screenRectangle.Height / 2 - balRectangle.Height / 2;
            speed = 10;
            angle = 40;

            _graphics.PreferredBackBufferWidth = screenRectangle.Width;
            _graphics.PreferredBackBufferHeight = screenRectangle.Height;
            _graphics.ApplyChanges();

            blueRectangle.Width = 17;
            blueRectangle.Height = 133;
            blueRectangle.X = 10;
            blueRectangle.Y = (int)bluePos.Y;
            movementSpeed = 10;

            redRectangle.Width = 17;
            redRectangle.Height = 133;
            redRectangle.X = 1370;
            redRectangle.Y = (int)redPos.Y;
            



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bal = Content.Load<Texture2D>("bal");
            blauweSpeler = Content.Load<Texture2D>("blauweSpeler");
            rodeSpeler = Content.Load<Texture2D>("rodeSpeler");
        }

        // converts degrees to radians
        protected double Radians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


           

            int xSpeed = (int)(speed * Math.Cos(Radians(angle)));
            int ySpeed = (int)(speed * Math.Sin(Radians(angle)));
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

            redRectangle.Y = (int)redPos.Y;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.White); 
            
            _spriteBatch.Begin();

            _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/scorefont"), (bluePoints.ToString()), new Vector2(250, 300), new Color(0, 0, 255, 0.5f));
            _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/scorefont"), (redPoints.ToString()), new Vector2(1050, 300), new Color(255, 0, 0, 0.5f));
            _spriteBatch.Draw(bal, balRectangle, Color.White);
            _spriteBatch.Draw(blauweSpeler, blueRectangle , Color.White);
            _spriteBatch.Draw(rodeSpeler, redRectangle, Color.White);
           
           
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}