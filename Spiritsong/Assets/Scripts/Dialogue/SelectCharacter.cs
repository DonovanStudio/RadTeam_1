using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Yarn.Unity;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject spotlight;
    public bool canSpeak;
    DialogueRunner dialogueRunner;
    DialogueControls dialogueControls;
    public GameObject[] canvasSprites;
    AbilityVariableStorage abilityVar;

    void Start()
    {
        AddPhysicsRaycaster();
        spotlight.SetActive(false);
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueControls = FindObjectOfType<DialogueControls>();
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
    }

    void AddPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = GameObject.FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        spotlight.SetActive(true);
        SetObject();
        canSpeak = true;
    }

    //Detect when Cursor leaves the GameObject
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
        if (name is "Piano Actor" && abilityVar.walkMechanic && !abilityVar.hintsAvailable[0])
        {
            dialogueRunner.startNode = "PianoMeet";
            PianoUISprite();
        } else if (name is "Piano Actor" && abilityVar.walkMechanic && abilityVar.hintsAvailable[0])
        {
            dialogueRunner.startNode = "Hint1";
            PianoUISprite();
        }

        // Set the nodes for the Violin.
        if (name is "Violin Actor" && abilityVar.jumpMechanic && !abilityVar.hintsAvailable[1])
        {
            dialogueRunner.startNode = "ViolinMeet";
            ViolinUISprite();
        } else if (name is "Violin Actor" && abilityVar.jumpMechanic && abilityVar.hintsAvailable[1])
        {
            dialogueRunner.startNode = "Hint2";
            ViolinUISprite();
        }

        // Set the nodes for the Flute.
        if (name is "Flute Actor" && abilityVar.dashMechanic && !abilityVar.hintsAvailable[2])
        {
            dialogueRunner.startNode = "FluteMeet";
            FluteUISprite();
        } else if (name is "Flute Actor" && abilityVar.dashMechanic && abilityVar.hintsAvailable[2])
        {
            dialogueRunner.startNode = "Hint3";
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
