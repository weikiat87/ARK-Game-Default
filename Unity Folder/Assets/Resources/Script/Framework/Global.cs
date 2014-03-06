using UnityEngine;
using System.Collections;

public enum LevelType { main, level };
public class Global : MonoBehaviour
{
	public static int ScreenWidth	= 480;
	public static int ScreenHeight	= 800;
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
	}

	private void Start()
	{
		Screen.SetResolution(ScreenWidth,ScreenHeight,false);
		
		transition = GetComponentInChildren<Transition>();
		transition.FadeIn();
	}

	public IEnumerator LoadLevel(LevelType _type)
	{
		RayCastingManager.Instance.UnHookDelegates();
		transition.FadeOut();
		yield return new WaitForSeconds(transition.mFadeDuration);
		transition.FadeIn();
		if(_type == LevelType.main)			Application.LoadLevel("Main");
		else if(_type == LevelType.level)	Application.LoadLevel("Game");
	}

	private void OnGUI()
	{
		if(GUI.Button(new Rect(0,0,100,40), "Main"))	StartCoroutine(LoadLevel(LevelType.main));
		if(GUI.Button(new Rect(0,40,100,40), "Level"))	StartCoroutine(LoadLevel(LevelType.level));
		GUI.Label(new Rect(10,80,100,100),"Use For Testing and Debugging, Don't worry it will be gone :)");
	}
}
