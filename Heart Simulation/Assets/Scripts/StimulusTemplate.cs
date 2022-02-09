using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StimulusTemplate : MonoBehaviour
{
    //public string stimulusName;
    //public AnimationClip response;
    //public Animation animation;
    public organTemplate[] organs;
    public string StimulusName;

    #region Singleton
    [System.Serializable]
    public class CellResponsePair
    {
        public string tag;
        public AnimationClip anim;
    }

    public static StimulusTemplate Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<CellResponsePair> animPairs;

    public void stimulate()
    {
        foreach (organTemplate organ in organs)
        {
            List<coOrdinateSystem> activeCells = organ.returnActiveCells();

            foreach (CellResponsePair pair in animPairs)
            {//First find the matching organs and their responses
                if (pair.tag == organ.organTag)
                {
                    Animation organTemp = organ.GetComponent<Animation>();
                    organTemp.AddClip(pair.anim, StimulusName + ", " + pair.tag);
                    organTemp.Play(StimulusName + ", " + pair.tag);

                }
            }

            foreach (coOrdinateSystem cell in activeCells)
            {// for every set of cells (per view)
                foreach (CellResponsePair pair in animPairs)
                {//find the mathcing cell type tag to identify animation
                    if (pair.tag == cell.cellType)
                    {
                        Animation temp = cell.GetObject().GetComponent<Animation>();
                        temp.AddClip(pair.anim, StimulusName + ", " + pair.tag);
                        temp.Play(StimulusName + ", " + pair.tag);
                    }

                    if(pair.tag == organ.organTag)
                    {
                        Animation organTemp = organ.GetComponent<Animation>();
                        organTemp.AddClip(pair.anim, StimulusName + ", " + pair.tag);
                        organTemp.Play(StimulusName + ", " + pair.tag);

                    }
                }
            }
        }
    }

    /*
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
    }*/
}
