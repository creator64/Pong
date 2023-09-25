using Pong.Sprites;
using Microsoft.Xna.Framework;

namespace Pong.Ults;

public class Smash : Ult
{
    private readonly double speedFactor = 2.5;
    public Smash()
    {
        coinsRequired = 4;
        startOn = StartOn.HitBall;
        endOn = new[] { EndOn.HitWallOpponent, EndOn.HitBallOpponent };
        color = Color.Orange;
        description = "You can smash the next ball you receive";
    }

    protected override void startUlt()
    {
        game.BallSprite.speed *= speedFactor;
        game.BallSprite.mask = color;
    }

    protected override void executeUlt()
    {
        game.BallSprite.speed -= .001;
    }

    protected override void stopUlt()
    {
        game.BallSprite.speed /= speedFactor;
        game.BallSprite.mask = Color.White;
    }
}