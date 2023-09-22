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
    private Player player;
    protected readonly Game1 game = Globals.game;

    protected Ult(Player player)
    {
        this.player = player;
    }
    protected abstract void startUlt();
    public abstract void executeUlt();
    public abstract void stopUlt();

    private void runUlt()
    {
        activated = false;
        running = true;
        startUlt();
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
        if (!activated) { return;}
        switch (startOn)
        {
            case StartOn.Activation:
                runUlt();
                break;
            case StartOn.HitBall:
                if (game.BallSprite.lastPlayerTouched == player) runUlt();
                break;
        }
    }

    public void Draw()
    {
        // draw stuff like how many coins we have collected
    }
}

public enum StartOn
{
    Activation,
    HitBall
}