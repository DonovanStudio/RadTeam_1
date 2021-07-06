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
        SceneManager.LoadScene("Hub");
    }

    public void LoadControlsScene()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
