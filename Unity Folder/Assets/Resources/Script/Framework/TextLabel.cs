using UnityEngine;
using System.Collections;

[RequireComponent (typeof(TextMesh))]
public class TextLabel : SortLayerComponent
{
	private TextMesh mText;
	protected override void Awake()
	{
		base.Awake();
		mText = GetComponent<TextMesh>();
	}

	public TextMesh textMesh	{	get {	return mText;	}	}
}