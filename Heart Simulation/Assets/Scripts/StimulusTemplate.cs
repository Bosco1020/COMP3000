using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StimulusTemplate : MonoBehaviour
{
    #region Singleton    
    [System.Serializable]
    public class ResponsePair : Ipairs
    {
        public string tag;

        public float range;

        public data Responses;

        public data returnResponses()
        {
            return Responses;
        }

        public string returnTag()
        {
            return tag;
        }
    }

    [System.Serializable]
    public class SpawnedPair : Ipairs
    {
        public string tag;

        public data Responses;

        public data returnResponses()
        {
            return Responses;
        }

        public string returnTag()
        {
            return tag;
        }
    }

    public interface Ipairs
    {
        public string returnTag();

        public data returnResponses();
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

        public bool dies;

        public SpawnedPair[] spawnedCellsTags;
    }

    public static StimulusTemplate Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public string StimulusName;

    public float duration = 10.0f;

    //public Material cellMatSet;

    public Sprite cursorIcon;

    public Transform StimulusSource;

    public GameObject coOrdiantePrefab;

    public data effects;

    public List<ResponsePair> cellPairs;

    public organTemplate[] organs;

    public List<ResponsePair> organPairs;

    private void Start()
    {
        /* //reset edothelial cell colour
        Color color = new Color();
        color.r = 0.7451f;
        color.g = 0.51993f;
        color.b = 0.44706f;
        color.a = 0.78039f;
        cellMatSet.color = color;*/
    }

    public void stimulateEveryOrgan(int index) 
    {
    foreach (organTemplate organ in organs)
    {
            //here specify layout index
        List<coOrdinateSystem> activeCells = organ.returnActiveCells(index);
            
        foreach (ResponsePair organResponse in organPairs)
        {//First find the matching organs and their responses
            if (organ.organTag == organResponse.tag){
            if (organResponse.Responses.hasAnimation)
            {
                Animation organTemp = organ.GetComponent<Animation>();
                organTemp.AddClip(organResponse.Responses.anim, StimulusName + ", " + organResponse.tag);
                organTemp.Play(StimulusName + ", " + organResponse.tag);
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
                        //change method to lerp between rates, and reduce once stimulus ended
            }}
        }

        foreach (ResponsePair pair in cellPairs)
        {// for every set of cells (per view)
            Material newMat, newNucMat;
            //create new materials to swap to for each cell type
            //get reference to default materials for each
            foreach (cellTemplate item in organ.display.cell)
            {
                    if (pair.tag != item.name) { return; }

                    int count = 0;
                    foreach (Material m in item.defaultMaterials)
                    {
                        count++;
                        if (count > 1)
                        {
                            //more than 1 material means cells has nucleus, presuming no more than 2 materials
                            newNucMat = new Material(m.shader);
                            newNucMat.color = m.color;
                            return;
                        }
                        // set newMat to same shader as default
                        newMat = new Material(m.shader);
                        newMat.color = m.color;
                    }
            }

            foreach (coOrdinateSystem cell in activeCells) 
            {//find the matching cell type and tag

                if (pair.tag != cell.cellType) { return; }
                //Check if within range, if not then return out of method
                float distance = (float) Math.Sqrt(Math.Pow(StimulusSource.position.x - cell.location.x, 2) + Math.Pow(StimulusSource.position.y - cell.location.y, 2) + Math.Pow(StimulusSource.position.z - cell.location.z, 2));
                if (pair.range != 0 && distance > pair.range) return;
                
                if (pair.Responses.hasAnimation)
                {
                    Animation temp = cell.GetObject().GetComponent<Animation>();
                    temp.AddClip(pair.Responses.anim, StimulusName + ", " + pair.tag);
                    temp.Play(StimulusName + ", " + pair.tag);
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
                }

                if (pair.Responses.spawnedCellsTags.Length > 0)
                {
                    //cells are to be spawned. Specify here what/how

                    //organ.IdleAnim.speed = Mathf.Clamp(organ.IdleAnim.speed + organResponse.Responses.speedChange, organ.idleRate, organ.idleRate + organResponse.Responses.speedChange);
                }
            }
        }
    }
    }

    private List<coOrdinateSystem> deletions = new List < coOrdinateSystem >();

    public void stimulateSpecific(int index, organTemplate organ) //check if already stimulated, as can't do so again unless want to make that a thing
    {
        List<coOrdinateSystem> activeCells = organ.returnActiveCells(index);

        foreach (ResponsePair organResponse in organPairs)
        {//First find the matching organs and their responses
            if (organ.organTag == organResponse.tag)
            {
                checkResponses(organResponse, true, organ.gameObject, organ);
            }
        }

        foreach (coOrdinateSystem cell in activeCells)
        {// for every set of cells (per view)

            //ResponsePair array = Array.FindAll(cellPairs, i => i != item).ToArray();

            foreach (ResponsePair pair in cellPairs)
            {//find the matching cell type and tag
                Vector3 distanceVec = new Vector3(StimulusSource.position.x - cell.location.x, StimulusSource.position.y - cell.location.y, StimulusSource.position.z - cell.location.z);
                //Check if within range, if not then return out of method
                float distance = (float)Math.Sqrt(Math.Pow(distanceVec.x, 2) + Math.Pow(distanceVec.y, 2) + Math.Pow(distanceVec.z, 2));
                if (pair.range != 0 && distance > pair.range) { goto AfterLoop; }

                if (pair.tag == cell.cellType)
                {
                    cell.setChanged(true);

                    checkResponses(pair, false, cell.GetObject(), null, cell);

                    if (pair.Responses.spawnedCellsTags.Length > 0)
                    {
                        //cells are to be spawned. Specify here what/how
                        //create new coOrdiante system for each and add to layout
                        
                        foreach (SpawnedPair spawn in pair.Responses.spawnedCellsTags)
                        {
                            // Instantiate a copy of the blank co-ordinate system object
                            GameObject cellObj = Instantiate(coOrdiantePrefab, cell.location - (0.1f * distanceVec), Quaternion.Euler(0f, 90f, 0f));

                            coOrdinateSystem newCell = cellObj.GetComponent<coOrdinateSystem>();

                            newCell.cellType = spawn.tag;
                            newCell.location = cell.location + (-0.1f * distanceVec); //move them 10% closer to the centre
                            newCell.rotation = Quaternion.Euler(0f, 90f, 0f);

                            organ.views[index].addCell(newCell);

                            newCell.SetObject(organ.display.Spawn(newCell.cellType, cell.location, organ.views[index].cameraCentre.transform));

                            // check if any other attributes for spawned cell

                            if (spawn.Responses.hasAnimation)
                            {
                                Animation temp = newCell.GetObject().GetComponentInParent<Animation>();
                                temp.AddClip(spawn.Responses.anim, StimulusName + ", " + spawn.tag);
                                temp.Play(StimulusName + ", " + spawn.tag);
                            }
                        }

                        //organ.IdleAnim.speed = Mathf.Clamp(organ.IdleAnim.speed + organResponse.Responses.speedChange, organ.idleRate, organ.idleRate + organResponse.Responses.speedChange);
                    }
                } //end of tag match
            AfterLoop:;
            } //end pair for each loop
        } //end of cell for each loop
    }

    public void checkResponses(Ipairs pair, bool isOrgan, GameObject cell, organTemplate organ = default, coOrdinateSystem coOrd = default)
    {
        if (pair.returnResponses().hasAnimation)
        {
            Animation temp = cell.GetComponentInParent<Animation>();
            temp.AddClip(pair.returnResponses().anim, StimulusName + ", " + pair.returnTag());
            temp.Play(StimulusName + ", " + pair.returnTag());
            Debug.Log("hasAnim" + temp.gameObject);
        }

        if (pair.returnResponses().changeColour)
        {
            Material temp = cell.GetComponentInParent<Material>();
            temp.color = pair.returnResponses().colour;
        }

        if (pair.returnResponses().IdleSpeedChange)
        {
            if(isOrgan)
            {
                //limits values to between the idle rate and the idle rate + increased speed
                organ.IdleAnim.speed = Mathf.Clamp(organ.IdleAnim.speed + pair.returnResponses().speedChange, organ.idleRate, organ.idleRate + pair.returnResponses().speedChange);
                return;
            }

            Animator temp = cell.GetComponentInParent<Animator>();
            temp.speed += pair.returnResponses().speedChange;
        }

        if (pair.returnResponses().dies && isOrgan == false)
        {
            deletions.Add(coOrd);
        }
    }

    public void endStimulus(organTemplate organ, int index)
    {
        //grab all co-ordinates
        //remove deleted ones
        //re-scale array so no blanks
        
        organ.removeActiveCells(deletions);
        
        deletions.Clear();

        //StartCoroutine(endStimulusDelay());
    }

    IEnumerator endStimulusDelay()
    {
        //yield on a new YieldInstruction that waits for x seconds.
        yield return new WaitForSeconds(duration);

        // end stmulus here
    }
}
