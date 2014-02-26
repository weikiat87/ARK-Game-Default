using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour 
{
	[SerializeField] private Animal[] mPrefabAnimal;	// Animals Animations
	[SerializeField] private Vector3 mStartPos;
	[SerializeField] private Vector3 mEndPos;

	private List<Animal> mList = new List<Animal>();
	private static AnimationManager mInstance;
	public static AnimationManager Instance
	{
		get
		{
			if(mInstance == null) 
				mInstance = GameObject.Find("AnimationManager").GetComponent<AnimationManager>();
			return mInstance;
		}
	}
	
	private void Awake()
	{
		if(mInstance == null)	mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else if(mInstance != this)					Destroy(this);
	}

	public void PlayAnimation()
	{
		//TODO: Add proper animation calls
		Animal temp = Instantiate(mPrefabAnimal[0]) as Animal; 
		temp.transform.position = mStartPos;
		temp.transform.parent = this.transform;
		temp.EndPos = mEndPos;
		mList.Add(temp);
		
	}
}
