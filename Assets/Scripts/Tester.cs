using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.W))
		{
			GameManager.Instance.ChangeRoom(Direction.Up);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			GameManager.Instance.ChangeRoom(Direction.Down);

		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			GameManager.Instance.ChangeRoom(Direction.Left);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			GameManager.Instance.ChangeRoom(Direction.Right);
		}
	}
}
