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
    bool pianoViolinVar;

    // Variables to progress dialogue in Unity scene.
    bool pianoViolin;
    [Header("Characters Met")]
    public bool pianoMet;
    public bool violinMet;
    public bool fluteMet;
    [Header("Hints Available")]
    public bool pianoHint;
    public bool violinHint;
    public bool fluteHint;

    // Audio 
    public FMOD.Studio.EventInstance pianoMusic;
    public FMOD.Studio.EventInstance violinMusic;

    // Component management.
    AbilityVariableStorage abilityStorage;
    DialogueRunner dialogueRunner;
    InMemoryVariableStorage varStorage;
    Scene scene;

    void Start()
    {
        pianoMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Piano Friendship Theme");
        violinMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Violin Friendship Theme");
        // fluteMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Flute Friendship Theme");
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

        if (abilityStorage.jumpMechanic)
        {
            pianoHint = false;
        } else if (abilityStorage.dashMechanic)
        {
            violinHint = false;
        }
    }

    // Capturing variables from Yarn.
    void YarnVariables()
    {
        pianoMetVar = varStorage.GetValue("$pianoMet").AsBool;
        violinMetVar = varStorage.GetValue("$violinMet").AsBool;
        fluteMetVar = varStorage.GetValue("$fluteMet").AsBool;
        pianoViolinVar = varStorage.GetValue("$pianoViolin").AsBool;
        VariableUpdater();
    }

    // Setting the nodes for the Piano.
    public void PianoConversations()
    {
        // Initial conversation.
        if (!pianoMet)
        {
            dialogueRunner.startNode = "PianoMeet";
        }

        // Hint dialogue that plays when conditions are met.
        if (pianoMet && pianoHint)
        {
            dialogueRunner.startNode = "Hint1";
        }

        // Dialogue from Piano. Plays when hints are not active, and should return to this after the dialogue about Violin has played.
        if (pianoMet && !pianoHint || pianoViolin)
        {
            dialogueRunner.startNode = "PianoRandom1";
        }

        // Dialogue from Piano about Violin. Only plays once, when the Piano AND Violin are met, hints are not active, and it has not played.
        if (pianoMet && violinMet && !pianoHint && !pianoViolin)
        {
            dialogueRunner.startNode = "PianoRandom2";
        }
    }

    // Setting the nodes for the Violin.
    public void ViolinConversations()
    {
        if (!violinMet)
        {
            dialogueRunner.startNode = "ViolinMeet";
        }

        if (violinMet && !violinHint)
        {
            dialogueRunner.startNode = "ViolinRandom1";
        }

        if (violinMet && violinHint)
        {
            dialogueRunner.startNode = "Hint2";
        }
    }

    // Setting the nodes for the Violin.
    public void FluteConversations()
    {
        if (!fluteMet)
        {
            dialogueRunner.startNode = "FluteMeet";
        }

        if (fluteMet && !fluteHint)
        {
            dialogueRunner.startNode = "FluteRandom1";
        }

        if (fluteMet && fluteHint)
        {
            dialogueRunner.startNode = "Hint3";
        }
    }

    // Updating the variables ONCE after they're captured from Yarn.
    void VariableUpdater()
    {
        if (pianoMetVar)
        {
            pianoMet = true;
        }

        if (pianoViolinVar)
        {
            pianoViolin = true;
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
    public void PlayDialogueAudio()
    {
        if (!pianoMet)
        {
            pianoMusic.start();
        }
        else if (pianoMet && pianoHint)
        {
            pianoMusic.start();
        }
        else if (pianoMet && !pianoHint || pianoViolin)
        {
            pianoMusic.start();
        }
        else if (pianoMet && violinMet && !pianoHint && !pianoViolin)
        {
            pianoMusic.start();
        }
        else if (!violinMet)
        {
            violinMusic.start();
        }
        else if (violinMet && !violinHint)
        {
            violinMusic.start();
        }
        else if (violinMet && violinHint)
        {
            violinMusic.start();
        }
        else if (!fluteMet)
        {
            // fluteMusic.start();
        }
        else if (fluteMet && !fluteHint)
        {
            // fluteMusic.start();
        }
        else if (fluteMet && fluteHint)
        {
            // fluteMusic.start();
        }
    }
    public void StopDialogueAudio()
    {
        pianoMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        pianoMusic.release();
        violinMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        violinMusic.release();

    }

}

