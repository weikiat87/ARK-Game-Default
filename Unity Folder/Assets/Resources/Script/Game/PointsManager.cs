
using UnityEngine;
using System.Collections;

public class PointsManager : MonoBehaviour 
{
	[SerializeField] private CountDownTimer mTimer;
	[SerializeField] private TextMesh		mPointsText;
	[SerializeField] private GameObject		mScoreboard;
	[SerializeField] private GameObject		mFinalboard;
	[SerializeField] private PopupText		mPrefPopup;

	private PopupText[]	mPopupPoints;
	private int mCurrentPoints;
	private int mTallyPoints;
	private bool mFlag;
	private int mCounter = 0;

	private static PointsManager mInstance;
	public static PointsManager Instance
	{
		get
		{
			if(mInstance == null)	
				mInstance = GameObject.Find("PointsManager").GetComponent<PointsManager>();
			return mInstance;
		}
	}
	private void Awake()
	{
		if(mInstance == null) mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else if(mInstance != this)					Destroy(this);
	}
	private void Start()
	{
		mFlag			 = false;
		mCurrentPoints	 = 0;
		mPopupPoints	 = new PopupText[2];
		mPopupPoints[0] = Instantiate(mPrefPopup) as PopupText;
		mPopupPoints[1] = Instantiate(mPrefPopup) as PopupText;

		mPopupPoints[0].transform.parent = transform;
		mPopupPoints[1].transform.parent = transform;
		mPopupPoints[0].Label.textMesh.text = mPopupPoints[1].Label.textMesh.text = (CardManager.Instance.CardPoints()/2).ToString();
	}
	
	public void AddPoints(int _value)
	{
		mCurrentPoints	+= _value;
		mPointsText.text = mCurrentPoints.ToString();
	}
	public void AddPointsWithPopup(int _value,Vector3 _pos1, Vector3 _pos2)
	{
		mCurrentPoints	+= _value;
		mPointsText.text = mCurrentPoints.ToString();
		mPopupPoints[0].transform.position = _pos1;
		mPopupPoints[1].transform.position = _pos2;
		mPopupPoints[0].Active = mPopupPoints[1].Active = true;
	}
	public int Points	{	get { return mCurrentPoints; }	}

	public void FinalScore()
	{
		mFinalboard.SetActive(true);
		mFinalboard.animation.Play();

		mTallyPoints = mCurrentPoints;
		for(int i=1;i <= GameManager.Instance.mLevelsCompleted;i++)
		{
			if(i<5) mCurrentPoints += i * 10;
			else    mCurrentPoints += 50;
		}
		if(mCurrentPoints > Global.Score)
		{
			mFlag = true;
			Global.Score = mCurrentPoints;
			Global.SetScore(mCurrentPoints);
		}

	}
	
	private void HandleCounterTimerHook ()
	{
		if(mCounter < GameManager.Instance.mLevelsCompleted)
		{
			if(mCounter++ < 5) 
			{
				mTallyPoints += mCounter * 10;
				mPointsText.text = mTallyPoints.ToString();
				mPointsText.transform.localScale = new Vector3 (mPointsText.transform.localScale.x + (0.01f*mCounter),
				                                                mPointsText.transform.localScale.y + (0.01f*mCounter),
				                                                mPointsText.transform.localScale.z);
			}
			else 
			{
				mTallyPoints += 50;
				mPointsText.text = mTallyPoints.ToString();
			}
		}
		else
		{
			if(mFlag) AnimationManager.Instance.PlayHighscore();
			AnimationManager.Instance.PlayRating();

			mTimer.IsStarted = false;
			mTimer.CounterTimerHook -= HandleCounterTimerHook;
		}
	}
	
	public void TallyPoints()
	{
		mPointsText.gameObject.SetActive(true);
		mPointsText.color = Color.white;
		mPointsText.transform.position = new Vector3(0.8642f,1.5322f,-0.2f);

		mTimer.CounterTimerHook += HandleCounterTimerHook;
		mTimer.IsStarted = true;
	}

	public void RemoveScoreboard()
	{
		mPointsText.gameObject.SetActive(false);
		mScoreboard.SetActive(false);
	}
}
