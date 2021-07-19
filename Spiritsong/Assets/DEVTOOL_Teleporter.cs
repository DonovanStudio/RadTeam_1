using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVTOOL_Teleporter : MonoBehaviour
{
    [SerializeField] Vector3 violin;
    [SerializeField] Vector3 fluteCheckpoint;
    [SerializeField] GameObject haveViolin;
    [SerializeField] Vector3 flute;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(violin, Vector3.one);
        Gizmos.DrawCube(flute, Vector3.one);
    }


}
