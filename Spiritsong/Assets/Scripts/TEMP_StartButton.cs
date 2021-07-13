using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEMP_StartButton : MonoBehaviour
{
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
        SceneManager.LoadScene("ControlsScene");
    }

    public void QuitGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        Application.Quit();
    }
}
