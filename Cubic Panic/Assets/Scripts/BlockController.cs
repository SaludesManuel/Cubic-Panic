using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    public GameObject m_Player;
    private RobotController RobotScript;
    // Start is called before the first frame update
    void Start()
    {
        RobotScript = m_Player.GetComponent<RobotController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Grabber"))
        {
            RobotScript.m_Block = this.gameObject;
            this.gameObject.SetActive(false);
            this.gameObject.transform.parent = m_Player.transform;
        }
    }

    public void ActivateBlock()
    {
        this.gameObject.SetActive(true);
        this.gameObject.transform.parent = null;
    }
}
