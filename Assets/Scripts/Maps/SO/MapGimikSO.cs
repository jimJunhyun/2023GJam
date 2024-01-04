using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapGimikSO : ScriptableObject
{
    [Header("Type")]
    public RoomType _room;

    public abstract string RoomInit();
}
