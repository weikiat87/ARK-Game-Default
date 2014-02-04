using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField] private CountDownTimer mGameTimer;
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
	}
	private void Update()
	{
	}


	public float GameTime		{	get { return mGameTimer.CurrentTime;	}	}
	public float GameMaxTime	{	get { return mGameTimer.MaxTime;		}	}
}