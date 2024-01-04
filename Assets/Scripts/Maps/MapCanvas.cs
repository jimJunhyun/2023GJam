using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapCanvas : PoolAble
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;

    public override void Reset()
    {
        _text.text = "";
    }

    public void Init(string value, float duration)
    {
        _text.text = value;
        StartCoroutine(IO(duration));
    }

    IEnumerator IO(float duration)
    {
        _image.DOColor(new Color(1, 1, 1, 1), 0.3f);
        _text.DOColor(new Color(1, 1, 1, 1), 0.3f);
        yield return new WaitForSeconds(duration);
        
        _image.DOColor(new Color(1, 1, 1, 0), 0.3f);
        _text.DOColor(new Color(1, 1, 1, 0), 0.3f).OnComplete(() =>
        {                    
            PoolManager.Instance.Push(this);
        });
    }
}
