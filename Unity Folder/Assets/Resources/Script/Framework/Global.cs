using UnityEngine;
using System.Collections;

public enum LevelType { main, level };
public class Global : MonoBehaviour
{
	public static int ScreenWidth	= 480;
	public static int ScreenHeight	= 800;
	public static int Score;
	public static bool Audio, SFX;
	public static LevelType CurrentLevel;
	public Transition transition;
	private static Global mInstance;
	public static Global Instance
	{
		get 
		{
			if(mInstance == null) mInstance = GameObject.Find("Global").GetComponent<Global>();
			return mInstance;
		}
	}


	private void Awake()
	{
		if(mInstance == null) mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(this.gameObject);

		DontDestroyOnLoad(gameObject);

		if(PlayerPrefs.HasKey("Audio"))	Audio	= PlayerPrefs.GetInt("Audio")	==1?true:false;
		else 							SetAudio(true);												// First time setting 
		if(PlayerPrefs.HasKey("SFX"))	SFX		= PlayerPrefs.GetInt("SFX")		==1?true:false;	
		else 							SetSFX(true);
		if(PlayerPrefs.HasKey("Score"))	Score	= PlayerPrefs.GetInt("Score");	
		else 							SetScore(0);
	}

	private void Start()
	{
		Screen.SetResolution(ScreenWidth,ScreenHeight,false);
		transition = GetComponentInChildren<Transition>();
		transition.FadeIn();
	}

	public static void SetAudio(bool _value)
	{
		Audio = _value;
		PlayerPrefs.SetInt("Audio", _value?1:0);
	}
	public static void SetSFX(bool _value)
	{
		SFX = _value;
		PlayerPrefs.SetInt("SFX", _value?1:0);
	}
	public static void SetScore(int _value)
	{
		PlayerPrefs.SetInt("Score", Score = _value);
	}

	public IEnumerator LoadLevel(LevelType _type)
	{
		RayCastingManager.Instance.UnHookDelegates();
		transition.FadeOut();
		yield return new WaitForSeconds(transition.mFadeDuration);
		transition.FadeIn();
		CurrentLevel = _type;
		if(_type == LevelType.main)			Application.LoadLevel("Main");
		else if(_type == LevelType.level)	Application.LoadLevel("Game");
	}

}
