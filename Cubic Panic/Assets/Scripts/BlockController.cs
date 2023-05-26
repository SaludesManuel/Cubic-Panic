using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public BoxCollider2D m_Collider2D;
    public Rigidbody2D m_Rigidbody2D;
    public Transform m_InicioCuadricula;
    public GameObject m_ScoreNumber;
    public enum COLOR
    { 
        RED,
        BLUE,
        GREEN,
        YELLOW,
    }
    public COLOR ThisBlockColor;

    private void Start()
    {
        m_Collider2D = GetComponent<BoxCollider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Rigidbody2D.gravityScale = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer is 7) //if it hits a block or the ground
        {
            
            if (transform.position.y > collision.transform.position.y) //if it's above
            {
                transform.position = new Vector2(collision.gameObject.transform.position.x, transform.position.y);
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
    public float Module(Vector2 Vector)
    {

        float value = Mathf.Sqrt((Vector.x * Vector.x) + (Vector.y * Vector.y));
        return value;
    }
    private void OnEnable()
    {
        Vector2 PositionToWarp = m_InicioCuadricula.position;
        for (float i = m_InicioCuadricula.position.x; i < m_InicioCuadricula.position.x + 8; i += 1)
        {
            for (float j = m_InicioCuadricula.position.y; j > m_InicioCuadricula.position.y - 8; j -= 1)
            {
                Vector2 Distance = new Vector2(i - transform.position.x, j - transform.position.y);
                if (Module(PositionToWarp) < Module(Distance))
                {
                    PositionToWarp = Distance;
                }
            }
        }
        transform.position = PositionToWarp;
    }
    private void OnDestroy()
    {
        m_ScoreNumber.GetComponent<ScoreController>().GainPoints(100);
    }
}
