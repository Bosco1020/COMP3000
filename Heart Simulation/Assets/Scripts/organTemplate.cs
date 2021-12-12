using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organTemplate : MonoBehaviour
{
    [SerializeField]
    private cellLayout[] views;

    [SerializeField]
    private DisplayCell display;

    [SerializeField]
    private CameraControl refCamera;

    //[SerializeField]
    //private cellGrouper[] cellGroups;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public List<coOrdinateSystem> returnActiveCells()
    {
        List<coOrdinateSystem> allCells = new List<coOrdinateSystem>();

        foreach (cellLayout view in views)
        {
            //combine all groups of cells together
            allCells.AddRange(view.cells);
        }

        return allCells;
    }

    public void showView(int index)
    {
        coOrdinateSystem[] temp = views[index].returnCells();

        foreach (coOrdinateSystem cell in temp)
        {
            cell.SetObject(display.Spawn(cell.cellType, cell.location, views[index].cameraCentre.transform));
        }

        refCamera.setTarget(views[index].cameraCentre.transform);
    }

    public void closeView(int index)
    {
        StartCoroutine(resetCoroutine(index));
    }

    IEnumerator resetCoroutine(int index)
    {
        refCamera.reset();

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        foreach (Transform child in views[index].cameraCentre.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
