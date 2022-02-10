using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StimulusTemplate : MonoBehaviour
{
    public string StimulusName;

    #region Singleton    
    [System.Serializable]
    public class ResponsePair
    {
        public string tag;

        public bool hasAnimation;
        public AnimationClip anim;

        public bool changeColour;
        public Color colour;

        public bool IdleSpeedChange;
        public float speedChange;
    }

    public static StimulusTemplate Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

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
            if (organResponse.hasAnimation)
            {
                Animation organTemp = organ.GetComponent<Animation>();
                organTemp.AddClip(organResponse.anim, StimulusName + ", " + organResponse.tag);
                organTemp.Play(StimulusName + ", " + organResponse.tag);
            }

            if (organResponse.changeColour)
            {
                Material organTemp = organ.GetComponent<Material>();
                organTemp.color = organResponse.colour;
            }

            if (organResponse.IdleSpeedChange)
            {
                organ.IdleAnim.speed += organResponse.speedChange;
            }}
        }

        foreach (coOrdinateSystem cell in activeCells)
        {// for every set of cells (per view)
            foreach (ResponsePair pair in cellPairs)
            {//find the mathcing cell type tag to identify animation
                if (pair.tag == cell.cellType){
                if (pair.hasAnimation)
                {
                    Animation temp = cell.GetObject().GetComponent<Animation>();
                    temp.AddClip(pair.anim, StimulusName + ", " + pair.tag);
                    temp.Play(StimulusName + ", " + pair.tag);
                }

                if (pair.changeColour)
                {
                    Material temp = cell.GetObject().GetComponent<Material>();
                    temp.color = pair.colour;
                }

                if (pair.IdleSpeedChange)
                {
                    Animator temp = cell.GetObject().GetComponent<Animator>();
                    temp.speed += pair.speedChange;
                }}
            }
        }
    }
    }
}
