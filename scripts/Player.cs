using Godot;
using System;

public class Player : KinematicBody2D
{

    private sbyte horizontalDir = 0, verticalDir = 0;
    private Vector2 velocity;
    [Export]
    public short speed = 500;
    [Export]
    private bool inControl = false;


    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
        if (inControl)
            DoMovement();
    }

    private void DoMovement()
    {
        if (Input.IsActionPressed("ui_down"))
        {
            verticalDir = 1;
        }
        else
        if (Input.IsActionPressed("ui_up"))
        {
            verticalDir = -1;
        }
        else
        {
            verticalDir = 0;
        }

        if (Input.IsActionPressed("ui_right"))
        {
            horizontalDir = 1;
        }
        else
        if (Input.IsActionPressed("ui_left"))
        {
            horizontalDir = -1;
        }
        else
        {
            horizontalDir = 0;
        }

        velocity.x = this.GetProcessDeltaTime() * speed * horizontalDir;
        velocity.y = this.GetProcessDeltaTime() * speed * verticalDir;
        MoveAndCollide(velocity);
    }

    public void setControl(bool control)
    {
        this.inControl = control;
    }

}
