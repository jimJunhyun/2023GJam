using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CurseUI : MonoBehaviour
{
    public List<Image> _child = new();
    
    private void Awake()
    {
        _child = GetComponentsInChildren<Image>().ToList();
        RemoveCurse();
        
    }

    void RemoveCurse()
    {
        for (int i = 0; i < _child.Count; i++)
        {
            _child[i].gameObject.SetActive(false);
        }
    }

    public void SetCurse(int a = -1)
    {
        RemoveCurse();

        if (a > -1)
        {
            _child[a%_child.Count].gameObject.SetActive(true);
        }
    }
}
