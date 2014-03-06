using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour 
{
	[SerializeField] private Animal[] mPrefabAnimal;	// Animals Animations
	[SerializeField] private GameObject mArk;

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

	public void PlayAnimation(int _cardType)
	{

		//TODO: Add proper animation calls
		Animal temp = Instantiate(mPrefabAnimal[_cardType]) as Animal;
		temp.transform.parent = this.transform;
		temp.SetEndPos();
		mList.Add(temp);
		temp = Instantiate(mPrefabAnimal[_cardType]) as Animal;
		temp.transform.Translate(-1.0f,0,-5);
		temp.SetEndPos();
		temp.transform.parent = this.transform;
		mList.Add(temp);
		
	}

	public void Remove(Animal _obj)	
	{
		mList.Remove(_obj);	
		Destroy(_obj.gameObject);
	}

	public bool ArkClose()
	{
		if(mList.Count != 0) return false;
		mArk.animation.Play("ArkCloseAnimation");
		return true;
	}
}
