using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{

	public const int FRONTCAM = 25;
	public const int BACKCAM = 15;

	private CinemachineVirtualCamera _pCam;

	public CinemachineVirtualCamera pCam
	{
		get
		{
			if(_pCam==null)
				_pCam = GameObject.Find("PCam").GetComponent<CinemachineVirtualCamera>();
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
}
