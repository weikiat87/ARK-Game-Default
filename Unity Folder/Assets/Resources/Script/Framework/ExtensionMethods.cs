using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
	public static void ResetTransformation(this Transform trans)
	{
		trans.position = Vector3.zero;
		trans.localRotation = Quaternion.identity;
		trans.localScale = new Vector3(1, 1, 1);
	}
	public static Vector2 XZ (this Vector3 v)	{	return new Vector2 (v.x,v.z);	}
	public static Vector2 XY (this Vector3 v)	{	return new Vector2 (v.x,v.y);	}

}
