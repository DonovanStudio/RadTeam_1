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
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stingerLength)
        {
            timer = 0;
            note = 0;
            CreateNote(note);
            //play stinger here
        }
        if (timer > delays[note])
        {
            if(note < delays.Length)
                note++;
            CreateNote(note);
        }
    }
    void CreateNote(int note)
    {
        GameObject p = Instantiate(particle, transform);
        p.GetComponent<BillboardSpirits>().playerCamera = camera;
        p.GetComponent<SpriteRenderer>().sprite = notes[note];

    }
}
