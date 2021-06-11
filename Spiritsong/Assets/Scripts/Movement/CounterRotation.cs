using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotation : MonoBehaviour
{

    // This counter rotates the cube as it spins along with the cylinder
    void Update()
    {
        Vector3 parentRotation = transform.parent.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, parentRotation.z);
    }
}
