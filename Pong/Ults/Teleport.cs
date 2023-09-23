﻿using Pong.Sprites;
using Microsoft.Xna.Framework;

namespace Pong.Ults;

public class Teleport : Ult
{
    public Teleport()
    {
        coinsRequired = 3;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.OneFrame };
        color = Color.Khaki;
        description = "You can teleport to the same height as the ball";
    }
    protected override void startUlt(){}
    protected override void executeUlt(){}
    protected override void stopUlt(){}
}