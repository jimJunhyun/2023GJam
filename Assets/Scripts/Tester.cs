using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			GameManager.Instance.ChangeRoom(Direction.Up);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			GameManager.Instance.ChangeRoom(Direction.Down);

		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			GameManager.Instance.ChangeRoom(Direction.Left);
		}
		if (Input.GetKeyDown(KeyCode.Alpha0))
		{
			GameManager.Instance.ChangeRoom(Direction.Right);
		}
	}
}
