using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
//using LitJson
//using UnityEngine.Purchasing.MiniJSON;


public enum SoundSetting
{
    Master,
    background,
    SFX,
    UISound,
    Battle,
    KickDrum,
    SnareDrum
}

public class SoundJson
{
    public float Master = 50.0f;
    public float SFX= 50.0f;
    public float UI= 50.0f;
    public float BGM= 50.0f;
    public float Battle = 50.0f;
    public float KickDrum = 50.0f;
    public float SnareDrum = 50.0f;
}

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioSource _backgroundSound;
    public AudioMixer _mixer;

    private SoundJson _data;

    private void Init()
    {
        SoundJson savedata = new SoundJson();
        try
        {
            string path = Path.Combine(Application.dataPath, "playerData.json");
            string jsonData = File.ReadAllText(path);
            savedata = JsonUtility.FromJson<SoundJson>(jsonData);
            Debug.LogWarning("성공");
        }
        catch
        {
            string jsonData = JsonUtility.ToJson(savedata);
            string path = Path.Combine(Application.dataPath, "playerData.json");
            File.WriteAllText(path, jsonData);
            Debug.LogWarning("첫실행 or 그냥 애러");
        }


        _data = savedata;
        _data.Master = 100;
        print(savedata.Master);
        MixerSave(SoundSetting.Master, _data.Master);
        MixerSave(SoundSetting.background, _data.BGM);
        MixerSave(SoundSetting.UISound, _data.UI);
        MixerSave(SoundSetting.SFX,_data.SFX);
        MixerSave(SoundSetting.Battle, _data.Battle);
        MixerSave(SoundSetting.KickDrum, _data.KickDrum);
        MixerSave(SoundSetting.SnareDrum, _data.SnareDrum);
    }

    public void MixerSave(SoundSetting ss, float value)
    {
        if(_data==null)
            Init();
        
        //value = Mathf.RoundToInt(value);
        if (value<= .0f)
        {
            _mixer.SetFloat(ss.ToString(), -80f);
        }
        else
        {
            _mixer.SetFloat(ss.ToString(), Mathf.Lerp(-40,0, value));
        }
        switch (ss)
        {
            case SoundSetting.Master:
                _data.Master = value;
                break;
            case SoundSetting.UISound:
                _data.UI = value;
                break;
            case SoundSetting.SFX:
                _data.SFX = value;
                break;
            case SoundSetting.background:
                _data.BGM = value;
                break;
            case SoundSetting.Battle:
                _data.Battle = value;
                break;
            case SoundSetting.KickDrum:
                _data.KickDrum = value;
                break;
            case SoundSetting.SnareDrum:
                _data.SnareDrum = value;
                break;
            default:
                break;
        }


        string path = Path.Combine(Application.dataPath, "playerData.json");
        string jsonData = JsonUtility.ToJson(_data);
        File.WriteAllText(path, jsonData);
        
        Debug.LogWarning($"VOLUM SET : {value}");
    }

    public float GetValue(SoundSetting ss)
    {
        if(_data==null)
            Init();

        switch (ss)
        {
            case SoundSetting.Master:
                return _data.Master;
            case SoundSetting.UISound:
                return _data.UI;
            case SoundSetting.SFX:
                return _data.SFX;
            case SoundSetting.background:
                return _data.BGM;
            case SoundSetting.Battle:
                return _data.Battle;
            case SoundSetting.KickDrum:
                return _data.KickDrum;
            case SoundSetting.SnareDrum:
                return _data.SnareDrum;
            default:
                break;
        }

        return _data.Master;
    }

    private List<AudioSource> _sfxList = new();
    public void PlaySFX(AudioClip _soundType, SoundSetting soundSet, Transform tls = null)
    {
        if (_soundType == null)
        {
            Debug.LogWarning("Sound is Null");
            return;
        }
        
        
        GameObject bg = new GameObject();
        if (tls != null)
        {
            bg.transform.position = tls.position;
        }
        
        AudioSource _audioSource = bg.AddComponent<AudioSource>();

        AudioMixerGroup[] _ad = _mixer.FindMatchingGroups("SFX");
        
        switch (soundSet)
        {
            case SoundSetting.SFX:
                _ad = _mixer.FindMatchingGroups("SFX");
                break;
            case SoundSetting.UISound:
                _ad = _mixer.FindMatchingGroups("UISound");
                break;
            case SoundSetting.Battle:
                _ad = _mixer.FindMatchingGroups("Battle");
                break;
            case SoundSetting.KickDrum:
                _ad = _mixer.FindMatchingGroups("KickDrum");
                break;
            case SoundSetting.SnareDrum:
                _ad = _mixer.FindMatchingGroups("SnareDrum");
                break;
            default:
                break;
        }


        _audioSource.outputAudioMixerGroup = _ad[0];

        _audioSource.clip = _soundType;
        _audioSource.Play();
        _sfxList.Add(_audioSource);
        StartCoroutine(StopSound(_audioSource));
    }


    IEnumerator StopSound(AudioSource t)
    {
        yield return new WaitUntil(() => t.isPlaying == false);
        Debug.LogWarning("삭제");
        Destroy(t.gameObject);
    }

    public void StopAllSFXSound()
    {
        StopAllCoroutines();
        _sfxList.ForEach((a)=> Destroy(a));  
    }

    public void PlayBGM(AudioClip _bgType)
    {
        if (_backgroundSound != null)
        {
            _backgroundSound.Stop();

            Destroy(_backgroundSound.gameObject);
        }

        
        GameObject bg = Instantiate(new GameObject());
        
        //DontDestroyOnLoad(bg);
        _backgroundSound = bg.AddComponent<AudioSource>();
        AudioMixerGroup[] _ad = _mixer.FindMatchingGroups("background");
        
        _backgroundSound.outputAudioMixerGroup = _ad[0];
        _backgroundSound.clip = _bgType;
        _backgroundSound.loop = true;
        _backgroundSound.Play();
        
        Debug.LogWarning($"BGM Succcess");
    }


}
