using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour 
{
	private Color mAppearColor;
	private Color mStartColor;
	private Color mEndColor;
	private float mYMinimum;
	private float mSpeed;
	private float mTime;

	private float mRotationAngle;
	private float mRotationSpeed;

	private enum mState	{ none, appear, moving };
	private mState mCurrentState;
	[SerializeField] private SpriteRenderer mCloudSprite;

	private void Update()
	{
		if(mCurrentState == mState.appear && mTime < 1.0f)
		{
			mTime += Time.deltaTime;
			Color.Lerp(mAppearColor,mStartColor,mTime);
			if(mTime > 1.0f)	mCurrentState = mState.moving;
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

	public bool Active
	{
		get {	return gameObject.activeSelf;		}
		set	
		{	
			gameObject.SetActive(value);
			mTime = 0.0f;
			if(value)	
			{
				mCurrentState = mState.appear;
				CloudManager.Instance.ColorChangeHook += HandleColorChangeHook;
			}
			else 		
			{
				mCurrentState = mState.none;
				CloudManager.Instance.ColorChangeHook -= HandleColorChangeHook;
			}
		}
	}

	private void HandleColorChangeHook(float _timePassed, float _maxTime)
	{		
		mCloudSprite.color = Color.Lerp(mStartColor,mEndColor,((_maxTime-_timePassed)/_maxTime));	
	}
	public float	Speed
	{	
		set 
		{
			mSpeed = Random.Range(0.1f,value);
		}	
	}
	public Vector2	Position	
	{	set 
		{ 
			transform.position	= new Vector2(value.x,Random.Range(mYMinimum,value.y));	
		}	
	}
}