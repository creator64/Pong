using Pong.Sprites;

namespace Pong.Ults;

public class Smash : Ult
{
    public Smash(Player player) : base(player)
    {
        coinsRequired = 5;
        startOn = StartOn.HitBall;
        endOn = new[] { EndOn.HitWallOpponent, EndOn.HitBallOpponent };
    }
    protected override void startUlt(){}
    protected override void executeUlt(){}
    protected override void stopUlt(){}
}