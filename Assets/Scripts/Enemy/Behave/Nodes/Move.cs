using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : INode
{
	public Transform target;
	public NavMeshAgent agent;

	public Move(Transform targ, NavMeshAgent agt)
	{
		target = targ;
		agent = agt;
	}

	public NodeStat Examine()
	{
		agent.isStopped = false;
		agent.SetDestination(target.position);
		return NodeStat.Run;
	}
}
