using UnityEngine;
using System.Collections;

public class RayCastingManager : MonoBehaviour
{
	private Ray mRay;
	private static RayCastingManager mInstance;
	public static RayCastingManager Instance
	{
		get
		{
			if(mInstance == null) mInstance = GameObject.Find("RayCastingManager").GetComponent<RayCastingManager>();
			return mInstance;
		}
	}

	private void Awake()
	{
		if(mInstance == null)	mInstance = this;
		if(mInstance != this)
		{
			if(mInstance.gameObject != this.gameObject)	Destroy(this.gameObject);
			else if(mInstance != this)					Destroy(this);
		}
		DontDestroyOnLoad(gameObject);
	}

	private void Update()
	{
		mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(RayCastingHook != null)	
			RayCastingHook(mRay);
	}
	public void UnHookDelegates()	{	RayCastingHook = null;	}

	public delegate void RayCastingDelegate(Ray _ray);
	public event RayCastingDelegate RayCastingHook;
}