using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : INode
{
	public Transform target;
	public Rigidbody agent;
	AI self;

	public Move(Transform targ, Rigidbody agt, AI s)
	{
		target = targ;
		agent = agt;
		self = s;
	}

	public NodeStat Examine()
	{
		agent.velocity = ((target.position - agent.position).normalized * self.stat.SPEED);
		return NodeStat.Run;
	}
}
