using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Rectangle screenRectangle;
        Texture2D bal;
        Rectangle balRectangle;
        int xSpeed, ySpeed;


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
            xSpeed = 1;
            ySpeed = 1;

            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            /*_spriteBatch = new SpriteBatch(GraphicsDevice);
             background = Content.Load<Texture2D>("shuttle"); // change these names to the names of your images*/
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bal = Content.Load<Texture2D>("bal");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            balRectangle.X += xSpeed;
            balRectangle.Y += ySpeed;
            if (balRectangle.Bottom >= screenRectangle.Bottom)
            {
                ySpeed = -ySpeed;
            }
            if (balRectangle.Right >= screenRectangle.Right)
            {
                xSpeed = -xSpeed;
            }
            if (balRectangle.Top <= screenRectangle.Top)
            {
                ySpeed = -ySpeed;
            }
            if (balRectangle.Left <= screenRectangle.Left)
            {
                xSpeed = -xSpeed;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here

            GraphicsDevice.Clear(Color.White); 
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(bal, balRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}