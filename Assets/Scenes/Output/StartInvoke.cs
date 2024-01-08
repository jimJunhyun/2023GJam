using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EasyTransition;
using UnityEngine;
using UnityEngine.UI;

public class StartInvoke : MonoBehaviour
{
    [SerializeField] private TransitionSettings std;
    [SerializeField] private Image BG;

    private void Awake()
    {
        BG.color = new Color(0, 0, 0, 1);
        BG.DOColor(new Color(0, 0, 0, 0), 2f).OnComplete(() =>
        {
            TransitionManager.Instance.Transition("StartScene", std,  1f);
        });
    }
    

}
