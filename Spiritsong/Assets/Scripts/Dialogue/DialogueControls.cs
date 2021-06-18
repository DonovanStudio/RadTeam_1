using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using TMPro;

public class DialogueControls : MonoBehaviour
{
    DialogueUI dialogueUI;
    DialogueRunner dialogueRunner;
    InMemoryVariableStorage variableStorage;
    public TextMeshProUGUI nameLabel;
    string speakerName;
    bool dialogueStart;

    [Header("Camera Animations")]
    public Animator cameraAnimator;
    public Animator panelOpacity;

    void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        variableStorage = FindObjectOfType<InMemoryVariableStorage>();

        // Getting variables from Yarn Spinner. //
        speakerName = variableStorage.GetValue("$name").AsString;
    }

    void Update()
    {
        SetName();
    }

    void SetName()
    {
        // Continue updating the variables from Yarn Spinner. //
        speakerName = variableStorage.GetValue("$name").AsString;

        // Update name label to new speaker name. //
        nameLabel.text = speakerName;
    }

    // Skip to the next line on input.
    public void Skip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dialogueUI.MarkLineComplete();
        }
    }

    // Move the camera forwards when conversation starts.
    public void MoveToConversation()
    {
        cameraAnimator.SetBool("Camera Move", true);
        StartCoroutine(WaitToStartDialogue());
        StartCoroutine(DarkenScreen());
    }

    // Move the camera back when dialogue complete-- set in Dialogue Runner.
    public void MoveToHub()
    {
        cameraAnimator.SetBool("Camera Move", false);
    }


    // Wait before starting dialogue.
    IEnumerator WaitToStartDialogue()
    {
        
        yield return new WaitForSeconds(1);

        dialogueRunner.StartDialogue();
    }

    // Darken the screen as camera moves.
    IEnumerator DarkenScreen()
    {
        
        yield return new WaitForSeconds(0.3f);

        panelOpacity.SetBool("Dialogue Start", true);
    }

    // Undarken the screen as dialogue ends.
    public void UndarkenScreen()
    {
        panelOpacity.SetBool("Dialogue Start", false);
    }
}
