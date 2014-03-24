using UnityEngine;
using System.Collections;

public class MovementScript : AnimationSprite 
{
	[SerializeField] private Vector3	mDistance;
	[SerializeField] private float		mSpeed;
	[SerializeField] private bool		mMove;

	// Update is called once per frame
	protected void Update () 
	{
		if(mMove)
		{
			if(mDistance.magnitude > 0.1)		
			{
				gameObject.transform.Translate( mDistance.normalized * mSpeed * Time.deltaTime );
				mDistance -= (mDistance.normalized * mSpeed * Time.deltaTime);
			}
			else
			{	Move = Active = false;	}
		}
	}
	public bool Move
	{
		get {	return mMove;	}
		set 
		{	
			mMove = value;	
			if(value)	animation.Play();
			else 		animation.Stop();
		}
	}
}
