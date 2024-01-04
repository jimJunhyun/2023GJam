using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurseManager : MonoBehaviour
{
    [SerializeField] private TransitionSettings _setted;
    public Transform miniMapUI;
    public Transform rhythmUI;
    public Transform purseUI;

    public bool isPurse = false;

    public float leftPressTime = 2.0f;
    private float pressTime = 0.0f;
    public Slider slider;

    private void OnEnable()
    {
        miniMapUI.gameObject.SetActive(true);
        rhythmUI.gameObject.SetActive(true);
        purseUI.gameObject.SetActive(false);

        isPurse = false;
    }

    public void Purse(bool value)
    {
        miniMapUI.gameObject.SetActive(!value);
        rhythmUI.gameObject.SetActive(!value);
        purseUI.gameObject.SetActive(value);

        if (value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pressTime += Time.unscaledDeltaTime;
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(pressTime >= leftPressTime)
            {
                Time.timeScale = 1;
                TransitionManager.Instance.Transition("StartScene", _setted, 0.1f);
            }
            else if(pressTime <= leftPressTime/4)
            {
                isPurse = !isPurse;
                Purse(isPurse);
            }
            pressTime = 0.0f;
        }
        slider.value = Mathf.Lerp(0, 1, pressTime / leftPressTime);
    }
}
