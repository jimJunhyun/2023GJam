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
    [SerializeField] private UIUpFont _up;

    [SerializeField] private Transform SettingUICanvas;
    [SerializeField] private Transform MainUICanvas;

    private SceneEnum _scene;

    private int cnt = 0;
    private bool Go = false;
    

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
                TransitionManager.Instance.Transition("SelectScene",  GameScene, 0f);
                break;
            case SceneEnum.setting:
                SettingUICanvas.gameObject.SetActive(true);
                MainUICanvas.gameObject.SetActive(false);
                break;
            case SceneEnum.exit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
                break;
        }
    }

    private void Update()
    {
        if (cnt > 8 && Go ==false)
        {
            Go = true;
            LoadScene();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SettingUICanvas.gameObject.activeSelf == true)
            {
                SettingUICanvas.gameObject.SetActive(false);
                MainUICanvas.gameObject.SetActive(true);
            }
            cnt = 0;
            Go = false;
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
            
            UIUpFont ui = PoolManager.Instance.Pop(_up.name) as UIUpFont;
            if (9 - cnt >= 1)
            {
                ui.Init(_tutorial.rectTransform,$"{9-cnt}");
                
            }
            else
            {
                ui.Init(_tutorial.rectTransform,$"GO!");
            }

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
            UIUpFont ui = PoolManager.Instance.Pop(_up.name) as UIUpFont;
            if (9 - cnt >= 1)
            {
                ui.Init(_start.rectTransform,$"{9-cnt}");
                
            }
            else
            {
                ui.Init(_start.rectTransform,$"GO!");
            }

        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            Tweening(_setting);
            if (_scene != SceneEnum.setting)
            {
                _scene = SceneEnum.setting;
                cnt = 0;
            }
            cnt++;
            UIUpFont ui = PoolManager.Instance.Pop(_up.name) as UIUpFont;
            if (9 - cnt >= 1)
            {
                ui.Init(_setting.rectTransform,$"{9-cnt}");
                
            }
            else
            {
                ui.Init(_setting.rectTransform,$"GO!");
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
            cnt++;


            UIUpFont ui = PoolManager.Instance.Pop(_up.name) as UIUpFont;
            if (9 - cnt >= 1)
            {
                ui.Init(_exit.rectTransform,$"{9-cnt}");

                
            }
            else
            {
                ui.Init(_exit.rectTransform,$"GO!");
            }
        }
    }
}
