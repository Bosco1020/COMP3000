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

    private Material[] Mat;
    private Material originalMat;

    private bool start = true, changed = false;

    public float x = 1f, y = 1f, z = 1f;

    private void Start()
    {
        location = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
    }

    public void SetObject(GameObject cell)
    {
        //set the reference equal to the first child
        objectRef = cell.transform.GetChild(0).gameObject;
        cell.transform.rotation = rotation;

        //Runs once on the first call, saving the original material
        if (!start)
        {
            originalMat = objectRef.GetComponent<Renderer>().material;
            start = false;
        }
        else
        {
            if (!changed) { return; }

            //after the first time, update the new cell with the saved material if changes are made
            objectRef.GetComponent<Renderer>().material = Mat[0];

            //If it has sub-aspectc (nuclei etc) then apply those aswell
            if (Mat.Length > 1)
            {
                Material[] children = objectRef.GetComponentsInChildren<Material>();
                for (int i = 0; i < children.Length; i++)
                {
                    children[i] = Mat[i + 1];
                }
            }
        }
    }

    public GameObject GetObject()
    {
        return objectRef;
    }

    public bool returnChange()
    {
        return changed;
    }

    public void setChanged(bool value)
    {
        changed = value;
    }

    public void setNewMaterial(Material newMat, int index)
    {
        Mat[index] = newMat;
    }

    public Material getOriginalMaterial()
    {
        return originalMat;
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
