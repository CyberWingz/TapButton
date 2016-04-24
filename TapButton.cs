using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TapButton : Graphic, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {


	readonly byte flagIsDown = 0x01;
	readonly byte flagIsExit = 0x02;

	byte myFlag = 0x0;

	[SerializeField] TapButtonAnim animShrink;
	[SerializeField] TapButtonAnim animBounce;
	[SerializeField] Image image;

	Coroutine coro;
	RectTransform _rectT;



	RectTransform rectT
	{
		get
		{
			if(_rectT == null)
			{
				if(image != null)
					_rectT = image.rectTransform;
				else
					_rectT = rectTransform;
			}
			
			return _rectT;
		}
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		print("OnPointerUp");

		if(eventData.dragging == true || (myFlag & flagIsExit) != 0)
		{
			rectT.localScale = Vector3.one;
		}
		else
		{
			StartCoroutine(animBounce.iScale(rectT));
		}
		myFlag = 0;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		print("OnPointerDown");

		StartCoroutine(animShrink.iScale(rectT));
		ApplyFlag(ref myFlag, flagIsDown);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		print("OnPointerEnter");

		if(CheckFlag(myFlag, flagIsDown))
		{
			StartCoroutine(animShrink.iScale(rectT));
			RemoveFlag(ref myFlag, flagIsExit);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		print("OnPointerExit");

		if(CheckFlag(myFlag, flagIsDown))
		{
			rectT.localScale = Vector3.one;
			ApplyFlag(ref myFlag, flagIsExit);
		}
	}

	bool CheckFlag(byte a, byte b)
	{
		print((a & b) != 0);
		return (a & b) != 0;
	}

	void ApplyFlag(ref byte a, byte b)
	{
		a |= b;
	}

	void RemoveFlag(ref byte a, byte b)
	{
		a &= (byte)~b;
	}

}
