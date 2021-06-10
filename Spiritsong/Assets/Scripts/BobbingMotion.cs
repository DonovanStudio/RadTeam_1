using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingMotion : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Bobbing motion
    private Vector3 tempPos = new Vector3();
    private Vector3 posOffset = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = new Vector3(currentPos.x, tempPos.y, currentPos.z);
    }
}
