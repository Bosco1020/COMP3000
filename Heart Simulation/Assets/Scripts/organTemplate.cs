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

    public Renderer[] heartMats;

    //public Material FadeMat;

    [Range (0, 10)]
    public float idleRate = 1.0f;

    public bool speedOveride = false;

    //private bool fadingOut = false;

    void Start()
    {
        currentView = -1;
        IdleAnim.speed = idleRate;
        //fadeOut();
    }

    void Update()
    {
        if(speedOveride)
        {
            IdleAnim.speed = idleRate;
        }

        
        /*
        if(fadingOut)
        {
            foreach (Renderer mat in heartMats)
            {
                mat.material.Lerp(mat.material, FadeMat, 1.0f * Time.deltaTime);
            }  
        }*/
    }

    public List<coOrdinateSystem> returnActiveCells()
    {
        //NEEDS to check if cell is active or not
        //Currently returns EVERY cell in organ, may be a bit much... may not tbh

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

    public void spawnCell(string tag, Vector3 pos, Transform parent)
    {
        // can use this to spawn necessary cells from DisplayCell
        display.Spawn(tag, pos, parent);
    }

    public void fadeOut()
    {
        foreach (Renderer mat in heartMats)
        {
            
            mat.material.SetFloat("_Mode", 2);
            mat.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.material.SetInt("_DstBelnd", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //mat.material.SetInt("_ZWrite", 0);
            mat.material.DisableKeyword("_ALPHATEST_ON");
            mat.material.EnableKeyword("_ALPHABLEBD_ON");
            mat.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.material.renderQueue = 3000;
        }
        //fadingOut = true;
        StartCoroutine(fadeOutCoroutine());
    }

    public void fadeIn()
    {
        foreach (Renderer mat in heartMats)
        {
            mat.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.material.SetInt("_DstBelnd", (int)UnityEngine.Rendering.BlendMode.Zero);
            //mat.material.SetInt("_ZWrite", 1);
            mat.material.DisableKeyword("_ALPHATEST_ON");
            mat.material.DisableKeyword("_ALPHABLEBD_ON");
            mat.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.material.renderQueue = 1;
            mat.material.EnableKeyword("_NORMALMAP");
            mat.material.EnableKeyword("_DETAIL_MULX2");
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

    IEnumerator fadeOutCoroutine()
    {
        float count = 1.0f;
        Color c;

        while (count > 0.8f)
        {
            foreach (Renderer mat in heartMats)
            {
                c = mat.material.color;
                c.a = count;
                mat.material.SetColor("_Color", c);
                Debug.Log(mat.material.color.a);
            }
            count = count - 0.01f;
            Debug.Log("-----");
        }

        yield return new WaitForSeconds(0.1f);
    }
}
