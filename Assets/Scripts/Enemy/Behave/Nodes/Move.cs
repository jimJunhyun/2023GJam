using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : INode
{
	public Transform target;
	public Rigidbody rig;
	public NavMeshAgent agent;
	AI self;

	public Move(Transform targ, Rigidbody rig, AI s, NavMeshAgent agt)
	{
		target = targ;
		this.rig = rig;
		self = s;
		agent = agt;
	}

	public NodeStat Examine()
	{
		agent.isStopped = false;
		agent.SetDestination(target.position);
		rig.velocity = Vector3.zero;
		rig.constraints |= RigidbodyConstraints.FreezePositionX;
		rig.constraints |= RigidbodyConstraints.FreezePositionY;
		rig.constraints |= RigidbodyConstraints.FreezePositionZ;
		return NodeStat.Run;
	}
}
