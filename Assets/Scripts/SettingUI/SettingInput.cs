using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SettingInput : MonoBehaviour
{
    int currnetIdx = 0;
    public List<SoundSettingType> types = new();

    public Color offColor = new Color(239f, 215f, 155f);
    public Color onColor = new Color(182f, 239f, 155f);

    private WaitForSeconds shortWait = new WaitForSeconds(0.01f);
    private WaitForSeconds longWait = new WaitForSeconds(0.1f);
    private void OnEnable()
    {
        currnetIdx = 0;
        StopAllCoroutines();    
        StartCoroutine(HorizonInput());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Turn(false);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Turn(true);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetCurrentSoundValue(false);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetCurrentSoundValue(true);
        }
    }
    public void Turn(bool isDown)
    {
        types[currnetIdx].ChangeColor(offColor);

        if(isDown)
        {
            currnetIdx++;
        }
        else
        {
            currnetIdx--;
        }
        currnetIdx =  Mathf.Clamp(currnetIdx, 0, types.Count - 1);

        types[currnetIdx].ChangeColor(onColor);
    }

    public void SetCurrentSoundValue(bool isUp)
    {
        types[currnetIdx].SetValueByKeyboard(isUp);
    }

    private IEnumerator HorizonInput()
    {
        float pressedTime = 0f;
        bool lastPressIsLeft = false;
        while (true)
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (h > 0)
            {
                SetCurrentSoundValue(true);
                if (lastPressIsLeft)
                {
                    pressedTime = 0f;
                }
                else pressedTime += (pressedTime >= 1f ? 0.01f : 0.1f);

                lastPressIsLeft = false;
            }
            else if (h < 0)
            {
                SetCurrentSoundValue(false);
                if (lastPressIsLeft)
                {
                    pressedTime += (pressedTime >= 1f ? 0.01f : 0.1f);
                }
                else pressedTime = 0f;
                lastPressIsLeft = true;
            }
            else pressedTime = 0.0f;
            if (pressedTime >= 0.35f) yield return shortWait;
            else yield return longWait;
        }
    }
}
