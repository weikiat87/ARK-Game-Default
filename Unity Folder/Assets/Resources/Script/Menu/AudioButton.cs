using UnityEngine;
using System.Collections;

public class AudioButton : Buttons
{
	private enum mAudioType { Audio, SFX };
	[SerializeField] private mAudioType mType;
	[SerializeField] private bool mOnOffFlag;
	// Use this for initialization
	private void Start ()
	{
		ButtonsManager.Instance.ButtonHook += Clicked;
	}
	protected override void Clicked(Ray _ray)
	{
		if(Physics.Raycast(_ray,out mRayCastHit,Mathf.Infinity))
		{
			if(Input.GetMouseButtonDown(0))
			{
				if(mRayCastHit.collider.gameObject == this.gameObject)
				{
					ButtonsManager.Instance.ButtonHook -= Clicked;
					ButtonsManager.Instance.ButtonHook += Release;
					
					// Do Something when Clicked
					Debug.Log(gameObject.name + " clicked");
					animation.Play("ButtonExpand");
				}
			}
		}
	}
		
	
	protected override void Release(Ray _ray)
	{
		if(Input.GetMouseButtonUp(0))
		{
			if(Physics.Raycast(_ray,out mRayCastHit,Mathf.Infinity))
			{
				if(mRayCastHit.collider.gameObject == this.gameObject)
				{
					
					//Do Something when release
					Debug.Log(gameObject.name + " release");
					if(mType == mAudioType.Audio)		
					{
						Global.SetAudio(mOnOffFlag);
						SoundEffectManager.Instance.SetAudio(Global.Audio);
					}
					else if(mType == mAudioType.SFX)
					{
						Global.SetSFX(mOnOffFlag);
						SoundEffectManager.Instance.SetSFX(Global.SFX);
					}
					SoundEffectManager.Instance.PlayEffect("select");
				}
			}
			
			animation.Play("ButtonIdle");
			
			ButtonsManager.Instance.ButtonHook += Clicked;
			ButtonsManager.Instance.ButtonHook -= Release;
		}
		
	}
}

