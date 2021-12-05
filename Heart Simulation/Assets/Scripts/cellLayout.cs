using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellLayout : MonoBehaviour
{
    //public GameObject reticle;
    public coOrdinateSystem[] cells;
    public GameObject cameraCentre;

    void Start()
    {
        cells = GetComponentsInChildren<coOrdinateSystem>();
    }

    public coOrdinateSystem[] returnCells()
    {
        return cells;
    }
}
