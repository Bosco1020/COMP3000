using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organTemplate : MonoBehaviour
{
    public string organTag;

    [SerializeField]
    private cellLayout[] views;

    [SerializeField]
    private DisplayCell display;

    [SerializeField]
    private CameraControl refCamera;

    private int currentView;

    [SerializeField]
    private Animator IdleAnim;

    [Range (0, 10)]
    public float idleRate = 1.0f;

    //[SerializeField]
    //private cellGrouper[] cellGroups;

    void Start()
    {
        currentView = -1;
        IdleAnim.speed = idleRate;
    }

    void Update()
    {
        IdleAnim.speed = idleRate;
    }

    public void stimulate()
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
        currentView = index;
        coOrdinateSystem[] temp = views[currentView].returnCells();

        foreach (coOrdinateSystem cell in temp)
        {
            cell.SetObject(display.Spawn(cell.cellType, cell.location, views[currentView].cameraCentre.transform));
        }

        refCamera.setTarget(views[currentView].cameraCentre.transform, views[currentView].cameraMovePoint);
    }

    public void closeView()
    {
        StartCoroutine(resetCoroutine(currentView));
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
