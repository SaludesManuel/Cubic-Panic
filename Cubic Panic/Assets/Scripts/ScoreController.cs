using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController m_Score;
    private TextMeshProUGUI m_Text;
    public int m_Points = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        m_Points = 0; m_Text.text = 0.ToString();
    }
    public void GainPoints(int PointsGet)
    {
        m_Points += PointsGet;
        m_Text.text = m_Points.ToString();
    }
}
