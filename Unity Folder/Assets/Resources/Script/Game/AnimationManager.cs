using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour 
{
	[SerializeField] private GameObject[] mPrefabAnimal;	// Animals Animations
	[SerializeField] private Ark mArk;
	[SerializeField] private GameObject mTree;
	[SerializeField] private Waves mWaves;
	[SerializeField] private StickingImage mHighscore;
	[SerializeField] private StickingImage mRating;

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

		GameObject temp = Instantiate(mPrefabAnimal[_cardType]) as GameObject;
		temp.AddComponent<Animal>();
		temp.transform.parent = this.transform;
		temp.GetComponent<Animal>().SetEndPos();
		temp.animation.Play();
		mList.Add(temp.GetComponent<Animal>());
		temp = Instantiate(mPrefabAnimal[_cardType]) as GameObject;
		temp.AddComponent<Animal>();
		temp.transform.Translate(-1.0f,0,-5);
		temp.GetComponent<Animal>().SetEndPos();
		temp.transform.parent = this.transform;
		mList.Add(temp.GetComponent<Animal>());
		temp.animation.Play();
	}

	public void Remove(Animal _obj)	
	{
		mList.Remove(_obj);	
		Destroy(_obj.gameObject);
	}

	public void RemoveTree()	{	mTree.SetActive(false);	}

	public void PlayHighscore()
	{
		mHighscore.Active = true;
		mHighscore.animation.Play();
	}
	public void PlayRating()
	{
		mRating.Active = true;
		mRating.animation.Play();
	}

	public bool ArkClose()
	{
		if(mList.Count != 0) return false;
		mArk.animation.PlayQueued("ArkCloseAnimation");
		mArk.animation.PlayQueued("ArkFloating",QueueMode.CompleteOthers);
		mWaves.Active = mWaves.Move = true;
		return true;
	}


}
