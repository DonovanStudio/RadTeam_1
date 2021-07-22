using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsAnimation : MonoBehaviour
{
    [SerializeField] Animator creditsAnimator;
    [SerializeField] Animator logoAnimator;

    public void LogoTrigger()
    {
        logoAnimator.SetTrigger("Opacity");
    }
}
