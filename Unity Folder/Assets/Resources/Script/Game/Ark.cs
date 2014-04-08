using UnityEngine;
using System.Collections;

public class Ark : MonoBehaviour 
{
	[SerializeField] private GameObject mWater;

	private void ActiveWater()
	{
		mWater.SetActive(true);
	}
}
