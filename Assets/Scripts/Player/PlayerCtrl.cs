using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
	public float speed = 5f;
	float h;
	float v;

	CharacterController ctrl;

	Vector3 moveVec = Vector3.zero;
	Vector3 forceVec = Vector3.zero;

	private void Awake()
	{
		 ctrl = GetComponent<CharacterController>();
	}

	public void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		moveVec = new Vector3(h, 0, v) * speed;

		if (!ctrl.isGrounded)
		{
			forceVec.y -= GameManager.GRAVITY * Time.deltaTime;
		}
		else
		{
			forceVec.y = 0;
		}
		ctrl.Move((moveVec + forceVec) * Time.deltaTime);
	}
}
