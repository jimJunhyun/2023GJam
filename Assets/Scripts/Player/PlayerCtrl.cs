using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeObject))]
public class PlayerCtrl : MonoBehaviour
{
	public float speed = 5f;
	float h;
	float v;

	CharacterController ctrl;
	internal Animator anim;
	//PlayerAttack atk;

	Vector3 moveVec = Vector3.zero;
	Vector3 forceVec = Vector3.zero;

	Vector3? predictPos = null;

	bool stopState = false;

	

	internal readonly int IdleHash = Animator.StringToHash("Idle");

	private void Awake()
	{
		ctrl = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();
		//atk = GetComponent<PlayerAttack>();
	}

	public void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		moveVec = new Vector3(h, 0, v).normalized * speed;

		moveVec = Quaternion.Euler(0, 45, 0) * moveVec;
		
		if(Mathf.Abs(h) >= 0.1 || Mathf.Abs(v) >= 0.1)
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
		if(predictPos != null)
		{
			ctrl.enabled = false;
			transform.position = (Vector3)predictPos;
			predictPos = null;
			ctrl.enabled = true;
		}
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

	public void SetPosition(Vector3 pos)
	{
		pos.y = transform.position.y;
		predictPos = pos;
	}

	public void SetStat(Stat s)
	{
		//life.maxHp = s.MaxHP;
		//life.ResetCompletely();
		//atk.attack = s.ATK;
		speed = s.SPEED;
	}
}
