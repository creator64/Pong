using Microsoft.Xna.Framework;

namespace Pong.Ults;

public class Teleport : Ult
{
    public Teleport()
    {
        coinsRequired = 2;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.OneFrame };
        color = Color.Khaki;
        description = "You can teleport to the same height as the ball";
    }

    protected override void startUlt()
    {
        player.Rect.Y = game.BallSprite.Rect.Y - player.Rect.Height / 2;
    }
    protected override void executeUlt(){}
    protected override void stopUlt(){}
    protected override void DrawUlt(){}
}