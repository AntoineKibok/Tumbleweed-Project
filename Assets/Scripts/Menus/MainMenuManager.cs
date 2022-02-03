using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject mainPanel, creditPanel;

    public AudioSource clic;
    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        creditPanel.SetActive(false);
    }

    public void startClic()
    {
        clic.PlayOneShot(clic.clip);
    }
    
    public void clicQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }
    
    public void startGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Single);
    }
}
