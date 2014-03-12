using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour 
{
	private bool mTransiting;
	private Vector3 mMoveTo;

	private static CameraLerp mInstance;
	public static CameraLerp Instance
	{
		get
		{
			if(mInstance == null) mInstance = GameObject.Find("Main Camera").GetComponent<CameraLerp>();
			return mInstance;
		}
	}

	private void Awake()
	{
		if (mInstance == null)		mInstance = this;
		if (mInstance.gameObject != this.gameObject)	Destroy (this.gameObject);
		else if (mInstance != this)						Destroy (this);

	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mTransiting)
		{
			if(Vector3.Distance(mMoveTo,transform.position) > 0.001f)
			{
				transform.position = Vector3.Lerp(transform.position,mMoveTo,Time.deltaTime*5);
			}
			else
			{
				ButtonsManager.Instance.clickable = true;
				mTransiting = false;
			}
		}
	}

	public void TransitCamera(Vector3 _toPos)
	{
		mTransiting = true;
		mMoveTo = _toPos;

		ButtonsManager.Instance.clickable = false;
	}

}
