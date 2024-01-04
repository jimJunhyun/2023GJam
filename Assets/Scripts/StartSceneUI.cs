using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public enum SceneEnum
{
    tutorial,
    start,
    setting,
    exit
}

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private Image _tutorial;
    [SerializeField] private Image _start;
    [SerializeField] private Image _setting;
    [SerializeField] private Image _exit;

    private SceneEnum _scene;

    private int cnt = 0;
    

    public void Tweening(Image img)
    {
        img.rectTransform.localScale = new Vector3(1, 1, 1);
        img.rectTransform.DOKill();
        img.rectTransform.DOScale(1.2f,0.4f ).SetEase(Ease.OutElastic).OnComplete(() =>
        {
            img.rectTransform.DOScale(1.0f, 0.1f);
                
        });
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Tweening(_tutorial);
            if (_scene != SceneEnum.tutorial)
            {
                _scene = SceneEnum.tutorial;
                
            }
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Tweening(_start);
            _scene = SceneEnum.tutorial;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Tweening(_setting);
            _scene = SceneEnum.tutorial;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Tweening(_exit);
            _scene = SceneEnum.tutorial;
        }
    }
}
