using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using TMPro;

public class DialogueControls : MonoBehaviour
{
    DialogueUI dialogueUI;
    InMemoryVariableStorage variableStorage;
    public TextMeshProUGUI nameLabel;
    string speakerName;

    void Awake()
    {
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
}
