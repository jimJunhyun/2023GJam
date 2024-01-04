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

	Rigidbody rig;
	NavMeshAgent agent;

	SetFlag metronome;

	ChargeAttack charge;
	SpinAttack spin;

	internal Animator anim;

	Vector3 prevPos;

	float initSpeed;
	float initAcc;
	int beatCnt = 0;

	internal Vector3 chargeDest;

	public bool examining;

	public bool spinning;
	public bool charging;
	public float chargeDamage;
	public float chargeThreshold;
	public float spinDamage;
	public float spinThreshold;
	public int spinDur;
	public float spinSpd;

	internal readonly int AttackHash = Animator.StringToHash("Attack");
	internal readonly int IdleHash = Animator.StringToHash("Idle");
	internal readonly int HitHash = Animator.StringToHash("Hit");

	[Header("Effect")] 
	public EffectObject _chargerEffectAttack;

	public EffectObject _longRangeEffectAttack;
	public EffectObject _sweepEffect;
	public EffectObject _bulletShootEffect;

	public virtual void Awake()
	{
		rig = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();

		initSpeed = stat.SPEED;

		life = GetComponent<LifeObject>();
		life.maxHp = stat.MaxHP;
		life.ResetCompletely();

		Animator[] anims = GetComponentsInChildren<Animator>();
		int idx = Random.Range(0, anims.Length);
		for (int i = 0; i < anims.Length; i++)
		{
			if(i == idx)
			{
				anims[i].gameObject.SetActive(true);
				anim = anims[i];
				if(type == AttackType.Shoot)
					shootPos = transform.Find("ShootPos");
				
			}
			else
			{
				anims[i].gameObject.SetActive(false);
			}
		}

		Sequencer doAttack = new Sequencer();

		metronome = new SetFlag();
		doAttack.childs.Add(metronome);

		IsInRange inRange = new IsInRange(atkRange, GameManager.Instance.player.transform, transform);
		doAttack.childs.Add(inRange);

		AnimSetter setter = new AnimSetter(anim, AttackHash);
		doAttack.childs.Add(setter);

		if( type == AttackType.Shoot)
		{
			ShootAttack shoot = new ShootAttack(rig, myBullet, GameManager.Instance.player.transform, shootPos, stat.ATK, shootPow,this,
				() =>
				{
					if (_bulletShootEffect)
					{
						EffectObject _obj = PoolManager.Instance.Pop(_bulletShootEffect.name) as EffectObject;
						_obj.Init(transform, Vector3.zero);
					}
					else
					{
						Debug.LogError($"Effect : {gameObject} 없음");
					}
					
				});
			doAttack.childs.Add(shoot);
		}
		else if(type == AttackType.Charge)
		{
			 charge = new ChargeAttack(rig, this, GameManager.Instance.player.transform, 25, 15, 1f, () =>
			 {
				 if (_chargerEffectAttack)
				 {
					 EffectObject _obj = PoolManager.Instance.Pop(_chargerEffectAttack.name) as EffectObject;
					 _obj.Init(transform, Vector3.zero);
				 }
				 else
				 {
					 Debug.LogError($"Effect : {gameObject} 없음");
				 }
			 });
			doAttack.childs.Add(charge);
		}
		else if(type == AttackType.Spin)
		{
			spin = new SpinAttack(this, spinDur, spinSpd, () =>
			{
				if (_longRangeEffectAttack)
				{
					EffectObject _obj = PoolManager.Instance.Pop(_longRangeEffectAttack.name) as EffectObject;
					_obj.Init(transform,Vector3.zero);
				}
				else
				{
					Debug.LogError($"Effect : {gameObject} 없음");
				}
			});
			doAttack.childs.Add(spin);
		}
		else
		{
			SweepAttack sweep = new SweepAttack(rig, atkRange, angle, GameManager.Instance.player.transform, stat.ATK,this,
				() =>
				{
					if (_sweepEffect)
					{
						EffectObject ef = PoolManager.Instance.Pop(_sweepEffect.name) as EffectObject;
						ef.Init(transform, Vector3.zero);
					}					
					else
					{
						Debug.LogError($"Effect : {gameObject} 없음");
					}
				});
			doAttack.childs.Add(sweep);
		}

		Move doMove = new Move(GameManager.Instance.player.transform, rig, this, agent);



		head = new Selecter();

		head.childs.Add(doAttack);
		head.childs.Add(doMove);
	}

	public virtual void Update()
	{
		

		if (examining)
		{
			head.Examine();
			Vector3 v = GameManager.Instance.player.transform.position - transform.position;
			v.y = 0;
			transform.rotation = Quaternion.LookRotation(v);
		}

		if((transform.position - prevPos).magnitude > 0.1f)
		{
			anim.SetBool(IdleHash, false);
		}
		else
		{
			anim.SetBool(IdleHash, true);
		}

		if (charging)
		{
			anim.SetTrigger(AttackHash);
			bool invalidDest = (rig.velocity.sqrMagnitude > 0.1f && (transform.position - chargeDest).magnitude < 0.5);
			if (invalidDest)
			{
				charge.StopCharge();
				rig.velocity = Vector3.zero;
				anim.ResetTrigger(AttackHash);
			}

			Collider[] cols = Physics.OverlapSphere(transform.position, chargeThreshold, (1 << 8) | (1 << 9));
			if(cols.Length > 0)
			{
				// 차지 이팩트
				
				for(int i = 0; i < cols.Length; i++)
				{
					if(cols[i].gameObject == gameObject)
					{
						continue;
					}
					LifeObject p = cols[i].GetComponent<LifeObject>();
					p.Damage(chargeDamage);
					AI other;
					if (other = cols[i].GetComponent<AI>())
					{
						if (other.charging)
						{
							p.OnDead();
							life.OnDead();
						}
					}
					Debug.Log(p.name);
					charge.StopCharge();
					rig.velocity = Vector3.zero;
					anim.ResetTrigger(AttackHash);
				}
			}
		}

		if (spinning)
		{
			anim.SetTrigger(AttackHash);
			Collider[] cols = Physics.OverlapSphere(transform.position, spinThreshold, (1 << 8) | (1 << 9));
			for (int i = 0; i < cols.Length; i++)
			{
				if(cols[i].gameObject == gameObject)
					continue;
				LifeObject p = cols[i].GetComponent<LifeObject>();
				Debug.Log(p.transform.name);
				p.Damage(spinDamage);
			}
		}
	}

	private void LateUpdate()
	{
		prevPos = transform.position;
	}

	

	//private void OnDrawGizmos()
	//{
	//	for (int i = 0; i < angle; i++)
	//	{
	//		Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * Mathf.Cos(i * Mathf.Deg2Rad) * atkRange ) + Vector3.forward * Mathf.Sin(i * Mathf.Deg2Rad) * atkRange);
	//	}
	//}

	public void ResetSpeed()
	{
		rig.velocity = Vector3.zero;
	}

	public void StopFor(float sec)
	{
		GameManager.Instance. StartCoroutine(DelStopper(sec));
	}

	IEnumerator DelStopper(float s)
	{
		StopAI();
		yield return new WaitForSeconds(s);
		StartAI();
	}

	public void StopAI()
	{
		examining = false;
		rig.velocity = Vector3.zero;
		agent.velocity = Vector3.zero;
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
