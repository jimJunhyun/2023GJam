using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoNothing : INode
{
	public NavMeshAgent agent;

	public DoNothing(NavMeshAgent agt)
	{
		agent = agt;
	}

	public NodeStat Examine()
	{
		agent.SetDestination(agent.transform.position);
		agent.isStopped = true;
		return NodeStat.Run;
	}
}
