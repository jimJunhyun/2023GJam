using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType
{
	Shoot,
	Sweep, 
	Body,

}

public class AI : MonoBehaviour, IRhythm
{
	public float atkRange;
	public float atkGap;
	public float atkDam;

	public AttackType type;
	
	public Bullet myBullet;
	public Transform shootPos;
	public float shootPow;

	public float angle;

    Selecter head;
	NavMeshAgent agent;

	SetFlag metronome;

	public bool examining;

	public virtual void Awake()
	{
		agent = GetComponent<NavMeshAgent>();

		Sequencer doAttack = new Sequencer();

		metronome = new SetFlag();
		doAttack.childs.Add(metronome);

		IsInRange inRange = new IsInRange(atkRange, GameManager.instance.player.transform, transform);
		doAttack.childs.Add(inRange);

		if(type == AttackType.Sweep)
		{
			SweepAttack sweep = new SweepAttack(agent, atkRange, angle, GameManager.instance.player.transform, atkDam, atkGap);
			doAttack.childs.Add(sweep);
		}
		else if( type == AttackType.Shoot)
		{
			ShootAttack shoot = new ShootAttack(agent, myBullet, GameManager.instance.player.transform, shootPos, atkGap, atkDam, shootPow);
			doAttack.childs.Add(shoot);
		}

		Move doMove = new Move(GameManager.instance.player.transform, agent);



		head = new Selecter();

		if(type != AttackType.Body)
		{
			head.childs.Add(doAttack);
		}
		head.childs.Add(doMove);
	}

	public virtual void Update()
	{
		if (examining)
		{
			head.Examine();
		}
	}

	public void BeatUpdate()
	{
		metronome.Set();
	}

	public void BeatUpdateDivideFour()
	{
		metronome.Set();
	}
}
