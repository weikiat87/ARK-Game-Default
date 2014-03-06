using UnityEngine;
using System.Collections;

public class SortLayerComponent : MonoBehaviour
{
	[UnityToolbag.SortingLayer]
	public int sortLayer;
	public int sortOrder;

	protected virtual void Awake()	
	{
		renderer.sortingLayerID	= sortLayer;	
		renderer.sortingOrder	= sortOrder;
	}
}