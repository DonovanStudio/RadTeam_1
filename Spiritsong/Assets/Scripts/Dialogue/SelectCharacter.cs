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
        //Debug.Log("Cursor Entering " + name + " GameObject");
        spotlight.SetActive(true);
        SetObject();
        canSpeak = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Debug.Log("Cursor Exiting " + name + " GameObject");
        spotlight.SetActive(false);
        canSpeak = false;

        if (dialogueControls.dialogueStart is false)
        {
            dialogueRunner.startNode = "";
        }
    }

    void SetObject()
    {
        if (name is "Piano Actor" && abilityVar.walkMechanic is true)
        {
            dialogueRunner.startNode = "PianoMeet";
            canvasSprites[0].SetActive(true);
            canvasSprites[1].SetActive(false);
            canvasSprites[2].SetActive(false);
        }
        else if (name is "Violin Actor" && abilityVar.jumpMechanic is true)
        {
            dialogueRunner.startNode = "ViolinMeet";
            canvasSprites[0].SetActive(false);
            canvasSprites[1].SetActive(true);
            canvasSprites[2].SetActive(false);
        }
        else if (name is "Flute Actor" && abilityVar.dashMechanic is true)
        {
            dialogueRunner.startNode = "FluteMeet";
            canvasSprites[0].SetActive(false);
            canvasSprites[1].SetActive(false);
            canvasSprites[2].SetActive(true);
        }
    }
}
