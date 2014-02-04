using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GridNode
{
	public int x;												// x pos
	public int y;												// y pos
	public Card mCard;											// Card in the Node
	public Vector3 mPosition;									// position of node
	public List<GridNode> mNeighbors = new List<GridNode>();	// neighbors

	public GridNode()
	{
		x = 0;
		y = 0;
		mCard = null;
		mPosition = Vector3.zero;
	}
}