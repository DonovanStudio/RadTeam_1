using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hubaudiomanager : MonoBehaviour
{
    // Audio 
    public FMOD.Studio.EventInstance pianoMusic;
    public FMOD.Studio.EventInstance violinMusic;
    public FMOD.Studio.EventInstance fluteMusic;
    public ConversationManager conversationManager;

    // Start is called before the first frame update
    void Start()
    {
        pianoMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Piano Friendship Theme");
        violinMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Violin Friendship Theme");
        fluteMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Flute Friendship Theme");
        conversationManager = GameObject.Find("Ability Variable Storage").GetComponent("ConversationManager") as ConversationManager;
    }

    public void PlayDialogueAudio()
    {
        if (conversationManager.talkingPiano)
        {
            pianoMusic.start();
        }
        else if (conversationManager.talkingViolin)
        {
            violinMusic.start();
        }else if (conversationManager.talkingFlute)
        {
            fluteMusic.start();
        }

    }

    public void StopDialogueAudio()
    {
        pianoMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
        violinMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        fluteMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        

    }
    
    void Update()
    {
        //Debug.Log(conversationManager.talkingPiano);
    }
}
