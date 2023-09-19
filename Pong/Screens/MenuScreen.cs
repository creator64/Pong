using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Components;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pong.Screens
{
    internal class MenuScreen
    {
        ContentManager Content;
        private List<Component> Components;
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;
        public static Color BGColor = new Color(133, 94, 66);
        private string instructions = "Controls \n Left Player -> Moving: W and S, Ult: A \n Right Player -> Moving: Up and Down arrow, Ult: Left arrow";
        public PaddleChooser PaddleChooserOne;
        public PaddleChooser PaddleChooserTwo;

        public MenuScreen(ContentManager Content, GraphicsDeviceManager _graphics)
        {
            this._graphics = _graphics;
            this.Content = Content;
            spriteBatch = new SpriteBatch(this._graphics.GraphicsDevice);

            var PlayButton = new Button(this.Content.Load<Texture2D>("button"), this.Content.Load<SpriteFont>("fonts/buttonfont"), new Color(103, 73, 51), new Color(177, 132, 99), 2)
            {
                Position = new Vector2(Game1.screenRectangle.Center.X - 100, Game1.screenRectangle.Center.Y),
                Text = "Play",
            };
            PlayButton.Click += (object sender, System.EventArgs e) => { Game1.screen = "game"; };

            PaddleChooserOne = new PaddleChooser(new Vector2(Game1.screenRectangle.Center.X + 320, 600), BGColor, Content);
            PaddleChooserTwo = new PaddleChooser(new Vector2(Game1.screenRectangle.Center.X - 350, 600), BGColor, Content);

            Components = new List<Component>()
            {
                PlayButton,
                PaddleChooserOne,
                PaddleChooserTwo
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                component.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(BGColor);

            spriteBatch.Begin();

            foreach (var component in Components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(this.Content.Load<SpriteFont>("fonts/headerfont"), "WELCOME TO PONG", new Vector2(320, 170), Color.AntiqueWhite);
            spriteBatch.DrawString(this.Content.Load<SpriteFont>("fonts/description"), instructions, Vector2.Zero, Color.AntiqueWhite);

            spriteBatch.End();
        }
    }
}
