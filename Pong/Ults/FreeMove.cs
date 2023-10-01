using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.Sprites;

namespace Pong.Ults;

public class FreeMove : Ult
{
    private Keys keyLeft, keyRight;
    public FreeMove()
    {
        coinsRequired = 3;
        startOn = StartOn.Activation;
        endOn = new[] { EndOn.Timed };
        duration = 12;
        color = Color.DarkCyan;
        description = "You can move left and right for " + duration + " seconds";
    }

    protected override void startUlt()
    {
        keyLeft = Keys.Left; keyRight = Keys.Right;
        if (player.side == Side.Left)
        {
            keyLeft = Keys.A; keyRight = Keys.D;
        }
    }

    protected override void executeUlt()
    {
        var blueVelocity = Vector2.Zero;
        var keyState = Keyboard.GetState();
        if (keyState.IsKeyDown(keyLeft)) {
            if (player.Rect.X > player.wallOffset && player.side == Side.Left) blueVelocity.X = -1;
            if (player.Rect.Center.X > game.screenRectangle.Width / 2 && player.side == Side.Right) blueVelocity.X = -1;
        }
        if (keyState.IsKeyDown(keyRight)) {
            if (player.Rect.Center.X < game.screenRectangle.Width / 2 && player.side == Side.Left) blueVelocity.X = 1;
            if (player.Rect.X < game.screenRectangle.Width - player.wallOffset - player.Rect.Width && player.side == Side.Right) blueVelocity.X = 1;
        }
        
        if (blueVelocity != Vector2.Zero) blueVelocity.Normalize(); 
        
        player.Move(blueVelocity.X * player.movementSpeed, 0 , checkThrough: true);
    }

    protected override void stopUlt()
    {
        if (player.side == Side.Left)
        {
            player.Rect.X = player.wallOffset;
        }
        else player.Rect.X = game.screenRectangle.Width - player.wallOffset - player.Rect.Width;
    }

    protected override void DrawUlt(){}
}