using Pong.Sprites;

namespace Pong.Ults;

public class Smash : Ult
{
    public Smash(Player player) : base(player)
    {
        coinsRequired = 5;
    }
    protected override void startUlt(){}
    public override void executeUlt(){}
    public override void stopUlt(){}
}