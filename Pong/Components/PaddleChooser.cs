using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using Pong.Ults;

namespace Pong.Components
{
    internal class PaddleChooser : Component
    {
        readonly Pong game = Globals.game;
        private int UltIndex;
        public Ult ult;
        readonly Vector2 position;
        readonly Texture2D RectangleTexture;
        readonly ContentManager Content;

        readonly Button ArrowUpButton;
        readonly Button ArrowDownButton;
        private readonly List<Component> Components;

        Color BGColor;
        static double size = .3;
        
        private readonly Ult[] UltList;

        public PaddleChooser(Vector2 position, Color BGColor, ContentManager Content)
        {
            this.BGColor = BGColor;
            this.position = position;
            this.Content = Content;
            UltList = new Ult[] { new Smash(), new Teleport(), new FreeMove(), new InvisibleBall() } ;
            ult = UltList[UltIndex];

            RectangleTexture = new Texture2D(game.Graphics.GraphicsDevice, 1, 1);
            RectangleTexture.SetData<Color>(new Color[] { Color.White });


            ArrowUpButton = new Button(Content.Load<Texture2D>("arrowup"), Content.Load<SpriteFont>("fonts/buttonfont"), BGColor, BGColor, size)
            {
                Position = new Vector2(position.X, (int)(position.Y - 480 * size))
            };
            ArrowUpButton.Click += (object sender, System.EventArgs e) => { ChangeUlt("up"); };
            ArrowDownButton = new Button(Content.Load<Texture2D>("arrowdown"), Content.Load<SpriteFont>("fonts/buttonfont"), BGColor, BGColor, size)
            { 
                Position = new Vector2(position.X, position.Y)
            };
            ArrowDownButton.Click += (object sender, System.EventArgs e) => { ChangeUlt("down"); };

            Components = new List<Component>()
            {
                ArrowUpButton,
                ArrowDownButton
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var rect = new Rectangle((int)(this.position.X - 175 * size), (int)(position.Y - 205 * size), (int)(600 * size), (int)(144 * size));
            var text = ult.description;
            var font = this.Content.Load<SpriteFont>("fonts/description");

            var x = (float)((rect.X + (rect.Width / 2)) - (font.MeasureString(text).X / 2) + 40 * size);
            var y = (float)((rect.Y + (rect.Height / 2)) - (font.MeasureString(text).Y / 2) + 450 * size);

            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.AntiqueWhite); // Draw description
            spriteBatch.Draw(RectangleTexture, rect, ult.color); // Draw colored rectangle

            foreach (var component in Components) // Draw buttons
                component.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                component.Update(gameTime);
        }

        private void ChangeUlt(string direction)
        {
            Debug.WriteLine("clicked " + direction);
            if (direction == "up")
            {
                if (this.UltIndex == UltList.Length - 1)
                {
                    this.UltIndex = 0;
                }
                else this.UltIndex += 1;
            }
            else if (direction == "down")
            {
                if (this.UltIndex == 0)
                {
                    this.UltIndex = UltList.Length - 1;
                }
                else this.UltIndex -= 1;
            }
            this.ult = UltList[UltIndex];
        }
    }
}
