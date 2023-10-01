using System;
using Microsoft.Xna.Framework;
using Pong.Sprites;

namespace Pong.Ults;

public class BlackBall: Ult
{
    private Ball blackBall;
    private Player otherPlayer;
    private double angle;
    
    public BlackBall()
    {
        coinsRequired = 4;
        color = Color.Black;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.Custom, EndOn.HitSideWall };
        description = "You can throw a giant black ball that your\nopponent has to dodge";
    } 
    protected override void startUlt()
    {
        otherPlayer = game.PlayerRight;
        if (player.side == Side.Right) otherPlayer = game.PlayerLeft;
        blackBall = new Ball(new Vector2(player.Rect.Center.X, player.Rect.Center.Y), size: 100)
        {
            mask = Color.Black,
            speed = 35,
        };
        float diffX = otherPlayer.Rect.X - player.Rect.X;
        float diffY = otherPlayer.Rect.Y - player.Rect.Y;
        angle = Math.Atan(diffY / diffX); // doesnt return angles over 180 degrees
        if (player.side == Side.Right) angle += Math.PI;
    }

    protected override void executeUlt()
    {
        var x = (blackBall.speed * Math.Cos(angle));
        var y = (blackBall.speed * Math.Sin(angle));
        blackBall.Move(x, y, checkThrough: false);
        
        (Sprite sprite, Border border) = blackBall.Collision(game.ObjectList);
        if (sprite == otherPlayer)
        {
            player.points++;
            killUlt();
        }
        if (
            (otherPlayer.side == Side.Right && border == Border.RightBorder) ||
            (otherPlayer.side == Side.Left && border == Border.LeftBorder)) // if the black ball hits the border end the ult
        {
            killUlt();
        }
    }

    protected override void stopUlt()
    {
        blackBall = null;
    }

    protected override void DrawUlt()
    {
        if (running) blackBall.Draw();
    }
}