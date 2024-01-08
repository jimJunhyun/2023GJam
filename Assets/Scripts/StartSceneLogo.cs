using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneLogo : MonoBehaviour, IRhythm
{
    [SerializeField] private Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    public void BeatUpdate()
    {
        Tweening1(_img);
    }

    public void BeatUpdateDivideFour()
    {
        Tweening2(_img);
    }
    
    public void Tweening1(Image img)
    {
        img.rectTransform.DOKill();
        img.rectTransform.localScale = new Vector3(1, 1, 1) * 0.7f;
        img.rectTransform.DOScale(1.1f,0.3f ).SetEase(Ease.OutElastic).OnComplete(() =>
        {
            img.rectTransform.DOScale(0.7f, 0.4f);
                
        });
    }
    
    public void Tweening2(Image img)
    {
        img.rectTransform.DOKill();
        img.rectTransform.localScale = new Vector3(1,1,1) * 0.7f;
        img.rectTransform.DOScale(0.9f,0.1f ).OnComplete(() =>
        {
            img.rectTransform.DOScale(0.7f, 0.1f);
                
        });
    }
}
