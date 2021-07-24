using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class ConversationManager : MonoBehaviour
{
    // Variables set in Yarn Script.
    bool pianoMetVar;
    bool violinMetVar;
    bool fluteMetVar;

    // Variables to progress dialogue in Unity scene.
    //[Header("Characters Met")]
    [HideInInspector] public bool pianoMet;
    [HideInInspector] public bool violinMet;
    [HideInInspector] public bool fluteMet;
    //[Header("Hints Available")]
    public bool pianoHint;
    public bool violinHint;
    public bool fluteHint;

    //Audio
    [HideInInspector] public bool talkingPiano;
    [HideInInspector] public bool talkingViolin;
    [HideInInspector] public bool talkingFlute;

    // Component management.
    AbilityVariableStorage abilityStorage;
    DialogueRunner dialogueRunner;
    InMemoryVariableStorage varStorage;
    Scene scene;

    // Conversation enums.
    public PianoConversations pianoDialogue;
    public ViolinConversations violinDialogue;
    public FluteConversations fluteDialogue;

    public enum PianoConversations
    {
        NotMetPiano, PianoWithoutViolin, PianoWithViolin, PianoHint
    }
    public enum ViolinConversations
    {
        NotMetViolin, ViolinRandom, ViolinHint
    }

    public enum FluteConversations
    {
        NotMetFlute, FluteRandom, FluteHint
    }

    private void Awake()
    {
        abilityStorage = GetComponent<AbilityVariableStorage>();
        varStorage = GetComponent<InMemoryVariableStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        // If in the hub, then run dialogue runner commands.
        if (scene.name == "Hub")
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
        }

        YarnVariables();
        SetDialogue();

        if (abilityStorage.jumpMechanic)
        {
            pianoHint = false;
        } else if (abilityStorage.dashMechanic)
        {
            violinHint = false;
        }
    }

    // Switch cases for piano dialogue.
    public void SetPianoConversations()
    {
        switch (pianoDialogue)
        {
            case PianoConversations.NotMetPiano:
                dialogueRunner.startNode = "PianoMeet";
                talkingFlute = false;
                talkingViolin = false;
                talkingPiano = true;
                break;
            case PianoConversations.PianoHint:
                dialogueRunner.startNode = "Hint1";
                talkingFlute = false;
                talkingViolin = false;
                talkingPiano = true;
                break;
            case PianoConversations.PianoWithoutViolin:
                dialogueRunner.startNode = "PianoRandom1";
                talkingFlute = false;
                talkingViolin = false;
                talkingPiano = true;
                break;
            case PianoConversations.PianoWithViolin:
                dialogueRunner.startNode = "PianoRandom2";
                talkingFlute = false;
                talkingViolin = false;
                talkingPiano = true;
                break;
        }
    }

    // Switch cases for violin dialogue.
    public void SetViolinConversations()
    {
        switch (violinDialogue)
        {
            case ViolinConversations.ViolinHint:
                dialogueRunner.startNode = "Hint2";
                talkingFlute = false;
                talkingPiano = false;
                talkingViolin = true;
                break;
            case ViolinConversations.NotMetViolin:
                dialogueRunner.startNode = "ViolinMeet";
                talkingFlute = false;
                talkingPiano = false;
                talkingViolin = true;
                break;
            case ViolinConversations.ViolinRandom:
                dialogueRunner.startNode = "ViolinRandom1";
                talkingFlute = false;
                talkingPiano = false;
                talkingViolin = true;
                break;
        }
    }

    // Switch cases for flute dialogue.
    public void SetFluteConversations()
    {
        switch (fluteDialogue)
        {
            case FluteConversations.FluteHint:
                dialogueRunner.startNode = "Hint3";
                talkingPiano = false;
                talkingViolin = false;
                talkingFlute = true;
                break;
            case FluteConversations.NotMetFlute:
                dialogueRunner.startNode = "FluteMeet";
                talkingPiano = false;
                talkingViolin = false;
                talkingFlute = true;
                break;
            case FluteConversations.FluteRandom:
                dialogueRunner.startNode = "FluteRandom1";
                talkingPiano = false;
                talkingViolin = false;
                talkingFlute = true;
                break;
        }
    }

    // Capturing variables from Yarn.
    void YarnVariables()
    {
        pianoMetVar = varStorage.GetValue("$pianoMet").AsBool;
        violinMetVar = varStorage.GetValue("$violinMet").AsBool;
        fluteMetVar = varStorage.GetValue("$fluteMet").AsBool;
        VariableUpdater();
    }

    // Updating the variables ONCE after they're captured from Yarn.
    void VariableUpdater()
    {
        if (pianoMetVar)
        {
            pianoMet = true;
        }

        if (violinMetVar)
        {
            violinMet = true;
        }

        if (fluteMetVar)
        {
            fluteMet = true;
        }
    }

    // Updating the enums for each character.
    void SetDialogue()
    {
        if (!pianoMet)
            pianoDialogue = PianoConversations.NotMetPiano;
        if (pianoMet && pianoHint)
            pianoDialogue = PianoConversations.PianoHint;
        if (pianoMet && !abilityStorage.jumpMechanic && !pianoHint)
            pianoDialogue = PianoConversations.PianoWithoutViolin;
        if (pianoMet && abilityStorage.jumpMechanic && !pianoHint)
            pianoDialogue = PianoConversations.PianoWithViolin;

        if (!violinMet && abilityStorage.jumpMechanic)
            violinDialogue = ViolinConversations.NotMetViolin;
        if (violinMet && !violinHint)
            violinDialogue = ViolinConversations.ViolinRandom;
        if (violinMet && violinHint)
            violinDialogue = ViolinConversations.ViolinHint;

        if (!fluteMet && abilityStorage.dashMechanic)
            fluteDialogue = FluteConversations.NotMetFlute;
        if (fluteMet && !fluteHint)
            fluteDialogue = FluteConversations.FluteRandom;
        if (fluteMet && fluteHint)
            fluteDialogue = FluteConversations.FluteHint;
    }
}

