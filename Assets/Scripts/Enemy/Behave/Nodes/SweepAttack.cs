using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SweepAttack : INode
{
	public Rigidbody agent;
	public float radius;
	public float angle;
	public Transform target;
	public float damage;

	private Action _objs;

	public SweepAttack(Rigidbody agt, float rad, float agl, Transform targ, float dam, Action obj = null)
	{
		agent = agt;
		radius = rad;
		angle = agl;
		target = targ;
		damage = dam;
		_objs = obj;
	}


	public NodeStat Examine()
	{
		agent.velocity = Vector3.zero;

		Vector3 v = (target.transform.position - agent.transform.position);
		v.y = 0;
		agent.transform.rotation = Quaternion.LookRotation(v.normalized);
		
		_objs?.Invoke();		
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
