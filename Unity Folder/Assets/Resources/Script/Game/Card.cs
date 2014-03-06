
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : AnimationSprite 
{
	[SerializeField] private SpriteRenderer mCardImage;
	[SerializeField] private CountDownTimer	mCounter;

	private RaycastHit	mRayCastHit;
	private bool		mFlipped;
	[SerializeField] private int mCurrentType;

	private void Start()		
	{	
		mCounter = gameObject.GetComponent<CountDownTimer>();
	}

	public float CurrentTime	{	get { return mCounter.CurrentTime;	}	}
	public Vector3 WorldPosition
	{
		get { return transform.position;	}
		set { transform.position = value;	}
	}
	public int CardType
	{
		get { return mCurrentType;	}
		set 
		{ 
			mCurrentType = value;
			mCardImage.sprite = CardManager.Instance.GetSpriteImage(value);
		}
	}
	public bool IsFlipped
	{
		get {	return mFlipped;		}
		set 
		{	
			mCounter.IsStarted = mFlipped = value;
			if(value)
			{
				animation.Play("Opening");
				mCounter.CounterTimerHook += CounterTimerEnded;
			}
			else
			{

				animation.PlayQueued("Closing",QueueMode.PlayNow);
				animation.PlayQueued("Idle",QueueMode.CompleteOthers);
				mCounter.CounterTimerHook -= CounterTimerEnded;
			}
		}
	}
	public override bool Active
	{
		get { return base.Active;	}
		set 
		{ 
			base.Active = value;
			if(value)	CardManager.Instance.OpenCardHook 	+= OpenCard;
			else 		CardManager.Instance.OpenCardHook	-= OpenCard;
		}
	}

	private void CompareCards()
	{
		CardManager.Instance.CompareCards();
	}
		
	// Timer Function
	private void CounterTimerEnded()	{	CardManager.Instance.RemoveCard(this);	}
	// Card Manager Function
	private void OpenCard(Ray _ray)
	{
		if(Physics.Raycast(_ray,out mRayCastHit,Mathf.Infinity))
		{
			if(Input.GetMouseButtonUp(0))
			{
				if(mRayCastHit.collider.gameObject == mCardImage.gameObject)
					CardManager.Instance.AddCard(this);	
			}
		}
	}
}