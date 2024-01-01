using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootAttack : INode
{
	public NavMeshAgent agent;
	public Bullet bullet;
	public Transform target;
	public Transform shootPos;
	public float cooldown;
	public float damage;
	public float power;

	float prevOp;

	public ShootAttack(NavMeshAgent agt, Bullet blt, Transform targ, Transform shPos, float cd, float dam, float pow)
	{
		agent = agt;
		bullet = blt;
		target = targ;
		shootPos = shPos;
		cooldown = cd;
		damage = dam;
		power = pow;
	}

	public NodeStat Examine()
	{
		if(Time.time - prevOp > cooldown)
		{
			prevOp = Time.time;

			agent.isStopped = true;
			agent.SetDestination(agent.transform.position);

			agent.transform.LookAt(target);

			Bullet blt = GameObject.Instantiate(bullet, shootPos.position, bullet.transform.rotation);
			blt.dam = damage;
			blt.Shoot(power);
		}

		return NodeStat.Run;
	}
}
