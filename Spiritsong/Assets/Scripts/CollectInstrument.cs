using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInstrument : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "ViolinSpirit" && GameManager.instance.GetJump())
        {
            Destroy(gameObject);
        }

        if (gameObject.name == "FluteSpirit" && GameManager.instance.GetDash())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
