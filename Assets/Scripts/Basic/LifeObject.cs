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


	public AudioClip hitSound;
	public AudioClip critSound;

	public EffectObject dieEff;

	
	public bool isDieable = true;


	public virtual void Damage(float amt, Detection _dec = Detection.none)
	{
		Debug.Log("판정 : " + _dec);
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
				if(_dec == Detection.Perfect)
				{
					SoundManager.Instance.PlaySFX(critSound, SoundSetting.SFX);
					DamageFont damageObj = PoolManager.Instance.Pop("DamageFont") as DamageFont;
					damageObj.Init(transform, (int)amt, Color.blue);
				}
				else
				{
					SoundManager.Instance.PlaySFX(hitSound, SoundSetting.SFX);
					DamageFont damageObj = PoolManager.Instance.Pop("DamageFont") as DamageFont;
					damageObj.Init(transform, (int)amt, Color.red);
				}

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
				if (_dec == Detection.Perfect)
				{
					SoundManager.Instance.PlaySFX(critSound, SoundSetting.SFX);
					DamageFont damageObj = PoolManager.Instance.Pop("DamageFont") as DamageFont;
					damageObj.Init(transform, (int)amt, Color.blue);
				}
				else
				{
					DamageFont damageObj = PoolManager.Instance.Pop("DamageFont") as DamageFont;
					damageObj.Init(transform, (int)amt, Color.white);
					SoundManager.Instance.PlaySFX(hitSound, SoundSetting.SFX);
				}
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
			ShootEffect(dieEff, gameObject);
			dead = true;
			if (isDieable)
			{
				gameObject.SetActive(false);
			}
			AI ai;
			if(ai = GetComponent<AI>())
			{
				switch (ai.type)
				{
					case AttackType.Shoot:
						GameManager.Instance.player.Gold += 2;
						break;
					case AttackType.Spin:
					case AttackType.Sweep:
						GameManager.Instance.player.Gold += 2;
						break;
					case AttackType.Body:
						GameManager.Instance.player.Gold += 1;
						break;
					case AttackType.Charge:
						GameManager.Instance.player.Gold += 3;
						break;
				}
				GameManager.Instance.player.RemoveArrow(transform);
			}
			onDead?.Invoke();
		}
	}

	public void ResetCompletely()
	{
		hp = maxHp;
		dead = false;
	}

	public void ShootEffect(PoolAble _mono, GameObject obj)
	{
		EffectObject ef = PoolManager.Instance.Pop(_mono.name) as EffectObject;
		Debug.LogWarning($"Effect : {GameManager.Instance.player.GetInstanceID()}");
		ef.Init(obj.transform.position);
	}
}
