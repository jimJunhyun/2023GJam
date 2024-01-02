using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Rigidbody rig;

	public float dam;

	private void OnCollisionEnter(Collision collision)
	{
		LifeObject lf;
		if (lf = collision.gameObject.GetComponent<LifeObject>())
		{
			lf.Damage(dam);
		}
	}

	public void Shoot(float pow)
	{
		rig = GetComponent<Rigidbody>();
		rig.AddForce(transform.forward * pow);
	}
}
