using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{

    //click events will check here first, whether stimulus selected or not

    //when clicking on stimulus, if zoomed on layout, then apply stimulus to that layout instead

    public CursorController cursor;

    private bool stimulating;
    private StimulusTemplate refStimulus;
    private int currentLayoutIndex = -1;
    private organTemplate refOrgan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCurrentStimulus(StimulusTemplate stimulus)
    {
        refStimulus = stimulus;

        // player has clicked on the stimulus button
        if (currentLayoutIndex >= 0)
        {
            // if currently zoomed in, then immediately trigger stimulus
            refStimulus.stimulateSpecific(currentLayoutIndex, refOrgan);
            stimulating = false;
            cursor.resetIcon();
            return;
        }

        // otherwise save this as reference for then updating the stimulus
        stimulating = true;
        cursor.updateIcon(refStimulus.cursorIcon);
    }

    public void updateCurrentLayout(int index)
    {
        // Player has clicked on a selection icon
        if (index == -1 && currentLayoutIndex == -1) return;

            //if set to -1, then exiting from layout
            if (index == -1 && currentLayoutIndex != -1) //check if any change
        {
            currentLayoutIndex = index;
            refOrgan.closeView();

            try
            {
                refStimulus.endStimulus(refOrgan, currentLayoutIndex);
            }
            catch (System.Exception)
            {
                return;
            }
            
            return; 
        }

        currentLayoutIndex = index;
        cursor.resetIcon();

        if (stimulating)
        {
            // if stimulus selected, then trigger at location
            refOrgan.showView(currentLayoutIndex);
            refStimulus.stimulateSpecific(currentLayoutIndex, refOrgan);
            stimulating = false;
            return;
        }

        refOrgan.showView(currentLayoutIndex);
    }

    public void updateTargetOrgan(organTemplate organ)
    {
        refOrgan = organ;
    }
}
