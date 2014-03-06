using UnityEngine;
using System.Collections;

[RequireComponent (typeof(TextLabel))]
public class PopupText : SpriteBase
{
	[SerializeField] private Vector3 mDistance;
	[SerializeField] private float	mSpeed;
	[SerializeField] private float mMaxTime;
	private float 	mTime;


	private TextLabel mLabel;

	private void Awake()
	{
		mLabel = GetComponent<TextLabel>();
	}
	private void Start()	{	Active = false;	}
	private void Update()
	{
		if(Active)
		{
			if(mTime > 0)
			{
				mTime		-= Time.deltaTime;
				gameObject.transform.Translate(new Vector3(mDistance.x*mSpeed*Time.deltaTime,
				                                           mDistance.y*mSpeed*Time.deltaTime,
				                                           mDistance.z*mSpeed*Time.deltaTime));
			}
			else
				Active = false;
		}
	}

	public TextLabel Label
	{
		get { return mLabel; }
	}

	public override bool Active 
	{
		get 	{	return base.Active;		}
		set 
		{
			base.Active	= value;
			mTime		= mMaxTime;
		}
	}

}