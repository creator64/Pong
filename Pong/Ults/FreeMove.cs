﻿using Pong.Sprites;

namespace Pong.Ults;

public class FreeMove : Ult
{
    public FreeMove(Player player) : base(player)
    {
        coinsRequired = 4;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.Timed };
    }
    protected override void startUlt(){}
    protected override void executeUlt(){}
    protected override void stopUlt(){}
}