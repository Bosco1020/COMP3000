using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Cell")]
public class cellTemplate : ScriptableObject
{
    public new string name;
    public GameObject model;

    public void setParent(Transform parent)
    {
        this.model.transform.parent = parent;
    }

    public void setModel(GameObject template)
    {
        this.model = template;
    }
}
