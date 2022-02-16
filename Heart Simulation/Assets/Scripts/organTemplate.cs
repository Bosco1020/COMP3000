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

    public Animator IdleAnim;

    public Material[] heartMats;

    [Range (0, 10)]
    public float idleRate = 1.0f;

    public bool speedOveride = false;

    [SerializeField, Range(0,1)]
    private float alpha = 1;

    void Start()
    {
        currentView = -1;
        IdleAnim.speed = idleRate;
    }

    void Update()
    {
        if(speedOveride)
        {
            IdleAnim.speed = idleRate;
        }

        foreach (Renderer mat in this.gameObject.GetComponentsInChildren<Renderer>())
        {
            mat.material.color.a.Equals(Mathf.Lerp(1, 0, 0.1f * Time.deltaTime));
        }
    }

    public List<coOrdinateSystem> returnActiveCells()
    {
        List<coOrdinateSystem> allCells = new List<coOrdinateSystem>();

        foreach (cellLayout view in views)
        {
            //combine all groups of cells together
            allCells.AddRange(view.returnCells());
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

    public void fadeOut()
    {
        foreach (Renderer mat in this.gameObject.GetComponentsInChildren<Renderer>())
        {
            mat.material.SetFloat("_Mode", 2);
            mat.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.material.SetInt("_DstBelnd", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.material.SetInt("_ZWrite", 0);
            mat.material.DisableKeyword("_ALPHATEST_ON");
            mat.material.EnableKeyword("_ALPHABLEBD_ON");
            mat.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.material.renderQueue = 3000;
        }
    }

    public void fadeIn()
    {
        foreach (Renderer mat in this.gameObject.GetComponentsInChildren<Renderer>())
        {
            mat.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.material.SetInt("_DstBelnd", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.material.SetInt("_ZWrite", 1);
            mat.material.DisableKeyword("_ALPHATEST_ON");
            mat.material.DisableKeyword("_ALPHABLEBD_ON");
            mat.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.material.renderQueue = 1;
        }
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

    IEnumerator fadeCoroutine(float change)
    {
        while (true)
        {

        }
    }
}
