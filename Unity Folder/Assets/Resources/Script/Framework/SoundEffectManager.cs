using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour 
{
	[SerializeField] private AudioSource[] mSoundEffectList;
	[SerializeField] private AudioClip[]   mBGMClip;
	[SerializeField] private AudioSource   mBGM;
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
		if(Global.CurrentLevel == LevelType.level)		mBGM.clip = mBGMClip[1];
		else if(Global.CurrentLevel == LevelType.main)	mBGM.clip = mBGMClip[0];

		SetAudio(Global.Audio);
		SetSFX(Global.SFX);
	}

	public void SetAudio(bool _value)
	{
		mBGM.enabled = _value;
		if(mBGM.enabled) mBGM.Play();
	}
	public void SetSFX(bool _value)
	{
		foreach (AudioSource a in mSoundEffectList)	a.enabled = _value;		
	}

	public void PlayEffect(string _name)
	{
		switch(_name.ToLower())
		{
		case "raining":	mSoundEffectList[1].Play();	break;
		case "thunder":	mSoundEffectList[0].Play();	break;
		case "select":  mSoundEffectList[2].Play(); break;

		default: Debug.Log("No such Effect");		break;
		}
	}

}
