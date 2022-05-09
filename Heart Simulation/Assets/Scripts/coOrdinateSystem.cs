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

    private List<Material> newMats = new List<Material>();
    public List<Material> originalMat = new List<Material>();

    private bool start = true, changed = false;

    [Header ("Control Gizmo Size")]
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

        objectRef.GetComponent<ObjToCellLink>().setLink(this);

        //Runs once on the first call, saving the original material
        if (start)
        {
            Renderer[] temp = cell.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < temp.Length; i++)
            {
                originalMat.Add(temp[i].material);
            }
            start = false;
        }
        else
        {
            if (!changed) { return; }
            //after the first time, update the new cell with the saved material if changes are made
            objectRef.GetComponent<Renderer>().material = newMats[0];

            //If it has sub-aspect (nuclei etc) then apply those aswell
            if (newMats.Count > 1)
            {
                Renderer[] children = objectRef.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < children.Length -1; i++)
                {
                    //we skip the parents material, so apply the new materials counting from array locaiton 1
                    children[i +1].material = newMats[i + 1];
                }
            }
        }
    }

    public List<Material> returnCurrentMat()
    {
        if(changed)
        {
            return newMats;
        }

        return originalMat;
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
        //check whether the list has been initialised or not,
        //decides whether updating current values or adding to list

        if (newMats.Count >= index +1)
        {
            //if it exists, then update
            newMats[index] = (new Material(newMat.shader));
        }
        else
        {
            newMats.Add(new Material(newMat.shader));
            newMats[index].SetFloat("_Mode", 2); //Set material to Fade
        }

        newMats[index].color = newMat.color;

        if(index == 0)
        {//if 0, the aplying to parent material
            objectRef.GetComponent<Renderer>().material = newMats[index];
        }
        else
        {
            Renderer[] temp = objectRef.GetComponentsInChildren<Renderer>();
            temp[index - 1].material = newMats[index];
        }
    }

    public Material getOriginalMaterial(int index)
    {
        return originalMat[index];
    }

    public List<Renderer> returnRenderers()
    {
        List<Renderer> temp = new List<Renderer>();

        temp.Add(objectRef.GetComponent<Renderer>());
        if(temp[0] != objectRef.GetComponentInChildren<Renderer>())
        {
            temp.AddRange(objectRef.GetComponentsInChildren<Renderer>());
        }

        return temp;
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
