using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour 
{
	[SerializeField] private AudioSource[] mSoundEffectList;

	private static SoundEffectManager mInstance;
	public static SoundEffectManager Instance
	{
		get
		{
			if(mInstance == null) 
				mInstance = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
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

	}

	public void PlayEffect(string _name)
	{
		switch(_name.ToLower())
		{
		case "raining":	mSoundEffectList[1].Play();	break;
		case "thunder":	mSoundEffectList[0].Play();	break;

		default: Debug.Log("No such Effect");		break;
		}
	}

}
