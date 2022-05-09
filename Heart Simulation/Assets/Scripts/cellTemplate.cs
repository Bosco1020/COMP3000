using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Cell")]
public class cellTemplate : ScriptableObject
{
    #region SINGLETON
    [System.Serializable]
    public class textData
    {
        public stringSO triggeringSimulus;
        public string text;
    }

    public static cellTemplate Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public new string name;
    public string modelPrefab;
    public Material[] defaultMaterials;

    [Header("any idle animation")]
    public AnimationClip anim;

    [Header("default information to be shown")]
    public string defaultText;

    [Header("specific information related to each stimulus")]
    public List<textData> responseText;

    public void activate()
    {
        //stimulus.ChangeColor();
    }

    public void OnObjectSpawn()
    {
        //stimulus.BuildAnim(value.r);
        
    }

    public void returnText()
    {

    }
}
