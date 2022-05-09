using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmartHeart", menuName = "Templates/stringSO")]
public class stringSO : ScriptableObject
{
    private string name;

    public void setVal(string val)
    {
        name = val;
    }

    public string getVal()
    {
        return name;
    }
}
