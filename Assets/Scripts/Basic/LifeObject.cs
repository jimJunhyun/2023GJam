using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeObject : MonoBehaviour
{
    public float hp;
	public float maxHp;

	public bool dead = false;

	public UnityAction onDead;


	public void Damage(float amt)
	{
		hp -= amt;
		if(hp <= 0)
		{
			
			OnDead();
		}
	}

	public void OnDead()
	{
		dead = true;
		gameObject.SetActive(false);
		onDead?.Invoke();
	}

	public void ResetCompletely()
	{
		hp = maxHp;
		dead = false;
	}
}
