using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Sprites;

public class Coin : Sprite
{
    public bool collected;
    private int size = 105;

    public Coin(int x, int y)
    {
        Rect = new Rectangle(x, y, size, size);
        image = game.Content.Load<Texture2D>("coin");
    }
    public override void Update() { }
    public override void Draw()
    {
        game._spriteBatch.Draw(image, Rect, Color.White);
    }

    public void getCollected()
    {
        game.BallSprite.lastPlayerTouched.collectCoin();
        collected = true;
    }
}