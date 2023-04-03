using Godot;
using System;

public class BowWeapon : Node2D
{

    private AnimationNodeStateMachinePlayback stateMachine;
    private AnimationPlayer player;

    public override void _Ready()
    {
        stateMachine = (AnimationNodeStateMachinePlayback) GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
        player = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void Shoot()
    {
        stateMachine.Travel("shoot");
    }

    public void OnShootAnimationFinished()
    {
        GD.Print("Shoot");
    }
}
