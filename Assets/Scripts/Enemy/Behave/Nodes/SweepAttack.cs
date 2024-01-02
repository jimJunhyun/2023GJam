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

	public SweepAttack(NavMeshAgent agt, float rad, float agl, Transform targ, float dam)
	{
		agent = agt;
		radius = rad;
		angle = agl;
		target = targ;
		damage = dam;
	}


	public NodeStat Examine()
	{
		agent.isStopped = true;
		agent.SetDestination(agent.transform.position);

		Vector3 v = (target.transform.position - agent.transform.position);
		v.y = 0;
		agent.transform.rotation = Quaternion.LookRotation(v.normalized);
		if (v.sqrMagnitude < radius * radius)
		{
			if (Mathf.Acos(Vector3.Dot(Vector3.forward, v) / v.magnitude) < angle)
			{
				Debug.Log(damage);
				target.GetComponent<LifeObject>().Damage(damage);
			}
		}
		

		return NodeStat.Run;
	}
}
