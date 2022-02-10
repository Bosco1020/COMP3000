using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class coOrdinateSystem : MonoBehaviour
{
    public Vector3 location;
    public Quaternion rotation;
    public string cellType;
    [SerializeField]
    private GameObject objectRef;

    public float x = 1f, y = 1f, z = 1f;

    private void Start()
    {
        location = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
    }

    public void SetObject(GameObject cell)
    {
        objectRef = cell;
        objectRef.transform.rotation = rotation;
    }

    public GameObject GetObject()
    {
        return objectRef;
    }

    void OnDrawGizmos()
    {
        // Draw a cube at the transform position
        if (cellType == "Fibroblast")
        {
            Gizmos.color = Color.blue;
        }
        else if (cellType == "Endothelial")
        {
            Gizmos.color = Color.red;
        }
        else if (cellType == "Arteriole")
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        
        Gizmos.DrawWireCube(transform.position, new Vector3(x, z, y));
        //Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), new Vector3(0.25f, 0.25f, 0.1f));
    }
}
