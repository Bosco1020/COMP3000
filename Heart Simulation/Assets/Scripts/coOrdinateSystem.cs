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
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}
