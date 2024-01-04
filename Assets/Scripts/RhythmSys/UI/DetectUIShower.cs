using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUIShower : MonoBehaviour
{
	public DetectUI detImg;

	public Sprite perfect;
	public Sprite good;
	public Sprite bad;

	Transform parent;

	private void Awake()
	{
		parent = GameObject.Find("DetectUIs").transform;
	}


	public void ShowDetectionUI(Vector3 pos, Detection data)
	{
		DetectUI img = Instantiate(detImg, pos, Quaternion.identity, parent);
		switch (data)
		{
			case Detection.Perfect:
				img.SetInfo(perfect, 0.5f);
				break;
			case Detection.Good:
				img.SetInfo(good, 0.5f);
				break;
			case Detection.Bad:
				img.SetInfo(bad, 0.5f);
				break;
		}
	}
}
