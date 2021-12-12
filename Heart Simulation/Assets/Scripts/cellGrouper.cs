using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellGrouper : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> cells;

    void Start()
    {
        cells.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void assemble()
    {
        cells.Clear();

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                cells.Add(child.gameObject);
            }
        }
    }

    public List<GameObject> returnCells()
    {
        return cells;
    }
}
