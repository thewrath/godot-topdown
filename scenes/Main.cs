using Godot;

public class Main : Node
{

    private PackedScene _enemyPrefab;

    public override void _Ready()
    {
        GD.Randomize();

        _enemyPrefab = GD.Load<PackedScene>("res://scenes/Enemy.tscn");
    }

    public void SpawnEnemy()
    {
        var spawner = GetNode<PathFollow2D>("EnemiesPath/CurrentSpawn");
        spawner.Offset = GD.Randi();

        var enemy = _enemyPrefab.Instance<Enemy>();
        enemy.Target = GetNode<Node2D>("Player");
        enemy.Position = spawner.Position;

        AddChild(enemy);
    }
}
