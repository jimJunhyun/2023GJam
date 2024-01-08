using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//2.블랙박스 - 5개박자
//3.검기발사 - 7마디
//4.콩알탄발사 - 3마디
//5.플레이어 주변을 둥글게 이동
[RequireComponent(typeof(LifeObject))]
public class BossAI : MonoBehaviour, IRhythm
{

    public float wanderRadius;
    public int bulletGap = 3;
    public int slashGap = 7;
    public int bBoxGapSp = 5;
    public int bBoxDurationSp = 2;

    public Bullet bullet;
    public Bullet slash;

    public Transform shootPos;

    public float bulletPow;
    public float slashPow;

    public Stat stat;

    Rigidbody rig;
    internal LifeObject life;
    internal Animator anim;
    Vector3 dest;
    float curAngle = 90;
    float curAngleRad => curAngle * Mathf.Deg2Rad;
    bool blocking = false;

    int beatCnt = 0;
    int tempoCnt = 0;

    bool beating = false;
    bool updating = false;

    readonly int Attack1Hash = Animator.StringToHash("Attack1");
    readonly int Attack2Hash = Animator.StringToHash("Attack2");
    readonly int Attack3Hash = Animator.StringToHash("Attack3");
    internal readonly int HitHash = Animator.StringToHash("Hit");
    readonly int IdleHash = Animator.StringToHash("Idle");


	public void BeatUpdate()
	{
		if (beating)
		{
            float dist = (GameManager.Instance.player.transform.position - transform.position).magnitude;
            tempoCnt += 1;
            beatCnt += 1;
            if(blocking && tempoCnt % bBoxDurationSp == 0)
			{
                BeatUISystem.Instance.Curse.SetCurse();
                blocking = false;
            }
            if (tempoCnt % bBoxGapSp == 0)
            {
                anim.SetTrigger(Attack3Hash);
                StopFor(0.8f);
                
                blocking = true;
                BeatUISystem.Instance.Curse.SetCurse(Random.Range(0, 4));
            }
            else if (beatCnt % slashGap == 0)
            {
                anim.SetTrigger(Attack2Hash);
                StopFor(0.8f);
                Vector3 v = GameManager.Instance.player.transform.position - transform.position;
                v.y = 0;
                transform.rotation = Quaternion.LookRotation(v);
                Bullet ssh = GameObject.Instantiate(slash, shootPos.position, transform.rotation);
                ssh.Shoot(slashPow);
            }
            else if (beatCnt % bulletGap == 0)
            {
                anim.SetTrigger(Attack1Hash);
                StopFor(0.8f);
                Vector3 v = GameManager.Instance.player.transform.position - transform.position;
                v.y = 0;
                transform.rotation = Quaternion.LookRotation(v);
                Bullet blt = GameObject.Instantiate(bullet, shootPos.position, transform.rotation);
                blt.Shoot(bulletPow);
            }
        }
		
    }

	public void BeatUpdateDivideFour()
	{
		if (beating)
		{
            float dist = (GameManager.Instance.player.transform.position - transform.position).magnitude;
            beatCnt += 1;
            if (beatCnt % slashGap == 0)
            {
                anim.SetTrigger(Attack2Hash);
                StopFor(0.8f);
                Vector3 v = GameManager.Instance.player.transform.position - transform.position;
                v.y = 0;
                transform.rotation = Quaternion.LookRotation(v);
                Bullet ssh = GameObject.Instantiate(slash, shootPos.position, transform.rotation);
                ssh.Shoot(slashPow);
               
            }
            else if (beatCnt % bulletGap == 0)
            {
                anim.SetTrigger(Attack1Hash);
                StopFor(0.8f);
                Vector3 v = GameManager.Instance.player.transform.position - transform.position;
                v.y = 0;
                transform.rotation = Quaternion.LookRotation(v);
                Bullet blt = GameObject.Instantiate(bullet, shootPos.position, transform.rotation);
                blt.Shoot(bulletPow);
                
                    
            }
        }
		
	}


    [SerializeField] TransitionSettings set;
    [SerializeField] MapCanvas _map;
	// Start is called before the first frame update
	void Awake()
    {
        rig = GetComponent<Rigidbody>();

        life = GetComponent<LifeObject>();
        life.maxHp = stat.MaxHP;
        life.hp = stat.HP;

        life.onDead += () => 
        {
            Debug.Log("zmfflrj");

            MapCanvas md = PoolManager.Instance.Pop(_map.name) as MapCanvas;

            md.Init("축하합니다 연주가님! 리드미컬 시티를 정화하는데 성공하셨습니다!", 5f);

            TransitionManager.Instance.Transition("StartScene", set, 5f);


        };

        anim = transform.Find("PolyArtWizardStandardMat").GetComponent<Animator>();

        GameManager.Instance.boss = this;
    }

    // Update is called once per frame
    void Update()
    {

		if (updating)
		{
            Vector3 dir = (dest - transform.position);
            dir.y = 0;
            if (dir.magnitude < 1f)
            {
                SampleNewPos();
            }
			else
			{
                //Vector3 v = (GameManager.Instance.player.transform.position - transform.position);
                //v.y = 0;
                anim.SetBool(IdleHash, rig.velocity.magnitude < 0.1f);
                transform.rotation = Quaternion.LookRotation(dir);
                rig.velocity = dir.normalized * stat.SPEED;
            }
        }
        
    }
    public void SampleNewPos()
	{
        if (NavMesh.SamplePosition(GameManager.Instance.player.transform.position
                    + (Vector3.forward * Mathf.Cos(curAngleRad) * wanderRadius)
                    + (Vector3.right * Mathf.Sin(curAngleRad) * wanderRadius)
                    , out NavMeshHit hit, 15f, -1))
        {
            dest = hit.position;
            curAngle += 90;
            curAngle %= 360;
        }
    }

    public void StopFor(float sec)
	{
        StartCoroutine(DelStopper(sec));
	}

    IEnumerator DelStopper(float s)
	{
        StopMove();
        yield return new WaitForSeconds(s);
            Activate();
	}

    public void ActivateWithDel(float sec)
	{
        StartCoroutine(DelActivate(sec));
	}

    IEnumerator DelActivate(float s)
	{
        yield return new WaitForSeconds(s);
        Activate();
	}

    public void Activate()
	{
        beating = true;
        SampleNewPos();
        updating = true;
    }

    public void StopMove()
	{
        rig.velocity = Vector3.zero;
        beating = false;
        updating = false; 
	}
}
