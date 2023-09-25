using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Components;
using System.Collections.Generic;


namespace Pong.Screens
{
    internal class MenuScreen
    {
        private readonly ContentManager _content;
        private readonly List<Component> _components;
        private readonly GraphicsDeviceManager _graphics;
        private readonly SpriteBatch _spriteBatch;
        private readonly Color _bgColor = new (133, 94, 66);
        private const string Instructions = "Controls \n Left Player -> Moving: W and S, Ult: A \n Right Player -> Moving: Up and Down Arrow, Ult: Left Arrow";
        public readonly PaddleChooser PaddleChooserOne;
        public readonly PaddleChooser PaddleChooserTwo;
        private readonly Game1 game = Globals.game;

        public MenuScreen(ContentManager content, GraphicsDeviceManager _graphics)
        {
            this._graphics = _graphics;
            this._content = content;
            _spriteBatch = new SpriteBatch(this._graphics.GraphicsDevice);

            var playButton = new Button(this._content.Load<Texture2D>("button"), this._content.Load<SpriteFont>("fonts/buttonfont"), new Color(103, 73, 51), new Color(177, 132, 99), 2)
            {
                Position = new Vector2(game.screenRectangle.Center.X - 100, game.screenRectangle.Center.Y),
                Text = "Play",
            };
            playButton.Click += (object sender, System.EventArgs e) => { game.NewGame(); };

            PaddleChooserOne = new PaddleChooser(new Vector2(game.screenRectangle.Center.X + 320, 600), _bgColor, content);
            PaddleChooserTwo = new PaddleChooser(new Vector2(game.screenRectangle.Center.X - 350, 600), _bgColor, content);

            _components = new List<Component>()
            {
                playButton,
                PaddleChooserOne,
                PaddleChooserTwo
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(_bgColor);

            _spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.DrawString(this._content.Load<SpriteFont>("fonts/headerfont"), "WELCOME TO PONG", new Vector2(320, 170), Color.AntiqueWhite);
            _spriteBatch.DrawString(this._content.Load<SpriteFont>("fonts/description"), Instructions, Vector2.Zero, Color.AntiqueWhite);

            _spriteBatch.End();
        }
    }
}
