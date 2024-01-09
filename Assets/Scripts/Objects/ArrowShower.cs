using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArrowType
{
	Enemy,
	Portal,
	Room,
}

public class ArrowShower : MonoBehaviour
{
    public Transform target;

	Renderer rend;
    ArrowType type;
	Material mat;

    public void Init(Transform target, ArrowType t)
	{
        this.target = target;
		type = t;
		rend = GetComponentInChildren<Renderer>();
		mat = new Material(rend.material);
		switch (t)
		{
			case ArrowType.Enemy:
				mat.color = Color.red;
				break;
			case ArrowType.Portal:
				mat.color = Color.cyan;
				break;
			case ArrowType.Room:
				mat.color = new Color(1, 1, 1);
				break;
			default:
				break;
		}
		rend.material = mat;
	}

	private void Update()
	{
		Vector3 dirVec = target.position - GameManager.Instance.player.transform.position;
		dirVec.y = 0;
		transform.rotation = Quaternion.LookRotation(dirVec);
	}
}
