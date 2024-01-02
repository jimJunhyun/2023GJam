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
			GameManager.instance.ChangeRoom(Direction.Up);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			GameManager.instance.ChangeRoom(Direction.Down);

		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			GameManager.instance.ChangeRoom(Direction.Left);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			GameManager.instance.ChangeRoom(Direction.Right);
		}
	}
}
