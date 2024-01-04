using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HallwayUI : MonoBehaviour
{
	Image up;
	Image down;
	Image left;
	Image right;

	public void SetConState(int con)
	{
		up = transform.Find("Up").GetComponent<Image>();
		down = transform.Find("Down").GetComponent<Image>();
		left = transform.Find("Left").GetComponent<Image>();
		right = transform.Find("Right").GetComponent<Image>();

		up.enabled = false;
		down.enabled = false;
		left.enabled = false;
		right.enabled = false;

		if ((con >> 0) % 2 == 1)
		{
			up.enabled = true;
		}
		if ((con >> 1) % 2 == 1)
		{
			down.enabled = true;
		}
		if ((con >> 2) % 2 == 1)
		{
			left.enabled = true;
		}
		if ((con >> 3) % 2 == 1)
		{
			right.enabled = true;
		}
	}

	public void SetAlpha(float val)
	{
		Color temp;
		temp = up.color;
		temp.a = val;
		up.color = temp;

		temp = down.color;
		temp.a = val;
		down.color = temp; 

		temp = left.color;
		temp.a = val;
		left.color = temp; 

		temp = right.color;
		temp.a = val;
		right.color = temp;
	}
}
