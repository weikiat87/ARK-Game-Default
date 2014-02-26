using UnityEngine;
using System.Collections;

public class SpriteBase : MonoBehaviour
{
	public virtual bool Active
	{
		get{ return gameObject.activeSelf;	}
		set{ gameObject.SetActive(value);	}
	}
}

