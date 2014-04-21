

using UnityEngine;
using System.Collections;

public class Highscore : MonoBehaviour 
{
	[SerializeField] private TextMesh mText;
	private static Highscore mInstance;
	public static Highscore Instance
	{
		get
		{
			if(mInstance == null) mInstance = GameObject.Find("Highscore").GetComponent<Highscore>();
			return mInstance;
		}
	}
	private void Awake()
	{
		if(mInstance == null)	mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else if(mInstance != this)					Destroy(this);
	}

	private void Start () 
	{
		UpdateScore();
	}

	public void UpdateScore()
	{
		if(mText)	
			mText.text = Global.Score.ToString();
	}
}
