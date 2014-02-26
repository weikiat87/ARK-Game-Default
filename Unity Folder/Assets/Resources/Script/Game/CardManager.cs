using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{	
	[SerializeField] private Card	mPrefabCard;				// Prefab of Card
	[SerializeField] private int	mTotalPairs;				// Max Pairs
	[SerializeField] private float	mDistance;					// Distance between Cards in Grid
	[SerializeField] private int	mMaxRows;					// Max Row
	[SerializeField] private int	mMaxCols;					// Max Col
	[SerializeField] private Sprite[]	mSpriteImageList;		// Images of Sprite
	[SerializeField] private Card[] 	mCardHolderList;		// Temp Holder

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
		mGrid.Init(mMaxRows,mMaxCols,mDistance);

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
				
				Debug.Log(_card.IsFlipped);
			}
		}
		else
		{
			mOpenedCards[0].IsFlipped = false;
			mOpenedCards.RemoveAt(0);
			_card.IsFlipped = true;
			mOpenedCards.Add(_card);
		}

		CompareCards();
	}
	public void RemoveCard(Card _card)
	{
		if(mOpenedCards.Count < 2)
		{
			if(mOpenedCards.Contains(_card)) 
			{
				_card.IsFlipped		= false;
				
				Debug.Log(_card.IsFlipped);
				mOpenedCards.Remove(_card);
			}
		}
	}

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
	private void CompareCards()
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
				AnimationManager.Instance.PlayAnimation();
				mOpenedCards.Clear();	// Empty Opened Cards
				CheckComplete();
			}
		}
	}
	private void CheckComplete()
	{
		if(mGrid.CardsOnGrid() == 0)
		{
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
			Debug.Log(mCurrentPairs);
			mGrid.UpdateGridPosition();
			SetPlayingCards();
		}
	}

	public delegate void OpenCardDelegate(Ray _ray);
	public event OpenCardDelegate OpenCardHook;	
}