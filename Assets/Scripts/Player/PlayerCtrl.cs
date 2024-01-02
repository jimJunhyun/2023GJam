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
	LifeObject life;
	//PlayerAttack atk;

	Vector3 moveVec = Vector3.zero;
	Vector3 forceVec = Vector3.zero;

	Vector3? predictPos = null;

	private void Awake()
	{
		ctrl = GetComponent<CharacterController>();
		life = GetComponent<LifeObject>();
		//atk = GetComponent<PlayerAttack>();
	}

	public void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		moveVec = new Vector3(h, 0, v).normalized * speed;
		
		if(Mathf.Abs(h) >= 0.2 || Mathf.Abs(v) >= 0.2)
			transform.rotation = Quaternion.LookRotation(moveVec);

		if (!ctrl.isGrounded)
		{
			forceVec.y -= GameManager.GRAVITY * Time.deltaTime;
		}
		else
		{
			forceVec.y = 0;
		}
		ctrl.Move((moveVec + forceVec) * Time.deltaTime);
	}

	private void LateUpdate()
	{
		if(predictPos != null)
		{
			transform.position = (Vector3)predictPos;
			predictPos = null;
		}
	}

	public void SetPosition(Vector3 pos)
	{
		pos.y = transform.position.y;
		predictPos = pos;
	}

	public void SetStat(Stat s)
	{
		life.maxHp = s.MaxHP;
		life.ResetCompletely();
		//atk.attack = s.ATK;
		speed = s.SPEED;
	}
}
