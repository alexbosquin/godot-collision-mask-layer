using Godot;
using System;
using System.Collections.Generic;

public class CollisionController : Container
{
    Label lbTitle;
    SpinBox nLayer, nCollision;
    Button addLayer, addCollision;
    float layerValue, collisionValue;
    GridContainer layerContainer, collisionContainer;

    Player owner;

    public override void _Ready()
    {
        lbTitle = GetNode("title") as Label;
        nLayer = GetNode("CLayer/nLayer") as SpinBox;
        nCollision = GetNode("CColl/nColl") as SpinBox;
        addLayer = GetNode("CLayer/btAddLayer") as Button;
        addCollision = GetNode("CColl/btAddColl") as Button;
        layerContainer = GetNode("CLayer/grid") as GridContainer;
        collisionContainer = GetNode("CColl/grid") as GridContainer;



        nLayer.Connect("value_changed", this, nameof(changedLayerValue));
        nCollision.Connect("value_changed", this, nameof(changedCollisionValue));
        addLayer.Connect("pressed",this, nameof(addMaskLayer));
        addCollision.Connect("pressed", this, nameof(addMaskCollision));

    }

    public void setTitle(string title)
    {
        lbTitle.SetText(title);
    }

    private void changedLayerValue(float value)
    {
        layerValue = value;
    }

    private void changedCollisionValue(float value)
    {
        collisionValue = value;
    }


    PackedScene representor = GD.Load("res://representor.tscn") as PackedScene;

    byte collisions = 0;
    List<float> colValues = new List<float>();
    private void addMaskCollision()
    {
        if (isCollisionOnList())
            return;

        Representor tempRep = representor.Instance() as Representor;
        collisionContainer.AddChild(tempRep);
        tempRep.setNumber(collisionValue.ToString());
        colValues.Add(collisionValue);

        Button remove = tempRep.GetChild(0) as Button;

        object[] obj = new object[1];
        obj[0] = collisionValue;
        remove.Connect("pressed", this, nameof(removeCollisionLayer), obj);
        collisions++;

        owner.SetCollisionMaskBit((int)collisionValue, true);
    }

    byte masks = 0;
    List<float> layerValues = new List<float>();
    private void addMaskLayer()
    {
        if (isLayerOnList())
            return;

        Representor tempRep = representor.Instance() as Representor;
        layerContainer.AddChild(tempRep);
        tempRep.setNumber(layerValue.ToString());
        layerValues.Add(layerValue);

        Button remove = tempRep.GetChild(0) as Button;

        object[] obj = new object[1];
        obj[0] = layerValue;
        remove.Connect("pressed", this, nameof(removeMaskLayer),obj);
        masks++;


        owner.SetCollisionLayerBit((int)layerValue, true);

    }

    private void removeMaskLayer(float val)
    {
        masks--;
        layerValues.Remove(val);
        owner.SetCollisionLayerBit((int)val, false);
        
    }

    private void removeCollisionLayer(float val)
    {
        collisions--;
        colValues.Remove(val);
        owner.SetCollisionMaskBit((int)val, false);
    }

    private bool isLayerOnList()
    {
        if (layerValues.Contains(layerValue))
            return true;

        return false;
    }

    private bool isCollisionOnList()
    {
        if (colValues.Contains(collisionValue))
            return true;

        return false;
    }

    public void setOwner(Player owner)
    {
        this.owner = owner;
    }

}
