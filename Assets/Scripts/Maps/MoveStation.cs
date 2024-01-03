using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveStation : MonoBehaviour
{
	public bool isValid = false;
	public UnityEvent onEnterPoint;

	public void SetValid()
	{
		isValid = true;
	}

	public void ResetValid()
	{
		isValid = false;
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
