using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueControls : MonoBehaviour
{
    // Scripts
    DialogueUI dialogueUI;
    DialogueRunner dialogueRunner;
    InMemoryVariableStorage varStorage;
    SelectStartNode selectStart;

    // UI Elements
    GameObject dialogueCanvas;
    GraphicRaycaster graphicRaycaster;
    [SerializeField] TextMeshProUGUI[] nameLabels;
    [SerializeField] Image[] dialogueBoxes; // 0 is player tag, 1 is NPC tag, 2 is textbox
    [SerializeField] Sprite[] tagSprites;
    [SerializeField] Sprite[] containerSprites;
    string speakerName;
    public bool dialogueStart;

    public hubaudiomanager hubAudioManager;

    [Header("Camera Animations")]
    public Animator cameraAnimator;
    public Animator panelOpacity;

    void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        varStorage = FindObjectOfType<InMemoryVariableStorage>();
        selectStart = FindObjectOfType<SelectStartNode>();

        dialogueCanvas = GameObject.Find("Dialogue Canvas");
        graphicRaycaster = dialogueCanvas.GetComponent<GraphicRaycaster>();
        graphicRaycaster.enabled = false;

        // Getting variables from Yarn Spinner.
        speakerName = varStorage.GetValue("$name").AsString;

        // Lock the cursor to the game view.
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        GameManager.instance.SceneFinishedLoading();
        //hubAudioManager = GameObject.Find("Audio Manager").GetComponent("Hubaudiomanager") as hubaudiomanager;
    }

    void Update()
    {
        SetName();
    }

    // Captures name values from Yarn scripts to set in the UI.
    void SetName()
    {
        // Continue updating the variables from Yarn Spinner.
        speakerName = varStorage.GetValue("$name").AsString;

        // Update name label to new speaker name.
        nameLabels[0].text = speakerName;
        nameLabels[1].text = speakerName;

        if (speakerName is "Conductor")
        {
            dialogueBoxes[0].sprite = tagSprites[0];
            dialogueBoxes[2].sprite = containerSprites[0];
            dialogueBoxes[2].type = Image.Type.Sliced;
        } else if (speakerName is "Piano")
        {
            dialogueBoxes[1].sprite = tagSprites[1];
            dialogueBoxes[2].sprite = containerSprites[1];
            dialogueBoxes[2].type = Image.Type.Sliced;
        } else if (speakerName is "Violin")
        {
            dialogueBoxes[1].sprite = tagSprites[2];
            dialogueBoxes[2].sprite = containerSprites[2];
            dialogueBoxes[2].type = Image.Type.Sliced;
        } else if (speakerName is "Flute")
        {
            dialogueBoxes[1].sprite = tagSprites[3];
            dialogueBoxes[2].sprite = containerSprites[3];
            dialogueBoxes[2].type = Image.Type.Sliced;
        }
    }

    // Skip to the next line on input.
    public void Skip(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dialogueUI.MarkLineComplete();
        }
    }

    // If clicking with the mouse in the scene on the right canvas, move to the conversation.
    public void Mouse(InputAction.CallbackContext context)
    {
        if (context.started && selectStart.canSpeak)
        {
            graphicRaycaster.enabled = true;
            MoveToConversation();
        }
    }

    // Move the camera forwards when conversation starts.
    public void MoveToConversation()
    {
        cameraAnimator.SetBool("Camera Move", true);
        StartCoroutine(WaitToStartDialogue());
        StartCoroutine(DarkenScreen());
        dialogueStart = true;
    }

    // Move the camera back when dialogue complete-- set in Dialogue Runner.
    public void MoveToHub()
    {
        cameraAnimator.SetBool("Camera Move", false);
        dialogueStart = false;
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
        graphicRaycaster.enabled = false;
    }

    // Close the hub and return to the level
    public void OnCloseHub()
    {
        if (!GameManager.instance.GetLoadingStatus())
        {
            hubAudioManager.StopDialogueAudio();
            GameManager.instance.StartSceneLoading();
            SceneManager.LoadScene(2);
        }
    }
}
