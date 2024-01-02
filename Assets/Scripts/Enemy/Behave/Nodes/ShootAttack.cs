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
	public float damage;
	public float power;

	public ShootAttack(NavMeshAgent agt, Bullet blt, Transform targ, Transform shPos, float dam, float pow)
	{
		agent = agt;
		bullet = blt;
		target = targ;
		shootPos = shPos;
		damage = dam;
		power = pow;
	}

	public NodeStat Examine()
	{
		agent.isStopped = true;
		agent.SetDestination(agent.transform.position);

		Vector3 dir = (target.position - agent.transform.position);
		dir.y = 0;
		agent.transform.rotation = Quaternion.LookRotation(dir.normalized);

		Bullet blt = GameObject.Instantiate(bullet, shootPos.position, agent.transform.rotation);
		blt.dam = damage;
		blt.Shoot(power);
		

		return NodeStat.Run;
	}
}
