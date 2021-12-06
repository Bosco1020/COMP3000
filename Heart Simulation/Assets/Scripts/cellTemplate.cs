using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Cell")]
public class cellTemplate : ScriptableObject, IPooledCell
{
    public new string name;
    public string modelPrefab;
    public Color value;

    [SerializeField]
    private StimulusTemplate tempStimulus;
    public StimulusTemplate stimulus { get; set; }

    public Animation activate()
    {
        return stimulus.response;
    }

    public void OnObjectSpawn()
    {
        //when spawn object, do something idk
        
    }
}
