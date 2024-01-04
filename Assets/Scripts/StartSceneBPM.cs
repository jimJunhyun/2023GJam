using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneBPM : MonoBehaviour
{
    [SerializeField] private Slider _sld;
    [SerializeField] private TextMeshProUGUI _tmp;
    
    void Update()
    {
        BeatSystem.Instance.ReturnBPM = (int)_sld.value;
        _tmp.text = $"{(int)_sld.value} BPM";
    }
}
