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
	public AI ai;

	public ShootAttack(Rigidbody agt, Bullet blt, Transform targ, Transform shPos, float dam, float pow, AI ai)
	{
		agent = agt;
		bullet = blt;
		target = targ;
		shootPos = shPos;
		damage = dam;
		power = pow;
		this.ai = ai;
	}

	public NodeStat Examine()
	{
		ai.StopFor(1f);
		if (target)
		{
			Vector3 dir = (target.position - agent.transform.position);
			dir.y = 0;
			agent.transform.rotation = Quaternion.LookRotation(dir.normalized);
			Debug.Log(agent.transform.name + " SHOOTING");
			Bullet blt = GameObject.Instantiate(bullet, shootPos.position, agent.transform.rotation);
			blt.dam = damage;
			blt.Shoot(power);
		}
		
		

		return NodeStat.Run;
	}
}
