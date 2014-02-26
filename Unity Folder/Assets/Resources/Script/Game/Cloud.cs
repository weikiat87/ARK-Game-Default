using UnityEngine;
using System.Collections;

public class Cloud : SpriteBase 
{
	private Color	mAppearColor;
	private Color	mStartColor;
	private Color	mEndColor;
	private float	mYMinimum;
	private float	mSpeed;
	private bool	mRaining;

	private float mRotationAngle;
	private float mRotationSpeed;

	private enum mState	{ none, appear, moving };
	private mState mCurrentState;
	[SerializeField] private SpriteRenderer mCloudSprite;
	[SerializeField] private CountDownTimer mTimer;

	private void Start()
	{
		mTimer = gameObject.GetComponent<CountDownTimer>();
		mTimer.IsStarted = false;
	}

	private void Update()
	{
		if(mCurrentState == mState.appear)
		{
			mCurrentState = mState.moving;
			//timer for raindrop
		}

		mCloudSprite.transform.rotation = Quaternion.Euler(0,0, Mathf.Sin(Time.realtimeSinceStartup*mRotationSpeed) * mRotationAngle);
		transform.Translate(new Vector3(mSpeed*Time.deltaTime,0,0));
	}

	public void Init(Color _appearColor, Color _startColor, Color _endColor,Sprite _cloudSprite,float _speed,float _yMinimum)
	{
		mAppearColor		= _appearColor;
		mStartColor			= _startColor;
		mEndColor			= _endColor;
		mCloudSprite.sprite = _cloudSprite;
		mSpeed 				= _speed;
		mYMinimum			= _yMinimum;
		mRotationAngle		= Random.Range(5,15);
		mRotationSpeed		= Random.Range(1,3);
	}

	public override bool Active
	{
		get {	return base.Active;		}
		set	
		{	
			base.Active = value;
			if(value)	
			{
				mCurrentState = mState.appear;
				CloudManager.Instance.ColorChangeHook	+= HandleColorChangeHook;
				mTimer.CounterTimerHook					+= HandleTimerHook;
			}
			else 		
			{
				mCurrentState = mState.none;
				CloudManager.Instance.ColorChangeHook	-= HandleColorChangeHook;
				mTimer.CounterTimerHook					-= HandleTimerHook;
			}
		}
	}

	public void Raining(bool _value)
	{
		mTimer.IsStarted = mRaining = _value;
	}

	private void HandleColorChangeHook(float _timePassed, float _maxTime)
	{		
		mCloudSprite.color = Color.Lerp(mStartColor,mEndColor,((_maxTime-_timePassed)/_maxTime));	
	}
	private void HandleTimerHook()
	{
		// call the rain to come
		if(mRaining)	VisualEffectManager.Instance.PlayRaindrop(transform.position);
	}

	public float	Speed		{	set {	mSpeed = Random.Range(0.1f,value);		}		}
	public Vector2	Position	{	set { 	transform.position	= new Vector2(value.x,Random.Range(mYMinimum,value.y));	}	}
}