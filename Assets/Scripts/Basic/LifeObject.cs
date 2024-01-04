using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeObject : MonoBehaviour
{
	[Header("HitEffect")] public EffectObject _effect;
	public Vector3 _correctionVector = new Vector3(0, 0.5f, 0);
	
    public float hp;
	public float maxHp;

	public bool dead = false;

	public bool isImmune = false;

	public UnityAction onDead;


	public virtual void Damage(float amt, Detection _dec = Detection.none)
	{
		Debug.Log($"{gameObject.name} 데미지!!!");
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

			if (_effect)
			{
				EffectObject ef = PoolManager.Instance.Pop(_effect.name) as EffectObject;
				ef.Init(transform.position + _correctionVector);
			}
			else
			{
				Debug.LogError($"{gameObject} 히트 이팩트 없음");
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
		else
		{
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
