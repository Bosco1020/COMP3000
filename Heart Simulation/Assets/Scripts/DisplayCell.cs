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
    }

    public void Spawn(string tag)
    {
        string model = "";

        Vector3 temp = centre.transform.position;
        temp.x += 1;
        centre.transform.position = temp;

        foreach (cellTemplate template in cell)
        {
            if (template.name == tag)
            {
                model = template.modelPrefab;
            }
        }

        GameObject spawned = cellPooler.SpawnFromPool(model, temp, Quaternion.identity);
        var renderer = spawned.GetComponent<Renderer>();
        //getComponent isn't very efficient
    }

    public GameObject Spawn(string tag, Vector3 pos, Transform parent)
    {
        string model = "";
        AnimationClip clip = new AnimationClip();

        foreach (cellTemplate template in cell)
        {
            if (template.name == tag)
            {
                model = template.modelPrefab;

                try
                {
                    //can't recognise when doesn't exist
                    //clip = template.anim;
                }
                catch
                {}
            }
        }

        GameObject spawned = cellPooler.SpawnFromPool(model, pos, Quaternion.identity);
        spawned.transform.SetParent(parent);

        if(!clip.empty)
        { //If clip isn't empty, then play the on spawn animation
            Animation animation = spawned.GetComponent<Animation>();
            animation.AddClip(clip, clip.name);
            animation.Play(clip.name);
        }
        //getComponent isn't very efficient

        return spawned;
    }

}
