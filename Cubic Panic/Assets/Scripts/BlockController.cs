using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private BoxCollider2D m_Collider2D;
    private Rigidbody2D m_Rigidbody2D;
    private SpriteRenderer m_SpriteRenderer;
    private Transform m_Transform;
    public Transform m_InicioCuadricula;
    //Point related variables
    public ScoreController m_Score;
    public int m_PointsGained;
    //differenciate the color of the block
    public enum COLOR
    { 
        RED,
        BLUE,
        YELLOW,
        GREEN,
    }
    public COLOR ThisBlockColor;

    private void Start()
    {
        m_Collider2D = GetComponent<BoxCollider2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Transform = GetComponent<Transform>();
        m_Rigidbody2D.gravityScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Grabber"))
        {
            other.gameObject.GetComponentInParent<RobotController>().SuccessfulGrab(gameObject);
        }
    }
    public float Module(Vector2 Vector)
    {
        float value = Mathf.Sqrt((Vector.x * Vector.x) + (Vector.y * Vector.y));
        return value;
    }
    //public Vector2 DeterminePosition()
    //{
    //    Vector2 PositionToWarp = m_InicioCuadricula.position;
    //    for (float i = m_InicioCuadricula.position.x; i < m_InicioCuadricula.position.x + 8.08f; i += 1.01f)
    //    {
    //        for (float j = m_InicioCuadricula.position.y; j > m_InicioCuadricula.position.y - 8.16f; j -= 1.02f)
    //        {
    //            Vector2 Distance = new Vector2(i+m_InicioCuadricula.position.x - transform.position.x, 
    //                m_InicioCuadricula.position.y - j - transform.position.y);
    //            if (Module(PositionToWarp) < Module(Distance))
    //            {
    //                PositionToWarp = Distance;
    //            }
    //        }
    //    }
    //    return PositionToWarp;
    //}
    private void OnDestroy()
    {
        m_Score.GainPoints(m_PointsGained);
    }
}
