using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organTemplate : MonoBehaviour
{
    public string organTag;

    [SerializeField]
    public cellLayout[] views;

    [SerializeField]
    public DisplayCell display;

    [SerializeField]
    private CameraControl refCamera;

    private int currentView;

    public Animator IdleAnim;

    public Renderer[] heartMats;

    public Material[] Mats;

    [Range (0, 10)]
    public float idleRate = 1.0f;

    public string defaultText;

    void Start()
    {
        currentView = -1;
        IdleAnim.speed = idleRate;

        foreach (Material mat in Mats)
        {
            Color tempCol = mat.color;
            tempCol.a = 1.0f;
            mat.SetColor("_Color", tempCol);
        }
        //fadeOut();
    }

    public List<coOrdinateSystem> returnActiveCells(int index)
    {
        //returns cells within specified layout

        List<coOrdinateSystem> allCells = new List<coOrdinateSystem>();

        allCells.AddRange(views[index].returnCells());

        return allCells;
    }

    public Transform returnStimulusCentre(int index)
    {
        return views[index].cameraCentre.transform;
    }

    public void removeActiveCells(List<coOrdinateSystem> deletions)
    {
        /*for (int i = 0; i < 5; i++)
        {
            Debug.Log(deletions[i]);
        }*/

        views[currentView].removeCell(deletions);

        List<cellLayout> gameObjectList = new List<cellLayout>(views);
        gameObjectList.RemoveAll(x => x == null);
        views = gameObjectList.ToArray();
    }


    public void showView(int index)
    {
        //when exiting zoom, twice, calls this method which breaks line 100
        currentView = index;
        coOrdinateSystem[] temp = views[currentView].returnCells();

        foreach (coOrdinateSystem cell in temp)
        {
            cell.SetObject(display.Spawn(cell.cellType, cell.location, views[currentView].cameraCentre.transform));
        }

        refCamera.setTarget(views[currentView].cameraCentre.transform, views[currentView].cameraMovePoint);

        StartCoroutine(fadeOutCoroutine());
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
        coOrdinateSystem[] cell = views[index].returnCells();

        //SET & SAVE
        for (int i = 0; i < cell.Length; i++)
        {
            if (cell[i].returnChange())
            { //only make changes if the cells has altered from the norm
                Renderer[] childRenders = cell[i].GetObject().GetComponentsInChildren<Renderer>();
                //Debug.Log(childRenders.Length);

                for (int x = 0; x < childRenders.Length; x++)
                {
                    //update the co-ordiante system with the cells new material(s)
                    Material temp = new Material(childRenders[x].material.shader);
                    temp.color = childRenders[x].material.color;
                    //Debug.Log(cell.getOriginalMaterial());
                    cell[i].setNewMaterial(temp, x);
                }

                //reset the object's material to go back into the pool
                Renderer[] revert = cell[i].returnRenderers().ToArray();

                for (int a = 0; a < revert.Length; a++)
                {
                    revert[a].material = cell[i].getOriginalMaterial(a);
                }
            }
        }

        //reset alpha for each material
        foreach (Material mat in Mats)
        {
            Color tempCol = mat.color;
            tempCol.a = 1.0f;
            mat.SetColor("_Color", tempCol);
        }

        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(1);
        
        foreach (Transform child in views[index].cameraCentre.transform)
        {
            //turn off all cells
            child.gameObject.SetActive(false);
        }
    }

    IEnumerator fadeOutCoroutine()
    {
        /*float count = 1.0f;
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
        }*/

        yield return new WaitForSeconds(1.0f);

        foreach (Material mat in Mats)
        {
            Color tempCol = mat.color;
            tempCol.a = 0.01f;
            mat.SetColor("_Color", tempCol);
        }
    }
}
