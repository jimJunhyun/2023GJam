using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveStation : MonoBehaviour
{
	public bool isValid = false;
	public UnityEvent onEnterPoint;

	public List<ParticleSystem> portals;

	private void Awake()
	{
		portals = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
		for (int i = 0; i < portals.Count; i++)
		{
			portals[i].Stop();
		}
	}

	public void SetValid()
	{
		isValid = true;
		for (int i = 0; i < portals.Count; i++)
		{
			portals[i].Play();
		}
	}

	public void ResetValid()
	{
		isValid = false;
		for (int i = 0; i < portals.Count; i++)
		{
			portals[i].Stop();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isValid)
		{
			if (other.GetComponent<Player>())
			{
				onEnterPoint.Invoke();
			}
		}
	}
}
