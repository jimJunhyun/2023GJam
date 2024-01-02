using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeAttack : INode
{
	public float chargeSpeed;
	public float maxChargeDist;
	public float maxChargeSec;

	public NavMeshAgent agent;
	public AI self;
	public Transform target;


	bool charging = false;
	

	public ChargeAttack(NavMeshAgent agt, AI ai, Transform targ, float chrSpd, float maxChrDist, float maxChrSec)
	{
		agent = agt;
		self = ai;
		target = targ;
		chargeSpeed = chrSpd;
		maxChargeDist = maxChrDist;
		maxChargeSec = maxChrSec;
	}

	public void StopCharge()
	{
		if (charging)
		{
			charging = false;
			self.examining = true;
			self.charging = false;
			self.ResetSpeed();
		}
	}

	IEnumerator DelStopCharge()
	{
		yield return new WaitForSeconds(maxChargeSec);
		if (charging)
		{
			charging = false;
			self.examining = true;
			self.charging = false;
		}
	}

	public NodeStat Examine()
	{
		charging = true;
		agent.speed = chargeSpeed;
		agent.acceleration = chargeSpeed;
		self.examining = false;
		Vector3 dest = (target.transform.position - agent.transform.position).normalized * maxChargeDist;
		if(NavMesh.SamplePosition(dest, out NavMeshHit hit, 20, -1))
		{
			agent.SetDestination(hit.position);
		}
		self.charging = true;
		GameManager.Instance.StartCoroutine(DelStopCharge());
		return NodeStat.Run;
	}
}
