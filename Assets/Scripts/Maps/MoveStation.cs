using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveStation : MonoBehaviour
{
	public bool isValid = false;
	public UnityEvent onEnterPoint;

	public List<ParticleSystem> portals;
	public ArrowType type;

	private void Awake()
	{
		portals = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
		GameManager.Instance.player.RemoveArrow(transform);
		for (int i = 0; i < portals.Count; i++)
		{
			portals[i].Stop();
		}
	}

	public void SetValid()
	{
		isValid = true;
		GameManager.Instance.player.ShowArrow(transform, type);
		for (int i = 0; i < portals.Count; i++)
		{
			portals[i].Play();
		}
	}

	public void ResetValid()
	{
		isValid = false;
		GameManager.Instance.player.RemoveArrow(transform);
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
