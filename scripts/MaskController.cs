using Godot;
using System;
using System.Collections.Generic;

public class MaskController : Control
{
    List<Player> players;
    Controller controller;
    CollisionController collisionController;
    List<CollisionController> listCollisionControlers;


    public override void _Ready()
    {
        try
        {


            controller = GetNode("../Control") as Controller;

            players = controller.GetPlayerList();

            GD.Print("test");
            PackedScene res = GD.Load("res://CollisionController.tscn") as PackedScene;

            for (byte i = 0; i < players.Count; i++)
            {
                CollisionController cc = res.Instance() as CollisionController;
                AddChild(cc);
                cc.SetPosition(new Vector2(cc.GetSize().x * i, cc.GetPosition().y));
                cc.setTitle(players[i].GetName().ToString());
                cc.setOwner(players[i]);
                //listCollisionControlers.Add(cc);
            }
            GD.Print("passed fior");
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
		
		GD.Print("what the fuck");

    }

}
