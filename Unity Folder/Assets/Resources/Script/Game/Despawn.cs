using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Despawn : MonoBehaviour
{
	private void OnTriggerEnter(Collider _c)
	{
		Debug.Log(_c.name);
		if(_c.GetComponent<Cloud>())		{	_c.GetComponent<Cloud>().Active = false;		}
		if(_c.GetComponent<Raindrop>())		{	_c.GetComponent<Raindrop>().Active = false;		}
	}
}
