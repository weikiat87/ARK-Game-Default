using UnityEngine;
using System.Collections;

public class Animal : AnimationSprite
{
	private Vector3 mEndPos;

	private void Update()
	{
		if(Vector3.Distance(gameObject.transform.position,mEndPos) > 0.2f)		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,mEndPos,Time.deltaTime);
		else Destroy (gameObject);
	}

	public Vector3 EndPos
	{
		set { mEndPos = value; }
	}
}

