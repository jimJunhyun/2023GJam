using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeStat
{
    Sucs,
    Fail,
    Run,
}

public interface INode
{
    public NodeStat Examine();
}
