using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectUI : MonoBehaviour
{
	Image image;
	float lifetime;
	float curTime = 0;

	public Vector3 moveDiff;

	public void SetInfo(Sprite img, float lf)
	{
		image = GetComponent<Image>();
		image.sprite = img;
		lifetime= lf;
		StartCoroutine(Ease());
	}

	IEnumerator Ease()
	{
		Vector3 initPos = transform.position;
		Vector3 obj = transform.position + moveDiff;
		while(curTime < lifetime)
		{
			curTime += Time.deltaTime;
			yield return null;
			Color c = image.color;
			c.a = Mathf.Lerp(1, 0, curTime / lifetime);
			image.color = c;
			transform.position = Vector3.Lerp(initPos, obj, curTime / lifetime);
		}
		
		Destroy(gameObject);
	}


}
