using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Despawn : MonoBehaviour
{
	private void OnTriggerEnter(Collider _c)
	{
		if(_c.GetComponent<Cloud>())
		{
			_c.GetComponent<Cloud>().Active = false;
		}
	}
}
