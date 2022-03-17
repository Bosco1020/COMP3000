using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        List<coOrdinateSystem> temp = new List<coOrdinateSystem>(cells);
        temp.Add(addition);

        cells = temp.ToArray();
    }

    public void removeCell(List<coOrdinateSystem> deletions)
    {
        foreach (coOrdinateSystem cell in deletions)
        {
            //re-formats array to contain everything but the deleted cells
            cells = cells.Where(e => e != cell).ToArray();
            Destroy(cell.gameObject);
        }
    }
}
