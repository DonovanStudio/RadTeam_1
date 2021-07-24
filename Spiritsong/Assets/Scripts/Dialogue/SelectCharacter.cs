using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Yarn.Unity;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Used to allow the player to select the character free of the canvas.
    public GameObject spotlight;
    public bool canSpeak;
    DialogueRunner dialogueRunner;
    DialogueControls dialogueControls;
    public GameObject[] canvasSprites;
    AbilityVariableStorage abilityVar;
    ConversationManager conversationManager;

    void Start()
    {
        AddPhysicsRaycaster();
        spotlight.SetActive(false);

        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueControls = FindObjectOfType<DialogueControls>();
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
        conversationManager = FindObjectOfType<ConversationManager>();
    }

    // Allow the player to select sprites not on the UI.
    void AddPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    // Detect if the Cursor starts to pass over the GameObject.
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        spotlight.SetActive(true);
        SetObject();
        canSpeak = true;
    }

    // Detect when Cursor leaves the GameObject.
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        spotlight.SetActive(false);
        canSpeak = false;

        if (dialogueControls.dialogueStart is false)
        {
            dialogueRunner.startNode = "";
        }
    }

    void SetObject()
    {
        // Set the nodes for the Piano.
        if (name is "Piano Actor" && abilityVar.walkMechanic)
        {
            conversationManager.SetPianoConversations();
            PianoUISprite();
        }

        // Set the nodes for the Violin.
        if (name is "Violin Actor" && abilityVar.jumpMechanic)
        {
            conversationManager.SetViolinConversations();
            ViolinUISprite();
        }

        // Set the nodes for the Flute.
        if (name is "Flute Actor" && abilityVar.dashMechanic)
        {
            conversationManager.SetFluteConversations();
            FluteUISprite();
        }

        // Set the active sprite on the UI. Called in SetObject();
        void PianoUISprite()
        {
            canvasSprites[0].SetActive(true);
            canvasSprites[1].SetActive(false);
            canvasSprites[2].SetActive(false);
        }

        void ViolinUISprite()
        {
            canvasSprites[0].SetActive(false);
            canvasSprites[1].SetActive(true);
            canvasSprites[2].SetActive(false);
        }

        void FluteUISprite()
        {
            canvasSprites[0].SetActive(false);
            canvasSprites[1].SetActive(false);
            canvasSprites[2].SetActive(true);
        }
    }
}
