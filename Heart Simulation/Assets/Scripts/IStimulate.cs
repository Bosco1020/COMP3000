using UnityEngine;

public interface IStimulate
{
    StimulusTemplate stimulus
    {
        get;
        set;
    }
    Animation activate();

}
