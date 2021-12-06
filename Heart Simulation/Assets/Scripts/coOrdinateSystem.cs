using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coOrdinateSystem : MonoBehaviour
{
    public Vector3 location;
    public string cellType;

    private void Start()
    {
        location = gameObject.transform.position;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a cube at the transform position
        if (cellType == "Type A")
        {
            Gizmos.color = Color.blue;
        }
        else if (cellType == "Type B")
        {
            Gizmos.color = Color.red;
        }
        else if (cellType == "Type C")
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }

        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
