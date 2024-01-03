using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Volume))]
public class MapPP : MonoBehaviour
{
    private Volume volume;
    //[Range(0f, 1f)]
    //public float testValue;
    public float minSaturation = -58f, maxSaturation = 11f;
    public float time = 3f;
    private WaitForSeconds wait = new WaitForSeconds(0.01f);
    private void OnEnable()
    {
        volume = GetComponent<Volume>();
    }
    /*
    private void Update()
    {
        AdjustSaturation(testValue);
    }
    */

    /// <summary>
    /// value는 0과 1 사이여야함.
    /// </summary>
    /// <param name="value"></param>
    public void AdjustSaturation(bool isClear)
    {
        volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments ca);
        if(ca != null)
        {
            //-58에서 11까지
            if (isClear)
            {
                StartCoroutine(Adjust(ca));
            }
            else
            {
                ca.saturation.value = minSaturation;
            }
        }
    }

    private IEnumerator Adjust(ColorAdjustments ca)
    {
        float t = 0.0f;
        while(t <= time)
        {
            t += 0.01f;
            ca.saturation.value = Mathf.Lerp(minSaturation,maxSaturation,t);
            yield return wait;
        }
    }
}
