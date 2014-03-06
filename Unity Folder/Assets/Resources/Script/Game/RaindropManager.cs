using UnityEngine;
using System.Collections;

public class RaindropManager : MonoBehaviour
{
	[SerializeField] private Raindrop mPrefRaindrop;
	[SerializeField] private int mRaindropSize;
	[Range (3f, 5f)][SerializeField] private float mRaindropSpeed;
	private Raindrop[] mList;
	private static RaindropManager mInstance;
	public static RaindropManager Instance
	{
		get
		{
			if(mInstance == null) 
				mInstance = GameObject.Find("RaindropManager").GetComponent<RaindropManager>();
			return mInstance;
		}
	}

	private void Awake()
	{
		if(mInstance == null)	mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else if(mInstance != this)					Destroy(this);
	}

	private void Start()
	{
		mList = new Raindrop[mRaindropSize];
		for(int i=0;i<mRaindropSize;i++)
		{
			mList[i] = Instantiate(mPrefRaindrop) as Raindrop;
			mList[i].transform.parent = this.transform;
			mList[i].Active = false;
		}
	}

	public void PlayRaindrop(Vector3 _pos)
	{
		foreach(Raindrop r in mList)
		{
			if(!r.Active)	
			{
				r.Active = true;
				r.transform.position = _pos;
				r.Speed = Random.Range(1,mRaindropSpeed);
				return;
			}
		}
		Debug.Log("All raindrop on screen");
	}
}

