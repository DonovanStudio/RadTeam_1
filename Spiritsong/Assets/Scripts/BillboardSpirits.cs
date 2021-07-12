using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSpirits : MonoBehaviour
{
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(playerCamera.transform);


        transform.rotation = playerCamera.transform.rotation;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
