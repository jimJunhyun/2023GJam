using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeObject : MonoBehaviour
{
    public float hp;
	public float maxHp;

	public bool dead = false;

	public bool isImmune = false;

	public UnityAction onDead;


	public void Damage(float amt)
	{
		if (!isImmune)
		{

			hp -= amt;
			SetImmuneFor(0.3f);
			if(hp <= 0)
			{
				
				OnDead();
			}
		}
	}

	public void SetImmuneFor(float sec)
	{
		StartCoroutine(DelImmuner(sec));

	}


	IEnumerator DelImmuner(float sec)
	{
		isImmune  = true;
		yield return new WaitForSeconds(sec);
		isImmune = false;
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
