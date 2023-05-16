using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    // Start is called before the first frame update
    // Life and knockback
    // Invencible after hit

    // Components
    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private Transform m_Transform;
    public enum BLOCK_CARRIED
    {
        NOTHING,
        BLUE,
        GREEN,
        YELLOW,
        RED
    }

    public BLOCK_CARRIED m_ThisBlockCarried;

    // Inputs
    private float m_Horizontal;
    private bool m_Grabbed;
    private bool m_JumpPressed;

    // Horizontal movement variables
    public float m_Speed;


    // Jump variables
    public float m_JumpForce;
    public float m_GravityScaleJumping = 1f;
    public float m_GravityScaleFalling = 1f;
    public bool m_IsGrounded;

    // Attack variables
    public bool m_IsActing;

    // Grab variables
    public GameObject m_HorizontalGrabCollider;
    public GameObject m_UpGrabCollider;
    public GameObject m_DownGrabCollider;

    // Bow Variables
    public GameObject m_Block;
    public Transform m_ArrowSpawnPoint;

    //Interact Variable
    public Vector2 m_CheckPoint;

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


    void Start()
    {
        m_GoingRight = true;
        m_ThisBlockCarried = BLOCK_CARRIED.NOTHING;
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Transform = GetComponent<Transform>();
    }

    private void Update()
    {
        HandleInputs();
        HandleJump();
        HandleAnimations();
        HandleDeath();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInputs()
    {
            m_Horizontal = Input.GetAxisRaw("Horizontal");
            m_JumpPressed = Input.GetKeyDown(KeyCode.Space);
            m_Grabbed = Input.GetKeyDown(KeyCode.LeftShift & KeyCode.RightShift);
            

    }

    private void HandleJump()
    {
        if (m_JumpPressed && m_IsGrounded && !m_IsActing)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce);
        }
        if (m_Rigidbody.velocity.y > 0)
        {
            m_Rigidbody.gravityScale = m_GravityScaleJumping;
        }
        else if (m_Rigidbody.velocity.y < 0)
        {
            m_Rigidbody.gravityScale = m_GravityScaleFalling;
        }
    }

    private void HandleMovement()
    {
        // Running
        float speedMultiplier = 1;
        if (m_IsActing)
        {
            speedMultiplier = 0;
        }

        // Applying movement
        m_Rigidbody.velocity = new Vector2(m_Horizontal * m_Speed * speedMultiplier, m_Rigidbody.velocity.y);

    }

    private void HandleAnimations()
    {
        // Blend tree movement animation
        float animatorSpeed = Mathf.Abs(m_Horizontal);
        m_Animator.SetFloat("Speed", animatorSpeed);

        m_Animator.ResetTrigger("Jump");
        if (m_JumpPressed)
        {
            m_Animator.SetTrigger("Jump");
        }

        if (!m_IsGrounded && m_Rigidbody.velocity.y < 0)
        {
            m_Animator.SetBool("Falling", true);
        }
        else
        {
            m_Animator.SetBool("Falling", false);
        }

        m_Animator.SetBool("Grounded", m_IsGrounded);
        if (m_Grabbed)
        {
           // if(Input.GetKeyDown(KeyCode.W))
          //  {
         //       m_Animator.SetTrigger("UpGrab");
          //  }
            if (Input.GetKeyDown(KeyCode.S))
            {
                m_Animator.SetTrigger("DownGrab");
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                m_Animator.SetTrigger("Grab");
                ActivateGrabCollider();
            }

        }
        // Flip the player
        if (!m_IsActing)
        {
            if (GoingRight && m_Horizontal < 0)
            {
                GoingRight = false;
            }
            else if (!GoingRight && m_Horizontal > 0)
            {
                GoingRight = true;
            }
        }
    }

    public void StartedAction()
    {
        m_IsActing = true;
    }

    public void FinishedAction()
    {
        m_IsActing = false;
    }

    public void ActivateGrabCollider()
    {
        switch(m_ThisBlockCarried)
        {
            case BLOCK_CARRIED.NOTHING:
            m_HorizontalGrabCollider.SetActive(true);
            break;
            case BLOCK_CARRIED.BLUE:
           
                break;
            case BLOCK_CARRIED.RED:
                m_Block.gameObject.GetComponent<BlockController>().ActivateBlock();
                break;
            case BLOCK_CARRIED.GREEN:

                break;
            case BLOCK_CARRIED.YELLOW:

                break;
        }
    }

    public void DeactivateUpGrabCollider()
    {
        m_UpGrabCollider.SetActive(false);
    }
    public void ActivateInteractor()
    {

    }
    public void DeactivateInteractor()
    {

    }

    private void SpawnArrow()
    {
        //GameObject newArrow = GameObject.Instantiate(m_Arrow, m_ArrowSpawnPoint.position, m_ArrowSpawnPoint.rotation);
        //newArrow.GetComponent<ArrowController>().ShootArrow(GoingRight);
    }

    private void HandleDeath()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

}
