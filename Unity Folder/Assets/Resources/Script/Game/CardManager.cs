using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{	
	[SerializeField] private Card	mPrefabCard;				// Prefab of Card
	[SerializeField] private int	mTotalPairs;				// Max Pairs
	[SerializeField] private float	mGridSpacing;				// Distance between Cards in Grid
	[SerializeField] private int	mMaxRows;					// Max Row
	[SerializeField] private int	mMaxCols;					// Max Col
	[SerializeField] private Sprite[]	mSpriteImageList;		// Images of Sprite
	[SerializeField] private Card[] 	mCardHolderList;		// Temp Holder

	[SerializeField] private int	mPointsPerCard;

	private List<Card> mOpenedCards = new List<Card>();	// Cards that are opened
	private CardGrid mGrid;								// The Grid
	private int mCurrentPairs;							// Current Pairs for Grid

	private static CardManager mInstance;
	public static CardManager Instance
	{
		get
		{
			if(mInstance == null) 
				mInstance = GameObject.Find("CardManager").GetComponent<CardManager>();
			return mInstance;
		}
	}

	private void Awake()
	{
		if(mInstance == null)	mInstance = this;
		if(mInstance.gameObject != this.gameObject)	Destroy(gameObject);
		else if(mInstance != this)					Destroy(this);

		// Init Variables
		mCurrentPairs	= 2;							// Current Card Pairs
		mCardHolderList	= new Card[mTotalPairs*2];		// Total number of Cards
		// Create Holder List
		for(int i=0;i<mTotalPairs*2;i++)
		{
			mCardHolderList[i] = Instantiate(mPrefabCard) as Card;
			mCardHolderList[i].gameObject.name = "Card" + (i).ToString();
			mCardHolderList[i].gameObject.transform.parent = gameObject.transform;
		}

		mGrid = new CardGrid();
		mGrid.Init(mMaxRows,mMaxCols,mGridSpacing,transform.position);

	}
	private void Start()
	{
		RayCastingManager.Instance.RayCastingHook += HandleRayCastingHook;
		SetPlayingCards();
	}

	public void AddCard(Card _card)				
	{
		if(mOpenedCards.Count < 2)
		{
			if(!mOpenedCards.Contains(_card)) 
			{
				mOpenedCards.Add(_card);
				_card.IsFlipped	= true;
			}
		}
		else
		{
			if(!mOpenedCards.Contains(_card)) 
			{	
				mOpenedCards[0].IsFlipped = false;
				mOpenedCards.RemoveAt(0);
				_card.IsFlipped = true;
				mOpenedCards.Add(_card);
			}
		}

	}
	public void RemoveCard(Card _card)
	{
		if(mOpenedCards.Count <= 2)
		{
			if(mOpenedCards.Contains(_card)) 
			{
				_card.IsFlipped		= false;
				mOpenedCards.Remove(_card);
			}
		}
	}

	public int 	CardPoints()					{	return mPointsPerCard;				}
	public Card CardOnGrid(int _x, int _y)		{	return mGrid.GetCard(_x,_y);		}
	public void RemoveCardOnGrid(Card _card)	{	mGrid.RemoveCard(_card);			}
	public Sprite GetSpriteImage(int _index)	{	return mSpriteImageList[_index];	}

	private void HandleRayCastingHook(Ray _ray)
	{
		if(OpenCardHook != null)	OpenCardHook(_ray);
	}
	private void SetPlayingCards()
	{
		for(int i=0;i<mCurrentPairs*2;i++)
		{
			//Add Random Type
			int randomType = Random.Range(0,mTotalPairs);
			mCardHolderList[i].CardType = randomType;
			mGrid.InsertCard(mCardHolderList[i++]);
			mCardHolderList[i].CardType = randomType;
			mGrid.InsertCard(mCardHolderList[i]);
		}
	}
	public void CompareCards()
	{
		if(mOpenedCards.Count == 2)
		{
			if(mOpenedCards[0].CardType == mOpenedCards[1].CardType && mOpenedCards[0] != mOpenedCards[1])
			{
				foreach(Card c in mOpenedCards)
				{
					c.IsFlipped = false;
					RemoveCardOnGrid(c);
				}
				AnimationManager.Instance.PlayAnimation(mOpenedCards[0].CardType);
				PointsManager.Instance.AddPointsWithPopup(mPointsPerCard,mOpenedCards[0].WorldPosition,mOpenedCards[1].WorldPosition);
				mOpenedCards.Clear();	// Empty Opened Cards
				CheckComplete();
			}
		}
	}
	private void CheckComplete()
	{
		if(mGrid.CardsOnGrid() == 0)
		{
			GameManager.Instance.mLevelsCompleted +=1;
			if(mCurrentPairs < mTotalPairs) mCurrentPairs++;
			switch(mCurrentPairs)
			{
			case 3:
			case 4:
				mGrid.IncreaseCol();
				break;
			case 5:
			case 7:
				mCurrentPairs++;
				mGrid.IncreaseRow();
				break;
			case 9:
				mCurrentPairs++;
				mGrid.IncreaseCol();
				break;
			}
			mGrid.UpdateGridPosition();
			SetPlayingCards();
		}
	}

	public delegate void OpenCardDelegate(Ray _ray);
	public event OpenCardDelegate OpenCardHook;	
}