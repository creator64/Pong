using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Screens;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        public string screen = "menu";
        public GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        public static Rectangle screenRectangle;
        Texture2D bal;
        Rectangle balRectangle;
        double speed, angle;
        MenuScreen menuScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()

        {

            screenRectangle = new Rectangle(0, 0, 1400, 800);

            balRectangle.Width = 60;
            balRectangle.Height = 60;
            balRectangle.X = screenRectangle.Width / 2 - balRectangle.Width / 2;
            balRectangle.Y = screenRectangle.Height / 2 - balRectangle.Height / 2;
            speed = 5;
            angle = 72;

            _graphics.PreferredBackBufferWidth = screenRectangle.Width;
            _graphics.PreferredBackBufferHeight = screenRectangle.Height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            menuScreen = new MenuScreen(Content, _graphics);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bal = Content.Load<Texture2D>("bal");
        }

        // converts degrees to radians
        protected double Radians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private void updateGame(GameTime gameTime)
        {
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
            base.Update(gameTime);
        }

        private void DrawGame(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _spriteBatch.Draw(bal, balRectangle, Color.White);
            _spriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // this switch statement is the same as an if-else statement
            switch (screen)
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

            switch (screen)
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

        public void setHealth()
        {

        }
    }
}