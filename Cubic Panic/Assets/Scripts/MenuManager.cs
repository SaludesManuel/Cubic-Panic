using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button m_PlayButton;
    public Button m_QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayButton.onClick.AddListener(LoadGame);
        m_PlayButton.onClick.AddListener(ExitGame);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}