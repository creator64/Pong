using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Screens;
using Pong.Sprites;
using System;
using System.Collections.Generic;

namespace Pong
{
    public class Pong : Game
    {
        private StateScreen stateScreen = StateScreen.Menu;
        public readonly GraphicsDeviceManager Graphics;
        public SpriteBatch _spriteBatch;
        public List<Sprite> ObjectList;
        public List<Coin> CoinList;
        public Rectangle screenRectangle;
        public Ball BallSprite;
        public Player PlayerLeft;
        public Player PlayerRight;
        MenuScreen menuScreen;
        private bool frozen = true;
        private long lastTimeCoinSpawned;
        private readonly int coinSpawnTime = 3;

        public Pong()
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

        public void NewGame()
        {
            frozen = true;
            stateScreen = StateScreen.Game;
            BallSprite = new Ball(Vector2.Zero, moveToMiddle: true);
            PlayerLeft = new Player(Side.Left, menuScreen.PaddleChooserTwo.ult);
            PlayerRight = new Player(Side.Right, menuScreen.PaddleChooserOne.ult);
            ObjectList = new List<Sprite>(){PlayerLeft, PlayerRight, BallSprite};
            CoinList = new List<Coin>();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuScreen = new MenuScreen(Content, Graphics);
        }

        public void OnBallHitSideWall(Border border)
        {
            if (border == Border.LeftBorder | border == Border.RightBorder)
            {
                PlayerLeft.OnBallHitSideWall(); PlayerRight.OnBallHitSideWall();
                
                if (border == Border.LeftBorder)
                    PlayerRight.points++;
                else PlayerLeft.points++;
                
                BallSprite.MoveToMiddle();
                BallSprite.speed = 10;
                frozen = true;
                CoinList.RemoveAll(c => true);
            }
        }

        private void handleCoins()
        {
            if (DateTimeOffset.Now.ToUnixTimeSeconds() - lastTimeCoinSpawned > coinSpawnTime)
            {
                var x = new Random().Next(200, 1200);
                var y = new Random().Next(200, 600);
                CoinList.Add(new Coin(x,y));
                lastTimeCoinSpawned = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
            CoinList.RemoveAll(coin => coin.collected);
        }

        private void updateGame(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Space))
            {
                frozen = false;
                lastTimeCoinSpawned = DateTimeOffset.Now.ToUnixTimeSeconds();
            }

            if (!frozen)
            {
                foreach (var sprite in ObjectList) { sprite.Update(); }
                foreach (var coin in CoinList) { coin.Update(); }
                handleCoins();
            }
            
            base.Update(gameTime);
        }

        private void DrawGame(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            _spriteBatch.Begin();

            if (frozen)
            {
                var verb = "CONTINUE";
                var moretotheleft = 25;
                if (PlayerLeft.points == 0 && PlayerRight.points == 0)
                {
                    verb = "START";
                    moretotheleft = 0;
                }
                
                _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/description"), "PRESS <SPACE> TO " + verb,
                    new Vector2(523 - moretotheleft, 600), Color.Black);
            }

            foreach (var sprite in ObjectList) { sprite.Draw(); }

            foreach (var coin in CoinList) { coin.Draw(); }

            _spriteBatch.End();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                stateScreen = StateScreen.Menu;

            // this switch statement is the same as an if-else statement
            switch (stateScreen)
            {
                case StateScreen.Menu:
                    menuScreen.Update(gameTime);
                    break;
                default:
                    updateGame(gameTime);
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (stateScreen)
            {
                case StateScreen.Menu:
                    menuScreen.Draw(gameTime);
                    break;
                case StateScreen.Game:
                    DrawGame(gameTime);
                    break;
            }

            base.Draw(gameTime);
        }
    }

    public enum StateScreen
    {
        Menu,
        Game
    }
}