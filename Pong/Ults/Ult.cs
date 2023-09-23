using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Pong.Sprites;

namespace Pong.Ults;

public abstract class Ult
{
    Color _color;
    public int coinsRequired;
    public int coinsCollected;
    public bool activated;
    public bool running;
    public StartOn startOn;
    public EndOn[] endOn;
    private Player player;
    private long timeStarted;
    private int duration; // only used in combination with EndOn.Timed
    protected readonly Game1 game = Globals.game;

    protected Ult(Player player)
    {
        this.player = player;
    }
    protected abstract void startUlt();
    protected abstract void executeUlt();
    protected abstract void stopUlt();

    private void runUlt()
    {
        activated = false;
        running = true;
        timeStarted = DateTimeOffset.Now.ToUnixTimeSeconds();
        startUlt();
    }

    private void killUlt()
    {
        running = false;
        stopUlt();
    }

    // this function is called each
    public void activateUlt()
    {
        if (coinsCollected >= coinsRequired)
        {
            activated = true;
            coinsCollected -= coinsRequired;
        }
    }

    // this function is called each frame by a player
    public void Update()
    {
        if (activated)
        {
            switch (startOn)
            {
                case StartOn.Activation:
                    runUlt();
                    return;
                case StartOn.HitBall:
                    if (game.BallSprite.lastPlayerTouched == player) runUlt();
                    return;
            }
        }
        else if (running)
        {
            executeUlt();
            if (endOn.Contains(EndOn.OneFrame)) { killUlt(); }

            if (endOn.Contains(EndOn.HitBallOpponent))
            {
                if (game.BallSprite.lastPlayerTouched != player) { killUlt(); }
            }

            if (endOn.Contains(EndOn.Timed))
            {
                if (DateTimeOffset.Now.ToUnixTimeSeconds() - timeStarted >= duration) { killUlt(); }
            }
        }
    }

    public void Draw()
    {
        // draw stuff like how many coins we have collected
    }
    
    public void OnBallHitSideWall()
    {
        if (endOn.Contains(EndOn.HitWallOpponent) && running) { killUlt(); }
    }
}

public enum StartOn
{
    Activation,
    HitBall
}

public enum EndOn
{
    OneFrame, 
    HitBallOpponent,
    HitWallOpponent,
    Timed,
    Custom, // if custom is set the end condition will be set in executeUlt
}