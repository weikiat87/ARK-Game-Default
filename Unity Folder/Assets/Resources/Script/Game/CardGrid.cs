using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class CardGrid 
{
	private static GridNode[,]  m2DGrid;		// The Grid
	private int mNumberOfCard;					// Total Number of Card on field
	private int mCurrentRow;					// Current Row
	private int mCurrentCol;					// Current Col
	private float mGridSpacing;					// Distance per grid
	private Vector2 mGridPos;					// Transform Position of Card Manager

	public void Init(int _row, int _col,float _spacing,Vector3 _pos)
	{
		mGridPos = _pos.XY();
		// Create Initial Grid
		m2DGrid = new GridNode[_col,_row];
		for(int y=0;y<_row;y++)
		{
			for(int x=0;x<_col;x++)
			{
				m2DGrid[x,y] = new GridNode(x,y);
			}
		}

		mCurrentCol		= 2;
		mCurrentRow		= 2;
		mNumberOfCard	= 0;
		mGridSpacing 	= _spacing;

		UpdateGridPosition();
	}
	public void UpdateGridPosition()
	{
		// Do something to Update Grid Pos
		float startX = -(mGridSpacing/2 * (mCurrentCol-1))	+ mGridPos.x;
		float startY = (mGridSpacing/2 * (mCurrentRow-1))	+ mGridPos.y;

		for(int y=0;y<mCurrentRow;y++)
		{
			float deltaX = startX;
			for(int x=0;x<mCurrentCol;x++)
			{
				m2DGrid[x,y].mPosition = new Vector3(deltaX,startY,0);
				deltaX+= mGridSpacing;
			}
			startY-= mGridSpacing;
		}

	}
	public void IncreaseCol()	{ mCurrentCol++; }
	public void IncreaseRow()	{ mCurrentRow++; }
	public int	CardsOnGrid()	{ return mNumberOfCard; }
	public Card GetCard(int _x, int _y)	{	return m2DGrid[_x,_y].mCard;	}

	public bool ClickableCards
	{
		set
		{
			for(int y=0;y<mCurrentRow;y++)
			{
				for(int x=0;x<mCurrentCol;x++)
				{
					if(m2DGrid[x,y].mCard) m2DGrid[x,y].mCard.AttachHook = value;
				}
			}

		}
	}

	public void RemoveCard(Card _card)
	{
		for(int y=0;y<mCurrentRow;y++)
		{
			for(int x=0;x<mCurrentCol;x++)
			{
				if(m2DGrid[x,y].mCard == _card)
				{
					m2DGrid[x,y].mCard.animation.Stop();
					m2DGrid[x,y].mCard.animation.PlayQueued("Idle",QueueMode.PlayNow);
					m2DGrid[x,y].mCard.IsFlipped = m2DGrid[x,y].mCard.Active = false;
					m2DGrid[x,y].mCard = null;
					mNumberOfCard--;
					return;
				}
			}
		}
	}

	public void InsertCard(Card _card)
	{
		// Try to randomly add in
		int x = Random.Range(0,mCurrentCol-1);
		int y = Random.Range(0,mCurrentRow-1);
		if(m2DGrid[x,y].mCard == null)
		{
			m2DGrid[x,y].mCard = _card;
			m2DGrid[x,y].mCard.Active = true;
			_card.WorldPosition = m2DGrid[x,y].mPosition;
			mNumberOfCard++;
			return;
		}
	
		// Add in by order
		for(y=0;y<mCurrentRow;y++)
		{
			for(x=0;x<mCurrentCol;x++)
			{
				if(m2DGrid[x,y].mCard == null)
				{
					m2DGrid[x,y].mCard = _card;
					m2DGrid[x,y].mCard.Active = true;
					_card.WorldPosition = m2DGrid[x,y].mPosition;
					mNumberOfCard++;
					return;
				}
			}
		}
	}
}

