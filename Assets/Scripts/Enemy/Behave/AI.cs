using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType
{
	Shoot,
	Sweep, 
	Body,
	Charge,
	Spin,
}

[RequireComponent(typeof(LifeObject))]
public class AI : MonoBehaviour, IRhythm
{
	public float atkRange;
	public int atkGap;
	public Stat stat;

	public AttackType type;

	LifeObject life;
	
	public Bullet myBullet;
	public Transform shootPos;
	public float shootPow;

	public float angle;

    Selecter head;
	NavMeshAgent agent;

	SetFlag metronome;

	ChargeAttack charge;
	SpinAttack spin;

	float initSpeed;
	float initAcc;
	int beatCnt = 0;

	public bool examining;

	public bool spinning;
	public bool charging;
	public float chargeDamage;
	public float spinDamage;
	public int spinDur;
	public float spinSpd;

	public virtual void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = stat.SPEED;
		initSpeed = stat.SPEED;
		initAcc = agent.acceleration;

		life = GetComponent<LifeObject>();
		life.maxHp = stat.MaxHP;
		life.ResetCompletely();

		Sequencer doAttack = new Sequencer();

		metronome = new SetFlag();
		doAttack.childs.Add(metronome);

		IsInRange inRange = new IsInRange(atkRange, GameManager.Instance.player.transform, transform);
		doAttack.childs.Add(inRange);

		if( type == AttackType.Shoot)
		{
			ShootAttack shoot = new ShootAttack(agent, myBullet, GameManager.Instance.player.transform, shootPos, stat.ATK, shootPow);
			doAttack.childs.Add(shoot);
		}
		else if(type == AttackType.Charge)
		{
			 charge = new ChargeAttack(agent, this, GameManager.Instance.player.transform, 75, 25, 1f);
			doAttack.childs.Add(charge);
		}
		else if(type == AttackType.Spin)
		{
			spin = new SpinAttack(agent, this, spinDur, spinSpd);
			doAttack.childs.Add(spin);
		}
		else
		{
			SweepAttack sweep = new SweepAttack(agent, atkRange, angle, GameManager.Instance.player.transform, stat.ATK);
			doAttack.childs.Add(sweep);
		}

		Move doMove = new Move(GameManager.Instance.player.transform, agent);



		head = new Selecter();

		head.childs.Add(doAttack);
		head.childs.Add(doMove);
	}

	public virtual void Update()
	{
		if (examining)
		{
			head.Examine();
		}

		if (charging)
		{
			Collider[] cols = Physics.OverlapSphere(transform.position, atkRange, 1 << 8);
			for(int i = 0; i < cols.Length; i++)
			{
				LifeObject p = cols[0].GetComponent<LifeObject>();
				p.Damage(chargeDamage);
			}
			charge.StopCharge();
		}

		if (spinning)
		{
			Collider[] cols = Physics.OverlapSphere(transform.position, atkRange, 1 << 8);
			for (int i = 0; i < cols.Length; i++)
			{
				LifeObject p = cols[i].GetComponent<LifeObject>();
				p.Damage(spinDamage);
			}
		}
	}

	public void ResetSpeed()
	{
		agent.speed = initSpeed;
		agent.acceleration =initAcc;
	}

	public void StopAI()
	{
		examining = false;
		agent.isStopped = true;
		agent.SetDestination(transform.position);
	}

	public void StartAI()
	{
		examining = true;
		agent.isStopped = false;
	}

	public void BeatUpdate()
	{
		++beatCnt;
		if(beatCnt >= atkGap)
		{
			metronome.Set();
			beatCnt= 0;
			
		}
		if (spinning)
		{
			spin.OnNextBeat();
		}
	}

	public void BeatUpdateDivideFour()
	{
		++beatCnt;
		if (beatCnt >= atkGap)
		{
			metronome.Set();
			beatCnt = 0;
		}
		if (spinning)
		{
			spin.OnNextBeat();
		}
	}
}
