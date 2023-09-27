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