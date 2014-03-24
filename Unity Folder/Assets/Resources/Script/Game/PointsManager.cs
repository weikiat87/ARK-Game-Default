using UnityEngine;
using System.Collections;

public class PointsManager : MonoBehaviour 
{
	[SerializeField] private TextMesh		mPointsText;
	[SerializeField] private PopupText		mPrefPopup;

	private PopupText[]	mPopupPoints;
	private int mCurrentPoints;

	private static PointsManager mInstance;
	public static PointsManager Instance
	{
		get
		{
			if(mInstance == null)	
				mInstance = GameObject.Find("PointsManager").GetComponent<PointsManager>();
			return mInstance;
		}
	}
	private void Awake()
	{
		if(mInstance == null) mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else if(mInstance != this)					Destroy(this);
	}
	private void Start()
	{
		mCurrentPoints = 0;
		mPopupPoints = new PopupText[2];
		mPopupPoints[0] = Instantiate(mPrefPopup) as PopupText;
		mPopupPoints[1] = Instantiate(mPrefPopup) as PopupText;

		mPopupPoints[0].transform.parent = transform;
		mPopupPoints[1].transform.parent = transform;
		mPopupPoints[0].Label.textMesh.text = mPopupPoints[1].Label.textMesh.text = (CardManager.Instance.CardPoints()/2).ToString();
	}
	public void AddPoints(int _value)
	{
		mCurrentPoints	+= _value;
		mPointsText.text = mCurrentPoints.ToString();
	}
	public void AddPointsWithPopup(int _value,Vector3 _pos1, Vector3 _pos2)
	{
		mCurrentPoints	+= _value;
		mPointsText.text = mCurrentPoints.ToString();
		mPopupPoints[0].transform.position = _pos1;
		mPopupPoints[1].transform.position = _pos2;

		
		mPopupPoints[0].Active = mPopupPoints[1].Active = true;
	}

	public int Points
	{
		get { return mCurrentPoints; }
	}
}
