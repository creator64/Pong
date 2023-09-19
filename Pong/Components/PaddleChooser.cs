using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Pong.Components
{
    internal class PaddleChooser : Component
    {
        public int PaddleIndex = 0;
        Color PaddleColor;
        Vector2 position;
        Texture2D RectangleTexture;
        ContentManager Content;

        Button ArrowUpButton;
        Button ArrowDownButton;
        private List<Component> Components;

        Color BGColor;
        static double size = .3;

        public PaddleChooser(Vector2 position, Color BGColor, ContentManager Content)
        {
            this.BGColor = BGColor;
            this.position = position;
            this.Content = Content;

            RectangleTexture = new Texture2D(Game1._graphics.GraphicsDevice, 1, 1);
            RectangleTexture.SetData<Color>(new Color[] { Color.White });


            ArrowUpButton = new Button(Content.Load<Texture2D>("arrowup"), Content.Load<SpriteFont>("fonts/buttonfont"), BGColor, BGColor, size)
            {
                Position = new Vector2(position.X, (int)(position.Y - 480 * size))
            };
            ArrowUpButton.Click += (object sender, System.EventArgs e) => { ChangeColor("up"); };
            ArrowDownButton = new Button(Content.Load<Texture2D>("arrowdown"), Content.Load<SpriteFont>("fonts/buttonfont"), BGColor, BGColor, size)
            { 
                Position = new Vector2(position.X, position.Y)
            };
            ArrowDownButton.Click += (object sender, System.EventArgs e) => { ChangeColor("down"); };

            Components = new List<Component>()
            {
                ArrowUpButton,
                ArrowDownButton
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var rect = new Rectangle((int)(this.position.X - 175 * size), (int)(position.Y - 205 * size), (int)(600 * size), (int)(144 * size));
            var text = "Ult: Smashball";
            var font = this.Content.Load<SpriteFont>("fonts/description");

            var x = (float)((rect.X + (rect.Width / 2)) - (font.MeasureString(text).X / 2) + 40 * size);
            var y = (float)((rect.Y + (rect.Height / 2)) - (font.MeasureString(text).Y / 2) + 450 * size);

            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.AntiqueWhite); // Draw description
            spriteBatch.Draw(RectangleTexture, rect, Globals.PaddleList[this.PaddleIndex]); // Draw colored rectangle

            foreach (var component in Components) // Draw buttons
                component.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                component.Update(gameTime);
        }

        public void ChangeColor(string direction)
        {
            Debug.WriteLine("clicked " + direction);
            if (direction == "up")
            {
                if (this.PaddleIndex == Globals.PaddleList.Length - 1)
                {
                    this.PaddleIndex = 0;
                    return;
                }
                this.PaddleIndex += 1;
            }
            else if (direction == "down")
            {
                if (this.PaddleIndex == 0)
                {
                    this.PaddleIndex = Globals.PaddleList.Length - 1;
                    return;
                }
                this.PaddleIndex -= 1;
            }
        }
    }
}
