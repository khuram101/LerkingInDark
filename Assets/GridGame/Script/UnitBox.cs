using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBox : MonoBehaviour
{
    //tile x and y position
    public int tileX;
    public int tileY;

    public void SetTileTableNumber(int x, int y)
    {
        tileX = x;
        tileY = y;
        
    }
    public void SetTilePosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

}
