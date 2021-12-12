using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Cell")]
public class cellTemplate : ScriptableObject, IPooledCell
{
    public new string name;
    public string modelPrefab;
    public Color value;

    public void activate()
    {
        //stimulus.ChangeColor();
    }

    public void OnObjectSpawn()
    {
        //stimulus.BuildAnim(value.r);
    }
}
