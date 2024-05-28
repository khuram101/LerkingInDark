using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    private bool isHitted = false;

    public bool IsHitted()
    {
        return isHitted;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHitted = true;
       // Debug.Log(gameObject.name+ isHitted);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHitted = false;
       // Debug.Log(gameObject.name + isHitted);
    }
}
