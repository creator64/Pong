using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Sprites;

namespace Pong.Ults;

public abstract class Ult
{
    public Color color;
    protected int coinsRequired;
    protected StartOn startOn;
    protected EndOn[] endOn;
    protected int duration = 5; // only used in combination with EndOn.Timed
    public string description = "ult description";
    private bool activated;
    private bool running;
    private readonly int coinSize = 30;
    public Player player;
    private long timeStarted;
    protected readonly Pong game = Globals.game;
    private readonly Texture2D coinImage;
    private readonly Texture2D darkCoinImage;
    private Color oldColor;
    private readonly Color temporaryActivatedColor = Color.Gold;

    protected Ult()
    {
        coinImage = game.Content.Load<Texture2D>("coin");
        darkCoinImage = game.Content.Load<Texture2D>("dark coin");
    }

    protected abstract void startUlt();
    protected abstract void executeUlt();
    protected abstract void stopUlt();

    private void runUlt()
    {
        activated = false;
        color = oldColor;
        running = true;
        timeStarted = DateTimeOffset.Now.ToUnixTimeSeconds();
        startUlt();
    }

    private void killUlt()
    {
        running = false;
        stopUlt();
    }
    
    public void activateUlt()
    {
        if (player.coinsCollected >= coinsRequired)
        {
            oldColor = color; color = temporaryActivatedColor;
            activated = true;
            color.R += Color.White.R;
            player.coinsCollected = 0;
        }
    }

    // this function is called each frame by a player
    public void Update()
    {
        if (activated && startOn == StartOn.Activation)
        {
            runUlt(); return;
        }
        if (!running) return;
        
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

    public void Draw()
    {
        // draw coins
        var pos = new Rectangle(15, game.screenRectangle.Height - coinSize - 15, coinSize, coinSize); var sign = 1;
        if (player.side == Side.Right)
        {
            pos.X = game.screenRectangle.Width - coinSize - 15;
            sign = -1;
        }

        foreach (var number in Enumerable.Range(1, coinsRequired))
        {
            var image = coinImage;
            if (number > player.coinsCollected) image = darkCoinImage;
            
            game._spriteBatch.Draw(image, pos, Color.White);
            pos.X += (coinSize + 5) * sign;
        }
    }
    
    public void OnBallHitSideWall()
    {
        if (endOn.Contains(EndOn.HitWallOpponent) && running) { killUlt(); }
    }

    public void OnTouchBall()
    {
        if (activated && startOn == StartOn.HitBall) { runUlt(); }
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