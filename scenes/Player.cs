using Godot;
using System;

public class Player : Node2D
{
    [Export]
    public int Speed { get; set; } = 2;

    [Signal]
    public delegate void PrimaryAction();

    protected Vector2 ScreenSize { get; set; } 

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta)
    {
        LookAtMouse();
        Move(delta);

        if (Input.IsActionPressed("PlayerPrimaryAction"))
        {
            EmitSignal(nameof(PrimaryAction));
        }
    }

    private void LookAtMouse()
    {
        this.Rotation = this.Position.AngleToPoint(GetGlobalMousePosition());

        // Keep the head static
        GetNode<Sprite>("Head").Rotation = -this.Rotation;
    }

    private void Move(float delta)
    {
        var velocity = Vector2.Zero;

        // Up / Down
        if (Input.IsActionPressed("PlayerMoveUp"))
        {
            // velocity = this.Position.DirectionTo(GetGlobalMousePosition());
            velocity.y -= 1;
        }
        else if (Input.IsActionPressed("PlayerMoveDown"))
        {
            velocity.y += 1;
            // velocity = -this.Position.DirectionTo(GetGlobalMousePosition());
        }

        // Left / Right
        // Todo prevent circle movement
        if (Input.IsActionPressed("PlayerMoveRight"))
        {
            velocity.x += 1;
            // velocity += -this.Position.DirectionTo(GetGlobalMousePosition()).Perpendicular();
        }
        else if (Input.IsActionPressed("PlayerMoveLeft"))
        {
            velocity.x -= 1;
            // velocity += this.Position.DirectionTo(GetGlobalMousePosition()).Perpendicular();
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
