using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TEMP_StartButton : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject controlsPanel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        SceneManager.LoadScene("Hub");
    }

    public void LoadControlsScene()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        //SceneManager.LoadScene("ControlsScene");
        menuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        menuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        Application.Quit();
    }
}
