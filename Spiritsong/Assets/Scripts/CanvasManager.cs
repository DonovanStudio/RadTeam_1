using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject controlsPanel;
    public hubaudiomanager hubAudioManager;

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
        hubAudioManager = GameObject.Find("Audio Manager").GetComponent("Hubaudiomanager") as hubaudiomanager;
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
            hubAudioManager.StopDialogueAudio();
            SceneManager.LoadScene(2);
        }
    }
}
