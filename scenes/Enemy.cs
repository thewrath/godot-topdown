using Godot;

public class Enemy : Node2D
{
    [Export]
    public int Speed = 200;

    public Node2D Target { get; set; }

    protected Vector2 ScreenSize { get; set; }

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
        AddToGroup("enemies");
    }

    public override void _Process(float delta)
    {
        MoveToTarget(delta);
    }

    protected void MoveToTarget(float delta)
    {
        if (Target is null || Position.DistanceTo(Target.Position) < 1)
        {
            return;
        }

        var direction = Position.DirectionTo(Target.Position);

        Position += direction * Speed * delta;
    }
}
