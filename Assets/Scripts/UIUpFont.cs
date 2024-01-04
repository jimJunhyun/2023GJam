using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIUpFont : PoolAble
{
    [SerializeField] private TextMeshProUGUI _tmp;
    public override void Reset()
    {
        _tmp.color = Color.black;

    }

    public void Init(RectTransform tls, string a)
    {
        transform.parent = tls.transform;
        _tmp.text = $"{a}";
        _tmp.rectTransform.position = tls.position;
        _tmp.rectTransform.DOMove(tls.position + new Vector3(0,75,0), 0.8f).OnComplete(() =>
        {
            _tmp.DOColor(Color.clear, 0.3f);
        });
    }
}
