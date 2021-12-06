using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Stimulus")]
public class StimulusTemplate : ScriptableObject
{
    public string name;
    public Animation response;
}
