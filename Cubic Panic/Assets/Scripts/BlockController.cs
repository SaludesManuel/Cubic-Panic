using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public BoxCollider2D m_Collider2D;
    public enum COLOR
    { 
        NONE,
        RED,
        BLUE,
        GREEN,
        YELLOW,
    }
    public COLOR ThisBlockColor;

    private void Start()
    {
        m_Collider2D = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer is 7) //if it hits a block or the ground
        {
            
            if (transform.position.y > collision.transform.position.y) //if it's above
            {
                float DistanceX = transform.position.x - collision.transform.position.x;
                if (Mathf.Abs(DistanceX)< m_Collider2D.bounds.extents.x)
                {
                    transform.position += new Vector3(DistanceX, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(collision.gameObject.transform.position.x, transform.position.y,transform.position.z);
                }               
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Grab"))
        {
            other.gameObject.GetComponentInParent<RobotController>().GrabBlock(gameObject);
        }
    }
    private void OnDestroy()
    {
        
    }
}
