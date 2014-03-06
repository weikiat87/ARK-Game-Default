using UnityEngine;
using System.Collections;

public class CountDownTimer : Timer
{
	[SerializeField] private bool	mStarted;
	[SerializeField] private bool	mLoop;

	private void Update()
	{
		if(mStarted)
		{
			mCurrentTime -= Time.deltaTime;
			if(mCurrentTime < 0)
			{
				if(CounterTimerHook != null)	CounterTimerHook();
				if(mLoop)						mCurrentTime = mMaxTime;
			}
		}
	}

	public bool IsStarted
	{
		get {	return mStarted;	}
		set 
		{	
			mStarted = value;
			if(value) mCurrentTime = mMaxTime;
		}
	}
	public float CurrentTime	{ get { return mCurrentTime;	} }
	public float MaxTime		
	{
		get { return mMaxTime;	} 
		set { mMaxTime = value;	}
	}

	public delegate void CounterTimerEndedDelegate();
	public event CounterTimerEndedDelegate CounterTimerHook;
	
}