using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
	public static CameraManager instance;

	public const int FRONTCAM = 25;
	public const int BACKCAM = 15;

	public CinemachineVirtualCamera pCam;

	private void Awake()
	{
		instance = this;
		pCam = GameObject.Find("PCam").GetComponent<CinemachineVirtualCamera>();
	}
}
