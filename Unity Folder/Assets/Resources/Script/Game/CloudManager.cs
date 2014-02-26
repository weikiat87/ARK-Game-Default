using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour
{
	/* CLOUD VARUABLES */
	[SerializeField] private int mNumberOfClouds;
	[SerializeField] private Cloud mPrefabCloud;
	private Cloud[]	mCloudList;
	
	[SerializeField] private Sprite[]		mCloudImages;
	[SerializeField] private Color			mAppearColor;
	[SerializeField] private Color			mStartColor;
	[SerializeField] private Color			mEndColor;

	[SerializeField] private float			mYMinimum;
	[SerializeField] private Vector2		mStartPos;
	[SerializeField] private CountDownTimer mSpawnTimer;
	[SerializeField] private float			mSpeed;

	private bool mRaining;

	private static CloudManager mInstance;
	public static CloudManager Instance
	{
		get
		{
			if(mInstance == null) GameObject.Find("CloudManager").GetComponent<CloudManager>();
			return mInstance;
		}
	}
	private void Awake()
	{
		if(mInstance == null) mInstance = this;
		else if(mInstance != this) 
		{
			if(mInstance.gameObject != this.gameObject)	Destroy(this.gameObject);
			else 										Destroy(this);
		}
	}
	private void Start()
	{
		mCloudList = new Cloud[mNumberOfClouds];
		for(int i=0;i<mNumberOfClouds;i++)
		{
			mCloudList[i] = Instantiate(mPrefabCloud) as Cloud;
			mCloudList[i].name = "Cloud";
			mCloudList[i].Init(mAppearColor,mStartColor,mEndColor,mCloudImages[Random.Range(0,mCloudImages.Length)],mSpeed,mYMinimum);
			mCloudList[i].Active = false;
			mCloudList[i].transform.parent = transform;
		}

		mRaining = false;
		mSpawnTimer.IsStarted = true;
		mSpawnTimer.CounterTimerHook += SpawnCloud;
	}

	private void Update()
	{
		if(ColorChangeHook != null) ColorChangeHook(GameManager.Instance.GameTime,GameManager.Instance.GameMaxTime);
	}

	private void SpawnCloud()
	{
		for(int i=0;i<mCloudList.Length;i++)
		{
			if(!mCloudList[i].Active)
			{
				mCloudList[i].Active	= true;
				mCloudList[i].Position	= mStartPos;
				mCloudList[i].Raining(mRaining);
				return;
			}
		}
	}
	public bool Raining	
	{	
		set 
		{
			mRaining = value;
			foreach(Cloud c in mCloudList)
			{	
				if(c.Active)	c.Raining(mRaining);	
			}
		}	
	}

	public bool Thunder
	{	
		set 
		{
			mRaining = value;
			foreach(Cloud c in mCloudList)
			{	
				if(c.Active)	VisualEffectManager.Instance.PlayThunder(c.transform.position);	
			}
		}	
	}

	public delegate void ColorChangeDelegate(float _timePassed, float _maxTime);
	public event ColorChangeDelegate ColorChangeHook;

}
