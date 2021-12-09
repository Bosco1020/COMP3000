using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/Stimulus")]
public class StimulusTemplate : ScriptableObject
{
    public string name;
    public AnimationClip response;
    public Animation animation;

    public void BuildAnim(float Red)
    {
        response = new AnimationClip();
        response.ClearCurves();

        AnimationCurve curve = AnimationCurve.Linear(0.0F, Red, 2.0F, Red - 1);
        response.SetCurve("", typeof(Color), "Color.r", curve);
    }

    public void ChangeColor()
    {
        animation.Play(response.name);
    }
}
