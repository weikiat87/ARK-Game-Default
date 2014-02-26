using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public abstract class AnimationSprite : SpriteBase
{
	protected Animator mAnimator;
	protected void Awake()	{	mAnimator = gameObject.GetComponent<Animator>();	}

	public override bool Active 
	{
		get {	return base.Active;		}
		set {	base.Active = value;	}
	}
}