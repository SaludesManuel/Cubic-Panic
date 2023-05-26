using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    public GameObject m_CurrentBlock;
    public GameObject[] m_BlockList; //Rojo, Azul, Amarillo, Verde
    public Transform[] m_SpawnLocations;
    private Rigidbody2D BlockRigidbody;
    private bool m_CanSpawnABlock;
    // Start is called before the first frame update
    void Start()
    {
        BlockRigidbody = m_CurrentBlock.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MakeNewBlock());
    }
    public IEnumerator MakeNewBlock()
    {
        if (m_CanSpawnABlock)
        {
            m_CanSpawnABlock = false;
            yield return new WaitForSecondsRealtime(2);
            //Move the previous block to its spot and let it fall
            int RandomNumber = Random.Range(0, 7);
            m_CurrentBlock.transform.position = m_SpawnLocations[RandomNumber].transform.position;
            BlockRigidbody.gravityScale = 1;
            //Make a new block
            RandomNumber = Random.Range(0, 3);
            m_CurrentBlock = m_BlockList[RandomNumber];
            GameObject newBlock = GameObject.Instantiate(m_CurrentBlock, transform.position, transform.rotation);
        }
        
    }
}
