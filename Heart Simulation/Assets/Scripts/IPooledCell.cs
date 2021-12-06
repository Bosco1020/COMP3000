using UnityEngine;

public interface IPooledCell
{
    StimulusTemplate stimulus
    {
        get;
        set;
    }
    Animation activate();

    void OnObjectSpawn();
}
