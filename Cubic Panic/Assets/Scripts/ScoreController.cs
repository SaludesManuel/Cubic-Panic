using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController m_Score;
    private TextMeshProUGUI m_Text;
    public int m_Points = 0;
    public void Awake()
    {
        //Singleton
        if (m_Score != null && m_Score != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Score = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        GainPoints(-m_Points); 
    }
    public void GainPoints(int PointsGet)
    {
        m_Points += PointsGet;
        m_Text.text = m_Points.ToString();
    }
}
