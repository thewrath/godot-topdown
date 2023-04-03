using Godot;

public class BowWeapon : Node2D
{

    private AnimationNodeStateMachinePlayback _stateMachine;
    
    private AnimationPlayer _player;

    private PackedScene _bulletPrefab;

    public override void _Ready()
    {
        _stateMachine = (AnimationNodeStateMachinePlayback) GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
        _player = GetNode<AnimationPlayer>("AnimationPlayer");

        _bulletPrefab = GD.Load<PackedScene>("res://scenes/Bullet.tscn");
    }

    public override void _Process(float delta)
    {

    }

    public void Shoot()
    {
        _stateMachine.Travel("shoot");
    }

    public void OnShootAnimationFinished()
    {
        var bullet = _bulletPrefab.Instance<Bullet>();

        bullet.Position = GlobalPosition;
        bullet.Direction = GlobalPosition.DirectionTo(GetGlobalMousePosition());

        bullet.AddToGroup("playerBullets");

        GetTree().Root.AddChild(bullet);
    }
}
