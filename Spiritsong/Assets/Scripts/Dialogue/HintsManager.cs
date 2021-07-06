using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class HintsManager : MonoBehaviour
{
    AbilityVariableStorage abilityVar;
    [SerializeField] Animator notificationAnimator;
    [SerializeField] Image popupSprite;
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
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            abilityVar.hintsAvailable[1] = true;
            popupSprite.sprite = characterSprites[1];
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            abilityVar.hintsAvailable[2] = true;
            popupSprite.sprite = characterSprites[2];
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
