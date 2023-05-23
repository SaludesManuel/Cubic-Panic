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
            
            if (collision.gameObject.transform.position.y < transform.position.y) //if it's above
            {
                transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                //float PositionDifference = transform.position.x - collision.gameObject.transform.position.x;
                //if (PositionDifference==0)
                //{
                //    return;
                //}
                //else if (Math.Abs(PositionDifference) >= m_Collider2D.size.x /2)
                ////Put it on top of it
                //{
                //transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
                //}
                //else if (PositionDifference > 0) //move it off the block to the right
                //{
                //    transform.position = new Vector2(transform.position.x + PositionDifference,transform.position.y);
                //}
                //else  //move it off the block to the left
                //{
                //    transform.position = new Vector2(transform.position.x - PositionDifference, transform.position.y);
                //}
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
}
