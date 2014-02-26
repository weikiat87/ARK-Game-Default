using UnityEngine;
using System.Collections;

public class VisualEffectManager : MonoBehaviour
{
	[SerializeField] private Raindrop mPrefRaindrop;
	[SerializeField] private int mRaindropSize;
	[Range (1f, 5f)][SerializeField] private float mRaindropSpeed;
	private Raindrop[]		mRaindropList;

	private static VisualEffectManager mInstance;
	public static VisualEffectManager Instance
	{
		get
		{
			if(mInstance == null)
			{
				mInstance = GameObject.Find("VisualEffectManager").GetComponent<VisualEffectManager>();
			}
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
		GameObject temp = new GameObject();
		temp.name = "Raindrop Folder";
		temp.transform.parent = this.transform;

		mRaindropList = new Raindrop[mRaindropSize];
		for(int i=0;i<mRaindropSize;i++)
		{
			mRaindropList[i] = Instantiate(mPrefRaindrop) as Raindrop;
			mRaindropList[i].transform.parent = temp.transform;
			mRaindropList[i].Active = false;
		}
	}

	public void PlayRaindrop(Vector3 _pos)
	{
		foreach(Raindrop r in mRaindropList)
		{
			if(!r.Active)	
			{
				Debug.Log("raindrop dropped");
				r.Active = true;
				r.transform.position = _pos;
				r.Speed = Random.Range(1,mRaindropSpeed);
				return;
			}
		}
		Debug.Log("All raindrop on screen");
	}

	public void PlayThunder(Vector3 _pos)
	{

	}
}

