using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			GameManager.instance.ChangeRoom(Direction.Up);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			GameManager.instance.ChangeRoom(Direction.Down);

		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			GameManager.instance.ChangeRoom(Direction.Left);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			GameManager.instance.ChangeRoom(Direction.Right);
		}
	}
}
