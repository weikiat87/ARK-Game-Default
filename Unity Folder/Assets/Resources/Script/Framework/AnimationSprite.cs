using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animation))]
public abstract class AnimationSprite : SpriteBase
{

	public override bool Active 
	{
		get {	return base.Active;		}
		set {	base.Active = value;	}
	}
}