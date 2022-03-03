using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Cell")]
public class cellTemplate : ScriptableObject
{
    public new string name;
    public string modelPrefab;
    public Material[] defaultMaterials;
    public AnimationClip anim;

    public void activate()
    {
        //stimulus.ChangeColor();
    }

    public void OnObjectSpawn()
    {
        //stimulus.BuildAnim(value.r);
        
    }
}
