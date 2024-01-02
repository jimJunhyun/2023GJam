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
			GameManager.Instance.ChangeRoom(Direction.Up);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			GameManager.Instance.ChangeRoom(Direction.Down);

		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			GameManager.Instance.ChangeRoom(Direction.Left);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			GameManager.Instance.ChangeRoom(Direction.Right);
		}
	}
}
