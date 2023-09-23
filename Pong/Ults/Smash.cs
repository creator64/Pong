using Pong.Sprites;
using Microsoft.Xna.Framework;

namespace Pong.Ults;

public class Smash : Ult
{
    public Smash()
    {
        coinsRequired = 5;
        startOn = StartOn.HitBall;
        endOn = new[] { EndOn.HitWallOpponent, EndOn.HitBallOpponent };
        color = Color.Orange;
        description = "You can smash the next ball you receive";
    }
    protected override void startUlt(){}
    protected override void executeUlt(){}
    protected override void stopUlt(){}
}