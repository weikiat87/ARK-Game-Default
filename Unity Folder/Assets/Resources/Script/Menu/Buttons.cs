using UnityEngine;
using System.Collections;

public abstract class Buttons : MonoBehaviour 
{
	protected abstract void Clicked(Ray _ray);
	protected abstract void Release(Ray _ray);

	protected RaycastHit mRayCastHit;
}
