using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class HintsManager : MonoBehaviour
{
    AbilityVariableStorage abilityVar;
    [SerializeField] Animator notificationAnimator;

    // Start is called before the first frame update
    void Start()
    {
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Hint0Trigger")
        {
            abilityVar.hintsAvailable[0] = true;
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint1Trigger")
        {
            abilityVar.hintsAvailable[1] = true;
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }

        if (this.gameObject.name == "Hint2Trigger")
        {
            abilityVar.hintsAvailable[2] = true;
            notificationAnimator.SetBool("HintAvailable", true);
            StartCoroutine(HideNotifs());
        }
    }

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

    IEnumerator HideNotifs()
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        notificationAnimator.SetBool("HintAvailable", false);
    }
}
