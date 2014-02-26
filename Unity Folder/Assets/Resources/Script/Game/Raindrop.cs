using UnityEngine;
using System.Collections;

public class Raindrop : SpriteBase
{
	[SerializeField] private float mSpeed;

	private void Update()
	{
		if(Active)
		{
			transform.Translate(new Vector3(0,-mSpeed*Time.deltaTime,0));
		}
	}

	public override bool Active
	{
		get	{	return base.Active;		}
		set {	base.Active = value;	}
	}

	public float Speed	{	set { mSpeed = value; }		}
}

