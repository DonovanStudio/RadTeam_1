using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEmission : MonoBehaviour
{
    [SerializeField] float stingerLength;
    [SerializeField] float[] delays; //delays should be absolute
    [SerializeField] Sprite[] notes;
    [SerializeField] GameObject particle;
    [SerializeField] Camera camera;
    float timer;
    int note;
    //Audio
    [FMODUnity.EventRef]
    public string selectsound;
    FMOD.Studio.EventInstance fluteloop;
    // Update is called once per frame

    void Start()
    {
        fluteloop = FMODUnity.RuntimeManager.CreateInstance(selectsound);
        if (GameManager.instance.GetDash())
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fluteloop, GetComponent<Transform>(), GetComponent<Rigidbody>());
        if (timer >= stingerLength)
        {
            timer = 0;
            note = 0;
            CreateNote(note);
            //Debug.Log("Playing Puzzle Sound");
            Playsound();
        }
        if (timer > delays[note])
        {
            if(note < delays.Length -1)
            {
                note++;
                CreateNote(note);
            }
        }
    }
    void CreateNote(int note)
    {
        GameObject p = Instantiate(particle, transform);
        p.GetComponent<BillboardSpirits>().playerCamera = camera;
        p.GetComponent<SpriteRenderer>().sprite = notes[note];

    }
    void Playsound()
    {
        fluteloop.start();
    }

    public void Stopsound()
    {
        fluteloop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);


    }
}
