using Godot;
using System.Collections.Generic;

public class Controller : Control
{

    List<Player> players = new List<Player>();
    Player controlling = null;
    ColorRect selection;
    public override void _Ready()
    {
        Node2D participants = GetNode("../Participants") as Node2D;
        selection = GetNode("ColorRect") as ColorRect;

        foreach(Player player in participants.GetChildren())
        {
            players.Add(player);
        }

        nextController();
        
    }

    public override void _Process(float delta)
    {
        selection.SetPosition(controlling.GetGlobalPosition());
    }


    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("f"))
        {
            nextController();
        }
            
    }

    byte actual = 0;
    private void nextController()
    {
        if (controlling is null)
        {
            controlling = players[actual];
        }
        else
        {
            controlling.setControl(false);
            actual++;
            GD.Print("changed to: ", actual);
            if (actual >= players.Count)
                actual = 0;
            controlling = players[actual];
        }

        controlling.setControl(true);
        
    }

    public List<Player> GetPlayerList()
    {
        return players;
    }
}
