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
        Texture2D bal;
        Vector2 bluePos;
        Vector2 blueVelocity;
        Texture2D blauweSpeler;
        public string keyW = "keyW/Upblue";
        public string keyS = "keyS/Downblue";
        public float timepassed;

       
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
            speed = 5;
            angle = 72;

            _graphics.PreferredBackBufferWidth = screenRectangle.Width;
            _graphics.PreferredBackBufferHeight = screenRectangle.Height;
            _graphics.ApplyChanges();

            blueRectangle.Width = 17;
            blueRectangle.Height = 133;
            blueRectangle.X = 10;
            blueRectangle.Y = (int)bluePos.Y;
            movementSpeed = 8;





            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bal = Content.Load<Texture2D>("bal");
            blauweSpeler = Content.Load<Texture2D>("blauweSpeler");

            


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
                angle = 180 - angle;
            }
            if (balRectangle.Top <= screenRectangle.Top)
            {
                angle = 360 - angle;
            }
            if (balRectangle.Left <= screenRectangle.Left)
            {
                angle = 180 - angle;
            }


           

            KeyboardState keyState = Keyboard.GetState();




            if (blueRectangle.Top >= screenRectangle.Top)
            {
                if (keyState.IsKeyDown(Keys.W)) { blueVelocity.Y = 1; }

                if (keyState.IsKeyDown(Keys.S)) { blueVelocity.Y = -1; }

                if (blueVelocity != Vector2.Zero) { blueVelocity.Normalize(); }
            }
            if (blueRectangle.Top < screenRectangle.Top)
            { 
                if (keyState.IsKeyDown(Keys.S)) { blueVelocity.Y = -1; } 
            }



            if (blueRectangle.Bottom >= screenRectangle.Bottom)
            {
                if (keyState.IsKeyDown(Keys.W)) { blueVelocity.Y = 1; }

                if (keyState.IsKeyDown(Keys.S)) { blueVelocity.Y = -1; }

                if (blueVelocity != Vector2.Zero) { blueVelocity.Normalize(); }
            }
            if (blueRectangle.Bottom < screenRectangle.Bottom)
            {
                if (keyState.IsKeyDown(Keys.W)) { blueVelocity.Y = 1; }
            }



            bluePos.Y -= blueVelocity.Y * movementSpeed;

            blueVelocity = Vector2.Zero;

            blueRectangle.Y = (int)bluePos.Y;

           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.White); 
            
            _spriteBatch.Begin();

            _spriteBatch.Draw(bal, balRectangle, Color.White);
            _spriteBatch.Draw(blauweSpeler, blueRectangle , Color.White);

            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}