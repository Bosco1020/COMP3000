using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DisplayCell : MonoBehaviour
{
    public cellTemplate[] cell;

    public GameObject centre;

    CellPooler cellPooler;

    public void Start()
    {
        cellPooler = CellPooler.Instance;
        //current.transform.parent = this.transform;
        //Instantiate(current, transform.position, transform.rotation);
    }

    public void Spawn(string tag)
    {
        Color color = Color.white;
        string model = "";

        Vector3 temp = centre.transform.position;
        temp.x += 1;
        centre.transform.position = temp;

        foreach (cellTemplate template in cell)
        {
            if (template.name == tag)
            {
                color = template.value;
                model = template.modelPrefab;
            }
        }

        GameObject spawned = cellPooler.SpawnFromPool(model, temp, Quaternion.identity);
        var renderer = spawned.GetComponent<Renderer>();
        //getComponent isn't very efficient

        renderer.material.SetColor("_Color", color);
    }

    public GameObject Spawn(string tag, Vector3 pos, Transform parent)
    {
        string model = "";

        foreach (cellTemplate template in cell)
        {
            if (template.name == tag)
            {
                model = template.modelPrefab;
            }
        }

        GameObject spawned = cellPooler.SpawnFromPool(model, pos, Quaternion.identity);
        spawned.transform.SetParent(parent);
        //getComponent isn't very efficient

        //renderer.material.SetColor("_Color", color);

        return spawned;
    }

}
