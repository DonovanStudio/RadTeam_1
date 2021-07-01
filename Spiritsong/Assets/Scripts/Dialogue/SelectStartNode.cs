using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class SelectStartNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool canSpeak;
    [SerializeField] GameObject pianoParent;
    [SerializeField] GameObject violinParent;
    [SerializeField] GameObject fluteParent;

    AbilityVariableStorage abilityVar;

    void Start()
    {
        AddPhysicsRaycaster();
        abilityVar = FindObjectOfType<AbilityVariableStorage>();
        GetPiano();
        GetViolin();
        GetFlute();
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
        canSpeak = true;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        canSpeak = false;
    }

    void GetPiano()
    {
        // If the player can walk (on by default), show the Piano spirit.
        if (abilityVar.walkMechanic)
        {
            pianoParent.SetActive(true);
        }
        else if (!abilityVar.walkMechanic)
        {
            pianoParent.SetActive(false);
        }
    }

    void GetViolin()
    {
        // If the player can walk (on by default), show the Piano spirit.
        if (abilityVar.jumpMechanic)
        {
            violinParent.SetActive(true);
        }
        else if (!abilityVar.jumpMechanic)
        {
            violinParent.SetActive(false);
        }
    }

    void GetFlute()
    {
        // If the player can dash, show the Flute spirit.
        if (abilityVar.dashMechanic)
        {
            fluteParent.SetActive(true);
        }
        else if (!abilityVar.dashMechanic)
        {
            fluteParent.SetActive(false);
        }
    }
}
