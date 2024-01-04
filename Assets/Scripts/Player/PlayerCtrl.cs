using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeObject))]
public class PlayerCtrl : MonoBehaviour, IRhythm
{
	[Header("Effect")] public EffectObject _walkEffect;
	public EffectObject _walkTwo;
	
	float h;
	float v;

	CharacterController ctrl;
	internal Animator anim;
	//PlayerAttack atk;

	Vector3 moveVec = Vector3.zero;
	Vector3 forceVec = Vector3.zero;

	Vector3? predictPos = null;

	bool stopState = false;

	bool completeStop = false;

	internal readonly int IdleHash = Animator.StringToHash("Idle");

	private void Awake()
	{
		ctrl = GetComponent<CharacterController>();
		anim = transform.Find("LowMan").GetComponentInChildren<Animator>();
		//atk = GetComponent<PlayerAttack>();
	}

	public void Update()
	{

		if (!GameManager.Instance.player.IsInteractive && ! completeStop)
		{
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
			
		}
		else
		{
			h = 0;
			v = 0;
		}

		moveVec = new Vector3(h, 0, v).normalized * GameManager.Instance.player.PlayerStat.SPEED;

		moveVec = Quaternion.Euler(0, 45, 0) * moveVec;

		if (Mathf.Abs(h) >= 0.1 || Mathf.Abs(v) >= 0.1)
		{
			anim.SetBool(IdleHash, false);
			transform.rotation = Quaternion.LookRotation(moveVec);
		}
		else
		{
			anim.SetBool(IdleHash, true);
		}

		if (!ctrl.isGrounded)
		{
			forceVec.y -= GameManager.GRAVITY * Time.deltaTime;
		}
		else
		{
			forceVec.y = 0;
		}
		Vector3 mv = forceVec;
		if (!stopState)
		{
			mv += moveVec;
		}
		ctrl.Move(mv * Time.deltaTime);
		
			
	}

	private void LateUpdate()
	{
		if (GameManager.Instance.player.IsInteractive || completeStop)
			return;
		
		if(predictPos != null)
		{
			ctrl.enabled = false;
			transform.position = (Vector3)predictPos;
			predictPos = null;
			ctrl.enabled = true;
		}
	}

	public void CompleteStop()
	{
		completeStop = true;
	}

	public void StopFor(float sec)
	{
		GameManager.Instance.StartCoroutine(DelStopper(sec));
	}

	IEnumerator DelStopper(float s	)
	{
		stopState = true;
		yield return new WaitForSeconds(s);
		stopState = false;
	}

	public void SetPosition(Vector3 pos, bool yDiff = false)
	{
		if (GameManager.Instance.player.IsInteractive)
			return;
		if (!yDiff)
		{
			pos.y = transform.position.y;
		}

		predictPos = pos;
	}

	public void SetStat(Stat s)
	{
		//life.maxHp = s.MaxHP;
		//life.ResetCompletely();
		//atk.attack = s.ATK;
		//speed = s.SPEED;
	}

	public void BeatUpdate()
	{
		if (moveVec != Vector3.zero)
		{
			EffectObject ef = PoolManager.Instance.Pop(_walkEffect.name) as EffectObject;
			ef.Init(transform.position + new Vector3(0,-1,0));
			
			ef = PoolManager.Instance.Pop(_walkTwo.name) as EffectObject;
			ef.Init(transform.position + new Vector3(0,-1,0));
		}
	}

	public void BeatUpdateDivideFour()
	{
		if (moveVec != Vector3.zero)
		{
			Debug.Log(PoolManager.Instance);
			EffectObject ef = PoolManager.Instance.Pop(_walkEffect.name) as EffectObject;
			ef.Init(transform.position + new Vector3(0,-1,0));
			
			ef = PoolManager.Instance.Pop(_walkTwo.name) as EffectObject;
			ef.Init(transform.position + new Vector3(0,-1,0));
		}
	}
}
