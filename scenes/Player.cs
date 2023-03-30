using Godot;
using System;

public class Player : Node2D
{
    [Export]
    public int Speed { get; set; } = 2;

    protected Vector2 ScreenSize { get; set; } 

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta)
    {
        LookAtMouse();
        Move(delta);
    }

    private void LookAtMouse()
    {
        this.Rotation = this.Position.AngleToPoint(GetGlobalMousePosition());
    }

    private void Move(float delta)
    {
        var velocity = Vector2.Zero;

        // Up / Down
        if (Input.IsActionPressed("PlayerMoveUp"))
        {
            velocity = this.Position.DirectionTo(GetGlobalMousePosition());
        }
        else if (Input.IsActionPressed("PlayerMoveDown"))
        {
            velocity = -this.Position.DirectionTo(GetGlobalMousePosition());
        }

        // Left / Right
        // Todo prevent circle movement
        if (Input.IsActionPressed("PlayerMoveRight"))
        {
            velocity = -this.Position.DirectionTo(GetGlobalMousePosition()).Perpendicular();
        }
        else if (Input.IsActionPressed("PlayerMoveLeft"))
        {
            velocity = this.Position.DirectionTo(GetGlobalMousePosition()).Perpendicular();
        }

        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * Speed;
        }

        Position += velocity * delta;

        Position = new Vector2
        {
            x = Mathf.Clamp(Position.x, 0, ScreenSize.x),
            y =Mathf.Clamp(Position.y, 0, ScreenSize.y)
        };
    }
}
