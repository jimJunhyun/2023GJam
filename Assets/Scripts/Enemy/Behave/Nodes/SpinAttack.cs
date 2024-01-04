using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpinAttack : INode
{
	public float spinSpeed;
	public int spinDur;

	int curDur = 0;

	bool spinning = false;

	AI self;

	private Action _act = null;
	
	public SpinAttack( AI s, int dur, float spinSpd, Action act)
	{
		self = s;
		spinDur = dur;

		spinSpeed = spinSpd;

		_act = act;
	}

	public void OnNextBeat()
	{
		curDur += 1;
		if (curDur >= spinDur)
		{
			Debug.Log("SPINSTOP");
			curDur = 0;
			spinning = false;
			self.ResetSpeed();
			self.spinning = false;
			self.anim.ResetTrigger(self.AttackHash);
		}
	}

	public NodeStat Examine()
	{
		if (!spinning)
		{
			spinning =true;
			Debug.Log("SPINGO");
			self.stat.SPEED = spinSpeed;
			self.spinning = true;
			
			_act.Invoke();
			
		}

		return NodeStat.Run;
	}
}
