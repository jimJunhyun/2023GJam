using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EasyTransition;
using UnityEngine.SceneManagement;


public enum SceneEnum
{
    tutorial,
    start,
    setting,
    exit
}

public class StartSceneUI : MonoBehaviour
{
    [SerializeField] private TransitionSettings Tutorial;
    [SerializeField] private TransitionSettings GameScene;
    [SerializeField] private Image _tutorial;
    [SerializeField] private Image _start;
    [SerializeField] private Image _setting;
    [SerializeField] private Image _exit;

    [SerializeField] private Slider _slider;

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

    public void LoadScene()
    {
        switch (_scene)
        {
            case SceneEnum.tutorial:
                TransitionManager.Instance.Transition("Tutorial",  Tutorial, 0f);
                break;
            case SceneEnum.start:
                TransitionManager.Instance.Transition("Game",  GameScene, 0f);
                break;
            case SceneEnum.setting:
                break;
            case SceneEnum.exit:
                break;
        }
    }

    private void Update()
    {
        if (cnt > 8)
        {
            cnt = 0;
            LoadScene();
        }
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _slider.value -= 0.1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _slider.value += 0.1f;
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            Tweening(_tutorial);
            if (_scene != SceneEnum.tutorial)
            {
                _scene = SceneEnum.tutorial;
                cnt = 0;
            }
            cnt++;

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Tweening(_start);
            if (_scene != SceneEnum.start)
            {
                _scene = SceneEnum.start;
                cnt = 0;
            }
            cnt++;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Tweening(_setting);
            if (_scene != SceneEnum.setting)
            {
                _scene = SceneEnum.setting;
                cnt = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Tweening(_exit);
            if (_scene != SceneEnum.exit)
            {
                _scene = SceneEnum.exit;
                cnt = 0;
            }
        }
    }
}
