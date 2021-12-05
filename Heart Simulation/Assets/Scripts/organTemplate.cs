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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void showView(int index)
    {
        coOrdinateSystem[] temp = views[index].returnCells();

        foreach (coOrdinateSystem cell in temp)
        {
            display.Spawn(cell.cellType, cell.location, views[index].cameraCentre.transform);
        }

        refCamera.setTarget(views[index].cameraCentre.transform);
    }
}
