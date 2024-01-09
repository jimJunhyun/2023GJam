using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGimik : MonoBehaviour
{
    
    [SerializeField] private Interective _inter;
    [SerializeField] private MapGimikSO _maps;
    [SerializeField] private MapCanvas _cans;

    [TextArea][SerializeField] private string _beforeText;
    [TextArea][SerializeField] private string _afterText;
    public void Interect()
    {
        _inter._action = null;
        _inter._beforeText = _beforeText;
        _inter._afterText = _afterText;
        
        MapCanvas _cs = PoolManager.Instance.Pop(_cans.name) as MapCanvas;
        _cs.Init(_maps.RoomInit(),7f);
    }

}
