using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsAnimation : MonoBehaviour
{
    [SerializeField] Animator creditsAnimator;
    [SerializeField] Animator logoAnimator;

    public void LogoTrigger()
    {
        logoAnimator.SetTrigger("Opacity");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
