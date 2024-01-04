using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LifeObject : LifeObject
{

    internal readonly int HitHash = Animator.StringToHash("Hit");
    internal readonly int DeathHash = Animator.StringToHash("Death");


	private void Start()
	{
		onDead += () => 
        {
            //change scene to game over
            Debug.Log("!@#!@#!@#!@#");
            GameManager.Instance.player.playerCtrl.anim.SetTrigger(DeathHash);
            GameManager.Instance.player.playerCtrl.CompleteStop();
            GameManager.Instance.curRoom.ResetEnemy();
            GameManager.Instance.StopAllCoroutines();

            GameObject.FindAnyObjectByType<PlayerDeath>().Death();
        };
	}

	public override void Damage(float amt, Detection _dec = Detection.none)
    {
        if (!isImmune && !dead)
        {
            GameManager.Instance.player.ModifyHPPlus((int)(-amt));
            
            EffectObject ef = PoolManager.Instance.Pop(_effect.name) as EffectObject;
            ef.Init(transform.position);
            
            
            SetImmuneFor(0.6f);
            SoundManager.Instance.PlaySFX(hitSound, SoundSetting.SFX);
            GameManager.Instance.player.playerCtrl.anim.SetTrigger(HitHash);
            GameManager.Instance.player.playerCtrl.StopFor(0.4f);
            DieCheck();
        }
    }

    public void DieCheck()
    {
        if (hp <= 0)
        {
            OnDead();
        }
    }
}
