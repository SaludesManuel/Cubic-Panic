using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum blockColor
    { 
        NONE,
        RED,
        BLUE,
        YELLOW,
        GREEN,
    }

    public blockColor[][] gameMatrix;

    public Transform[] m_HorizontalPositions;
    public Transform[] m_VerticalPositions;

    public float m_BoxCasterX;
    public float m_BoxCasterY;
    public LayerMask m_LayersToCollide;
    // Start is called before the first frame update
    void Start()
    {
         gameMatrix = new blockColor[10][];

        for(int i=0; i<10; i++)
        {
            gameMatrix[i] = new blockColor[8];
            for(int j=0; j<8; j++)
            {
                gameMatrix[i][j] = blockColor.NONE;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GroundCheck(new Vector3(m_HorizontalPositions[i].position.y,m_VerticalPositions[j].position.x,0));
            }
        }
    }

    bool GroundCheck(Vector3 raycastOrigin)
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.BoxCast(raycastOrigin, new Vector3(m_BoxCasterX, m_BoxCasterY), 0f, Vector3.up, 0, m_LayersToCollide);

        // If it hits something (only can hit with boxes)
        if (hit.collider != null)
        {
            Debug.DrawRay(raycastOrigin + new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.blue);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.blue);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, m_BoxCasterY), Vector2.right * (m_BoxCasterX * 2f), Color.blue);
            return true;
        }
        else
        {
            Debug.DrawRay(raycastOrigin + new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.red);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.red);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, m_BoxCasterY), Vector2.right * (m_BoxCasterX * 2f), Color.red);
            return false;
        }
    } 
}
