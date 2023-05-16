using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_RaycastLenght;
    public LayerMask m_LayersToCollide;
    private RobotController m_PlayerController;
    private BoxCollider2D m_BoxCollider;

    public float m_BoxCasterX;
    public float m_BoxCasterY;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
        m_PlayerController = GetComponent<RobotController>();
    }

    private void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_RaycastLenght, m_LayersToCollide);
        //// Has raycast collided with something?
        //// Yes
        //if(hit.collider != null)
        //{
        //    Debug.DrawLine(transform.position, transform.position + Vector3.down * m_RaycastLenght, Color.red, 0.1f);
        //    m_PlayerController.m_IsGrounded = true;
        //}
        //// No
        //else
        //{
        //    Debug.DrawLine(transform.position, transform.position + Vector3.down * m_RaycastLenght, Color.blue, 0.1f);
        //    m_PlayerController.m_IsGrounded = false;
        //}
        GroundCheckUsingBox();



    }

    private void GroundCheckUsingBox()
    {
        Vector3 raycastOrigin = m_BoxCollider.bounds.center
            - new Vector3(0, m_BoxCollider.bounds.extents.y, 0);

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.BoxCast(raycastOrigin, new Vector3(m_BoxCasterX, m_BoxCasterY), 0f, Vector3.up, 0, m_LayersToCollide);

        // If it hits something...
        if (hit.collider != null)
        {
            Debug.DrawRay(raycastOrigin + new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.blue);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.blue);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, m_BoxCasterY), Vector2.right * (m_BoxCasterX * 2f), Color.blue);
            m_PlayerController.m_IsGrounded = true;
        }
        else
        {
            Debug.DrawRay(raycastOrigin + new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.red);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, 0), Vector2.down * (m_BoxCasterY), Color.red);
            Debug.DrawRay(raycastOrigin - new Vector3(m_BoxCasterX, m_BoxCasterY), Vector2.right * (m_BoxCasterX * 2f), Color.red);
            m_PlayerController.m_IsGrounded = false;
        }
    }
}

