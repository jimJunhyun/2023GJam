using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGimik : MonoBehaviour
{
    [SerializeField] private Interective _inter;
    [SerializeField] private MapGimikSO _maps;
    public void Interect()
    {
        _maps.RoomInit();
        _inter._action = null;
        _inter._beforeText = "축복받기";
        _inter._afterText = "이미\n축복을\n받았습니다";
    }

}
