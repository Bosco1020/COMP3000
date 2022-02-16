using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StimulusTemplate : MonoBehaviour
{
    #region Singleton    
    [System.Serializable]
    public class ResponsePair
    {
        public string tag;

        public float range;

        public data Responses;
    }

    [System.Serializable]
    public class data
    {
        public bool hasAnimation;
        public AnimationClip anim;

        public bool changeColour;
        public Color colour;

        public bool IdleSpeedChange;
        public float speedChange;
    }

    [System.Serializable]
    public class Stimulator
    {
        public string StimulusName;

        public Transform StimulusSource;

        public data effects;
    }

    public static StimulusTemplate Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public Stimulator stimulus;

    public List<ResponsePair> cellPairs;

    public organTemplate[] organs;

    public List<ResponsePair> organPairs;

    public void stimulate()
    {
    foreach (organTemplate organ in organs)
    {
        List<coOrdinateSystem> activeCells = organ.returnActiveCells();
            
        foreach (ResponsePair organResponse in organPairs)
        {//First find the matching organs and their responses
            if (organ.organTag == organResponse.tag){
            if (organResponse.Responses.hasAnimation)
            {
                Animation organTemp = organ.GetComponent<Animation>();
                organTemp.AddClip(organResponse.Responses.anim, stimulus.StimulusName + ", " + organResponse.tag);
                organTemp.Play(stimulus.StimulusName + ", " + organResponse.tag);
            }

            if (organResponse.Responses.changeColour)
            {
                Material organTemp = organ.GetComponent<Material>();
                organTemp.color = organResponse.Responses.colour;
            }

            if (organResponse.Responses.IdleSpeedChange)
            {
                //limits values to between the idle rate and the idle rate + increased speed
                organ.IdleAnim.speed = Mathf.Clamp(organ.IdleAnim.speed + organResponse.Responses.speedChange, organ.idleRate, organ.idleRate + organResponse.Responses.speedChange);
            }}
        }

        foreach (coOrdinateSystem cell in activeCells)
        {// for every set of cells (per view)
            foreach (ResponsePair pair in cellPairs)
            {//find the matching cell type and tag

                //Check if within range, if not then return out of method
                float distance = (float) Math.Sqrt(Math.Pow(stimulus.StimulusSource.position.x - cell.location.x, 2) + Math.Pow(stimulus.StimulusSource.position.y - cell.location.y, 2) + Math.Pow(stimulus.StimulusSource.position.z - cell.location.z, 2));
                if (pair.range != 0 && distance > pair.range) return;

                if (pair.tag == cell.cellType){
                if (pair.Responses.hasAnimation)
                {
                    Animation temp = cell.GetObject().GetComponent<Animation>();
                    temp.AddClip(pair.Responses.anim, stimulus.StimulusName + ", " + pair.tag);
                    temp.Play(stimulus.StimulusName + ", " + pair.tag);
                }

                if (pair.Responses.changeColour)
                {
                    Material temp = cell.GetObject().GetComponent<Material>();
                    temp.color = pair.Responses.colour;
                }

                if (pair.Responses.IdleSpeedChange)
                {
                    Animator temp = cell.GetObject().GetComponent<Animator>();
                    temp.speed += pair.Responses.speedChange;
                }}
            }
        }
    }
    }
}
