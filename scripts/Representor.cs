using Godot;
using System;

public class Representor : Control
{
    Label number;
    Button remove;

    public override void _Ready()
    {
        number = GetNode("number") as Label;
        remove = GetNode("remove") as Button;
        remove.Connect("pressed", this, nameof(removeMe));
    }

    public void setNumber(string value)
    {
        number.SetText(value);
    }

    private void removeMe()
    {
        QueueFree();
    }

    


}
