using Godot;
using Thomas;

public class Bullet : Node2D
{
    [Export]
    public int Speed { get; set; } = 1000;

    [Signal]
    public delegate void Hit();

    public Vector2 Direction { get; set; } = Vector2.Zero;

    protected Vector2 ScreenSize { get; set; }

    protected SignalAwaiter registredForDeletion = null;

    public override void _Ready()
    {
        AddToGroup("bullets");
        ScreenSize = GetViewportRect().Size;

        Rotation = Vector2.Zero.AngleToPoint(Direction);
    }

    public override void _Process(float delta)
    {
        if (!(GlobalPosition.x == ScreenSize.x || GlobalPosition.y == ScreenSize.y || GlobalPosition.x == 0 || GlobalPosition.y == 0))
        {
            GlobalPosition += Direction * Speed * delta;

            this.ClampToScreen(ScreenSize);
        } else if (registredForDeletion is null)
        {

            registredForDeletion = ToSignal(GetTree().CreateTimer(5), "timeout");
            registredForDeletion.OnCompleted(() => {
                CallDeferred("queue_free");
            });
        }
    }

    public void CollisionDetected(Area2D area)
    {
        EmitSignal(nameof(Hit));

        CallDeferred("free");
        area.GetParent().CallDeferred("free");
    }
}
