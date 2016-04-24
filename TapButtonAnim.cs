#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using UnityEngine;

public class TapButtonAnim : ScriptableObject
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/TapButtonData")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<TapButtonAnim> ();
	}
	#endif

	public AnimationCurve curve;
	public float duration;


	public IEnumerator iPosY(RectTransform rectT)
	{
		float t = 0;
		float val = curve.Evaluate(0);
		Vector2 pos = new Vector2(0, val);

		do
		{
			rectT.anchoredPosition = pos;
			yield return null;
			t += Time.deltaTime;
			pos.y = curve.Evaluate(t);

		} while (t <= duration);

		pos.y = curve.Evaluate(duration);
		rectT.anchoredPosition = pos;
	}

	public IEnumerator iScaleY(Transform transform)
	{
		float t = 0;

		Vector3 scale = transform.localScale;
		scale.y = curve.Evaluate(0);

		do
		{
			transform.localScale = scale;
			yield return null;
			t += Time.deltaTime;
			scale.y = curve.Evaluate(t);

		} while (t <= duration);

		scale.y = curve.Evaluate(duration);
		transform.localScale = scale;
	}

	public IEnumerator iScale(Transform transform)
	{
		float t = 0;
		float val = curve.Evaluate(0);
		Vector3 scale = new Vector3(val, val, 0);

		do
		{
			transform.localScale = scale;
			yield return null;
			t += Time.deltaTime;
			scale.x = scale.y = curve.Evaluate(t);

		} while (t <= duration);

		scale.x = scale.y = curve.Evaluate(duration);
		transform.localScale = scale;
	}

	public IEnumerator iAlpha(CanvasGroup canvasGrp)
	{
		float t = 0;
		float val = curve.Evaluate(0);

		do
		{
			canvasGrp.alpha = val;
			yield return null;
			t += Time.deltaTime;
			val = curve.Evaluate(t);

		} while (t <= duration);

		canvasGrp.alpha = curve.Evaluate(duration);
	}
}
