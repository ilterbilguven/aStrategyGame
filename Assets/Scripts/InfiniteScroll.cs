/// Credit Tomasz Schelenz 
/// Sourced from - https://bitbucket.org/ddreaper/unity-ui-extensions/issues/81/infinite-scrollrect
/// Demo - https://www.youtube.com/watch?v=uVTV7Udx78k  - configures automatically.  - works in both vertical and horizontal (but not both at the same time)  - drag and drop  - can be initialized by code (in case you populate your scrollview content from code)

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///   Infinite scroll view with automatic configuration
///   Fields
///   - InitByUSer - in case your scrollrect is populated from code, you can explicitly Initialize the infinite scroll
///   after your scroll is ready
///   by callin Init() method
///   Notes
///   - doesn't work in both vertical and horizontal orientation at the same time.
///   - in order to work it disables layout components and size fitter if present(automatically)
/// </summary>
public class InfiniteScroll : MonoBehaviour
{
	public ContentSizeFitter _contentSizeFitter;
	public float _disableMarginX;
	public float _disableMarginY;
	public GridLayoutGroup _gridLayoutGroup;
	public bool _hasDisabledGridComponents;
	public HorizontalLayoutGroup _horizontalLayoutGroup;
	public bool _isHorizontal;
	public bool _isVertical;

	public int _itemCount;

	public Vector2 _newAnchoredPosition = Vector2.zero;
	public float _recordOffsetX;
	public float _recordOffsetY;

	public ScrollRect _scrollRect;

	//TO DISABLE FLICKERING OBJECT WHEN SCROLL VIEW IS IDLE IN BETWEEN OBJECTS
	public float _treshold = 100f;

	public VerticalLayoutGroup _verticalLayoutGroup;

	//if true user will need to call Init() method manually (in case the contend of the scrollview is generated from code or requires special initialization)
	[Tooltip("If false, will Init automatically, otherwise you need to call Init() method")] public bool InitByUser;

	public List<RectTransform> items = new List<RectTransform>();

	private void Awake()
	{
		if (!InitByUser)
			Init();
	}

	public void Init()
	{
		if (GetComponent<ScrollRect>() != null)
		{
			_scrollRect = GetComponent<ScrollRect>();
			_scrollRect.onValueChanged.AddListener(OnScroll);
			_scrollRect.movementType = ScrollRect.MovementType.Unrestricted;

			for (var i = 0; i < _scrollRect.content.childCount; i++)
				items.Add(_scrollRect.content.GetChild(i).GetComponent<RectTransform>());
			if (_scrollRect.content.GetComponent<VerticalLayoutGroup>() != null)
				_verticalLayoutGroup = _scrollRect.content.GetComponent<VerticalLayoutGroup>();
			if (_scrollRect.content.GetComponent<HorizontalLayoutGroup>() != null)
				_horizontalLayoutGroup = _scrollRect.content.GetComponent<HorizontalLayoutGroup>();
			if (_scrollRect.content.GetComponent<GridLayoutGroup>() != null)
				_gridLayoutGroup = _scrollRect.content.GetComponent<GridLayoutGroup>();
			if (_scrollRect.content.GetComponent<ContentSizeFitter>() != null)
				_contentSizeFitter = _scrollRect.content.GetComponent<ContentSizeFitter>();

			_isHorizontal = _scrollRect.horizontal;
			_isVertical = _scrollRect.vertical;

			if (_isHorizontal && _isVertical)
				Debug.LogError(
					"UI_InfiniteScroll doesn't support scrolling in both directions, plase choose one direction (horizontal or vertical)");

			_itemCount = _scrollRect.content.childCount;
		}
		else
		{
			Debug.LogError("UI_InfiniteScroll => No ScrollRect component found");
		}
	}

	private void DisableGridComponents()
	{
		if (_isVertical)
		{
			_recordOffsetY = items[0].GetComponent<RectTransform>().anchoredPosition.y -
			                 items[1].GetComponent<RectTransform>().anchoredPosition.y;
			_disableMarginY =
				_recordOffsetY * _itemCount / 2; // _scrollRect.GetComponent<RectTransform>().rect.height/2 + items[0].sizeDelta.y;
		}
		if (_isHorizontal)
		{
			_recordOffsetX = items[1].GetComponent<RectTransform>().anchoredPosition.x -
			                 items[0].GetComponent<RectTransform>().anchoredPosition.x;
			_disableMarginX =
				_recordOffsetX * _itemCount / 2; //_scrollRect.GetComponent<RectTransform>().rect.width/2 + items[0].sizeDelta.x;
		}

		if (_verticalLayoutGroup)
			_verticalLayoutGroup.enabled = false;
		if (_horizontalLayoutGroup)
			_horizontalLayoutGroup.enabled = false;
		if (_contentSizeFitter)
			_contentSizeFitter.enabled = false;
		if (_gridLayoutGroup)
			_gridLayoutGroup.enabled = false;
		_hasDisabledGridComponents = true;
	}

	public void OnScroll(Vector2 pos)
	{
		if (!_hasDisabledGridComponents)
			DisableGridComponents();

		for (var i = 0; i < items.Count; i++)
		{
			if (_isHorizontal)
				if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x >
				    _disableMarginX + _treshold)
				{
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.x -= _itemCount * _recordOffsetX;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
				}
				else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).x < -_disableMarginX)
				{
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.x += _itemCount * _recordOffsetX;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(0).transform.SetAsLastSibling();
				}

			if (_isVertical)
				if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y >
				    _disableMarginY + _treshold)
				{
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.y -= _itemCount * _recordOffsetY;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(_itemCount - 1).transform.SetAsFirstSibling();
				}
				else if (_scrollRect.transform.InverseTransformPoint(items[i].gameObject.transform.position).y < -_disableMarginY)
				{
					_newAnchoredPosition = items[i].anchoredPosition;
					_newAnchoredPosition.y += _itemCount * _recordOffsetY;
					items[i].anchoredPosition = _newAnchoredPosition;
					_scrollRect.content.GetChild(0).transform.SetAsLastSibling();
				}
		}
	}
}