using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public enum COLOR
    { 
        NONE,
        RED,
        BLUE,
        GREEN,
        YELLOW,
    }
    public COLOR ThisBlockColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Grab"))
        {
            other.gameObject.GetComponentInParent<RobotController>().GrabBlock(gameObject);
        }
    }
}
