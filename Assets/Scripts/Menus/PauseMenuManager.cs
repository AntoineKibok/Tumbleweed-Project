using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public CinemachineVirtualCamera pauseCam;
    public GameObject panel;
    public Volume cinematicVolume, playVolume;
    public AudioSource menuMusic, clic;
    public bool inPause = false;
    
    void Start()
    {
        panel.SetActive(false);
        pauseCam.gameObject.SetActive(false);
    }

    IEnumerator MenuStart()
    {
        pauseCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        Cursor.visible = true;

        panel.SetActive(true);
        playVolume.gameObject.SetActive(false);
        cinematicVolume.gameObject.SetActive(true);
        AudioListener.pause = true;
        menuMusic.ignoreListenerPause = true;
        menuMusic.Play();
        clic.ignoreListenerPause = true;
        Time.timeScale = 0;
        inPause = true;
        yield return null;
    }
    
    public void HideMenu()
    {
        Cursor.visible = false;
        pauseCam.gameObject.SetActive(false);
        panel.SetActive(false);
        playVolume.gameObject.SetActive(true);
        cinematicVolume.gameObject.SetActive(false);
        AudioListener.pause = false;
        menuMusic.ignoreListenerPause = false;
        menuMusic.Stop();
        clic.ignoreListenerPause = false;
        Time.timeScale = 1;
        inPause = false;
    }

    public void SoundClic()
    {
        clic.PlayOneShot(clic.clip);
    }
    
    public void QuitClic()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }
    
    public void GoMenu(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Single);
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (inPause)
            {
                Debug.Log("Hide ?" + inPause);
                HideMenu();
            }

            else
            {
                StartCoroutine(MenuStart());
            }
        }
    }
}

