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
        // player has clicked on the stimulus button
        if (currentLayoutIndex > 0)
        {
            // if currently zoomed in, then immediately trigger stimulus
            refStimulus.stimulate();
            stimulating = false;
            return;
        }

        // otherwise save this as reference for then updating the stimulus
        stimulating = true;
        refStimulus = stimulus;
    }

    public void updateCurrentLayout(int index)
    {
        // Player has clicked on a selection icon
        currentLayoutIndex = index;

        //if set to -1, then no layout currently
        if (index == -1){ return; }

        if (stimulating)
        {
            // if stimulus selected, then trigger at location
            refOrgan.showView(currentLayoutIndex);
            stimulating = false;
        }
    }

    public void updateTargetOrgan(organTemplate organ)
    {
        refOrgan = organ;
    }
}
