using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootAttack : INode
{
	public Rigidbody agent;
	public Bullet bullet;
	public Transform target;
	public Transform shootPos;
	public float damage;
	public float power;

	public Action _act;

	public ShootAttack(Rigidbody agt, Bullet blt, Transform targ, Transform shPos, float dam, float pow, Action act)
	{
		agent = agt;
		bullet = blt;
		target = targ;
		shootPos = shPos;
		damage = dam;
		power = pow;

		_act = act;
	}

	public NodeStat Examine()
	{
		agent.velocity = Vector3.zero;
		if (target)
		{
			Vector3 dir = (target.position - agent.transform.position);
			dir.y = 0;
			agent.transform.rotation = Quaternion.LookRotation(dir.normalized);
			Debug.Log(agent.transform.name + " SHOOTING");
			Bullet blt = GameObject.Instantiate(bullet, shootPos.position, agent.transform.rotation);
			blt.dam = damage;
			blt.Shoot(power);
			
			_act.Invoke();
		}
		
		

		return NodeStat.Run;
	}
}
