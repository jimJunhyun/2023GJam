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


	public virtual void Damage(float amt, Detection _dec = Detection.none)
	{
		if (!isImmune)
		{

			hp -= amt;
			SetImmuneFor(0.6f);
			AI ai = GetComponent<AI>();
			if (ai)
			{
				ai.StopFor(0.4f);
				ai.anim.SetTrigger(ai.HitHash);
			}
			BossAI boss = GetComponent<BossAI>();
			if (boss)
			{
				boss.StopFor(0.2f);
				boss.anim.SetTrigger(boss.HitHash);
			}
		
			if(hp <= 0)
			{
				
				OnDead();
			}
			
		}
	}

	public void SetImmuneFor(float sec)
	{
		GameManager.Instance.StartCoroutine(DelImmuner(sec));

	}


	IEnumerator DelImmuner(float sec)
	{
		isImmune  = true;
		yield return new WaitForSeconds(sec);
		isImmune = false;
	}

	public void OnDead()
	{
		if (!dead)
		{

		dead = true;
		gameObject.SetActive(false);
		onDead?.Invoke();
		}
	}

	public void ResetCompletely()
	{
		hp = maxHp;
		dead = false;
	}
}
