using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using TMPro;

public class HintsManager : MonoBehaviour
{
    AbilityVariableStorage abilityVar;
    [SerializeField] Animator notificationAnimator;
    [SerializeField] Image popupSprite;
    [SerializeField] TextMeshProUGUI popupText;
    [SerializeField] Sprite[] characterSprites;

    // Start is called before the first frame update
    void Start()
    {
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
    }

    // If the player is in the designated locations, set hints to be available, and play the notification animation.
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Hint0Trigger")
        {
            abilityVar.hintsAvailable[0] = true;
            popupSprite.sprite = characterSprites[0];
            popupText.text = "I'm here if you need aid!";
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            abilityVar.hintsAvailable[1] = true;
            popupSprite.sprite = characterSprites[1];
            popupText.text = "Hey, I got a hint for ya!";
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            abilityVar.hintsAvailable[2] = true;
            popupSprite.sprite = characterSprites[2];
            popupText.text = "I might be able to help here!";
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }
    }

    // No hints available if not in the designated location.
    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "Hint0Trigger")
        {
            abilityVar.hintsAvailable[0] = false;
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            abilityVar.hintsAvailable[1] = false;
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            abilityVar.hintsAvailable[2] = false;
        }
    }

    // Hide notifications after 5 seconds.
    IEnumerator HideNotifs()
    {
        yield return new WaitForSeconds(5);
        notificationAnimator.SetBool("HintAvailable", false);
    }
}
