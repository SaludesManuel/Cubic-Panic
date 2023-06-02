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
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
