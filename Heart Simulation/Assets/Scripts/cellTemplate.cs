using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Cell")]
public class cellTemplate : ScriptableObject, IPooledCell
{
    public new string name;
    public string modelPrefab;
    public Color value;

    public void OnObjectSpawn()
    {
        //when spawn object, do something idk
        
    }
}
