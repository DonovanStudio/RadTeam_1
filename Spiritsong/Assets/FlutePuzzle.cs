using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlutePuzzle : MonoBehaviour
{
    [SerializeField] int[] code;
    [SerializeField] Vector3 directionLaunched;
    [SerializeField] float force;
    [SerializeField] GameObject notes;
    [SerializeField] bool completeMe;
    [SerializeField] int correct;
    Rigidbody rb;
    bool completed = false;
    //Audio
    public TimeEmission timeEmission;
    void Start()
    {
        timeEmission = GameObject.Find("Flute Audio").GetComponent("TimeEmission") as TimeEmission;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + directionLaunched.normalized * force);
        Gizmos.color = Color.red;
    }
    private void Awake()
    {   
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (completeMe)
            PuzzleComplete();
        completeMe = false;
    }
    private void OnCollisionEnter(Collision collision)
    {   
        if(completed)
        {
            rb.velocity = Vector3.zero;
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(rb);
        }
    }
    public void ButtonPressed(int num)
    {
        Debug.Log("num: " + num);
        if (num == code[correct])
        {
            correct++;
            Debug.Log("incremented: " + correct);
        }
        if (correct == code.Length)
            PuzzleComplete();
        //else correct = 0;
        Debug.Log("current: " + correct);

    }
    void PuzzleComplete()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = directionLaunched.normalized * force;
        Destroy(notes);
        completed = true;
        timeEmission.Stopsound();
      
    }
}
