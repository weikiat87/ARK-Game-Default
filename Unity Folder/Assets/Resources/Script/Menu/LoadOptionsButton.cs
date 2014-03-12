using UnityEngine;
using System.Collections;

public class LoadOptionsButton : Buttons
{
	[SerializeField] private Vector3 mPosition;
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
					CameraLerp.Instance.TransitCamera(mPosition);
				}
			}
			
			animation.Play("ButtonIdle");
			
			ButtonsManager.Instance.ButtonHook += Clicked;
			ButtonsManager.Instance.ButtonHook -= Release;
		}
		
	}
}
