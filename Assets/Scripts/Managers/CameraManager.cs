using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{

	public const int FRONTCAM = 25;
	public const int BACKCAM = 15;

	private CinemachineVirtualCamera _pCam;
	CinemachineBasicMultiChannelPerlin noise;

	public CinemachineVirtualCamera pCam
	{
		get
		{
			if (_pCam == null)
			{
				_pCam = GameObject.Find("PCam").GetComponent<CinemachineVirtualCamera>();
			}
			return _pCam;
		}
	}


	private Camera _mainCam;
	public Camera MainCamera
	{
		get
		{
			if(_mainCam==null)
				_mainCam =Camera.main;

			return _mainCam;
		}
	}

	private void Awake()
	{
		noise = pCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		Debug.Log(noise);
	}

	public void ShakeCamFor(float amt, float freq, float dur)
	{
		
		StartCoroutine(DelShaker(amt, freq, dur));
	}

	IEnumerator DelShaker(float amt, float freq, float dur)
	{
		noise.m_AmplitudeGain = amt;
		noise.m_FrequencyGain = freq;
		yield return new WaitForSecondsRealtime(dur);
		noise.m_AmplitudeGain = 0;
		noise.m_FrequencyGain = 0;
	}
}
