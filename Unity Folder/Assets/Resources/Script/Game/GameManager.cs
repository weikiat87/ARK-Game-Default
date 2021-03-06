using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField] private CountDownTimer mGameTimer;
	[Range (0.0f,1.0f)][SerializeField] private float mRainWarningTimer;
	[Range (0.0f,1.0f)][SerializeField] private float mThunderWarningTimer;
	
	private bool mRaining;
	private bool mThunder;

	public int mLevelsCompleted = 0;

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
		mGameTimer = GetComponentInChildren<CountDownTimer>();
		mGameTimer.CounterTimerHook += HandleCounterTimerHook;
		mGameTimer.IsStarted = true;
		mRaining = mThunder = false;
	}

	private void Update()
	{
		if( (mGameTimer.CurrentTime/mGameTimer.MaxTime) < mRainWarningTimer && !mRaining )	
		{
			mRaining = true;
			CloudManager.Instance.PlayRain();
			SoundEffectManager.Instance.PlayEffect("raining");
		}
		if( (mGameTimer.CurrentTime/mGameTimer.MaxTime) < mThunderWarningTimer && !mThunder )	
		{
			mThunder = true;
			CloudManager.Instance.PlayLightning();
			SoundEffectManager.Instance.PlayEffect("thunder");	
		}
	}

	
	private void HandleCounterTimerHook()
	{
		Debug.Log("Game Ended");
		
		RayCastingManager.Instance.UnHookDelegates();
		CardManager.Instance.gameObject.SetActive(false);

		if(AnimationManager.Instance.ArkClose())
		{
			mGameTimer.CounterTimerHook -= HandleCounterTimerHook;
			mGameTimer.enabled = false;
			StartCoroutine(Scoring());
		}
	}
	private IEnumerator Scoring()
	{
		Global.Instance.transition.FadeOut();
		yield return new WaitForSeconds(Global.Instance.transition.mFadeDuration);
		Global.Instance.transition.FadeIn();

		AnimationManager.Instance.RemoveTree();
		PointsManager.Instance.RemoveScoreboard();
		yield return new WaitForSeconds(Global.Instance.transition.mFadeDuration);
		Debug.Log("end transit in");
		// Run the Score

		PointsManager.Instance.FinalScore();
		ButtonsManager.Instance.AttachToRayCast();
		ButtonsManager.Instance.Active(true);
	}

	public float GameTime		{	get { return mGameTimer.CurrentTime;	}	}
	public float GameMaxTime	{	get { return mGameTimer.MaxTime;		}	}
}