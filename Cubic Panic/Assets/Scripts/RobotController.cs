using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    // Components
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    public GameObject m_GrabbedBlock;
    private SpriteRenderer m_SpriteRenderer;
    // Player flipping
    private bool m_GoingRight;

    public bool GoingRight
    {
        get { return m_GoingRight; }
        set
        {
            if (m_GoingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            m_GoingRight = value;
        }
    }
    // Inputs
    private float m_Horizontal;
    private float m_Vertical;
    public bool m_GrabPressed;
    private bool m_JumpPressed;
    public bool m_IsActing;
    //Movement variables
    public float m_Speed;
    public float m_JumpForce;
    public bool m_IsGrounded;
    // Grab variables
    public GameObject m_HorizontalGrabCollider;
    public GameObject m_UpGrabCollider;
    public GameObject m_DownGrabCollider;
    //DropBlock positions
    public Transform[] m_HorizontalPositions;
    public Transform[] m_VerticalPositions;
    float m_AuxHorDiff;
    float m_AuxVerDiff;                               
    float m_ClosestHorPos;
    float m_ClosestVerPos;

    // Start is called before the first frame update
    void Start()
    {
        m_IsActing = false;
        m_GoingRight = true;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
        HandleJump();
        HandleGrab();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleInputs()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");
        m_JumpPressed = Input.GetKeyDown(KeyCode.Space);
        m_GrabPressed = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
    }
    private void HandleJump()
    {
        if (m_JumpPressed && m_IsGrounded && !m_IsActing)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce);
        }
    }
    private void HandleMovement()
    {
        if(!m_IsActing)
        {
            // Applying movement
            m_Rigidbody.velocity = new Vector2(m_Horizontal * m_Speed, m_Rigidbody.velocity.y);
            if(GoingRight && m_Horizontal < 0)
            {
                GoingRight = false;
            }
            else if (!GoingRight && m_Horizontal > 0)
            {
                GoingRight = true;
            }
        }


    }
    private void HandleGrab()
    {
        if (m_GrabPressed)
        {
            if (m_GrabbedBlock == null)
            {
                //try to grab something
                if (m_Vertical > 0)
                {
                    m_HorizontalGrabCollider.SetActive(false);
                    m_UpGrabCollider.SetActive(true);
                    m_DownGrabCollider.SetActive(false);
                }
                else if (m_Vertical < 0)
                {
                    m_HorizontalGrabCollider.SetActive(false);
                    m_UpGrabCollider.SetActive(false);
                    m_DownGrabCollider.SetActive(true);
                }
                else
                {
                    m_HorizontalGrabCollider.SetActive(true);
                    m_UpGrabCollider.SetActive(false);
                    m_DownGrabCollider.SetActive(false);
                }
                StartCoroutine(FailedGrab());
            }
            else
            {
                DropBlock();
            }
        }
    }
    public IEnumerator FailedGrab()
    {
        yield return new WaitForSeconds(1);
        m_HorizontalGrabCollider.SetActive(false);
        m_UpGrabCollider.SetActive(false);
        m_DownGrabCollider.SetActive(false);
    }
    public void SuccessfulGrab(GameObject Block)
    {
        StopCoroutine(FailedGrab());
        m_GrabbedBlock = Block;
        m_GrabbedBlock.SetActive(false);
        m_HorizontalGrabCollider.SetActive(false);
        m_UpGrabCollider.SetActive(false);
        m_DownGrabCollider.SetActive(false);
    }

    private void DropBlock()
    {
        float closestHorizontalPos = float.MaxValue;
        float closestVerticalPos = float.MaxValue;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < m_HorizontalPositions.Length; i++)
            {
                float horizontalDiff = Mathf.Abs(m_HorizontalPositions[i].position.y - m_UpGrabCollider.transform.position.y);
                if (horizontalDiff < closestHorizontalPos)
                {
                    closestHorizontalPos = horizontalDiff;
                    m_ClosestHorPos = m_HorizontalPositions[i].position.y;
                }
            }

            for (int j = 0; j < m_VerticalPositions.Length; j++)
            {
                float verticalDiff = Mathf.Abs(m_VerticalPositions[j].position.x - m_UpGrabCollider.transform.position.x);
                if (verticalDiff < closestVerticalPos)
                {
                    closestVerticalPos = verticalDiff;
                    m_ClosestVerPos = m_VerticalPositions[j].position.x;
                }
            }
            m_GrabbedBlock.transform.position = new Vector2(m_ClosestVerPos, m_ClosestHorPos);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
        for (int i = 0; i < m_HorizontalPositions.Length; i++)
            {
                float horizontalDiff = Mathf.Abs(m_HorizontalPositions[i].position.y - m_DownGrabCollider.transform.position.y);
                if (horizontalDiff < closestHorizontalPos)
                {
                    closestHorizontalPos = horizontalDiff;
                    m_ClosestHorPos = m_HorizontalPositions[i].position.y;
                }
            }

            for (int j = 0; j < m_VerticalPositions.Length; j++)
            {
                float verticalDiff = Mathf.Abs(m_VerticalPositions[j].position.x - m_DownGrabCollider.transform.position.x);
                if (verticalDiff < closestVerticalPos)
                {
                    closestVerticalPos = verticalDiff;
                    m_ClosestVerPos = m_VerticalPositions[j].position.x;
                }
            }

            m_GrabbedBlock.transform.position = new Vector2(m_ClosestVerPos, m_ClosestHorPos);
        }
        else
        {
            for (int i = 0; i < m_HorizontalPositions.Length; i++)
            {
                float horizontalDiff = Mathf.Abs(m_HorizontalPositions[i].position.y - m_HorizontalGrabCollider.transform.position.y);
                if (horizontalDiff < closestHorizontalPos)
                {
                    closestHorizontalPos = horizontalDiff;
                    m_ClosestHorPos = m_HorizontalPositions[i].position.y;
                }
            }

            for (int j = 0; j < m_VerticalPositions.Length; j++)
            {
                float verticalDiff = Mathf.Abs(m_VerticalPositions[j].position.x - m_HorizontalGrabCollider.transform.position.x);
                if (verticalDiff < closestVerticalPos)
                {
                    closestVerticalPos = verticalDiff;
                    m_ClosestVerPos = m_VerticalPositions[j].position.x;
                }
            }

            m_GrabbedBlock.transform.position = new Vector2(m_ClosestVerPos, m_ClosestHorPos);
        }

        m_GrabbedBlock.SetActive(true);
        m_GrabbedBlock = null;
    }
}

