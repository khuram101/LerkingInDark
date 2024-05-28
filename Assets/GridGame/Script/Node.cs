using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Node are connected neighbours
[System.Serializable]
public class Node
{
    public List<Node> neighbours;
    public float X;
    public float Y;

    public Node()
    {
        neighbours = new List<Node>();
    }

    public float DistanceTo(Node n)
    {
        return Vector2.Distance(new Vector2(X, Y), new Vector2(n.X, n.Y));
    }

}
