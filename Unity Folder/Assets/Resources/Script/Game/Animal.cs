using UnityEngine;
using System.Collections;

public class Animal : AnimationSprite
{
	private Vector3 mEndPos;

	private void Update()
	{
		if(Vector3.Distance(gameObject.transform.position,mEndPos) > 0.2f)		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,mEndPos,Time.deltaTime);
		else AnimationManager.Instance.Remove(this);

	}
	public void SetEndPos()
	{
		mEndPos = new Vector3(1.0f,this.gameObject.transform.position.y+0.03f,this.gameObject.transform.position.z);
	}
}

