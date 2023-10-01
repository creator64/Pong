using Microsoft.Xna.Framework;

namespace Pong.Ults;

public class InvisibleBall: Ult
{
    private float visibility = 1f;

    public InvisibleBall()
    {
        coinsRequired = 3;
        startOn = StartOn.HitBall;
        endOn = new[] { EndOn.HitBallOpponent, EndOn.HitSideWall };
        color = Color.Coral;
        description = "The next ball you hit will slowly become invisible";
    }
    
    protected override void startUlt() {}

    protected override void executeUlt()
    {
        visibility -= .0020f * (float)game.BallSprite.speed;
        game.BallSprite.mask = new Color(Color.White, visibility);
    }

    protected override void stopUlt()
    {
        visibility = 1f;
        game.BallSprite.mask = Color.White;
    }

    protected override void DrawUlt(){}
}