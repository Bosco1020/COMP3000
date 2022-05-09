using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjToCellLink : MonoBehaviour
{
    private coOrdinateSystem cellLink;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLink(coOrdinateSystem target)
    {
        cellLink = target;
    }

    public coOrdinateSystem returnLink()
    {
        //no need to check if it's null, as this method can't be called unless the cell is spawned
        return cellLink;
    }
}
