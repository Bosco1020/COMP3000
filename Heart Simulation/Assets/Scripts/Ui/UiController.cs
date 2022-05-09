using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UiController : MonoBehaviour
{

    //click events will check here first, whether stimulus selected or not

    //when clicking on stimulus, if zoomed on layout, then apply stimulus to that layout instead

    public CursorController cursor;

    public TextMeshProUGUI infoText;

    private bool stimSelected = false, stimulating = false;
    private StimulusTemplate refStimulus;
    private int currentLayoutIndex = -1;

    [SerializeField]
    private organTemplate refOrgan;

    public UnityEvent zoomIn, zoomOut;

    [SerializeField]
    private GameObject infoPanel;

    private void Start()
    {
        infoText.SetText(refOrgan.defaultText);
    }

    private void Update()
    {
        if (currentLayoutIndex == -1) //zoomed out
        {
            if(stimulating)
            {
                infoText.SetText(refOrgan.defaultText);
            }
            else
            {
                infoText.SetText(refOrgan.defaultText);
            }
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) //otherwise fire ray to select
            {
                //Debug.Log("You selected the " + hit.transform.name);
                if (hit.transform.tag == "Cell") //hit a valid object
                {
                    infoPanel.SetActive(true);
                    coOrdinateSystem hitCoOrd = hit.transform.GetComponent<ObjToCellLink>().returnLink();

                    foreach (cellTemplate cell in refOrgan.display.cell)
                    {
                        if (cell.name == hitCoOrd.cellType) //filter through scriptable objects for matching cell names
                        {

                            if(hitCoOrd.returnChange()) // identify whether target is affected by stimulus or not
                            {
                                for (int i = 0; i < cell.responseText.Count; i++)
                                {
                                    if (cell.responseText[i].triggeringSimulus == refStimulus.StimulusName) // find matching stimuli for taregt text
                                    {
                                        infoText.SetText(cell.responseText[i].text);
                                        break;
                                    }
                                }
                            }
                            else // use default text
                            {
                                infoText.SetText(cell.defaultText);
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    public void updateCurrentStimulus(StimulusTemplate stimulus)
    {
        if (stimSelected && refStimulus == stimulus)
        {
            stimSelected = false;
            cursor.resetIcon();
            return;
        }

        refStimulus = stimulus;

        // player has clicked on the stimulus button
        if (currentLayoutIndex >= 0)
        {
            if (stimulating) //currently "playing" a stimulus
            {
                return;
            }

            // if currently zoomed in, then immediately trigger stimulus
            refStimulus.stimulateSpecific(currentLayoutIndex, refOrgan);
            stimSelected = false;
            stimulating = true;
            cursor.resetIcon();
            return;
        }

        // otherwise save this as reference for then updating the stimulus
        stimSelected = true;
        cursor.updateIcon(refStimulus.cursorIcon);
    }

    public void updateCurrentLayout(int index)
    {
        // Player has clicked on a selection icon
        if (index == -1 && currentLayoutIndex == -1) return;

            //if set to -1, then exiting from layout
        if (index == -1)
        {
            currentLayoutIndex = index;
            refOrgan.closeView();

            stimulating = false;

            zoomOut.Invoke();

            return; 
        }

        currentLayoutIndex = index;
        cursor.resetIcon();

        zoomIn.Invoke();

        if (stimSelected)
        {
            // if stimulus selected, then trigger at location
            refOrgan.showView(currentLayoutIndex);
            refStimulus.stimulateSpecific(currentLayoutIndex, refOrgan);
            stimSelected = false;
            stimulating = true;
            return;
        }

        refOrgan.showView(currentLayoutIndex);
    }

    public void updateTargetOrgan(organTemplate organ)
    {
        refOrgan = organ;
    }
}
