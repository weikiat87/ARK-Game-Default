using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField] private CountDownTimer mGameTimer;
	[Range (0.0f,1.0f)][SerializeField] private float mRainWarningTimer;
	[Range (0.0f,1.0f)][SerializeField] private float mThunderWarningTimer;

	private bool mRaining;
	private bool mThunder;

	private static GameManager mInstance;
	public static GameManager Instance
	{
		get
		{
			if(mInstance == null) GameObject.Find("GameManager").GetComponent<GameManager>();
			return mInstance;
		}
	}
	private void Awake()
	{
		if(mInstance == null)	mInstance = this;
		else if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else 												Destroy(this);

		//Change Global Settings
		Global.CurrentLevel = LevelType.level;
		mGameTimer = GetComponentInChildren<CountDownTimer>();
		mGameTimer.IsStarted = true;
		mRaining = mThunder = false;
	}
	private void Update()
	{
		if( (mGameTimer.CurrentTime/mGameTimer.MaxTime) < mRainWarningTimer && !mRaining )	
		{
			CloudManager.Instance.Raining = mRaining = true;
			SoundEffectManager.Instance.PlayEffect("raining");
		}
		if( (mGameTimer.CurrentTime/mGameTimer.MaxTime) < mThunderWarningTimer && !mThunder )	
		{
			CloudManager.Instance.Thunder = mThunder = true;
			SoundEffectManager.Instance.PlayEffect("thunder");	
		}
	}

	public float GameTime		{	get { return mGameTimer.CurrentTime;	}	}
	public float GameMaxTime	{	get { return mGameTimer.MaxTime;		}	}
}