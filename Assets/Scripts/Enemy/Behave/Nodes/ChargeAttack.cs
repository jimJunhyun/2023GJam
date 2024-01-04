using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAttack : INode
{
	public float chargeSpeed;
	public float maxChargeDist;
	public float maxChargeSec;

	public Rigidbody rig;
	public AI self;
	public Transform target;
	Coroutine c;

	bool charging = false;

	public Action _act;

	public ChargeAttack(Rigidbody rb, AI ai, Transform targ, float chrSpd, float maxChrDist, float maxChrSec, Action act = null)
	{
		rig = rb;
		self = ai;
		target = targ;
		chargeSpeed = chrSpd;
		maxChargeDist = maxChrDist;
		maxChargeSec = maxChrSec;

		_act = act;
	}

	public void StopCharge()
	{
		if (charging)
		{
			//Debug.Log("CHARGE CANCELEDD : "  + self.chargeDest);
			charging = false;
			self.examining = true;
			self.charging = false;
			self.ResetSpeed();
			self.chargeDest = Vector3.zero;
			GameManager.Instance.StopCoroutine(c);
			rig.velocity = Vector3.zero;
			self.anim.ResetTrigger(self.AttackHash);
		}
	}

	IEnumerator DelStopCharge()
	{
		yield return new WaitForSeconds(maxChargeSec);
		if (charging)
		{
			//Debug.Log("CHARGE STOPPED : " + self.chargeDest);
			charging = false;
			self.examining = true;
			self.charging = false;
			self.chargeDest = Vector3.zero;
			rig.velocity = Vector3.zero;
		}
	}

	public NodeStat Examine()
	{
		if (!charging)
		{
			_act.Invoke();	
			
			charging = true;
			rig.velocity = Vector3.zero;
			self.examining = false;
			Vector3 dest = (target.transform.position - rig.transform.position).normalized * maxChargeDist;
			//dest += rig.transform.position;
			self.chargeDest = dest + rig.transform.position;
			rig.AddForce(dest, ForceMode.Impulse);
			//Debug.Log("CHARGE STARTED TO : " + dest);
			self.charging = true;
			c = GameManager.Instance.StartCoroutine(DelStopCharge());
		}
		
		return NodeStat.Run;
	}
}
