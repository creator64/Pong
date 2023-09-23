using Pong.Sprites;

namespace Pong.Ults;

public class Teleport : Ult
{
    public Teleport(Player player) : base(player)
    {
        coinsRequired = 3;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.OneFrame };
    }
    protected override void startUlt(){}
    protected override void executeUlt(){}
    protected override void stopUlt(){}
}