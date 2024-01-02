using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeObject))]
public class Bullet : MonoBehaviour
{
	Rigidbody rig;
	LifeObject life;
	public float dam;

	private void OnCollisionEnter(Collision collision)
	{
		LifeObject lf;
		if (lf = collision.gameObject.GetComponent<LifeObject>())
		{
			lf.Damage(dam);
			life.OnDead();
		}
	}

	public void Shoot(float pow)
	{
		rig = GetComponent<Rigidbody>();
		rig.AddForce(transform.forward * pow, ForceMode.Impulse);
		life = GetComponent<LifeObject>();
		life.onDead += ()=>Destroy(gameObject, 3f);
	}
}
