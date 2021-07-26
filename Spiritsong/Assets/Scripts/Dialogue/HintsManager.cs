using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using TMPro;

public class HintsManager : MonoBehaviour
{
    // Components needed.
    ConversationManager convoManager;
    AbilityVariableStorage abilityStorage;
    GameObject hint1;
    GameObject hint2;

    // Set in inspector.
    [SerializeField] Animator notificationAnimator;
    [SerializeField] Image popupSprite;
    [SerializeField] TextMeshProUGUI popupText;
    [SerializeField] Sprite[] characterSprites;

    // Start is called before the first frame update
    void Start()
    {
        convoManager = FindObjectOfType<ConversationManager>();
        abilityStorage = FindObjectOfType<AbilityVariableStorage>();

        // Finding the hint objects in the scene.
        hint1 = GameObject.Find("Hint0Trigger");
        hint2 = GameObject.Find("Hint1Trigger");
    }

    private void Update()
    {
        // If you have the mechanics, no longer display hints.
        if (abilityStorage.jumpMechanic)
        {
            hint1.SetActive(false);
        }

        if (abilityStorage.dashMechanic)
        {
            hint2.SetActive(false);
        }

        // If you have the mechanic and the hints are disabled, hide the notification.
        if (abilityStorage.jumpMechanic && !convoManager.pianoHint)
        {
            //notificationAnimator.SetBool("HintAvailable", false);
        }

        if (abilityStorage.dashMechanic && !convoManager.violinHint)
        {
            notificationAnimator.SetBool("HintAvailable", false);
        }
    }

    // If the player is in the designated locations, set hints to be available, and play the notification animation.
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Hint0Trigger" && other.gameObject.tag == "Player")
        {
            convoManager.pianoHint = true;
            popupSprite.sprite = characterSprites[0];
            popupText.text = "I'm here if you need aid!";
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint1Trigger" && other.gameObject.tag == "Player")
        {
            convoManager.violinHint = true;
            popupSprite.sprite = characterSprites[1];
            popupText.text = "Hey, I got a hint for ya!";
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint2Trigger" && other.gameObject.tag == "Player")
        {
            convoManager.fluteHint = true;
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
            convoManager.pianoHint = false;
            notificationAnimator.SetBool("HintAvailable", false);
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            convoManager.violinHint = false;
            notificationAnimator.SetBool("HintAvailable", false);
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            convoManager.fluteHint = false;
            notificationAnimator.SetBool("HintAvailable", false);
        }
    }

    // Hide notifications after 5 seconds.
    IEnumerator HideNotifs()
    {
        yield return new WaitForSeconds(5);
        notificationAnimator.SetBool("HintAvailable", false);
    }
}
