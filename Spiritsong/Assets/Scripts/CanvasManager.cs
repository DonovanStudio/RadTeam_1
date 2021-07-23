using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject controlsPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level Design Scene")
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        //if (SceneManager.GetActiveScene().name == "EndScene")
        //{
        //    GameManager.instance.ResetGame();
        //    StartCoroutine(ReturnToMain());
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Main Menu
    public void LoadGameScene()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        SceneManager.LoadScene(1);
    }

    public void LoadControlsScene()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Menu SFX");
        menuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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

    // End Scene
    private IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    // Hub
    public void OnCloseHub()
    {
        if (SceneManager.GetActiveScene().name == "Hub")
        {
            SceneManager.LoadScene(2);
        }
    }
}
