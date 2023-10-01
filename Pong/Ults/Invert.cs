using Microsoft.Xna.Framework;
using Pong.Sprites;

namespace Pong.Ults;

public class Invert : Ult
{
    
    private Player otherPlayer;
    public Invert()
    {
        coinsRequired = 3;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.Timed };
        duration = 12;
        color = Color.Purple;
        description = "You can invert the opponents controls for " + duration + " seconds";
    }

    protected override void startUlt()
    {
        otherPlayer = game.PlayerRight;
        if (player.side == Side.Right) otherPlayer = game.PlayerLeft;

        otherPlayer.inverted = true;
    }

    protected override void executeUlt() {}

    protected override void stopUlt()
    {
        otherPlayer.inverted = false;
    }

    protected override void DrawUlt(){}
}