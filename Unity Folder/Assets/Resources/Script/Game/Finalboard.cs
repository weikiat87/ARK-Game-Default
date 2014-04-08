using UnityEngine;
using System.Collections;

public class Finalboard : MonoBehaviour 
{
	private void FinalScoring()
	{
		PointsManager.Instance.TallyPoints();
	}
}
