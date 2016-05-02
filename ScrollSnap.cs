using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollSnap : MonoBehaviour, IEndDragHandler {

	public int pageCount;
	public bool fastSwipe;
	[Range(0.01f,1)]
	public float duration;

	float endDragX = 0;
	float goalX = 0;
	float t = 0;

	int m_pageNum = 0;
	float _pageSize = -1;
	float _pageSizeHalfed = -1;
	ScrollRect _sRect;
	RectTransform _rectT;

	public int pageNum
	{
		set
		{
			if(value == pageCount)
				value = pageCount - 1;
			else if ( value < 0)
				value = 0;
			
			m_pageNum = value;

			goalX = value * -200;

			endDragX = sRect.content.anchoredPosition.x;

			t = 0;
		}
	}

	float pageSizedHalfed
	{
		get
		{
			if(_pageSizeHalfed == -1)
				_pageSizeHalfed = pageSize * 0.5f;

			return _pageSizeHalfed;
		}
	}

	float pageSize
	{
		get
		{
			if(_pageSize == -1)
				_pageSize = rectT.sizeDelta.x;
			
			return _pageSize;
		}
	}

	ScrollRect sRect
	{
		get
		{
			if(_sRect == null)
			{
				_sRect = GetComponent<ScrollRect>();
				_sRect.inertia = false;
			}
			return _sRect;
		}
	}

	RectTransform rectT
	{
		get
		{
			if(_rectT == null)
			{
				_rectT = GetComponent<RectTransform>();
			}
			return _rectT;
		}
	}

	void Update()
	{
		if(t < duration)
		{
			t += Time.deltaTime;

			if(t > duration)
				t = duration;

			float currX = Mathf.Lerp(endDragX, goalX, t / duration);
			sRect.content.anchoredPosition = new Vector2(currX, 0);
		}

		InputTest();
	}

	private void InputTest()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
			pageNum = 0;
		if(Input.GetKeyDown(KeyCode.Alpha2))
			pageNum = 1;
		if(Input.GetKeyDown(KeyCode.Alpha3))
			pageNum = 2;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(fastSwipe)
		{
			float deltaX = eventData.delta.x;

			if( deltaX != 0)
			{
				if(deltaX > 0)
					pageNum = m_pageNum - 1;
				else
					pageNum = m_pageNum + 1;

				return;
			}
		}

		pageNum = (int)(((endDragX - pageSizedHalfed) * - 1) / pageSize);
	}
}
