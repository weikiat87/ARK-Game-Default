using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour
{
	[SerializeField] private Animation mAnimation;
	private void Update()
	{

		if(!mAnimation.isPlaying)	Destroy(gameObject);
	}
}

