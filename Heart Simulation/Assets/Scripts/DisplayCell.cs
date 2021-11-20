using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DisplayCell : MonoBehaviour
{
    public cellTemplate[] cell;
    public TMP_Text display;
    public GameObject current;

    /*public void Start()
    {
        current.transform.parent = this.transform;
        Instantiate(current, transform.position, transform.rotation);
    }*/

    public void Spawn(int index)
    {
        //current = cell[index].model;
        Instantiate(current, transform.position, transform.rotation);
        //temp.setParent(this.transform);
        display.text = cell[index].name;
    }
    
}
