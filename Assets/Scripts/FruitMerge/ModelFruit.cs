using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelFruit : MonoBehaviour
{
    [SerializeField] private int limitLevel;
    public int LimitLevel
    {
        get { return limitLevel; }
        set { limitLevel = value; }
    }

    [SerializeField] private List<InfoFruit> list;
    public List<InfoFruit> DataFruit
    {
        get { return list; }
        set { list = value; }
    }

}
