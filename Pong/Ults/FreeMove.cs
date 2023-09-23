using Microsoft.Xna.Framework;
using Pong.Sprites;

namespace Pong.Ults;

public class FreeMove : Ult
{
    public FreeMove()
    {
        coinsRequired = 4;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.Timed };
        duration = 6;
        color = Color.DarkCyan;
        description = "You can move left and right for a small period of time";
    }
    protected override void startUlt(){}
    protected override void executeUlt(){}
    protected override void stopUlt(){}
}