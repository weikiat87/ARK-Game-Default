using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonsManager : MonoBehaviour 
{
	private bool mClickable = true;

	private static ButtonsManager mInstance;
	public static ButtonsManager Instance
	{
		get
		{
			if(mInstance == null) mInstance = GameObject.Find("ButtonsManager").GetComponent<ButtonsManager>();
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
		RayCastingManager.Instance.RayCastingHook += HandleRayCastingHook;
	}


	public bool clickable	{	set { mClickable = value;	}	}
	public void Active(bool _value)
	{
		foreach(Transform child in transform)
		{
			child.gameObject.SetActive(_value);
		}
	}
	public void AttachToRayCast()
	{
		RayCastingManager.Instance.RayCastingHook += HandleRayCastingHook;
	}
	public delegate void ButtonHookDelete(Ray _ray);
	public event ButtonHookDelete ButtonHook;

	private void HandleRayCastingHook (Ray _ray)
	{
		if(mClickable)	
		{
			if(ButtonHook != null) 	ButtonHook(_ray);
		}
	}
}