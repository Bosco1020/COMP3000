using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellLayout : MonoBehaviour
{
    //public GameObject reticle;
    public coOrdinateSystem[] cells;
    public GameObject cameraCentre;
    public Transform cameraMovePoint;

    void Start()
    {
        cells = GetComponentsInChildren<coOrdinateSystem>();
    }

    public coOrdinateSystem[] returnCells()
    {
        return cells;
    }

    public void addCell(coOrdinateSystem addition)
    {
        coOrdinateSystem[] temp = new coOrdinateSystem[cells.Length + 1];
        temp[cells.Length] = addition;
        cells = new coOrdinateSystem[cells.Length + 1];
        cells = temp;

        //cells[cells.Length] = addition;
    }
}
