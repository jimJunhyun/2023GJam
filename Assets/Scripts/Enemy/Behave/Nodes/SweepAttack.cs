using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SweepAttack : INode
{
	public NavMeshAgent agent;
	public float radius;
	public float angle;
	public Transform target;
	public float damage;
	public float cooldown;
	
	float prevOp;

	public SweepAttack(NavMeshAgent agt, float rad, float agl, Transform targ, float dam, float cd)
	{
		agent = agt;
		radius = rad;
		angle = agl;
		target = targ;
		damage = dam;
		cooldown = cd;
	}


	public NodeStat Examine()
	{
		if(Time.time - prevOp > cooldown)
		{
			prevOp = Time.time;

			agent.isStopped = true;
			agent.SetDestination(agent.transform.position);

			agent.transform.LookAt(target);
			Vector3 v = (target.transform.position - agent.transform.position);
			if (v.sqrMagnitude < radius * radius)
			{
				if (Mathf.Acos(Vector3.Dot(Vector3.forward, v) / v.magnitude) < angle)
				{
					target.GetComponent<LifeObject>().Damage(damage);
				}
			}
		}

		return NodeStat.Run;
	}
}
