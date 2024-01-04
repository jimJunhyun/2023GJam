using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSettingType : MonoBehaviour
{
    public SoundSetting soundSetting;

    public TMP_Text typeText;
    public Slider slider;
    public TMP_Text percentText;
    public Toggle toggle;


    private void OnEnable()
    {
        Load();

        if(slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }
        if(percentText == null)
        {
            percentText = transform.Find("Percent").GetComponent<TMP_Text>();
        }
        if(toggle == null)
        {
            toggle = GetComponentInChildren<Toggle>();
        }
    }

    public void SetValue(float value)
    {
        if (value <= 0.0f) toggle.isOn = true;
        else toggle.isOn = false;

        SoundManager.Instance.MixerSave(soundSetting, value);
        percentText.text = $"{Mathf.Round(value*100)}%";
    }
    public void Load()
    {
        float value = SoundManager.Instance.GetValue(soundSetting);

        //if (value <= 0.0f) toggle.isOn = true;
        //else toggle.isOn = false;
        slider.value = value;
        //percentText.text = $"{value}%";
    }
    public void MuteToggle(bool value)
    {
        if (value) //true면 음량 0으로
        {
            slider.value = 0.0f;
        }
    }

    public void ChangeColor(Color color)
    {
        typeText.color = color;
    }

    public void SetValueByKeyboard(bool isUp)
    {
        if (isUp)
        {
            slider.value += 0.01f;
        }
        else
        {
            slider.value -= 0.01f;
        }
    }
}
